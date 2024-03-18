#define AID_PING
#define COM_PORT //Позволяет выбрать связь через Ethernet или COM порт (?прописать в .config)
#define PARSE_MSG //Парсер сообщений по COM-порту (теперь и по IP)
//#define PROB_CDCMESSAGE  //проба разбора сообщений от кодека по формату Tiscada
//#define CMD_HISTORY 

using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
//IP сеть
using System.Net;
using System.Net.NetworkInformation; //Ping 
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

//using File = System.IO.File;

namespace TCPclient
{
    public partial class EditCodecForm : Form, OnFileRemoteChanged
    {
        EditCodecOptionController controller;

        private static ILog logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public EditCodecForm()
        {
            InitializeComponent();
#if COM_PORT
            ReadComConfig();
#else
            rButtCOM.Enabled = false;
#endif
            ReadNetConfig();
            InitNetParam();

            controller = new EditCodecOptionController(this, this);
        }

        void ReadNetConfig()
        {
            try
            {
                System.Configuration.AppSettingsReader configAppSettings = new System.Configuration.AppSettingsReader();
                string s;
                try
                {
                    s = (string)(configAppSettings.GetValue("IPcodec", typeof(string)));
                    tbIPcdc.Text = s;
                }
                catch (System.SystemException e)
                {
                    MsgToCon("Ошибка в файле конфигурации (парам.IPcodec). " + e.Message);
                }

                IPport = ((int)(configAppSettings.GetValue("IPport", typeof(int))));
                /*try
                {
                    if ((bool)(configAppSettings.GetValue("DosEncoding", typeof(bool))))
                        myEncoding = System.Text.Encoding.GetEncoding("cp866");
                }
                catch (System.SystemException e)
                { MsgToCon(e.Message); } */
            }
            catch (System.SystemException e)
            {
                MsgToCon("Ошибка в файле конфигурации. " + e.Message);
            }
        }
#if COM_PORT
        void ReadComConfig()
        {
            my_serial.Encoding = System.Text.Encoding.GetEncoding("windows-1251");
            System.Configuration.AppSettingsReader configAppSettings = new System.Configuration.AppSettingsReader();
            int i = 1;
            try
            {
                try
                {
                    log_enbl = !((bool)(configAppSettings.GetValue("WriteLogFile", typeof(bool))));
                    btLogEnbl_Click(null, null); //инвертирует log_enbl
                }
                catch (System.SystemException e)
                {
                    PutNewLineToCon(e.Message);
                }

                try
                {
                    i = ((int)(configAppSettings.GetValue("ComNumber", typeof(int))));
                }
                catch (System.SystemException e)
                {
                    PutNewLineToCon(e.Message);
                }

                try
                {
                    if ((bool)(configAppSettings.GetValue("DosEncoding", typeof(bool))))
                    {
                        //need_dos_encode = true;
                        my_serial.Encoding = System.Text.Encoding.GetEncoding("cp866");
                    }
                }
                catch (System.SystemException e)
                {
                    PutNewLineToCon(e.Message);
                }

                try
                {
                    my_serial.BaudRate = ((int)(configAppSettings.GetValue("ComBaudRate", typeof(int))));
                }
                catch (System.SystemException e)
                {
                    PutNewLineToCon(e.Message);
                }

                try
                {
                    my_serial.DataBits = ((int)(configAppSettings.GetValue("ComDataBits", typeof(int))));
                }
                catch (System.SystemException e)
                {
                    PutNewLineToCon(e.Message);
                }

                string par;
                try
                {
                    par = ((string)(configAppSettings.GetValue("ComParity", typeof(string)))).ToUpper();
                    if (par == "NONE")
                        my_serial.Parity = System.IO.Ports.Parity.None;
                    else if (par == "EVEN")
                        my_serial.Parity = System.IO.Ports.Parity.Even;
                    else if (par == "ODD")
                        my_serial.Parity = System.IO.Ports.Parity.Odd;
                }
                catch (System.SystemException e)
                {
                    PutNewLineToCon(e.Message);
                }

                try
                {
                    par = ((string)(configAppSettings.GetValue("ComStopBits", typeof(string)))).ToUpper();
                    if (par == "0")
                        my_serial.StopBits = System.IO.Ports.StopBits.None;
                    else if (par == "1")
                        my_serial.StopBits = System.IO.Ports.StopBits.One;
                    else if (par == "1.5")
                        my_serial.StopBits = System.IO.Ports.StopBits.OnePointFive;
                    else if (par == "2")
                        my_serial.StopBits = System.IO.Ports.StopBits.Two;
                }
                catch (System.SystemException e)
                {
                    PutNewLineToCon(e.Message);
                }

                try
                {
                    if ((bool)(configAppSettings.GetValue("ComUseCTS", typeof(bool))))
                        UseCTS = true; //сами вручную следим за CTS
                }
                catch (System.SystemException e)
                {
                    PutNewLineToCon(e.Message);
                }

                try
                {
                    if ((bool)(configAppSettings.GetValue("ComRTShandshake", typeof(bool))))
                    {
                        my_serial.RtsEnable = false;
                        my_serial.Handshake = System.IO.Ports.Handshake.RequestToSend; //снимает RTS, если 
                        RTShandshake = true; //во вход.буфере < 1024 своб.байт ??и передает только при CTS==1 
                        UseCTS = false;
                    }
                }
                catch (System.SystemException e)
                {
                    PutNewLineToCon(e.Message);
                }

                try
                {
                    if ((bool)(configAppSettings.GetValue("ShowDebugMsg", typeof(bool))))
                        enabl_dbg_msg = true;
                }
                catch (System.SystemException e)
                {
                    PutNewLineToCon(e.Message);
                }
            }
            catch
            {
                PutNewLineToCon("Неопознанная ошибка в файле конфигурации");
            }

            UpDnNport.Value = i; //по старту имя "COM", чтобы не открывать здесь
            my_serial.PortName = "COM" + i;
            //!! может 2 раза вызывать btOpen_Click: из Nport_Chngd
            //   и PortModeSet(из-за смены lbBaud.SelectedIndex)
            int si = 0;
            for (i = 0; i < lbBaud.Items.Count + 1; i++)
            {
                if (i == lbBaud.Items.Count)
                {
                    si = lbBaud.Items.Count;
                    for (int ii = 0; ii < lbBaud.Items.Count; ii++)
                    {
                        if (int.Parse(lbBaud.Items[ii].ToString()) > my_serial.BaudRate)
                        {
                            si = ii;
                            lbBaud.Items.Insert(si, my_serial.BaudRate);
                            break;
                        }
                    }

                    if (si == lbBaud.Items.Count)
                        lbBaud.Items.Add(my_serial.BaudRate);
                    break;
                }
                else
                    si = i;

                if (lbBaud.Items[si].ToString() == my_serial.BaudRate.ToString())
                    break;
            }

            lbBaud.SelectedIndex = si;
        }
#endif
        bool test_ip(string ip, string msg)
        {
            if (ip == "")
                return false;
            bool ok = true;
            string[] ss = ip.Split('.');
            if (ss.Length != 4) //< 4
                ok = false;
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    short res;
                    ok = Int16.TryParse(ss[i], out res);
                    if (ok == false || res < 0 || res > 255)
                    {
                        ok = false;
                        break;
                    }
                }
            }

            return ok;
        }

        void InitNetParam( /*bool okcfg*/)
        {
            //Dns.GetHostByName(Dns.GetHostName()).AddressList[0]; 
            string HostName = "";
            // Getting Ip address of local machine...
            // First get the host name of local machine.
            //можно Dns.GetHostByAddress(ip).HostName
            HostName = System.Net.Dns.GetHostName();
            MsgToCon("Имя компьютера(HostName): " + HostName);
            // Then using host name, get the IP address list..
            string IPhost = "";
            IPAddress[] addr = System.Net.Dns.GetHostAddresses(HostName);
            for (int i = 0; i < addr.Length; i++)
            {
                string fam = addr[i].AddressFamily.ToString();
                // if server is IPv6-enabled this value is: InterNetworkV6
                //if (!fam.Contains("V6"))
                if (fam == "InterNetwork")
                {
                    IPhost = addr[i].ToString();
                    MsgToCon("IP компьютера: " + IPhost + " (" + fam + ")");
                    //break;
                }
            }

            MsgToCon("");
            tbIPcomp.Text = IPhost;
            tbPort.Text = IPport.ToString();
#if AID_PING
            pingSender = new System.Net.NetworkInformation.Ping();
            //Create an event handler for ping complete
            pingSender.PingCompleted += new PingCompletedEventHandler(pingSender_Complete);
#endif
            ReadStationsFile();
            //if (!okcfg)
            //    MessageBox.RemoteShow("Некорректное значение IP кодека", "Ошибка");
            timerTstConnect.Start();
        }

        //
        public bool intrf_com = false;

        bool //need_dos_encode = false,
            enabl_dbg_msg = false;

        bool RTShandshake = false,
            log_enbl = false,
            first_rec = true;
#if COM_PORT
        bool UseCTS = false;
#endif
        //
        public TcpClient tcp_client;
        public NetworkStream netstream = null;
        Thread rtcp_Thread = null;
        private bool rtcp_Started = false; //rtcp_Finished = false,
        public bool connect = false;
        System.Text.Encoding myEncoding = Encoding.GetEncoding("windows-1251");
        const int IPport_dflt = 1201;
        int IPport = IPport_dflt;

        public string rmsg_net = "",
            rmsg_unicast = "",
            rmsg_fm = "",
            rmsg_fms = "",
            rmsg_icecast = "",
            rmsg_program = "",
            rmsg_ls = "";

        public string[] rmsg_stat = new string[4] { "", "", "", "" };
        public string t_cpu = "";
        string StationsFile = Application.StartupPath + "\\" + "stations.txt";
        public List<string> listURLs = new List<string>();

        private void ThreadProc()
        {
            while (rtcp_Started /*!rtcp_Finished*/)
            {
                Thread.Sleep(1); //2
                if (netstream != null && netstream.CanRead) //Check if NetworkStream is readable
                {
                    try
                    {
                        if (netstream.DataAvailable)
                        {
                            //controller.Parse();
                            Parse();

                            //Parse(nbytesread, myEncoding.GetChars(myReadBuffer));


                            //ParseTCPpack(nbytesread, ref myReadBuffer);

                            //if ((i = stream.Read(datalength, 0, 4)) != 0)
                            //{
                            //    byte[] data = new byte[BitConverter.ToInt32(datalength, 0)];
                            //    stream.Read(data, 0, data.Length);
                            //    MessageBox.RemoteShow(System.Environment.NewLine + "Client : " + Encoding.Default.GetString(data));
                            //}
                        }
                    }
                    catch //Exception e)
                    {
                        /*if (connect)
                          {
  // string s= "ThreadEx: "+e.Message;
  // if (tcp_client == null)
  //     s += "  tcp_client=null";
  // else if (!tcp_client.Connected)
  //         s += "  tcp_client.Connected=false";
  // MessageBox.RemoteShow(s);
                              LossConnect();
                          } */
                        //Class1.server.Start(); 
                        //MessageBox.RemoteShow("Waiting For Connection");
                        //new Thread(() =>
                        //{
                        //    Class1.client = Class1.server.AcceptTcpClient();
                        //    MessageBox.RemoteShow("Connected To Client");
                        //    if (Class1.client.Connected)
                        //    {
                        //        Class1.ServerReceive();
                        //    }
                        //}).Start();
                        //stop = true;
                    }
                }
            }
        }

        private void ClosedForm(object sender, FormClosedEventArgs e)
        {
            //timerTstConnect.Stop();
            if (rtcp_Thread != null)
            {
                try
                {
                    rtcp_Started = false; //rtcp_Finished = true;
                    //rtcp_Thread.Join();
                }
                finally
                {
                    rtcp_Thread = null;
                    if (netstream != null)
                        netstream.Close(); //??надо ли
                    //if (tcp_client != null)
                    //    tcp_client.Close(); //??надо ли
                }
            }

            System.Diagnostics.Process proc = System.Diagnostics.Process.GetCurrentProcess();
            proc.Kill(); //на вс.сл. ??Kill или Close
            proc.WaitForExit(1500); // ожидать 1?2 сек до завершения процесса
            //GC.SuppressFinalize(this);
        }
        //<Поиск кодеков>
