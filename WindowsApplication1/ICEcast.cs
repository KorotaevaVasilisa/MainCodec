using System;
using System.Windows.Forms;

namespace TCPclient
{
    public partial class ICEcast : Form
    {
        EditCodecForm MyParentForm = null;
        public ICEcast(EditCodecForm form)
        {
            InitializeComponent();
            this.MyParentForm = form;
            this.DialogResult = DialogResult.Cancel;
            FillURLlist();
            Parse(MyParentForm.rmsg_icecast);
            //Getstat();
            Repaint();
        }
        public void Repaint()
        {
        }
        public void Parse(string msg)
        { //: url1 -Останов "icecast.vgtrk.cdnvideo.ru/rrzonam_mp3_192kbps" Радио России 192k
            string[] stringSeparators = new string[] { "\r", "\n" };
            string[] subs = msg.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            short nrec; //N приемника
            try
            {
                TextBox tbstat;
                ComboBox cburl;
                for (int i = 0; i < subs.Length; i++)
                {
                    int sind = subs[i].IndexOf("url");
                    if (sind < 0)
                        continue;
                    subs[i] = subs[i].Substring(sind + 3); //отшибли "url"
                    if (subs[i].Length < 4)
                        continue;
                    if (!Int16.TryParse(subs[i].Substring(0, 1), out nrec))
                        continue;
                    subs[i] = subs[i].Substring(2);
                    string[] argv = subs[i].Split((string[])null,
                        StringSplitOptions.RemoveEmptyEntries); // сепаратор ' '
                    //argv: [0]-состояние  [1]-"URL"  [2]..[]-имя_станции
                    if (argv.Length < 3)
                        continue;
                    if (nrec == 1)
                    {
                        tbstat = tbStat1;
                        cburl = cbURL1;
                    }
                    else if (nrec == 2)
                    {
                        tbstat = tbStat2;
                        cburl = cbURL2;
                    }
                    else if (nrec == 3)
                    {
                        tbstat = tbStat3;
                        cburl = cbURL3;
                    }
                    else if (nrec == 4)
                    {
                        tbstat = tbStat4;
                        cburl = cbURL4;
                    }
                    else
                        continue;
                    //!!все параметры после argv[0] собрать в name_station  <name><URL>
                    ShowURL(tbstat, cburl, argv[1],
                        subs[i].Substring(argv[0].Length + argv[1].Length + 2));
                    if (nrec == 4)
                        return;
                }
            }
            catch (Exception e)
            {
                MyParentForm.MsgToCon("ICEcastEdit.Parse.Exception " + e.Message);
            }
        }
        void ShowURL(TextBox tbstat, ComboBox cburl, string url, string station)
        { //станция и URL
            tbstat.Text = station.Trim();
            cburl.Text = url.Trim('"');
        }
        private void FillURLlist()
        {
            cbURL1.Items.Clear();
            cbURL2.Items.Clear();
            cbURL3.Items.Clear();
            cbURL4.Items.Clear();
            for (int i = 0; i < MyParentForm.listURLs.Count; i++)
            {
                cbURL1.Items.Add(MyParentForm.listURLs[i]);
                cbURL2.Items.Add(MyParentForm.listURLs[i]);
                cbURL3.Items.Add(MyParentForm.listURLs[i]);
                cbURL4.Items.Add(MyParentForm.listURLs[i]);
            }
        }
        /*        //Запрос состояния всех станций
                internal void SendStatRqst()
                {
                        SendCdcCommand("url_status");
                }
                //Запрос параметров всех станций (одноразовый)
                void SendListRqst()
                {
                    if (ctlObj != null)
                    {
                        ctlObj.SendCdcCommand("url_list");
                        // получим для всех станций: status URL Station
                        if (stat_rqst)
                            Timer1.Start();
                    }
                }
                //Отправить команду установки станции
                internal void SendCmdSet(int nrec, string url, string station)
                {
                    //Timer1.Stop(); //5сек не запрашивать состояние станций
                    ctlObj.SendCdcCommand("url_set " + nrec.ToString() + " " + url + " " + station);
                    SendListRqst();
                } */
        //
        ComboBox curr_cb;
        string url = "";
        private void cbURL1_SelChangeCommit(object sender, EventArgs e)
        {
            cbURLs_SelIndChngd(tbStat1, cbURL1);
        }
        private void cbURL2_SelChangeCommit(object sender, EventArgs e)
        {
            cbURLs_SelIndChngd(tbStat2, cbURL2);
        }
        private void cbURL3_SelChangeCommit(object sender, EventArgs e)
        {
            cbURLs_SelIndChngd(tbStat3, cbURL3);
        }
        private void cbURL4_SelChangeCommit(object sender, EventArgs e)
        {
            cbURLs_SelIndChngd(tbStat4, cbURL4);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            if (curr_cb != null)
                curr_cb.Text = url;
        }
        void cbURLs_SelIndChngd(TextBox tbstat, ComboBox cburl)
        {
            string s = cburl.Items[cburl.SelectedIndex].ToString();
            string[] argv = s.Split((string[])null, StringSplitOptions.RemoveEmptyEntries); // сепаратор ' '
            if (argv.Length <= 0)
                return;
            tbstat.Text = s.Substring(argv[0].Length);
            //cburl.Text = argv[0];
            url = argv[0];
            curr_cb = cburl;
            timer1.Start();
        }
        string cmd_icecast = "";
        bool MakeCmd()
        {
            cmd_icecast = "";
            TextBox tbstat;
            ComboBox cburl;
            string[] argv;
            for (int ns = 1; ns <= 4; ns++) //N станции 1..4
            {
                if (ns == 1)
                {
                    cburl = cbURL1;
                    tbstat = tbStat1;
                    //argv = cbURL1.Text.Split((string[])null,
                    //    StringSplitOptions.RemoveEmptyEntries); //сепаратор ' '
                    //if (tbStat1.Text != "" && cbURL1.Text != "")
                    //    cmd_icecast += "url_set 1 " + argv[0] + " " + tbStat1.Text + ";";
                }
                else if (ns == 2)
                {
                    cburl = cbURL2;
                    tbstat = tbStat2;
                }
                else if (ns == 3)
                {
                    cburl = cbURL3;
                    tbstat = tbStat3;
                }
                else if (ns == 4)
                {
                    cburl = cbURL4;
                    tbstat = tbStat4;
                }
                else
                    continue;
                argv = cburl.Text.Split((string[])null, StringSplitOptions.RemoveEmptyEntries); //сепаратор ' '
                if (tbstat.Text != "" && argv[0] != "")
                    cmd_icecast += "url_set " + ns.ToString() + " " + argv[0] + " " + tbstat.Text + "\r";//+ ";";
            }
            return true;
        }
        private void btICEcastCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
        private void btICEcastApply_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Применить изменения настроек потокового радио."
                + "Вы уверены ?", "Подтвердите", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                MakeCmd();
                // MyParentForm.MsgToCon("ICEcastEdit. cmd:" + cmd_icecast);
                MyParentForm.SendMsg(cmd_icecast + "\r"); //"url_save\r"
                                                          //res = MessageBox.Show("\nЧтобы изменения вступили в силу,\n"
                                                          //   + "необходимо перезапустить БПР\n" + "Перезапустить сейчас ?\n", "",
                                                          //   MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                                          //if (res == DialogResult.Yes)
                                                          //{
                                                          //    MyParentForm.SendMsg("system reboot\r");
                this.DialogResult = DialogResult.OK;
                //}
                //else
                //    this.DialogResult = DialogResult.Cancel;
                //Close();
            }
            Close();
        }
    }
}