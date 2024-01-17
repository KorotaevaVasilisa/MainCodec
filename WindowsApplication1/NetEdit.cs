#define EDIT_MPEGTS // с редактированием каналов TS
//
using System;
using System.Windows.Forms;

namespace TCPclient
{
    public partial class NetEdit : Form
    {
        EditCodecForm MyParentForm = null;
#if EDIT_MPEGTS
        string[] ip_ts = new string[4] { "", "", "", "" },
            port_ts = new string[4] { "", "", "", "" },
            id_ts = new string[4] { "", "", "", "" };
#endif
        const int MAX_UNICAST = 8; //Unicast по запросу
        //string ip_unicasts = "";
        public static string[] args = new string[20];
        //public static string cdname = ""; //12.2018
        public NetEdit(EditCodecForm form)
        {
            InitializeComponent();
            this.MyParentForm = form;
            //Get_netstat();
            this.DialogResult = DialogResult.Cancel;
            Parse(MyParentForm.rmsg_net);
            Repaint();
        }
        public void Repaint()
        {
            // MyParentForm.MsgToCon("NetEdit.Repaint");
            if (args[1].StartsWith("1"))
                radioBtDHCP.Checked = true;
            else
                radioBtStatAddr.Checked = true;
            EnblIPs(radioBtStatAddr.Checked);
            string ss = "cd";
            if (args[15].StartsWith("cs"))
                ss = "cs";
            tbCDname.Text = ss + args[0]; //cdname; 
            tbIP.Text = args[3];
            tbMask.Text = args[4];
            tbGate.Text = args[2];
            tbDNS.Text = args[5];
            tbServ1.Text = args[6];
            tbServ2.Text = args[7];
            tbServ3.Text = args[8];
            tbServ4.Text = args[9];
            lbproc.Text = args[16] + "  ver: " + MyParentForm.version_dfrm;//тип процессора 19.03.2021: + дата версии прошивки
            tbMulticast.Text = args[10];
            tbMPEGTS1.Text = args[11].Replace('/', ':');
            tbMPEGTS2.Text = args[12].Replace('/', ':');
            tbMPEGTS3.Text = args[13].Replace('/', ':');
            tbMPEGTS4.Text = args[14].Replace('/', ':');
            tbUnicast.Text = reform_unicast(MyParentForm.rmsg_unicast);
        }

        bool test_all_ip()
        {
            bool res = true;
            if (radioBtStatAddr.Checked)
            {
                if (!test_ip(tbIP.Text, "IP кодека", 1))
                    res = false;
                if (!test_ip(tbMask.Text, "Маска", 2))
                    res = false;
                if (!test_ip(tbGate.Text, "Шлюз", 3))
                    res = false;
                if (res && !test_subnet())
                {
                    MessageBox.Show("IP адреса обьекта и шлюза в разных подсетях.\nУстановки отвергнуты", "Ошибка");
                    return false;
                }
            }
            if (!test_ip(tbDNS.Text, "IP DNS_сервера", 0))
                res = false;
            if (!test_ip(tbServ1.Text, "IP Связного сервера_1", 0))
                res = false;
            if (!test_ip(tbServ2.Text, "IP Связного сервера_2", 0))
                res = false;
            if (!test_ip(tbServ3.Text, "IP Связного сервера_3", 0))
                res = false;
            if (!test_ip(tbServ4.Text, "IP Связного сервера_4", 0))
                res = false;
            if (!test_ip(tbMulticast.Text, "IP Мультикаста", 0))
                res = false;
#if EDIT_MPEGTS
            if (Parse_ts(tbMPEGTS1, 0))
            {
                if (!test_ip(ip_ts[0], "IP MPEG_TS1", 0))
                    res = false;
            }
            if (Parse_ts(tbMPEGTS2, 1))
            {
                if (!test_ip(ip_ts[1], "IP MPEG_TS2", 0))
                    res = false;
            }
            if (Parse_ts(tbMPEGTS3, 2))
            {
                if (!test_ip(ip_ts[2], "IP MPEG_TS3", 0))
                    res = false;
            }
            if (Parse_ts(tbMPEGTS4, 3))
            {
                if (!test_ip(ip_ts[3], "IP MPEG_TS4", 0))
                    res = false;
            }
#endif
            if (!test_ip_unicast(tbUnicast))
                res = false;
            return res;
        }
        short[] addr = new short[4], gate = new short[4], mask = new short[4];
        bool test_subnet()
        {
            for (int i = 0; i < 4; i++)
            {
                if ((addr[i] & mask[i]) != (gate[i] & mask[i]))
                {
                    // MyParentForm.MsgToCon("EdNet.tstsubnet.i addr gate mask:" +i+" "+addr[i]+" "+gate[i]+" "+mask[i]);
                    return false;
                }
            }
            return true;
        }
        bool test_ip(string ip, string msg, int npar)
        {
            if (ip == "")
                return true;
            bool err = false;
            string[] ss = ip.Split('.');
            if (ss.Length != 4) // < 4)
                err = true;
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    short res;
                    bool ok = Int16.TryParse(ss[i], out res);
                    if (ok == false || res < 0 || res > 255)
                    {
                        err = true;
                        break;
                    }
                    if (npar == 1)
                        addr[i] = res;
                    else if (npar == 2)
                        mask[i] = res;
                    else if (npar == 3)
                        gate[i] = res;
                }
            }
            if (err)
            {
                MessageBox.Show("Некорректное значение параметра: " + msg, "Ошибка");
                return false;
            }
            return true;
        }
        bool test_ip_unicast(TextBox tb)
        {
            if (tb.Text == "")
                return true;
            //string[] stringSeparators = new string[] { " " };
            string[] ss = tb.Text.Split(new string[] { " " }, //stringSeparators,
                StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < ss.Length; i++)
            {
                if (i >= MAX_UNICAST)
                    break;
                //unicast[i] = ss[i];
                if (!test_ip(ss[i]/*unicast[i]*/, "IP UNICAST_" + (i + 1), 0))
                    return false;
            }
            return true;
        }
        string reform_unicast(string s)
        {
            string ns = "";
            int ind = s.IndexOfAny(new char[] { '\r', '\n' });
            if (ind > 0)
            {
                s = s.Substring(0, ind);
                //MyParentForm.MsgToCon("NetEdit.reform_unicast len:" + s.Length + " ind:" + ind + " s:" + s + " END");
                //string[] ss = s.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                string[] ss = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < ss.Length; i++)
                {
                    if (i >= MAX_UNICAST)
                        break;
                    ns += ss[i] + "   "; //сделаем 3 пробела между адресами
                }
            }
            return ns;
        }