#if NOTH
  public static bool TestOpenPort(int Port)
    {
        var tcpListener = default(TcpListener);

        try
        {
            var ipAddress = Dns.GetHostEntry("localhost").AddressList[0];

            tcpListener = new TcpListener(ipAddress, Port);
            tcpListener.Start();

            return true;
        }
        catch (SocketException)
        {
        }
        finally
        {
            if (tcpListener != null)
                tcpListener.Stop();
        }

        return false;
    }
        private bool isPortAvalaible(int myPort)
        {
            var avalaiblePorts = new List<int>();
            var properties = IPGlobalProperties.GetIPGlobalProperties();

            // Active connections
            var connections = properties.GetActiveTcpConnections();
            avalaiblePorts.AddRange(connections);

            // Active tcp listners
            var endPointsTcp = properties.GetActiveTcpListeners();
            avalaiblePorts.AddRange(endPointsTcp);

            // Active udp listeners
            var endPointsUdp = properties.GetActiveUdpListeners();
            avalaiblePorts.AddRange(endPointsUdp);

            foreach (int p in avalaiblePorts)
            {
                if (p == myPort) return false;
            }
            return true;
        }
#endif
        /*      public static class SocketExtensions
                {
                    /// <summary>
                    /// Connects the specified socket.
                    /// </summary>
                    /// <param name="socket">The socket.</param>
                    /// <param name="endpoint">The IP endpoint.</param>
                    /// <param name="timeout">The timeout.</param>
                    public static void Connect(this Socket socket, EndPoint endpoint, TimeSpan timeout)
                    {
                        var result = socket.BeginConnect(endpoint, null, null);

                        bool success = result.AsyncWaitHandle.WaitOne(timeout, true);
                        if (success)
                        {
                            socket.EndConnect(result);
                        }
                        else
                        {
                            socket.Close();
                            throw new SocketException(10060); // Connection timed out.
                        }
                    }
                } */
        public bool findcdcs = false, //режим сканирования подсети отключен
            cdc_newver = false; //кодек не поддерж.настройку параметров

        List<string> Codecs_ip = new List<string>();

        private void FindCodecs()
        {
            string subnet_ip = tbIPcdc.Text;
            subnet_ip = subnet_ip.Substring(0, subnet_ip.LastIndexOf('.') + 1);
            if (subnet_ip == "")
            {
                MessageBox.Show("Нет IP адреса подсети (бокс IP на панели <Подключение>)", "Ошибка");
                lbRegistrStat.Text = "Ждем подключения";
                return;
            }

            DialogResult res = MessageBox.Show("Адрес подсети выбран из бокса IP на панели <Подключение>\nПродолжить?",
                "Сканирование подсети " + subnet_ip, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res != DialogResult.Yes)
            {
                ConsoleHide();
                lbRegistrStat.Text = "Ждем подключения";
                return;
            }

            if (!paConsole.Visible)
            {
                this.Height = 612;
                paConsole.Visible = true;
            }

            pbFindCodecs.Visible = true;
            btListCdcs.Text = "Стоп";
            btListCdcs.Visible = true;
            this.Refresh();
            string curr_ip;
            string msg;
            int ncdcs = 0;
            //UdpClient udp_clnt = null; //для проверки на порт 1117
            TcpClient tcp_clnt = null;
            Ping pingsender = new Ping();
            PingOptions options = new PingOptions();
            options.DontFragment = true;
            byte[] buffer = Encoding.ASCII.GetBytes("test_ping");
            MsgToCon("Сканируем подсеть " + subnet_ip + "[1..255]");
            findcdcs = true;
            for (int i = 1; i < 255; i++) //= 161
            {
                Application.DoEvents();
                if (!findcdcs)
                    break;
                curr_ip = subnet_ip + i.ToString();
                msg = "IP=" + curr_ip + "  ";
                pbFindCodecs.Value = i;
                //попингуем
                PingReply reply = pingsender.Send(curr_ip, 2, buffer, options);
                if (reply != null && reply.Status == IPStatus.Success)
                {
                    MsgToCon(msg + "Есть в сети");
                }
                else
                {
                    MsgToCon(msg + "Нет в сети");
                    continue;
                }

                try
                {
#if NOTH
                    /*TcpListener tcpLsn= null;
                    try
                    {
                        tcpLsn = new TcpListener((IPAddress.Parse(curr_ip), IPport);
                        tcpLsn.Start();
                        MsgToCon("Listener Start");
                    }
                    catch (SocketException sex)
                    {
                        MsgToCon("Listener Err: " + sex.Message);
                    }
                    finally
                    {
                        if (tcpLsn != null)
                            tcpLsn.Stop();
                    }
                    continue; */
                    /*var properties = IPGlobalProperties.GetIPGlobalProperties();
                    var connections = properties.GetActiveTcpConnections();
                    for (int ind = 0; ind < connections.Length; ind++)
                        MsgToCon("conn_" + ind.ToString() + "  " + connections[ind].RemoteEndPoint + " :" + connections[ind].State);
                    continue; */
                    //
                    /*try
                    {
                        System.Net.Sockets.Socket sock = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, 
                            System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
                        IPAddress address = IPAddress.Parse(curr_ip);
                        sock.Connect(address, IPport);
                        sock.Close();
                    }
                    catch (System.Net.Sockets.SocketException ex)
                    {
                        if (ex.ErrorCode == 10061) //Port is unused and could not establish connection 
                        {
                            MsgToCon("Port is Open. Port is unused"); //Порт закрыт
// Происходит быстро ~1sec
//далее в new TcpClient(..) возникает Exception с Message:
// Подключение не установлено, т.к.конечный компьютер отверг запрос на подключение 192.168.0.75:1201
                        }
                        else
                        {
                            MsgToCon("SocketException");//Тайм-аут
// Происходит долго ~20sec
//ex.Message:
// Попытка установить соединение была безуспешной,т.к. от другого компьютера за требуемое время не получен нужный отклик,
// или было разорвано уже установленное соединение из-за неверного отклика уже подключенного компьютера IP:port
                            MsgToCon(ex.Message);
                        }
                        continue;
                    } */
                      /* Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        //IPAddress address = IPAddress.Parse(curr_ip);
                        IPEndPoint point = new IPEndPoint(IPAddress.Parse(curr_ip), IPport);
                        try
                        {
                            socket.Connect(point, TimeSpan.FromSeconds(5));
                            //socket.Connect((address,IPport, TimeSpan.FromSeconds(5));
                        }
                        catch (SocketException e)
                        {
                            if (e.ErrorCode == 10061)
                                MsgToCon("Port is closed");
                            else if (e.ErrorCode == 10060)
                                MsgToCon("TimeOut");
                            else
                                MsgToCon(e.Message);
                        } */
#endif
                    //if (udp_clnt != null)
                    //    udp_clnt.Close();
                    //udp_clnt = new UdpClient(curr_ip, 1117);
                    //
                    //tcp_clnt= new TcpClient(curr_ip, IPport); //до 06.2019
                    tcp_clnt = new TcpClient();
                    var result = tcp_clnt.BeginConnect(curr_ip, IPport, null, null);
                    bool success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(3), true);
                    if (success)
                    {
                        if (!tcp_clnt.Connected)
                            throw new SocketException(10061); //Порт закрыт
                    }
                    else
                    {
                        //tcp_clnt.Close();
                        throw new SocketException(10060); //Connection timed out
                    }

                    //
                    ncdcs++;
                    Codecs_ip.Add(curr_ip);
                    MsgToCon("  Порт отвечает. Запрашиваем имя кодека");
                    if (netstream != null)
                        netstream.Close();
                    netstream = tcp_clnt.GetStream();
                    if (!rtcp_Started)
                    {
                        rtcp_Thread = new Thread(new ThreadStart(ThreadProc));
                        rtcp_Thread.IsBackground = true;
                        rtcp_Started = true;
                        rtcp_Thread.Start();
                    }

                    //дать команды регистр-ии и чтения netstat
                    rmsg_net = "";
                    cdc_newver = true;
                    SendMsg("login temas\r");
                    //19.03.2021
                    //SendMsg("version\r");
                    //
                    SendMsg("netstat\r");
                    //дождаться ответа
                    while (true)
                    {
                        Application.DoEvents();
                        if (rmsg_net != "" || !cdc_newver)
                            break;
                    }

                    string nm = "не поддерж.";
                    if (cdc_newver) //прошивка кодека поддерживает настройку параметров
                    {
                        NetEdit.Parse(rmsg_net); //cd/cs
                        nm = "cd";
                        if (NetEdit.args[15].StartsWith("cs"))
                            nm = "cs";
                        nm = nm + NetEdit.args[0];
                    }

                    Codecs_ip[Codecs_ip.Count - 1] += "  " + nm;
                }
                catch (Exception e)
                {
                    //Попытки соед-я с PC в лок.сети всегда вызывает Exception (? мб и какие-то кодеки с очень старыми прошивками):
                    //Порт открыт и Порт не использ-ся(Error=10061) ex.Message:
                    //  Подключение не установлено, т.к.конечный компьютер отверг запрос на подключение IP:port
                    //  Происходит быстро ~1sec
                    //Порт закрыт(Error=10060 ConnectNoCheck) ex.Message:
                    //  Попытка установить соединение была безуспешной, т.к.от другого компьютера за требуемое время не получен нужный отклик,
                    //  или было разорвано уже установленное соединение из-за неверного отклика уже подключенного компьютера IP:port
                    //  Происходит долго ~20sec
                    if (e is System.Net.Sockets.SocketException)
                        MsgToCon("  Error " + (e as System.Net.Sockets.SocketException).ErrorCode + " " + e.Message);
                    else
                        MsgToCon("  Error " + e.Message);
                }
                finally
                {
                    if (netstream != null)
                        netstream.Close();
                    if (tcp_clnt != null)
                        tcp_clnt.Close();
                }
            }

            MsgToCon("Поиск кодеков закончен. Кодеков на связи: " + ncdcs);
            lbRegistrStat.BackColor = SystemColors.Control;
            lbRegistrStat.Text = "Не подключен";
            findcdcs = false;
            pbFindCodecs.Visible = false;
            if (ncdcs > 0)
            {
                for (int i = 0; i < Codecs_ip.Count; i++)
                    tbCodecs.AppendText(Codecs_ip[i] + "\r\n");
                tbCodecs.Visible = true;
                btListCdcs.Text = "Скрыть/показать список";
                btListCdcs.Visible = true;
            }
            else
                btListCdcs.Visible = false;
        }

        private void btFindCodecs_Click(object sender, EventArgs e)
        {
            connect = false;
            lbRegistrStat.BackColor = SystemColors.Control;
            lbRegistrStat.Text = "Поиск кодеков";
            Codecs_ip.Clear();
            tbCodecs.Visible = false;
            tbCodecs.Clear();
            bool res = Int32.TryParse(tbPort.Text, out IPport);
            if (!res || IPport > 65535)
            {
                lbRegistrStat.Text = "Не подключен";
                MessageBox.Show("Некорректный номер порта", "Ошибка");
                return;
            }

            //timerFindCdc.Start();
            FindCodecs();
        }

        private void timerFindCdc_Tick(object sender, EventArgs e)
        {
            timerFindCdc.Stop();
            //FindCodecs();
        }

        private void btListCdcs_Click(object sender, EventArgs e)
        {
            if (findcdcs)
            {
                findcdcs = false;
                return;
            }

            if (tbCodecs.Visible)
                tbCodecs.Visible = false;
            else
                tbCodecs.Visible = true;
        }

        //end <Поиск кодеков>
        static string IPcdc_str = "";

        private void btConnect_Click(object sender, EventArgs e)
        {
            connect = false;
            lbRegistrStat.BackColor = SystemColors.Control;
            lbRegistrStat.Text = "Не подключен";
            lbRegistrStat.Refresh();
            rmsg_net = "";
            rmsg_unicast = "";
            rmsg_fm = "";
            rmsg_fms = "";
            rmsg_icecast = "";
            rmsg_program = "";
            rmsg_stat[0] = "";
            rmsg_stat[1] = "";
            rmsg_stat[2] = "";
            rmsg_stat[3] = "";
            if (!test_ip(tbIPcdc.Text, "IP кодека"))
            {
                MessageBox.Show("Некорректное значение IP кодека", "Ошибка");
                return;
            }

            //Все порты разделены на три диапазона— общеизвестные(или системные,0—1023), зарегистрированные(или пользовательские,1024—49151)
            //и динамические(или частные,49152—65535)
            bool res = Int32.TryParse(tbPort.Text, out IPport);
            if (!res || IPport > 65535)
            {
                MessageBox.Show("Некорректный номер порта", "Ошибка");
                return;
            }

            lbRegistrStat.BackColor = Color.Yellow;
            lbRegistrStat.Text = "Ждем подключения";
            lbRegistrStat.Refresh();
            //открыть новое

            controller.CreateTCPstream(tbIPcdc.Text, IPport); //регистрация + запрос_настроек
            IPcdc_str = tbIPcdc.Text;
        }

        private void timerTstConnect_Tick(object sender, EventArgs e)
        {
            //Interval до 12.2018 :3сек, после :4сек
            if (connect)
            {
                SendMsg("echo tst_connect\r");
                if (tcp_client != null && !tcp_client.Connected)
                    LossConnect();
            }
        }

        void LossConnect()
        {
            connect = false;
            lbRegistrStat.BackColor = SystemColors.Control; //Color.Red;
            lbRegistrStat.Text = "Объект разорвал соединение";
        }

        public void SendMsg(string sMsg)
        {
            try
            {
#if COM_PORT
                if (intrf_com)
                {
                    puts(sMsg + "\r\n"); //?? или \r\n
                    byte[] data = myEncoding.GetBytes(sMsg);
                    netstream.Write(data, 0, data.Length);
                }
                else
#endif
                {
                    byte[] data = myEncoding.GetBytes(sMsg);
                    controller.netstream.Write(data, 0, data.Length);
                }
            }
            catch (Exception e)
            {
                logger.Error($"SEND MSG {e.StackTrace} {e.Message}");
                MessageBox.Show(e.Message);
            }
        }

        //запрос настроек с кодека
        private void btCdcOptionRqst_Click(object sender, EventArgs e)
        {
#if COM_PORT
            bool is_connect = connect;
            if (intrf_com)
                is_connect = my_serial.IsOpen;
            if (!is_connect)
#else
            if (!connect)
#endif
            {
                MessageBox.Show("Нет подключения к объекту", "Ошибка");
                return;
            }

            CdcOptionRqst();
        }

        public void CdcOptionRqst()
        {
            CdcOptionNetRqst();
            CdcOptionFmRqst();
            CdcOptionIceRqst();
            CdcOptionOutsRqst();
        }

        private void CdcOptionNetRqst()
        {
            //запрос сетевых настроек
            rmsg_net = "";
            rmsg_unicast = "";
            //SendMsg("netstat;get sound_servers\r");
            //19.03.2021
            SendMsg("version;netstat;get sound_servers\r");
            //
            //Thread.Sleep(100);
            //SendMsg("get sound_servers\r"); //unicast по запросу
        }

        private void CdcOptionFmRqst()
        {
            //запрос имя/частота и состояние FMприемников 
            rmsg_fm = "";
            rmsg_fms = "";
            SendMsg("get fmt1;get fmt2;get fmt3;get fmt4;get fmt5;get fmt6;fm_state\r");
            //Thread.Sleep(400);
            //SendMsg("fm_state\r"); //запрос состояния FMприемников
        }

        private void CdcOptionIceRqst()
        {
            //запрос имя/URL потоковых станций
            rmsg_icecast = "";
            SendMsg("url_list\r");
        }

        private void CdcOptionOutsRqst()
        {
            //запрос назначения выходов декодеров
            rmsg_program = "";
            SendMsg("get o1;get o2;get o3;get o4\r");
        }

        public void CdcOptionLsRqst(string text = "")
        {
            rmsg_ls = "";
            SendMsg("Ls " + text + "\r");
        }

        public void CdcOptionSflOpenrRqst(string path)
        {
            rmsg_sfl = "";
            SendMsg($"sfl openr {path}\r");
        }

        public void CdcOptionSflRRqst()
        {
            SendMsg("sfl r\r");
        }

        /*===================================================*/
        private void Transmit_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    //case Keys.Return:
                    btSentCmd_Click(null, null);
                    break;
                /*case Keys.Up:
                    cbTextToSent.Text = strHistory.Prev();
                    break;
                case Keys.Down:
                    cbTextToSent.Text = strHistory.Next();
                    break; */
                case Keys.F8:
                    //strHistory.Clear();
                    cbTextToSent.Text = "";
                    cbTextToSent.Items.Clear();
                    break;
            }
        }

        private void btSentCmd_Click(object sender, EventArgs e)
        {
            //if (cbTextToSent.Text != "")
            {
                string cmd = cbTextToSent.Text;
                //MsgToCon(cmd); //убрал для Eth/COM
#if COM_PORT
                if (intrf_com)
                    puts(cmd + "\r\n");
                else
#endif
                    SendMsg(cmd + "\r");
                //strHistory.Puts(cmd);
                if (cmd.Length > 0 && !cbTextToSent.Items.Contains(cmd))
                    cbTextToSent.Items.Add(cmd);
                if (cbTextToSent.Items.Count > 14)
                    cbTextToSent.Items.RemoveAt(0);
                cbTextToSent.Text = "";
                cbTextToSent.Focus(); //??для COM 
            }
        }

        private void btPing_Click(object sender, EventArgs e)
        {
#if AID_PING
            ping_stop = !ping_stop;
            if (!ping_stop)
            {
                //Маке addr for ping
                PingAddr = tbIPcdc.Text;
                if (ping_subnet) //пинговать подсеть
                {
                    //выбрать подсеть, в которую входит адрес
                    SubnetAddr = PingAddr.Substring(0, PingAddr.LastIndexOf(".") + 1);
                    //начнем с PingAddr
                    curr_ping_addr = int.Parse(tbIPcdc.Text.Substring(PingAddr.LastIndexOf(".") + 1));
                    MsgToCon("Pinging subnet");
                }

                StartPing();
            }
            else
            {
                MsgToCon("Pinging stopped");
                //if (ping_subnet) //на консоль список IP-адресов найденных обьектов (?и имена хостов)
            }
#endif
        }

        //19.03.2021
        string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        public string version_dfrm = "";

        public void SaveVersion(string mm, string dd, string yy) //сохранить дату версии в строке формата YYYY.MM.DD
        {
            version_dfrm = yy + ".";
            for (int i = 0; i < 12; i++)
            {
                if (months[i] == mm)
                {
                    version_dfrm += (i + 1).ToString("D2") + ".";
                    break;
                }
            }

            if (dd.Length < 2)
                dd = "0" + dd;
            version_dfrm += dd;
        }

        //
        //Читаем из файла список потоковых(ICE) станций
        void ReadStationsFile()
        {
            if (!System.IO.File.Exists(StationsFile))
                return;
            listURLs.Clear();
            string s;
            StreamReader Rdr;
            try
            {
                Rdr = new StreamReader(StationsFile, System.Text.Encoding.GetEncoding(1251), false);
            }
            catch
            {
                s = "Список потоковых станций.Ошибка открытия файла " + StationsFile;
                MsgToCon(s);
                MessageBox.Show(s, "Ошибка");
                return;
            }

            while ((s = Rdr.ReadLine()) != null)
            {
                if (s == "" || s.StartsWith("#"))
                    continue;
                listURLs.Add(s);
            }

            Rdr.Close();
            return;
        }

        //
        NetEdit net_edit = null;

        public int EditNet_Dialog()
        {
            net_edit = new NetEdit(this);
            DialogResult res = net_edit.ShowDialog();
            if (res == DialogResult.No)
                return 1;
            else if (res == DialogResult.OK)
                return 2;
            return 0;
        }

        FMedit fm_edit = null;

        public bool EditFM_Dialog()
        {
            fm_edit = new FMedit(this);
            DialogResult res = fm_edit.ShowDialog();
            return res == DialogResult.OK;
        }

        OpenFile open_edit = null;

        public bool OpenFile_Dialog(ActionStateEnum state, string fileName, string information, string path)
        {
            open_edit = new OpenFile(this, state, fileName, information, path);
            DialogResult res = open_edit.ShowDialog();
            return res == DialogResult.OK;
        }

        ICEcast icecast_edit = null;

        //public void Repaint_icecastedit()
        //{
        //    if (icecast_edit != null)
        //        icecast_edit.Repaint();
        //}
        public bool EditICEcast_Dialog()
        {
            icecast_edit = new ICEcast(this);
            //icecast_edit.Parse(rmsg_ice);
            DialogResult res = icecast_edit.ShowDialog();
            return res == DialogResult.OK;
        }

        OutsCodecs program_edit = null;

        public bool EditProgram_Dialog()
        {
            program_edit = new OutsCodecs(this);
            DialogResult res = program_edit.ShowDialog();
            return res == DialogResult.OK;
        }

        LoadingForm loading_form = null;

        public bool LoadingForm_Dialog(ActionStateEnum state,int sizeFile, string output, string input = "")
        {
            loading_form = new LoadingForm(this, state,sizeFile, output, input);
            DialogResult res = loading_form.ShowDialog();
            return res == DialogResult.OK;
        }

        public BitrStat bitrate_stat = null;

        private void MenuNet_Click(object sender, EventArgs e)
        {
            // ConsoleHide();
            int res = EditNet_Dialog();
            if (res == 2) //(ok)
                WaitCdcReboot(); //перезапросить параметры после перезагрузки(применяли с перезагрузкой кодека)
            else if (res == 1)
                CdcOptionNetRqst(); //перезапросить параметры, если применяли без перезагрузки
        }

        private void MenuFM_Click(object sender, EventArgs e)
        {
            ConsoleHide();
            bool ok = EditFM_Dialog();
            if (ok) //перезапросить параметры
                CdcOptionFmRqst(); //если меняли без перезагрузки кодека
            //WaitCdcReboot(); //если с перезагрузкой
        }

        private void MenuStreamRadio_Click(object sender, EventArgs e)
        {
            ConsoleHide();
            bool ok = EditICEcast_Dialog();
            if (ok) //перезапросить параметры
                CdcOptionIceRqst();
            //WaitCdcReboot(); //если с перезагрузкой кодека
        }

        private void MenuProgram_Click(object sender, EventArgs e)
        {
            ConsoleHide();
            bool ok = EditProgram_Dialog();
            if (ok) //перезапросить параметры
                CdcOptionOutsRqst();
            //CdcOptionRqst();
        }

        private void MenuConsole_Click(object sender, EventArgs e)
        {
            if (!tabControl.Visible)
            {
                this.Height = 630;
                tabControl.Visible = !tabControl.Visible;
                ConsoleText.ScrollToCaret();
            }
            else
                ConsoleHide();
        }

        private void MenuStat_Click(object sender, EventArgs e)
        {
            ConsoleHide();
            bitrate_stat = new BitrStat(this);
            bitrate_stat.ShowDialog();
        }

        void ConsoleHide()
        {
            tabControl.Visible = false;
            this.Height = 170;
        }

        private void MenuSentfile_Click(object sender, EventArgs e)
        {
            ConsoleHide();
            bool ok = Sentfile_Dialog();
            //if (ok)  //перезапросить параметры
            //{
            //}
        }

        Sentfiles sentfl = null;

        public bool Sentfile_Dialog()
        {
            sentfl = new Sentfiles(this, tbIPcdc.Text);
            DialogResult res = sentfl.ShowDialog();
            return res == DialogResult.OK;
        }

        private void EditCodecForm_Click(object sender, EventArgs e)
        {
            ConsoleHide();
        }

        //если меняли с перезагрузкой кодека:
        // подождать, пока перезапустится(60..90 сек) и перерегистрироваться с перезапросом параметров
        void WaitCdcReboot()
        {
            connect = false;
            lbRegistrStat.BackColor = Color.Yellow;
            lbRegistrStat.Text = "Подключиться после перезагрузки кодека (ок.70сек)";
            this.Refresh();
            /* //?? закрыть тек.подключение
            if (netstream != null)
                netstream.Close();
            if (tcp_client != null)
                tcp_client.Close(); */
            //Thread.Sleep(70000);
            //поднять соединение и запросить настройки
            //btConnect_Click(null, null);
            //CdcOptionRqst();
        }
        /***********************************************************/
