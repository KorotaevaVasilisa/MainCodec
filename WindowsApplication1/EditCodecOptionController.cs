using log4net;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TCPclient
{
    internal class EditCodecOptionController
    {
        private static ILog logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private OnFileRemoteChanged remoteChanged;
        private EditCodecForm MyParentForm = null;
        private Boolean rtcp_Started = false;
        public TcpClient tcp_client;
        public NetworkStream netstream = null;
        Thread rtcp_Thread = null;

        public EditCodecOptionController(OnFileRemoteChanged listener, EditCodecForm form)
        {
            remoteChanged = listener;
            this.MyParentForm = form;
        }

        public void CreateTCPstream(string id, int port)
        {
            Ping pingsender = new Ping();
            PingOptions options = new PingOptions();
            options.DontFragment = true;
            byte[] buffer = Encoding.ASCII.GetBytes("test_ping");

            try
            {
                //попингуем
                PingReply reply = pingsender.Send(id, 2, buffer, options);
                if (reply != null && reply.Status == IPStatus.Success)
                {
                    //Есть в сети
                }
                else
                {
                    MyParentForm.SetLabelStatus("noNetwork");
                    return;
                }

                //создание TCP_клиента и нитки на чтение
                //  закрыть тек.подключение
                if (netstream != null)
                    netstream.Close();
                if (tcp_client != null)
                    tcp_client.Close();
                //tcp_client = new TcpClient(tbIPcdc.Text, IPport); //до 06.2019
                tcp_client = new TcpClient();
                var result = tcp_client.BeginConnect(id, port, null, null);
                bool success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(3), true);
                if (success)
                {
                    if (!tcp_client.Connected)
                        throw new SocketException(10061); //Порт закрыт
                }
                else
                {
                    throw new SocketException(10060); //Connection timed out
                }

                //
                netstream = tcp_client.GetStream();

                if (!rtcp_Started)
                {
                    rtcp_Thread = new Thread(new ThreadStart(ThreadProc));
                    rtcp_Thread.IsBackground = true;
                    rtcp_Started = true;
                    rtcp_Thread.Start();
                }


                MyParentForm.SendMsg("login temas\r"); //регистрация
                Thread.Sleep(1000);
                MyParentForm.CdcOptionRqst(); //запрос настроек
            }
            catch (Exception e)
            {
                if (netstream != null)
                    netstream.Close();
                if (tcp_client != null)
                    tcp_client.Close();

                MyParentForm.SetLabelStatus("error");

                if (e is System.Net.Sockets.SocketException)
                    MessageBox.Show("Не поднимается соединение по TCP. Ошибка_" +
                                    (e as System.Net.Sockets.SocketException).ErrorCode + "\n"
                                    + e.Message + "\nПопробуйте повторить через 20сек", "Ошибка");
                else
                    MessageBox.Show("Не поднимается соединение по TCP\n" + e.Message, "Ошибка");
            }
        }

        public void Parse()
        {
            string str = "";
            while (true)
            {
                str = GetLine();
                // string s1 = str;
                if (!str.Contains("tst_connect") && !MyParentForm.findcdcs)
                    MyParentForm.AddMsgToCon(str, true);

                if (!MyParentForm.intrf_com)
                {
                    if (str.Contains("login OK"))
                    {
                        if (!MyParentForm.findcdcs)
                        {
                            //MyParentForm.findcdcs = true;
                            MyParentForm.connect = true;
                            MyParentForm.SetLabelStatus("connected");
                        }
                    }
                    else if (str.Contains("?unknown: login:")) //"?unknown: login: temas"
                    {
                        if (!MyParentForm.findcdcs)
                        {
                            MyParentForm.SetLabelStatus("unknown");
                        }

                        MyParentForm.cdc_newver = false;
                        //s = "";
                        //return;
                    }
                }

                if (str.StartsWith("Ok version"))
                {
                    //PutNewLineToCon(s);
                    string[] ss = str.Split(new string[] { " " }, //stringSeparators,
                        StringSplitOptions.RemoveEmptyEntries);
                    if (ss.Length > 5)
                        MyParentForm.SaveVersion(ss[3], ss[4], ss[5]);
                }

                if (str.StartsWith("Ok netstat"))
                {
                    MyParentForm.rmsg_net = str;
                    while (true)
                    {
                        str = GetLine();
                        MyParentForm.rmsg_net += str;
                        MyParentForm.AddMsgToCon(str, true);
                        if (str.StartsWith(":."))
                            break;
                    }
                }

                if (str.StartsWith("sound_servers: "))
                {
                    MyParentForm.rmsg_unicast = str;
                    while (true)
                    {
                        str = GetLine();
                        MyParentForm.rmsg_unicast += str;
                        MyParentForm.AddMsgToCon(str, true);
                        if (str.StartsWith(":."))
                            break;
                    }
                }

                if (str.StartsWith("Ok get: fmt"))
                {
                    MyParentForm.rmsg_fm = str;
                    while (true)
                    {
                        str = GetLine();
                        MyParentForm.rmsg_fm += str;
                        MyParentForm.AddMsgToCon(str, true);
                        if (str.StartsWith(":."))
                            break;
                    }
                }

                if (str.StartsWith("Ok get fm_state"))
                {
                    MyParentForm.rmsg_fms = str;
                    while (true)
                    {
                        str = GetLine();
                        MyParentForm.rmsg_fms += str;
                        MyParentForm.AddMsgToCon(str, true);
                        if (str.StartsWith(":."))
                            break;
                    }
                }

                if (str.StartsWith("Ok url_list"))
                {
                    MyParentForm.rmsg_icecast = str;
                    while (true)
                    {
                        str = GetLine();
                        MyParentForm.rmsg_icecast += str;
                        MyParentForm.AddMsgToCon(str, true);
                        if (str.StartsWith(":."))
                            break;
                    }
                }
                if (str.StartsWith(" Ok system:  rm -r")|| str.StartsWith(" Ok system:  mkdir"))
                    MyParentForm.CdcOptionLsRqst();

                if (str.StartsWith("Ok Ls"))
                {
                    MyParentForm.rmsg_ls = str;
                    while (true)
                    {
                        str = GetLine();
                        MyParentForm.rmsg_ls += str;
                        MyParentForm.AddMsgToCon(str, true);
                        if (str.StartsWith(":."))
                        {
                            string[] text = MyParentForm.rmsg_ls.Split('\r', '\n');
                            List<string> list = new List<string>(text);
                            logger.Info(MyParentForm.rmsg_ls);
                            remoteChanged.onChanged(list);
                            break;
                        }
                    }
                }

                if (str.StartsWith("sfl openr") && str.Contains("Ok"))
                {
                    MyParentForm.CdcOptionSflRRqst();
                }



                if (str.StartsWith("sfl r"))
                {
                    if (!str.Contains("sfl r :."))
                    {

                        MyParentForm.CdcOptionSflRRqst();
                        byte[] textAsBytes = System.Convert.FromBase64String(str.Substring(5));
                        string part  = System.Text.Encoding.Default.GetString(textAsBytes);
                        MyParentForm.rmsg_sfl += part;
                        int size= Encoding.Default.GetByteCount(MyParentForm.rmsg_sfl);
                        remoteChanged.onUpdateProgressBar(size);
                    }
                    else
                    {
                        remoteChanged.onCopyRemoteFile();
                    }
                }


                if (str.StartsWith("Ok get: o"))
                {
                    MyParentForm.rmsg_program = str;
                }

                if (str.StartsWith("bitrst"))
                {
                    int nchn = Convert.ToInt32(str.Substring(6, 1)) - 1;
                    if (nchn < 4)
                    {
                        MyParentForm.rmsg_stat[nchn] = str.Substring(8);
                        if (MyParentForm.bitrate_stat != null)
                            MyParentForm.bitrate_stat.Parse(nchn);
                    }
                }

                if (str.StartsWith("t_cpu"))
                {
                    MyParentForm.t_cpu = str.Substring(6, 2) + " °C";
                    if (MyParentForm.bitrate_stat != null)
                        MyParentForm.bitrate_stat.ShowTcpu();
                }
            }
        }

        private void ThreadProc()
        {
            while (rtcp_Started /*!rtcp_Finished*/)
            {
                Thread.Sleep(1); //2
                if (netstream != null &&
                    netstream.CanRead) //Check if NetworkStream is readable
                {
                    try
                    {
                        if (netstream.DataAvailable)
                        {
                            Parse();
                        }
                    }
                    catch (Exception e)
                    {
                        logger.Error(String.Format("ERROR THREAD " + e.Message.ToString(), this, DateTime.Now));
                    }
                }
            }
        }

        System.Text.Encoding myEncoding = Encoding.GetEncoding("windows-1251");


        string GetLine()
        {
            string line = "";
            byte[] RecBytes = new byte[2];
            char c;
            while (true)
            {
                netstream.Read(RecBytes, 0, 1);

                char[] chars = myEncoding.GetChars(RecBytes);
                c = chars[0];
                line += c;
                // c = (char)RecBytes[0];
                if (c == '\n')
                    break;
            }

            return line;
        }
    }
}