#if EDIT_MPEGTS
        bool Parse_ts(TextBox tb, int ind)
        {
            if (tb.Text == "")
            {
                return true;
            }
            string[] ss = tb.Text.Split('/');
            if (ss.Length != 2)
                return false;
            ip_ts[ind] = ss[0];
            string[] ss2 = ss[1].Split(' ');
            if (ss2.Length != 2)
                return false;
            port_ts[ind] = ss2[0];
            id_ts[ind] = ss2[1];
            return true;
        }
#endif
        /*Ok netstat : //args[0]..[15]
        !! новинка : type
        : type cd ADSP-BF537 500(MHz CCLK) 100(MHz SCLK)  kernel 2.6.22.19-ADI-2008R1.5-svn //args[15]
        : ident 27 //args[0]
        : dhcp0 0  //args[1]
        : gateway 192.168.0.99
        : eth0 192.168.0.161
        : mask0 255.255.255.0
        : dns 192.168.0.99
        : node1 192.168.0.161
        : node2 192.168.0.200
        : node3 
        : node4 
        : multicast 224.22.41.16
        : ts1 
        : ts2 
        : ts3 
        : ts4
        :. */
        public static void Parse(string msg)
        {
            //tbUnicast.Text= rmsg_unicast;
            string[] stringSeparators = new string[] { "\r", "\n" };
            string[] subs = msg.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < args.Length; i++)
                args[i] = "";
            try
            {
                for (int i = 0; i < subs.Length; i++)
                {
                    if (subs[i].StartsWith(":."))
                        return;
                    if (subs[i].Length <= 2)
                        continue;
                    string[] argv =
                        subs[i].Substring(2, subs[i].Length - 2).Split((string[])null,
                            StringSplitOptions.RemoveEmptyEntries); // сепаратор ' '
                    if (argv.Length < 2)
                        continue;
                    string val1 = argv[1], val2 = "";
                    if (argv.Length > 2)
                        val2 = argv[2];
                    switch (argv[0]) //param_name
                    {
                        case "ident":
                            args[0] = val1;
                            break;
                        case "type":
                            args[15] = val1; //cd/cs
                            if (argv.Length > 2) //proc
                                for (int ii = 2; ii < argv.Length; ii++)
                                    args[16] += argv[ii] + " ";
                            break;
                        case "dhcp0":
                            args[1] = val1;
                            break;
                        case "gateway":
                            args[2] = val1;
                            break;
                        case "eth0":
                            args[3] = val1;
                            break;
                        case "mask0":
                            args[4] = val1;
                            break;
                        case "dns":
                            args[5] = val1;
                            break;
                        case "node1":
                            args[6] = val1;
                            break;
                        case "node2":
                            args[7] = val1;
                            break;
                        case "node3":
                            args[8] = val1;
                            break;
                        case "node4":
                            args[9] = val1;
                            break;
                        case "multicast":
                            args[10] = val1;
                            break;
                        case "ts1":
                            args[11] = val1 + " " + val2;
                            break;
                        case "ts2":
                            args[12] = val1 + " " + val2;
                            break;
                        case "ts3":
                            args[13] = val1 + " " + val2;
                            break;
                        case "ts4":
                            args[14] = val1 + " " + val2;
                            break;
                    }
                }
                // 12.2018 для совмест-ти со старыми прошивками (без "type" в листе)
                //string ss = "cd"; //cd/cs
                //if (args[15].StartsWith("cs"))
                //    ss = "cs";
                //cdname = ss + args[0];
            }
            catch //(Exception e)
            {
                //MyParentForm.MsgToCon("NetEdit.Parse Exception " + e.Message);
            }
        }
        private void EnblIPs(bool static_ip)
        {
            /*maskedtb_IP.Enabled = static_ip;
            maskedtb_IPmask.Enabled = static_ip;
            maskedtb_IPgate.Enabled = static_ip;*/
            tbIP.Enabled = static_ip;
            tbMask.Enabled = static_ip;
            tbGate.Enabled = static_ip;
        }
        private void RadioBt_CheckChnd(object sender, EventArgs e)
        {
            EnblIPs(radioBtStatAddr.Checked);
        }
        private void btCastOption_Click(object sender, EventArgs e)
        {
            bool enbl = false;
            if (!tbMPEGTS1.Enabled)
                enbl = true;
            tbMPEGTS1.Enabled = enbl;
            tbMPEGTS2.Enabled = enbl;
            tbMPEGTS3.Enabled = enbl;
            tbMPEGTS4.Enabled = enbl;
        }
        /*        void Get_netstat()
                {
                    string type = TCore.cdc_type;
                    if (TCore.AppMaskEnabled(Const.mCdcWrite) == false)
                        type = "!" + type;
                    TCore.SendTypeCmd(ctlObj.client.nameClientLong, type, "netstat");
                } */

        string cmd_net;
        void AddIPToCmd(TextBox tb, string par, bool enbl_blank)
        {
            if (!enbl_blank && tb.Text == "")
                return;
            cmd_net += "\r: " + par + " " + tb.Text;
        }
        bool MakeCmd()
        {
            //cmd_net = "netstat :\r: ident " + tbCDname.Text;//.Substring(2);
            //19.03.2021
            cmd_net = "netstat :\n: ident ";
            if (String.Compare(MyParentForm.version_dfrm, "2021.03.15") >= 0)
                cmd_net += tbCDname.Text;
            else
                cmd_net += tbCDname.Text.Substring(2);
            //
            string par;
            par = "\r: dhcp0 ";
            if (radioBtStatAddr.Checked)
                par += "0";
            else
                par += "1";
            cmd_net += par;
            if (radioBtStatAddr.Checked)
            {
                AddIPToCmd(tbIP, "eth0", false);
                AddIPToCmd(tbMask, "mask0", false);
                AddIPToCmd(tbGate, "gateway", false);
            }
            AddIPToCmd(tbDNS, "dns", true);
            AddIPToCmd(tbServ1, "node1", true);
            AddIPToCmd(tbServ2, "node2", true);
            AddIPToCmd(tbServ3, "node3", true);
            AddIPToCmd(tbServ4, "node4", true);
            AddIPToCmd(tbMulticast, "multicast", false);
#if EDIT_MPEGTS
            AddIPToCmd(tbMPEGTS1, "ts1", true);
            AddIPToCmd(tbMPEGTS2, "ts2", true);
            AddIPToCmd(tbMPEGTS3, "ts3", true);
            AddIPToCmd(tbMPEGTS4, "ts4", true);
#endif
            cmd_net += "\r:.";
            return true;
        }
        //
        private void btEditNetCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
        private void btEditNetApply_Click(object sender, EventArgs e)
        {
            if (!test_all_ip())
                return;
            DialogResult res = MessageBox.Show("Применить изменения сетевых настроек."
                + "Вы уверены ?", "Подтвердите", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                MakeCmd();
                // MessageBox.Show(cmd_net, "", MessageBoxButtons.YesNo);
                // return;                    
                MyParentForm.SendMsg(cmd_net + "\r");
                MyParentForm.SendMsg("set sound_servers " + tbUnicast.Text + "\r");
                res = MessageBox.Show("\nЧтобы изменения вступили в силу,\n"
                   + "необходимо перезапустить БПР\n" + "Перезапустить сейчас ?\n", "",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    MyParentForm.SendMsg("system reboot\r");
                    this.DialogResult = DialogResult.OK;
                }
                else
                    this.DialogResult = DialogResult.No;
            }
            Close();
        }
#if NOTH
        void maskedTextBox_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            MaskedTextBox mtb = sender as MaskedTextBox;
            if (mtb.MaskFull)
            {
                toolTip1.ToolTipTitle = "Ошибка";
                toolTip1.Show("Слишком длинное поле. Удалите лишние символы перед вводом", 
                    mtb, 0, 20, 3000);
            }
            else if (e.Position == mtb.Mask.Length)
            {
                toolTip1.ToolTipTitle = "Ошибка";
                toolTip1.Show("Нельзя добавить символы к концу поля", 
                    mtb, 0, 20, 3000);
            }
            else
            {
                toolTip1.ToolTipTitle = "Ошибка";
                toolTip1.Show("Можно вводить только цифры", 
                    mtb, 0, 20, 3000);
            }
        }
        void maskedTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            //The balloon tip is visible for five seconds;
            //if the user types any data before it disappears, collapse it ourselves
            toolTip1.Hide(sender as MaskedTextBox/*maskedtb_IP*/);
        }
#endif
    }
}