#if AID_PING
        System.Net.NetworkInformation.Ping pingSender = null;
        private int pingsSent, pingsRec, curr_ping_addr = 1;
        private bool ping_stop = true, set_next_ping_addr, ping_subnet = false;
        private string PingAddr, SubnetAddr;
        int pings_num, pings_tout;

        const int GRP_PINGS_RPT = 1,
            GRP_PINGS_TOUT = 200,
            PINGS_RPT = 3,
            PINGS_TOUT = 2000;

        //Если делать Ping в отдельном потоке:
        //Can be used to notify when the operation completes
        //Уведомляет ожидающий поток о том, что произошло событие
        //AutoResetEvent resetEvent = new AutoResetEvent(false);
        private void StartPing()
        {
            if (ping_subnet)
            {
                PingAddr = SubnetAddr + curr_ping_addr.ToString();
                pings_num = GRP_PINGS_RPT;
                pings_tout = GRP_PINGS_TOUT;
            }
            else
            {
                pings_num = PINGS_RPT;
                pings_tout = PINGS_TOUT;
            }

            MsgToCon("Pinging " + PingAddr + " with 32 bytes of data:");
            //Reset the number of pings
            pingsSent = 0;
            pingsRec = 0;
            //Send the ping
            SendPing();
        }

        private void SendPing()
        {
            if (ping_stop)
                return;
            //Create a buffer(32 bytes) of data to be transmitted.
            byte[] packetData = Encoding.ASCII.GetBytes("................................");
            //Jump through 50(30) routing nodes tops, and don't fragment the packet
            PingOptions packetOptions = new PingOptions(30, true);
            //Send the ping asynchronously (Ttl=128 по умолчанию)
            pingSender.SendAsync(PingAddr, pings_tout, packetData, packetOptions); //, resetEvent);
            //Если Ping в отдельном потоке, можно заблокировать:
            //resetEvent.WaitOne()- Блокирует текущий поток до получения сигнала объектом WaitHandle
            //или resetEvent.WaitOne(int ms)- Блокирует текущий поток до получения текущим дескриптором WaitHandle
            // сигнала, используя 32-разрядное целочисленное значение со знаком для указания интервала времени
        }

        private void pingSender_Complete(object sender, PingCompletedEventArgs e)
        {
            set_next_ping_addr = true;
            //If the operation was canceled, display a message to the user.
            if (e.Cancelled)
            {
                MsgToCon("Ping was canceled...");
                //The main thread can resume
                //Задает сигнальное состояние события, позволяя ожидающим потокам продолжить
                //(AutoResetEvent)e.UserState).Set();
            }
            else if (e.Error != null)
            {
                MsgToCon("An error occured: " + e.Error);
                //The main thread can resume
                //((AutoResetEvent)e.UserState).Set();
            }
            else
            {
                //Call the method that displays the ping results, and pass the information with it
                ShowPingResults(e.Reply);
            }

            if (set_next_ping_addr)
            {
                if (ping_subnet && (++curr_ping_addr <= 255))
                    StartPing();
                else
                {
                    ping_stop = true;
                    MsgToCon("Pinging was stopped");
                }
            }
        }

        public void ShowPingResults(PingReply pingResponse)
        {
            if (pingResponse == null)
            {
                //We got no response
                MsgToCon("There was no response");
                return;
            }
            else if (pingResponse.Status == IPStatus.Success)
            {
                //We got a response, let's see the statistics
                pingsRec++;
                MsgToCon("Reply from " + pingResponse.Address.ToString() + ": bytes=" + pingResponse.Buffer.Length
                         + " time=" + pingResponse.RoundtripTime + "ms TTL=" + pingResponse.Options.Ttl);
            }
            else
            {
                //The packet didn't get back as expected, explain why
                MsgToCon("Ping was unsuccessful: " + pingResponse.Status);
            }

            //Increase the counter so that we can keep track of the pings sent
            //пингуем обьект:3пинга, пингуем подсеть:1пинг
            if (++pingsSent < pings_num)
            {
                set_next_ping_addr = false;
                SendPing();
            }
            else
            {
                if (pingsRec > 0)
                {
                    /* string HostName = "?";
                    try
                    {
                        //HostName = System.Net.Dns.GetHostByAddress(PingAddr).HostName; //obsolved
                        HostName = System.Net.Dns.GetHostEntry(PingAddr).HostName;
                        //IPHostEntry IPhe= System.Net.Dns.GetHostEntry(PingAddr);
                        //HostName= IPhe.HostName;
                    }
                    catch //(System.Net.Sockets.SocketException e)
                    {
                      //MsgToCon("SocketException.ErrorCode: " + e.ErrorCode.ToString()
                      //    +" "+ e.Message);
                      //Для БПР с linux: GetHostByAddress дает ошибку 11004 WSANO_DATA
                      //Valid name, no data record of requested type.
                      //The requested name is valid and was found in the database, but it does not have
                      //the correct associated data being resolved for. The usual example for this is
                      //a host name-to-address translation attempt (using gethostbyname or WSAAsyncGetHostByName)
                      //which uses the DNS (Domain Name Server). An MX record is returned
                      //but no A record—indicating the host itself exists, but is not directly reachable.
                    }*/
                    MsgToCon("------ Пинг " + PingAddr + "  Отправлено: "
                             + pingsSent + ". Получено:" + pingsRec);
                }
            }
        }
#endif //AID_PING
        /*===================================================*/
        private void rbEther_Click(object sender, EventArgs e)
        {
            intrf_com = false;
            panCOM.Visible = false;
            panIP.Visible = true;
            lbRegistrStat.Visible = true;
            btFindCodecs.Enabled = true;
            statStrip.Visible = false;
            MsgToCon("\n--- Интерфейс: Ethernet\r\n");
        }

        private void rbCOM_Click(object sender, EventArgs e)
        {
            intrf_com = true;
            panIP.Visible = false;
            panCOM.Visible = true;
            lbRegistrStat.Visible = false;
            btFindCodecs.Enabled = false;
            //
            if (netstream != null)
                netstream.Close();
            connect = false;
            lbRegistrStat.BackColor = SystemColors.Control;
            lbRegistrStat.Text = "Не подключен";
            //
            statStrip.Visible = true;
            MsgToCon("\n--- Интерфейс: COM-порт\r\n");
        }

        bool con_pause = false;

        private void btClearCon_Click(object sender, EventArgs e)
        {
            ConsoleText.Clear();
        }

        private void btConsolePause_Click(object sender, EventArgs e)
        {
            if (con_pause)
            {
                btConsolePause.BackColor = SystemColors.Control;
                con_pause = false;
            }
            else
            {
                btConsolePause.BackColor = Color.LimeGreen;
                con_pause = true;
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            int value = tabControl.SelectedIndex;
            if (value == 1)
            {
                //Заполнение таблицы и поля
                string path = Directory.GetCurrentDirectory();
                textLocalPath.Text = path;

                LocalRefresh();

                if (connect)
                {
                    CdcOptionLsRqst(textRemotePath.Text);
                }

                //Получение списка дисков
                GetDriversForComboBox();
            }
        }

        private void GetDriversForComboBox()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            comboBoxDriveInfo.Items.Clear();
            foreach (DriveInfo d in allDrives)
            {
                comboBoxDriveInfo.Items.Add(d.Name);
            }
        }

        private void ReadRemoteData()
        {
            if (!rmsg_ls.Equals(""))
            {
                string[] text = rmsg_ls.Split('\r', '\n');
                List<string> list = new List<string>(text);
                RemoteRefresh(list);
            }
        }

        private void comboBoxDriveInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedState = comboBoxDriveInfo.SelectedItem.ToString();
            textLocalPath.Clear();
            textLocalPath.Text = selectedState;
            LocalRefresh();
        }

        private void LocalRefresh()
        {
            string sPath = textLocalPath.Text;
            listViewLocal.Items.Clear();

            try
            {
                if (!Directory.GetDirectoryRoot(sPath).Equals(sPath))
                    AddDataInListView(sFile: sPath, isParent: true);
                string[] dirs = Directory.GetDirectories(sPath);
                Array.Sort(dirs);
                foreach (string dir in dirs)
                    AddDataInListView(dir, false);
                string[] files = Directory.GetFiles(sPath);
                Array.Sort(files);
                foreach (string file in files)
                    AddDataInListView(file, false);
            }
            catch (Exception e) //Устройство (например CD-ROM) может быть не готов
            {
                MessageBox.Show(e.Message);
            }
        }


        private void AddDataInListView(string sFile, bool isParent)
        {
            System.IO.FileInfo fInfo = new System.IO.FileInfo(sFile);
            ListViewItem item;
            if (isParent)
            {
                item = new ListViewItem("..");
            }
            else
            {
                item = new ListViewItem(fInfo.Name);
            }

            if ((fInfo.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
                item.SubItems.Add(""); //Size
            else
                item.SubItems.Add(fInfo.Length.ToString());
            item.SubItems.Add(fInfo.LastAccessTime.ToString());
            //item.SubItems.Add(fInfo.LastWriteTime.ToShortDateString() + " " + fInfo.LastWriteTime.ToLongTimeString());//Date&Time
            //item.SubItems.Add(fInfo.LastWriteTimeUtc.ToShortDateString() + " " + fInfo.LastWriteTimeUtc.ToLongTimeString());//Date&Time
            string sAtr = "";
            if ((fInfo.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
                sAtr = sAtr + "/";
            if ((fInfo.Attributes & FileAttributes.Archive) == FileAttributes.Archive)
                sAtr = sAtr + "a";
            if ((fInfo.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                sAtr = sAtr + "r";
            if ((fInfo.Attributes & FileAttributes.System) == FileAttributes.System)
                sAtr = sAtr + "s";
            if ((fInfo.Attributes & FileAttributes.Normal) == FileAttributes.Normal)
                sAtr = sAtr + "f";
            item.SubItems.Add(sAtr);
            listViewLocal.Items.Add(item);
        }

        private void LocalEnter()
        {
            ListView.SelectedListViewItemCollection items = listViewLocal.SelectedItems;
            if (items.Count == 0)
                return;

            string sFile = items[0].Text;

            if (items[0].Text.Equals(".."))
            {
                string text = textLocalPath.Text;
                if (text.IndexOf("\\").Equals(text.LastIndexOf("\\")))
                    textLocalPath.Text = text.Substring(0, text.LastIndexOf("\\") + 1);
                else
                    textLocalPath.Text = text.Substring(0, text.LastIndexOf("\\"));
                LocalRefresh();
            }
            else
            {
                System.IO.FileInfo file = new System.IO.FileInfo(sFile);
                if (!items[0].SubItems[3].Text.Contains("/"))
                {
                    pathLocal = @"" + textLocalPath.Text + "\\" + file.Name;
                    try
                    {
                        string fileText = System.IO.File.ReadAllText(pathLocal, Encoding.UTF8);
                        ActionState = ActionStateEnum.LocalShow;
                        OpenFile_Dialog(ActionState, file.Name, fileText, pathLocal);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        logger.Error(String.Format($"{ex.Message} {ex.StackTrace}", DateTime.Now));
                    }
                }
                else
                {
                    if (textLocalPath.Text.EndsWith("\\"))
                        textLocalPath.Text += file.Name.ToString();
                    else textLocalPath.Text = textLocalPath.Text + "\\" + file.Name.ToString();
                    LocalRefresh();
                }
            }
        }


        private void listView_DoubleClick(object sender, EventArgs e)
        {
            LocalEnter();
        }

        private void listViewRemote_DoubleClick(object sender, EventArgs e)
        {
            //TODO
            RemoteEnter();
        }


        private void RemoteRefresh(List<string> list) //? сделать public для ModifyFirmware
        {
            listViewRemote.Items.Clear();
            if (list.Count == 0)
                return;
            List<string[]> matrix = new List<string[]>();
            List<string[]> result = new List<string[]>();
            for (int i = 0; i < list.Count - 1; i++)
            {
                if (list[i].StartsWith("Ok"))
                    continue;
                if (list[i].StartsWith(":."))
                    break;
                matrix.Add(list[i].Split(' '));
            }

            matrix.Sort(CompareFiles);

            foreach (string[] file in matrix)
            {
                if (file[4] == "/")
                    result.Insert(0, file);
                else
                    result.Add(file);
            }

            if (!textRemotePath.Text.Equals("/"))
            {
                ListViewItem back = new ListViewItem(new[] { "/", ".." });
                listViewRemote.Items.Add(back);
            }

            foreach (string[] file in result)
            {
                ListViewItem item = new ListViewItem(new[]
                    { file[4], file[0], file[1], String.Concat(file[2] + " ", file[3]) });
                listViewRemote.Items.Add(item);
            }
        }

        private void RemoteEnter()
        {
            ListView.SelectedListViewItemCollection items = listViewRemote.SelectedItems;
            if (items.Count == 0)
                return;


            if (items[0].SubItems[1].Text.Equals(".."))
            {
                string path = textRemotePath.Text.TrimEnd('/');
                textRemotePath.Text = path.Substring(0, path.LastIndexOf("/") + 1);
                CdcOptionLsRqst(textRemotePath.Text);
            }
            else
            {
                sFile = items[0].SubItems[1].Text;
                System.IO.FileInfo file = new System.IO.FileInfo(sFile);
                if (!items[0].SubItems[0].Text.Contains("/"))
                {
                    string path = @"" + textRemotePath.Text + file.Name;

                    //sFile = items[0].Text;
                    int sizeFile = int.Parse(items[0].SubItems[2].Text);
                    pathRemote = textRemotePath.Text + sFile;
                    ActionState = ActionStateEnum.RemoteShow;
                    LoadingForm_Dialog(ActionState, sizeFile, path, pathRemote);
                }
                else
                {
                    textRemotePath.Text = textRemotePath.Text + file.Name + "/";
                }
            }
        }

        private void textRemotePath_TextChanged(object sender, EventArgs e)
        {
            CdcOptionLsRqst(textRemotePath.Text);
        }

        private static int CompareFiles(string[] item1, string[] item2)
        {
            return item1[0].CompareTo(item2[0]);
        }


        public void MsgToCon(string s)
        {
            if (con_pause || !paConsole.Visible)
                return;
            if (ConsoleText.Lines.Length >= 500)
            {
                if (this.WindowState != FormWindowState.Minimized) //наша форма не свернута
                {
                    ConsoleText.Select(0, ConsoleText.TextLength / 5);
                    ConsoleText.Cut(); //!!move в Clipboard
                }
                else
                    for (int i = 0; i < 50; i++)
                    {
                        if (ConsoleText.Lines[0].Length > 0)
                            ConsoleText.Lines[0].Remove(0);
                    }
            }

            s = "\n" + s;
#if COM_PORT
            WriteLogFile(s);
#endif
            //s = s.Replace("\n", "\r\n"); //12.2018- изменилось ПО кодека
            ConsoleText.AppendText(s);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetDriversForComboBox();
            CdcOptionLsRqst(textRemotePath.Text);
            LocalRefresh();
        }

        public void UpdateData()
        {
            GetDriversForComboBox();
            CdcOptionLsRqst(textRemotePath.Text);
            LocalRefresh();
        }
#if CMD_HISTORY
        static StrHistory strHistory = new StrHistory(20);
        public class StrHistory
        {
            string[] aStr;
            int iPuts; //последняя занесенная
            int iView; //текущая отображенная
            const bool beep = true;

            public StrHistory(int count)
            {
                aStr = new string[count];
                Clear();
            }
            public void Clear()
            {
                for (int i = 0; i < aStr.Length; i++)
                    aStr[i] = "";
                iPuts = -1;
                iView = 0;
            }
            public void Puts(string s)
            {
                if (s == "")
                    return; // пустая
                if (iPuts < 0 || aStr[iPuts] != s) //не совпадает с последней занесенной
                {
                    if (++iPuts >= aStr.Length)
                        iPuts = 0;
                    aStr[iPuts] = s;
                }
                //предыдущие нажатия стрелок могли изменить iView, потому даже при совпадении освежаем
                iView = iPuts + 1;
            }

            [DllImport("kernel32.dll")]
            static extern bool Beep(int freq, int duration);
            public string Prev()
            {
                int i = iView;
                if (--i < 0)
                    i = aStr.Length - 1;
                if (aStr[i] != "") //предыдущая не пуста
                    iView = i;
                else
                {
                    if (beep)
                        Beep(1000, 100);
                }
                return aStr[iView];
            }
            public string Next()
            {
                int i = iView; //текущая отображенная строка
                if (++i >= aStr.Length)
                    i = 0;
                if (aStr[i] != "") //следующая не пуста
                    iView = i;
                else
                {
                    if (beep)
                        Beep(1000, 100);
                }
                return aStr[iView];
            }
        }
#endif //CMD_HISTORY
        private void PutNewLineToCon(string s)
        {
            AddMsgToCon(s, true);
        }

        private void PutCurrLineToCon(string s)
        {
            AddMsgToCon(s, false);
        }

        public void AddMsgToCon(string s, bool newline)
        {
            if (con_pause || !paConsole.Visible)
                return;

            if (this.WindowState == FormWindowState.Minimized) //наша форма свернута
                return; //не выводить, иначе через некоторое время заклинивает
            Invoke((MethodInvoker)(() =>
            {
                if (ConsoleText.Lines.Length >= 250)
                {
                    if (this.WindowState != FormWindowState.Minimized) //наша форма не свернута
                    {
                        ConsoleText.Select(0, ConsoleText.TextLength / 5);
                        ConsoleText.Cut(); //!! move в Clipboard
                        //try  //бывают проблемы при интенсивном выводе
                        //{    //?? а если запущены 2 терминала
                        //    Clipboard.Clear(); //если свернута- можем навредить другим задачам
                        //}
                        //catch { }
                    }
                    else
                        for (int i = 0; i < 50; i++)
                        {
                            if (ConsoleText.Lines[0].Length > 0)
                                ConsoleText.Lines[0].Remove(0);
                        }
                }

                if (newline) // || (ConsoleText.Lines.Length > 0
                    //   && ConsoleText.Lines[ConsoleText.Lines.Length - 1].Length >= 80))
                    s = "\r\n" + s;
                else
                    s = s.Replace("\n", "\r\n"); //формат кодека 
                ConsoleText.AppendText(s);
                WriteLogFile(s);
            }));
        }

        private void PortMode_Set()
        {
            my_serial.BaudRate = int.Parse(lbBaud.Text);
            tSStatLbBaud.Text = "Baud " + my_serial.BaudRate.ToString() + "    ";
            tSStatLbLen.Text = "Bits " + my_serial.DataBits.ToString() + "    ";
            tSStatLbPrty.Text = "Parity " + my_serial.Parity.ToString() + "    ";
            tSStatLbStop.Text = "Stop " + my_serial.StopBits.ToString() + "    ";
            if (my_serial.IsOpen)
                my_serial.Close();
            btOpen_Click(null, null);
        }
#if COM_PORT
        //const int SNTBUF_LEN = 1024;
        private int puts(string s)
        {
            byte[] wBytes;

            if (!my_serial.IsOpen)
            {
                PutNewLineToCon("Порт " + my_serial.PortName + " Закрыт");
            }
            else if (s.Length != 0)
            {
                if (UseCTS) //!!использовать CTS
                {
                    if (!my_serial.CtsHolding)
                    {
                        if (enabl_dbg_msg)
                            PutNewLineToCon("?? Нет CTS");
                        return 0;
                    }
                }

                // Кириллица из Unicode в Windows (!не вся):
                // CU= 0x400 | (CW-176)  :   CW= (CU&0x7F)+176
                wBytes = my_serial.Encoding.GetBytes(s);
                try
                {
                    my_serial.Write(wBytes, 0, wBytes.Length);
                    return 1;
                }
                catch (Exception ex)
                {
                    if (enabl_dbg_msg)
                        PutNewLineToCon("WriteToCom fail: " + ex.GetType().Name
                                                            + ": " + ex.Message);
                }
                //!!или: установить  my_serial.NewLine= "\r";
                //my_serial.WriteLine(s); // записывает строку+SerialPort.NewLine(default:\r\n) в вых.буфер 
            }

            return 0;
        }
#endif //COM_PORT
        /* Получил по TCP:
        Ok netstat :
        : type cs Intel(R) Celeron(R) CPU 2.80GHz  kernel 2.6.19.7
        : ident 5
        : dhcp0 0
        : gateway 192.168.0.99
        : eth0 192.168.0.161
        : mask0 255.255.255.0
        : dns 192.168.0.99
        : node1 192.168.0.161
        : node2
        : node3
        : node4
        : multicast 224.22.41.16
        : ts1
        : ts2
        : ts3
        : ts4
        :.
        Ok get: sound_servers:
        Ok get: fmt1: 105.70 Ретро ФМ
        Ok get: fmt2: 101.00 Радио Дача
        Ok get: fmt3: 103.50 Радио 3
        Ok get: fmt4: 105.00 Love Radio
        Ok get: fmt5:
        Ok get: fmt6:
        Ok get fm_state :
        : fms1
        : fms2
        : fms3
        : fms4
        : fms5
        : fms6
        :.
        Ok url_list :
        : url1 -Останов "icecast.vgtrk.cdnvideo.ru/rrzonam_mp3_192kbps" Радио России 192k
        : url2 -Останов "icecast.vgtrk.cdnvideo.ru/mayakfm_mp3_192kbps" Маяк FM 192k
        : url3 -Останов "icecast.vgtrk.cdnvideo.ru/vestifm_mp3_64kbps" Вести FM 64k
        : url4 -Останов "icecast.vgtrk.cdnvideo.ru/kulturafm_mp3_192kbps" Радио Культура 192k
        :.
        Ok get: o1: gr
        Ok get: o2: gr
        Ok get: o3: gr
        Ok get: o4: gr */
        // Прочитать символы из входного потока (для вывода в консоль и парсера)
#if PROB_QUEUE
        public class Message   // tsnode bt Korotayew
        {
            public string Author = "";
            public string Type = "";
            public string Address = "";  // from whom
            public string _text = "";    // String text
            public byte[] Bin; 		// Raw text
            unsafe void BinToString()
            { 
	            fixed ( byte* b = &Bin[0] )
	            { 
                    _text = Marshal.PtrToStringAnsi( (System.IntPtr)b,Bin.Length );   
                }
            }
            public int  Length 
            {  
                get { return Bin.Length; } 
            }
            public string Text     
            {	
                get { 
                    if( _text.Length== 0 && Bin!=null && Bin.Length> 0 )
	                    BinToString();
	                return _text;
    	        }     
            }
        }  // Message
         
        private static System.Collections.Queue mQueue = new System.Collections.Queue();
        public static Message GetMessage()       
        {
            lock (mQueue)
            {
                return mQueue.Count > 0 ? (Message)mQueue.Dequeue() : null;
            }
        }
        //Message m = null;
        public static void ToQueue()
        {
            Message ms = new Message();
            ms._text = text;
            ms.Type = type;
            ms.Address = net_address;
            lock (mQueue)
            {
                mQueue.Enqueue(ms);
            }
        }
#endif //PROB_QUEUE
        //delegate void GetCharsDelegate(/*bool print*/);
        void GetChars( /*bool print*/)
        {
            int Rcvd = my_serial.BytesToRead;
            //      = my_serial.ReceivedBytesThreshold; //кол-во байт во вход.буфере до возникн-я прер-я прм
            if (Rcvd == 0)
                return;

            //try
            //{
            //    if (ConsoleText.InvokeRequired)
            //    {
            //        ConsoleText.BeginInvoke(new GetCharsDelegate(GetChars),
            //            new object[] { /*print*/ });
            //        return;
            //    }
            //}
            //catch
            //{  }
            //if (need_dos_encode) //??для БКТП_Z180
            //    my_serial.Encoding = System.Text.Encoding.GetEncoding("cp866");
            //else
            //    my_serial.Encoding = System.Text.Encoding.GetEncoding("windows-1251");//если в инициал-ии- не работает для кириллицы
            //
            //string s = "";
            char[] RecBytes = new char[Rcvd];
            my_serial.Read(RecBytes, 0, Rcvd);
            //if (!print)
            //    return;

            Parse(Rcvd, RecBytes);
        }

        string s = "",
            msg_t = "";
#if PARSE_MSG
        int curr_list = 0;
#else
        bool was_cr = false, was_lf = false;
#endif
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

        void Parse()
        {
            string str = "";
            while (true)
            {
                str = GetLine();
                // string s1 = str;
                if (!str.Contains("tst_connect") && !findcdcs)
                    AddMsgToCon(str, true);
                if (!intrf_com)
                {
                    if (str.Contains("login OK"))
                    {
                        if (!findcdcs)
                        {
                            connect = true;
                            lbRegistrStat.BackColor = Color.LimeGreen;
                            lbRegistrStat.Text = "Подключен";
                        }
                    }
                    else if (str.Contains("?unknown: login:")) //"?unknown: login: temas"
                    {
                        if (!findcdcs)
                        {
                            lbRegistrStat.BackColor = Color.Yellow;
                            lbRegistrStat.Text = "Нет поддержки настроек";
                        }

                        cdc_newver = false;
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
                        SaveVersion(ss[3], ss[4], ss[5]);
                }

                if (str.StartsWith("Ok netstat"))
                {
                    rmsg_net = str;
                    while (true)
                    {
                        str = GetLine();
                        rmsg_net += str;
                        AddMsgToCon(str, true);
                        if (str.StartsWith(":."))
                            break;
                    }
                }

                if (str.StartsWith("sound_servers: "))
                {
                    rmsg_unicast = str;
                    while (true)
                    {
                        str = GetLine();
                        rmsg_unicast += str;
                        AddMsgToCon(str, true);
                        if (str.StartsWith(":."))
                            break;
                    }
                }

                if (str.StartsWith("Ok get: fmt"))
                {
                    rmsg_fm = str;
                    while (true)
                    {
                        str = GetLine();
                        rmsg_fm += str;
                        AddMsgToCon(str, true);
                        if (str.StartsWith(":."))
                            break;
                    }
                }

                if (str.StartsWith("Ok get fm_state"))
                {
                    rmsg_fms = str;
                    while (true)
                    {
                        str = GetLine();
                        rmsg_fms += str;
                        AddMsgToCon(str, true);
                        if (str.StartsWith(":."))
                            break;
                    }
                }

                if (str.StartsWith("Ok url_list"))
                {
                    rmsg_icecast = str;
                    while (true)
                    {
                        str = GetLine();
                        rmsg_icecast += str;
                        AddMsgToCon(str, true);
                        if (str.StartsWith(":."))
                            break;
                    }
                }

                if (str.StartsWith("Ok Ls"))
                {
                    rmsg_ls = str;
                    while (true)
                    {
                        str = GetLine();
                        rmsg_ls += str;
                        AddMsgToCon(str, true);
                        if (str.StartsWith(":."))
                        {
                            ReadRemoteData();
                            listViewRemote.Refresh();
                            break;
                        }
                    }
                }

                if (str.StartsWith("Ok get: o"))
                {
                    rmsg_program = str;
                    while (true)
                    {
                        str = GetLine();
                        rmsg_program += str;
                        AddMsgToCon(str, true);
                        if (str.StartsWith(":."))
                            break;
                    }
                }

                if (str.StartsWith("bitrst"))
                {
                    int nchn = Convert.ToInt32(str.Substring(6, 1)) - 1;
                    if (nchn < 4)
                    {
                        rmsg_stat[nchn] = str.Substring(8);
                        if (bitrate_stat != null)
                            bitrate_stat.Parse(nchn);
                    }
                }

                if (str.StartsWith("t_cpu"))
                {
                    t_cpu = str.Substring(6, 2) + " °C";
                    if (bitrate_stat != null)
                        bitrate_stat.ShowTcpu();
                }
            }
        }

        void Parse(int Rcvd, char[] RecBytes)
        {
            for (int i = 0; i < Rcvd; i++)
            {
#if PARSE_MSG
                // if (RecBytes[i] == '\n')
                //    s += "<LF>";
                // else if (RecBytes[i] == '\r')
                //    s += "<CR>";
                s += RecBytes[i].ToString();
                if ( /*RecBytes[i] == '\r' ||*/ RecBytes[i] == '\n')
                {
                    //!! по IP строка заканч-ся \n ,по COM- \r\n
                    string s1 = s.Substring(0, s.Length - 1); //убрать \n
                    if ( /*s1.Length > 0 &&*/ /*s1 != "\n" &&*/ /*s1 != "\r" &&*/
                        !s1.Contains("tst_connect") && !findcdcs)
                        AddMsgToCon(s1, true);
                    if (!intrf_com)
                    {
                        if (s.Contains("login OK"))
                        {
                            if (!findcdcs)
                            {
                                connect = true;
                                lbRegistrStat.BackColor = Color.LimeGreen;
                                lbRegistrStat.Text = "Подключен";
                            }
                        }
                        else if (s.Contains("?unknown: login:")) //"?unknown: login: temas"
                        {
                            if (!findcdcs)
                            {
                                lbRegistrStat.BackColor = Color.Yellow;
                                lbRegistrStat.Text = "Нет поддержки настроек";
                            }

                            cdc_newver = false;
                            //s = "";
                            //return;
                        }
                    }

                    if (curr_list > 0)
                    {
                        msg_t += s;
                        if (s.StartsWith(":."))
                        {
                            // ArrayList al= Parse(msg_t);
                            // for (int il = 0; il < al.Count; il++ )
                            //   AddMsgToCon("al_"+il+"> " + al[il].ToString(), true);
                            if (curr_list == 1)
                                rmsg_net = msg_t;
                            else if (curr_list == 2)
                                rmsg_fms = msg_t;
                            else if (curr_list == 3)
                                rmsg_icecast = msg_t;
                            else if (curr_list == 4)
                                rmsg_ls = msg_t;
                            curr_list = 0;
                            // string[] stringSeparators = new string[] { "\r", "\n" };
                            // string[] subs = rmsg_net.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                            // for (int ii = 0; ii < subs.Length; ii++)
                            //   MsgToCon("subs_"+ii+"="+subs[ii]);
                            msg_t = "";
                        }
                    }

                    int ind;
                    //19.03.2021
                    if (s.StartsWith("Ok version"))
                    {
                        //PutNewLineToCon(s);
                        string[] ss = s.Split(new string[] { " " }, //stringSeparators,
                            StringSplitOptions.RemoveEmptyEntries);
                        if (ss.Length > 5)
                            SaveVersion(ss[3], ss[4], ss[5]);
                    }

                    //
                    //if (s.IndexOf("Ok netstat") >= 0)
                    if (s.StartsWith("Ok netstat"))
                    {
                        curr_list = 1;
                        msg_t = s; // "";
                    }
                    else
                    {
                        ind = s.IndexOf("sound_servers: ");
                        if (ind >= 0)
                        {
                            curr_list = 0;
                            rmsg_unicast = s.Substring(ind + 15);
                        }
                        else
                        {
                            ind = s.IndexOf("Ok get: fmt");
                            if (ind >= 0)
                            {
                                rmsg_fm += s.Substring(ind);
                            }

                            //ind = s.IndexOf("Ok get fm_state");
                            //if (ind >= 0)
                            if (s.StartsWith("Ok get fm_state"))
                            {
                                curr_list = 2;
                                msg_t = s; // "";
                            }

                            //ind = s.IndexOf("Ok url_list");
                            //if (ind >= 0)
                            if (s.StartsWith("Ok url_list"))
                            {
                                curr_list = 3;
                                msg_t = s;
                            }

                            if (s.StartsWith("Ok Ls"))
                            {
                                curr_list = 4;
                                msg_t = s;
                            }
                        }

                        ind = s.IndexOf("Ok get: o");
                        if (ind >= 0)
                        {
                            curr_list = 0;
                            rmsg_program += s.Substring(ind);
                        }

                        ind = s.IndexOf("bitrst");
                        if (ind >= 0)
                        {
                            curr_list = 0;
                            int nchn = Convert.ToInt32(s.Substring(ind + 6, 1)) - 1;
                            if (nchn < 4)
                            {
                                rmsg_stat[nchn] = s.Substring(ind + 8);
                                if (bitrate_stat != null)
                                    bitrate_stat.Parse(nchn);
                            }
                        }

                        ind = s.IndexOf("t_cpu");
                        if (ind >= 0)
                        {
                            //curr_list = 0;
                            t_cpu = s.Substring(ind + 6, 2) + " °C";
                            if (bitrate_stat != null)
                                bitrate_stat.ShowTcpu();
                        }
                    }

                    s = "";
                }
            }
#else
                if (RecBytes[i] >= 0x20)
                    s += RecBytes[i].ToString();
                if (RecBytes[i] == '\r') // Вывод: Translate CR -> newline (если не после LF)
                {
                    if (!was_lf)
                    {
                        PutCurrLineToCon(s);
                        PutNewLineToCon("");
                        was_cr = true;
                    }
                    else
                        was_lf = false;
                }
                else  // Вывод: Translate LF -> newline (если не после CR)
                {
                    if (RecBytes[i] == '\n')
                    {
                        if (!was_cr)
                        {
                            PutCurrLineToCon(s);
                            PutNewLineToCon("");
                            was_lf = true;
                        }
                    }
                    else
                        was_lf = false;
                    was_cr = false;
                }
                if (s.Length > 70)
                    PutCurrLineToCon(s);
            } // прошли все принятые байты 
            if (s.Length > 0)
                PutCurrLineToCon(s);
            s = "";
#endif
        }

        private void myserial_DtRec(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (intrf_com)
                GetChars();
        }

        private void myserial_ErrRec(object sender, System.IO.Ports.SerialErrorReceivedEventArgs e)
        {
            //Received byte error
            if (enabl_dbg_msg)
                PutNewLineToCon("{E:" + e.EventType.ToString() + "}");
        }

        private void myserial_PinChngd(object sender, System.IO.Ports.SerialPinChangedEventArgs e)
        {
            if (my_serial.IsOpen)
            {
                tSStatLbPinStat.Text = "RTS_CTS_handshake:" + (RTShandshake ? "Yes" : "No") + "   CD:" +
                                       my_serial.CDHolding
                                       + "   DSR:" + my_serial.DsrHolding + "   CTS:" + my_serial.CtsHolding;
                //+"  RtsEnable "+ my_serial.RtsEnable;
            }
        }

        private void lbBaud_SelIndChgd(object sender, System.EventArgs e)
        {
            if (lbBaud.SelectedIndex < 0)
                return;
            PortMode_Set(); //(int.Parse(lbBaud.Items[lbBaud.SelectedIndex].ToString()));
        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            string msg = null;
            // ?? что делать, если устройство недоступно или порт не существует
            if (!my_serial.IsOpen)
            {
                my_serial.PortName = "COM" + (int)UpDnNport.Value;
                try
                {
                    my_serial.Open();
                    msg = " Открыт";
                    //ReadStationsFile();
                }
                catch (Exception ex)
                {
                    //operation not be performed because the specified part of the file is locked
                    PutNewLineToCon("Operation <OPEN> could not be performed: " + ex.Message);
                }
            }
            else
            {
                try
                {
                    my_serial.Close();
                    msg = " Закрыт";
                }
                catch (Exception ex)
                {
                    //operation not be performed because the specified part of the file is locked
                    PutNewLineToCon("Operation <CLOSE> could not be performed: " + ex.Message); //ex.GetType().Name);
                }
            }

            if (msg != null)
                PutNewLineToCon("Порт " + my_serial.PortName + msg);
            if (my_serial.IsOpen)
            {
                myserial_PinChngd(null, null);
                btOpen.BackColor = Color.Green;
                //CdcOptionRqst(); //запрос настроек
            }
            else
            {
                tSStatLbPinStat.Text = " Closed";
                btOpen.BackColor = Color.Tomato;
            }

            statStrip.Refresh();
        }

        private void Nport_chngd(object sender, EventArgs e)
        {
            if (my_serial.PortName != "COM") //чтобы не открывать при заполнении UpDown из .config
            {
                if (my_serial.IsOpen)
                    btOpen_Click(null, null); //закрыть открытый COM-порт
                btOpen_Click(null, null);
            }
        }

        private void btLogEnbl_Click(object sender, EventArgs e)
        {
            log_enbl = !log_enbl;
            if (log_enbl)
                btLogEnbl.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            else
                btLogEnbl.BackColor = System.Drawing.SystemColors.Control;
        }

        void WriteLogFile(string s)
        {
            if (log_enbl)
            {
                FileStream wrFStream = new FileStream("term.log",
                    FileMode.Append, FileAccess.Write); //создать | перезаписать
                StreamWriter sWrt = new StreamWriter(wrFStream, System.Text.Encoding.GetEncoding("windows-1251"));
                if (first_rec)
                {
                    sWrt.Write("\r\n\r\n++++++++++ Протокол открыт [" + DateTime.Today.ToLongDateString() +
                               " " + DateTime.Now.ToLongTimeString() + "]\r\n");
                    first_rec = false;
                }

                sWrt.Write(s);
                sWrt.Close();
            }
        }

        public void onChanged(List<string> files)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() =>
                {
                    RemoteRefresh(files);
                    listViewRemote.Refresh();
                }));
            }
            else
            {
                RemoteRefresh(files);
                listViewRemote.Refresh();
            }
        }

        public void onConnected(bool connected)
        {
            if (connected)
            {
                lbRegistrStat.BackColor = Color.LimeGreen;
                lbRegistrStat.Text = "Подключен";
            }
            else
            {
                lbRegistrStat.BackColor = Color.Yellow;
                lbRegistrStat.Text = "Нет поддержки настроек";
            }
        }

        public void SetLabelStatus(string status)
        {
            switch (status)
            {
                case "error":
                    if (InvokeRequired)
                        Invoke((MethodInvoker)(() =>
                        {
                            lbRegistrStat.BackColor = Color.Red;
                            lbRegistrStat.Text = "Ошибка подключения";
                            lbRegistrStat.Refresh();
                        }));
                    else
                    {
                        lbRegistrStat.BackColor = Color.Red;
                        lbRegistrStat.Text = "Ошибка подключения";
                        lbRegistrStat.Refresh();
                    }

                    break;
                case "noNetwork":
                    if (InvokeRequired)
                        Invoke((MethodInvoker)(() => lbRegistrStat.Text = "Нет в сети"));
                    else
                        lbRegistrStat.Text = "Нет в сети";
                    break;
                case "connected":
                    if (InvokeRequired)
                        Invoke((MethodInvoker)(() => onConnected(true)));
                    else
                        onConnected(true);
                    break;
                case "unknown":
                    if (InvokeRequired)
                        Invoke((MethodInvoker)(() => onConnected(false)));
                    else
                        onConnected(false);
                    break;
            }
        }
        ////
#if PROB_CDCMESSAGE
        public static ArrayList Parse(string text) //( Message m )
        {
            //string text = m.Text;
            string tpName = ""; // Нет ТП
            string[] a = text.Split(null, 3);
            if (a.Length >= 3 && a[0] == "tp")
            {
                int minor = atoi(a[1]);
                if (minor <= 0)
                    return new ArrayList();
                tpName = a[0] + a[1];// +"." + nameClient; //Make name: tp5.cd18
                text = a[2];
            }
            ArrayList al = new ArrayList(35); //drop all
            //ListReader r = new ListReader(text);
            string[] stringSeparators = new string[] { "\r", "\n" }; //?? и ";"
            string[] subs = text.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            for (int ind = 0; ind < subs.Length; ind++) //while (r.Next)
            {
                string line = subs[ind]; //r.GetLine(); //"Ok get: list :"
                if (line == null)
                    break;
            l1: int shift = 0;
                bool Answer = false;
                bool Error = false;
                Var ar = new Var(line);  //split by " :"
                ar.nameTP = tpName;
                string Cmd = ar[0];
                if (Cmd[0] == '?')  //?set can't_write out1 1
                {
                    Cmd = Cmd.Substring(1);  //without ?
                    Error = true;	 // 
                    Answer = true;
                    ar.Replace(0, Cmd);  //without '?'
                    ar.errorStr = ar[1];
                    ar.Delete(1);  //delete the cause "set out1 1"
                }
                else if (Cmd == "Ok")
                {
                    shift = 1;
                    Cmd = ar[1];
                    Answer = true;
                }
                if (line.EndsWith(" :")) //we has got the list
                {
                    //do
                    for (int ind2 = ind + 1; ind2 < subs.Length; ind2++) //while (r.Next)
                    {   //the names of variables now!
                        line = subs[ind2]; //r.GetLine();   //": name: value1 value2"	    
                        if (line[0] != ':')  //!line.StartsWith(": ") )
                            goto l1;  //break;
                        else if (line[1] != ' ')
                            break;  //maybe ":." -- the end of the list
                        // here comes ": var value"      
                        ar = new Var(line);
                        //ar.oTag = (Object)m;// m: Message
                        ar.nameTP = tpName;
                        ar.answer = true;    //list is always answer
                        if (ar[0][0] == '?') // ": ?out1"
                        {
                            ar.Replace(0, ar[0].Substring(1));
                            ar.error = true;  //Error;
                        }
                        ar.var = true; //yes
                        ar.cmd = Cmd;
                        al.Add(ar);
                    } //while (r.Next);
                    break;
                } //MultiLine
                else
                {
                    if (Cmd == "get" || Cmd == "set")
                    {
                        shift++;
                        ar.var = true;
                    }
                    if (shift > 0)
                        ar.ShiftToLeft(shift);
                    //ar.oTag = (Object)m;
                    ar.answer = Answer;
                    ar.error = Error;
                    ar.cmd = Cmd;
                    if (Answer || Error)
                        ar.var = true;
                    al.Add(ar);	//kf: 2 3 4 5 -- it is realy not variable!
                }
            }
            return al;
        }
        /*public class ListReader //в utils.dll
        {
            public ListReader();
            public ListReader(string source);

            public bool Next { get; }

            public string GetLine();
            public void ReWind();
            public void Start(string source);
        } */
        public class Var : Argv, ICloneable
        {
            public bool answer;
            public bool error;
            public bool var;        // variable or command
            public string cmd;
            public string nameTP = "";
            public string errorStr = "";
            public Object oTag = null;   // mark

            public Var(string s) { MakeArgv(s, def); }
            public Var(string s, string spl) { MakeArgv(s, spl); }
            public new Object Clone()
            {
                Var v = new Var(base.ToString());
                v.answer = answer;
                v.error = error;
                v.var = var;
                v.cmd = cmd;
                v.nameTP = nameTP;
                v.errorStr = errorStr;
                v.oTag = oTag;
                return v;
            }
            public Var CloneAddParam(string param)
            {
                Var v = new Var(base.ToString() + " " + param);
                v.answer = answer;
                v.error = error;
                v.var = var;
                v.cmd = cmd;
                v.nameTP = nameTP;
                v.errorStr = errorStr;
                v.oTag = oTag;
                return v;
            }
        }
        //
        public class Argv : ICloneable, IEnumerable
        { // example: string arg= new Argv( "all  is  good"," :" );
            protected const string def = " :";  // by default
            private int Argc = 0;
            private string[] a;
            public IEnumerator GetEnumerator()
            {
                return a.GetEnumerator();
            }
            public int Length
            {
                get { return Argc; }
            }
            public string Argm(int argm)
            {
                return argm >= Argc ? "" : a[argm];
            }
            public string this[int argm]  // indexer
            {
                get { return argm >= Argc ? "" : a[argm]; }
            }
            public bool Delete(int argm)
            {
                if (argm >= Argc)
                    return false;
                string[] a1 = new string[Argc - 1];
                for (int j = 0, i = 0; j < Argc; j++)
                    if (j != argm)
                        a1[i++] = a[j];
                a = a1; Argc--;
                return true;
            }
            public bool Replace(int argm, string s)
            {
                if (argm >= Argc)
                    return false;
                a[argm] = s;
                return true;
            }
            public string RetArgm(int argm)
            {
                string s = "";
                for (int j = argm; j < Argc; j++)
                {
                    s += a[j];
                    if (j < Argc - 1)
                        s += " ";
                }
                return s;
            }
            public bool IsName(int argm)
            {
                if (argm >= Argc)
                    return false;
                if (a[argm].Length == 0)
                    return false;
                return Char.IsLetter(a[argm][0]);
            }
            public bool IsDigit(int argm)
            {
                if (argm >= Argc)
                    return false;
                if (a[argm].Length == 0)
                    return false;
                Char c = a[argm][0];
                return Char.IsDigit(c) || c == '+' || c == '-';
            }
            public int ToInt(int argm)
            {
                return IsDigit(argm) ? atoi(a[argm]) : 0;
            }

            public int MakeArgv(string s, string spl) //Korotaev NEW
            {
                int j, start, len;
                int argc = 0;
                bool was_kav = false;
                string[] t = new string[100];//temporary

                s.TrimEnd("\0\n\r ".ToCharArray()); //!!24.04.07
                //             s.TrimEnd('\0'); //24.04.07
                //             s.Trim('\n'); //24.04.07
                for (j = 0; j < s.Length; )
                {
                    if (spl.IndexOf(s[j]) >= 0)
                    {
                        j++; continue;
                    }
                    start = j;
                    was_kav = false;
                    for (; j < s.Length && spl.IndexOf(s[j]) < 0; j++)
                    {
                        if (start == j && s[j] == '\"')
                        {
                            j++; start = j;
                            for (; j < s.Length; j++)
                            {
                                if (s[j] == '\"')
                                {
                                    was_kav = true;
                                    break;
                                }
                            }
                        }
                        if (was_kav)
                            break;
                    }
                    if (j > s.Length) j = s.Length;
                    len = j - start;
                    if (was_kav)
                    {
                        j++;
                    }

                    if (len == 0)
                        t[argc++] = "";
                    else
                        t[argc++] = s.Substring(start, len);
                    if (argc >= t.Length)
                        break; // enough !
                } // main for

                //		    a = new string[argc];
                //              for( j = 0 ; j < argc ; j++ )
                //             {         
                //             a[j] = t[j];
                //         }
                a = t;
                Argc = argc;
                return Argc;
            }
            public string[] _MakeArgv(string s, string spl) // old
            {
                int argc = 0;
                s = s.Trim();
                string[] t = s.Split(spl.ToCharArray());
                for (int j = 0; j < t.Length; j++)
                {
                    if (t[j] != "")
                    {
                        if (t[j][0] == '\0') break;
                        t[j].TrimEnd('\0'); // !! 24.04.07
                        argc++;
                    }
                }
                a = new string[argc];
                for (int j = 0, i = 0; j < t.Length; j++)
                {
                    if (t[j] != "")
                    {
                        if (t[j][0] != '\0')
                            a[i++] = t[j];
                    }
                }
                return a;
            }

            public int MakeArgv(string s)
            {
                MakeArgv(s, def);
                return Argc;
            }
            public void ShiftToLeft(int num)
            {
                if (num <= 0)
                    return;
                if (num >= Argc)
                {
                    a = new string[0]; Argc = 0;
                    return;
                }
                string[] b = new string[Argc - num];
                for (int i = 0, j = num; j < Argc; j++)
                    b[i++] = a[j];
                a = b; Argc = b.Length;
            }
            public override string ToString()
            {
                string s = "";
                for (int j = 0; j < Argc; j++)
                {
                    s += a[j];
                    if (j + 1 < Argc)
                        s += ' ';
                }
                return s;
            }
            public Object Clone()
            {
                Argv ar = new Argv();
                ar.a = new string[Argc];
                for (int i = 0; i < Argc; i++)
                    ar.a[i] = a[i];
                ar.Argc = Argc;
                return ar;
            }
            public Argv(string s) { MakeArgv(s, def); }
            public Argv(string s, string spl) { MakeArgv(s, spl); }
            public Argv() { a = new string[0]; Argc = 0; }
        } // class Argv

        public static int atoi(string s)
        {
            bool minus = false;

            if (s == "")  // ""
                return 0;
            if (s[0] == '-')
            {
                s = s.Substring(1);
                minus = true;
            }
            else if (s[0] == '+')  // "+23"
                s = s.Substring(1);
            if (s == "" || !Char.IsDigit(s[0]))
                return 0;
            int j;
            for (j = 0; j < s.Length; j++)
                if (!Char.IsDigit(s[j]))
                {
                    s = s.Substring(0, j);
                    break;  // "22."
                }
            return minus ? -int.Parse(s) : int.Parse(s);
        }

#endif //PROB_CDCMESSAGE
        ////
        /***********************************************************/
        /*      string file_to_send = "";
                private void btLoadFile_Click(object sender, EventArgs e)
                {
                    openFileDialog1.FileName = "";
                    openFileDialog1.Filter = "ini files (*.ini)|*.ini|All files (*.*)|*.*";
                    //openFileDialog1.FilterIndex = 1;
                    openFileDialog1.RestoreDirectory = true;

                    if (openFileDialog1.ShowDialog() == DialogResult.OK
                        && openFileDialog1.FileName != "")
                    {
                        string curr_fname = openFileDialog1.FileName;
                        MsgToCon("Выбран файл " + curr_fname);
                        //Читаем заголовок WAV-файла
                        //fmt_wav= true;
                        //int res = read_fhdr(curr_fname, ref binRdr);
                        //if (res < 0)
                        //{
                        //    // не смогли открыть файл (или короче WHDR_SZ)
                        //    return;
                        //}
                        //else if (res == 0)
                        {
                            // короткие  дейтаграммы
                            //fmt_wav= false;
                            MsgToCon("Не wave файл: передаем весь файл");
                        }
                        file_to_send= curr_fname;
                    }
                    else if (file_to_send.Length == 0)
                        MsgToCon("Не выбран файл для передачи");
                } */
        //=====================
        /*      int oldw, oldh, first_pass = 1;
                private void Redraw_Control()
                {
                    if (this.WindowState == FormWindowState.Minimized) //сворачиваемся на панель задач
                        return;
                    if (first_pass != 1) // !! масштабирование
                    {
                        float dx, dy;
                        dx = this.Width / (float)oldw;
                        dy = this.Height / (float)oldh;
                        this.SuspendLayout();
                        foreach (Control childControl in this.Controls)
                            childControl.Scale(new SizeF(dx, dy));
                        this.ResumeLayout();
                    }
                    else
                        first_pass = 0;
                    oldw = this.Width;
                    oldh = this.Height;
                }
                private void Form_Resize(object sender, System.EventArgs e)
                {
                    Redraw_Control();
                }
                private void Form_VisibleChanged(object sender, System.EventArgs e)
                {
                    if (Visible)
                        Redraw_Control();
                } */
        private void showStripMenuItem_Click(object sender, EventArgs e)
        {
            LocalEnter();
        }

        private void contextMenuStrip1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F3:
                    showStripMenuItem_Click(sender, e);
                    break;
                case Keys.F4:
                    editStripMenuItem_Click(sender, e);
                    break;
                case Keys.F5:
                    copyToolStripMenuItem_Click(sender, e);
                    break;
                case Keys.F6:
                    transferStripMenuItem_Click(sender, e);
                    break;
                case Keys.F7:
                    createStripMenuItem_Click(sender, e);
                    break;
                case Keys.F8:
                    deleteStripMenuItem_Click(sender, e);
                    break;
            }
        }

        private void editStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection items = listViewLocal.SelectedItems;
            if (items.Count == 0)
                return;
            string sFile = items[0].Text;

            if (items[0].Text.Equals(".."))
            {
                return;
            }
            else
            {
                System.IO.FileInfo file = new System.IO.FileInfo(sFile);
                if (!items[0].SubItems[3].Text.Contains("/"))
                {
                    pathLocal = @"" + textLocalPath.Text + "\\" + file.Name;
                    /*
                                        if (items[0].SubItems[3].Text.Contains("r"))
                                            OpenFile_Dialog(path, true);
                                        else
                                            OpenFile_Dialog(path, false);
                    */
                    try
                    {
                        string fileText = System.IO.File.ReadAllText(pathLocal, Encoding.UTF8);
                        ActionState = ActionStateEnum.LocalEdit;
                        OpenFile_Dialog(ActionState, file.Name, fileText, pathLocal);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        logger.Error(String.Format($"{ex.Message} {ex.StackTrace}", DateTime.Now));
                    }
                }
            }
        }





        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection items = listViewLocal.SelectedItems;
            if (items.Count == 0)
                return;

            if (items[0].SubItems[3].Text == "/")
                return;

            ActionState = ActionStateEnum.LocalCopy;
            OpenLocalFileInLoadingForm(ActionState, items[0]);
        }

        private void transferStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection items = listViewLocal.SelectedItems;
            if (items.Count == 0)
                return;

            if (items[0].SubItems[3].Text == "/")
                return;

            ActionState = ActionStateEnum.LocalTransfer;
            OpenLocalFileInLoadingForm(ActionState, items[0]);
        }

        private void OpenLocalFileInLoadingForm(ActionStateEnum state, ListViewItem item)
        {
            sFile = item.Text;
            int sizeFile = int.Parse(item.SubItems[1].Text);
            if (item.SubItems[3].Text == "/")
                return;

            pathRemote = textRemotePath.Text + sFile;
            pathLocal = $"{textLocalPath.Text}\\{sFile}";
            LoadingForm_Dialog(state,sizeFile, pathLocal, pathRemote);
        }

        CreateFolderForm folderForm;

        private void createStripMenuItem_Click(object sender, EventArgs e)
        {
            folderForm = new CreateFolderForm(this, true);
            folderForm.ShowDialog();
        }

        private void deleteStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection items = listViewLocal.SelectedItems;
            if (items.Count == 0)
                return;

            string sFile = items[0].Text;

            if (!items[0].Text.Equals(".."))
            {
                string path = @"" + textLocalPath.Text + "\\" + sFile;

                DialogResult result;
                if (items[0].SubItems[3].Text.Contains("/"))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(path);

                    result = MessageBox.Show($"Вы точно хотите удалить папку {directoryInfo.Name}?",
                        "Удаление каталога", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                        if (directoryInfo.Exists)
                        {
                            directoryInfo.Delete();
                            LocalRefresh();
                        }
                }
                else
                {
                    System.IO.FileInfo file = new System.IO.FileInfo(path);
                    result = MessageBox.Show($"Вы точно хотите удалить файл {file.Name}?",
                        "Удаление файла", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                        if (file.Exists)
                        {
                            file.Delete();
                            LocalRefresh();
                        }
                }
            }
        }

        private void remoteMenuStrip_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F3:
                    showRemoteStripMenuItem_Click(sender, e);
                    break;
                case Keys.F4:
                    editRemoteStripMenuItem_Click(sender, e);
                    break;
                case Keys.F5:
                    copyRemoteStripMenuItem_Click(sender, e);
                    break;
                case Keys.F6:
                    transferRemoteStripMenuItem_Click(sender, e);
                    break;
                case Keys.F7:
                    createRemoteStripMenuItem_Click(sender, e);
                    break;
                case Keys.F8:
                    deleteRemoteStripMenuItem_Click(sender, e);
                    break;
            }
        }


        private void showRemoteStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoteEnter();
        }

        private void editRemoteStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection items = listViewRemote.SelectedItems;
            if (items.Count == 0)
                return;

            ActionState = ActionStateEnum.RemoteEdit;
            OpenRemoteFileInLoadingForm(ActionState, items[0]);
        }

        int sizeReadFile;
        string pathToCopy;
        public string textFileEdit = "";
        public void EditSaveFile(int size, string path)
        {
            SendMsg($"sfl openw {path}\r");
            this.sizeReadFile = size;
            this.pathToCopy = path;
        }

        public void EditSaveFile(string path, string text)
        {
            SendMsg($"sfl openw {path}\r");
            this.pathToCopy = path;
            this.textFileEdit = text;
        }

        public void ReadBlockRemoteFile()
        {
            System.IO.File.WriteAllText(textLocalPath.Text+"/tmp.tmp", textFileEdit);
            using (FileStream fstream = File.OpenRead(textLocalPath.Text + "/tmp.tmp"))
            {
                int count = 0;
                int size = (int)fstream.Length;
                int remainder = size;
                do
                {

                    fstream.Seek(count * 600, SeekOrigin.Begin);
                    byte[] buffer;
                    if (remainder < 600)
                    {
                        buffer = new byte[remainder];
                        fstream.Read(buffer, 0, remainder);
                    }
                    else
                    {
                        buffer = new byte[600];
                        fstream.Read(buffer, 0, 600);
                    }
                    //TODO
                    //string textFromFile = Encoding.UTF8.GetString(buffer);
                    //byte[] text = System.Text.Encoding.Default.GetBytes(textFromFile);
                    var data = System.Convert.ToBase64String(buffer);
                    SendMsg($"sfl w {data}\r");
                    count++;

                    remainder = (int)fstream.Length - count * 600;
                } while (remainder > 0);
                SendMsg($"sfl end\r");
                fstream.Close();
                
            }
            DirectoryInfo directoryInfo = new DirectoryInfo(textLocalPath.Text + "/tmp.tmp");
                if (directoryInfo.Exists)
                {
                    directoryInfo.Delete();
                }
            Invoke((MethodInvoker)(() => {
                UpdateData();
            }
));
            ActionState = ActionStateEnum.Inaction;
        }

        public void ReadBlockLocalFile()
        {
            using (FileStream fstream = File.OpenRead(pathLocal))
            {
                int count = 0;
                int size = (int)fstream.Length;
                int remainder = size;
                do
                {
                    
                    fstream.Seek(count * 600, SeekOrigin.Begin);
                    byte[] buffer;
                    if (remainder < 600)
                    {
                        buffer = new byte[remainder];
                        fstream.Read(buffer, 0, remainder);
                    }
                    else
                    {
                        buffer = new byte[600];
                        fstream.Read(buffer, 0, 600);
                    }
                    //TODO
                    //string textFromFile = Encoding.UTF8.GetString(buffer);
                    //byte[] text = System.Text.Encoding.Default.GetBytes(textFromFile);
                    var data = System.Convert.ToBase64String(buffer);
                    SendMsg($"sfl w {data}\r");
                    count++;
                    onUpdateProgressBar(buffer.Length);
                    
                    remainder = (int)fstream.Length - count * 600;
                } while (remainder > 0);

                    if (ActionState == ActionStateEnum.LocalTransfer)
                {
                    System.IO.FileInfo file = new System.IO.FileInfo(pathLocal);
                    if (file.Exists)
                    {
                        file.Delete();
                    }

                }
                SendMsg($"sfl end\r");
                fstream.Close();
            }
            ActionState = ActionStateEnum.Inaction;
        }

        private void copyRemoteStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection items = listViewRemote.SelectedItems;
            if (items.Count == 0)
                return;

            ActionState = ActionStateEnum.RemoteCopy;
            OpenRemoteFileInLoadingForm(ActionState, items[0]);
        }

        public string rmsg_sfl = "";
        public string pathRemote, pathLocal, sFile;
        public ActionStateEnum ActionState;
        public ListViewItem selectedItem;
        private void transferRemoteStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection items = listViewRemote.SelectedItems;
            if (items.Count == 0)
                return;

            selectedItem = items[0];
            ActionState = ActionStateEnum.RemoteTransfer;
            OpenRemoteFileInLoadingForm(ActionState, items[0]);
        }

        private void OpenRemoteFileInLoadingForm(ActionStateEnum state, ListViewItem item)
         {
            sFile = item.SubItems[1].Text;
            int sizeFile = int.Parse(item.SubItems[2].Text);
            if (item.Text == "/")
                return;
            rmsg_sfl = "";
            pathRemote = textRemotePath.Text + sFile;
            pathLocal = $"{textLocalPath.Text}\\{sFile}";
            LoadingForm_Dialog(state,sizeFile, pathRemote, pathLocal);
        }

        private void createRemoteStripMenuItem_Click(object sender, EventArgs e)
        {
            folderForm = new CreateFolderForm(this, false);
            folderForm.ShowDialog();
        }

        public void CreateFolder(bool isLocal, string nameFolder)
        {
            if (isLocal)
            {
                Directory.CreateDirectory($"{textLocalPath.Text}\\{nameFolder}");
                LocalRefresh();
            }
            else
            {
                SendMsg($"system mkdir {textRemotePath.Text}{nameFolder}\r");
            }
        }

        private void deleteRemoteStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection items = listViewRemote.SelectedItems;
            if (items.Count == 0)
                return;
            sFile = items[0].SubItems[1].Text;
            pathRemote = textRemotePath.Text + sFile;
            DialogResult result;
            string atrFile = items[0].SubItems[0].Text;
            if (atrFile == "/")
            {
                result = MessageBox.Show($"Вы точно хотите удалить папку {pathRemote}?",
                    "Удаление папки", MessageBoxButtons.YesNo);
            }
            else
            {
                result = MessageBox.Show($"Вы точно хотите удалить файл {pathRemote}?",
                    "Удаление файла", MessageBoxButtons.YesNo);
            }

            if (result == DialogResult.Yes)
            {
                SendMsg($"system rm -r {pathRemote}\r");
            }
        }

        public void onCopyRemoteFile()
        {
            switch (ActionState)
            {
                case ActionStateEnum.RemoteShow:
                {
                    OpenFile_Dialog(ActionState, sFile, rmsg_sfl, pathRemote);
                    break;
                }
                case ActionStateEnum.RemoteEdit:
                {
                    OpenFile_Dialog(ActionState, sFile, rmsg_sfl, pathRemote);
                    return;
                }
                case ActionStateEnum.RemoteCopy:
                {
                    File.WriteAllText(pathLocal, rmsg_sfl);
                        LocalRefresh();
                    break;
                }
                case ActionStateEnum.RemoteTransfer:
                {
                    File.WriteAllText(pathLocal, rmsg_sfl);
                    SendMsg($"system rm -r {pathRemote}\r");
                    LocalRefresh();
                        break;
                }
                case ActionStateEnum.Stop:
                {
                    Invoke((MethodInvoker)(() => { 
                        loading_form.Close(); }
                        ));

                    break;
                }
                default: break;
            }

            ActionState = ActionStateEnum.Inaction;
        }

        public void onUpdateProgressBar(int size)
        {
            try
            {
                if (ActionState != ActionStateEnum.Inaction)
                    if(!loading_form.IsDisposed)
                        loading_form.DownloadProgress(size);
            }
            catch (Exception ex)
            {
                logger.Error(String.Format($"PROGRESS BAR {ex.Message} {ex.StackTrace} {ex.Source}", DateTime.Now));
                MessageBox.Show(ex.Message);
            }
        }
    }

    public enum ActionStateEnum
    {
        Inaction,
        RemoteCopy,
        RemoteTransfer,
        LocalCopy,
        LocalTransfer,
        RemoteShow,
        RemoteEdit,
        LocalShow,
        LocalEdit,
        Stop
    }

    /*======================*/
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new EditCodecForm());
        }
    }
}