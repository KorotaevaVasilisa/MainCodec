#define REG_VOLUME
//
using System;
using System.Windows.Forms;

namespace TCPclient
{
    public partial class FMedit : Form
    {
        EditCodecForm MyParentForm = null;
        public FMedit(EditCodecForm form)
        {
            InitializeComponent();
            this.MyParentForm = form;
            this.DialogResult = DialogResult.Cancel;
            Parse(MyParentForm.rmsg_fm);
            Parse_fms(MyParentForm.rmsg_fm/*_fms*/);
            //Get_FMstat();
            //Repaint();
        }
        //public void Repaint()
        //{  }
        public void Parse(string msg)
        {
            //fmti: freq name
            string[] stringSeparators = new string[] { "\r", "\n" };
            string[] subs = msg.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            short nrec; //N приемника
            try
            {
                TextBox tbstat, tbfreq;
                for (int i = 0; i < subs.Length; i++)
                {
                    //убрать Ok get:< >
                    int sind = subs[i].IndexOf("fmt"); //fmt1:< >
                    if (sind < 0)
                        continue;
                    subs[i] = subs[i].Substring(sind + 3);
                    //fmti: частота и название станции
                    if (subs[i].Length < 4)
                        continue;
                    if (!Int16.TryParse(subs[i].Substring(0, 1), out nrec)) //номер от 1
                        continue;
                    subs[i] = subs[i].Substring(3);
                    string[] argv = subs[i].Split((string[])null,
                        StringSplitOptions.RemoveEmptyEntries); // сепаратор ' '
                                                                // MyParentForm.MsgToCon("Parsefms nrec:" + nrec+ " argv.Len:"+argv.Length);
                                                                // for (int ii= 0; ii< argv.Length; ii++)
                                                                //     MyParentForm.MsgToCon("Parsefms ii:" + ii + " arg>" + argv[ii]);
                    if (argv.Length < 1)
                        continue;
                    //< 1: нет параметров: нет приемника ??или не отвечает по RS485
                    //  очистить и Disable панель FMi
                    //else 
                    //  по номеру создать панель FMi,если не было
                    if (nrec == 1)
                    {
                        tbstat = tbStatname1;
                        tbfreq = tbFreq1;
                        if (argv[0] != "") //<freq>
                        {
                            paStat1.Enabled = true;
                            btAddStat1.Visible = false;
                        }
                    }
                    else if (nrec == 2)
                    {
                        tbstat = tbStatname2;
                        tbfreq = tbFreq2;
                        if (argv[0] != "")
                        {
                            paStat2.Enabled = true;
                            btAddStat2.Visible = false;
                        }
                    }
                    else if (nrec == 3)
                    {
                        tbstat = tbStatname3;
                        tbfreq = tbFreq3;
                        if (argv[0] != "")
                        {
                            paStat3.Enabled = true;
                            btAddStat3.Visible = false;
                        }
                    }
                    else if (nrec == 4)
                    {
                        tbstat = tbStatname4;
                        tbfreq = tbFreq4;
                        if (argv[0] != "")
                        {
                            paStat4.Enabled = true;
                            btAddStat4.Visible = false;
                        }
                    }
                    else if (nrec == 5)
                    {
                        tbstat = tbStatname5;
                        tbfreq = tbFreq5;
                        if (argv[0] != "")
                        {
                            paStat5.Enabled = true;
                            btAddStat5.Visible = false;
                        }
                    }
                    else if (nrec == 6)
                    {
                        tbstat = tbStatname6;
                        tbfreq = tbFreq6;
                        if (argv[0] != "")
                        {
                            paStat6.Enabled = true;
                            btAddStat6.Visible = false;
                        }
                    }
                    else
                        continue;
                    //string freq = argv[0], name = argv[1];
                    //!!все параметры после argv[0] собрать в name_station
                    ShowSets(tbstat, tbfreq, argv[0], subs[i].Substring(argv[0].Length)); //<freq><station> 
                    if (nrec == 6)
                        return;
                }
            }
            catch (Exception e)
            {
                MyParentForm.MsgToCon("FMedit.Parsefmt.Exception " + e.Message);
            }
        }
        void Parse_fms(string msg)
        { //fmsi: errcode  rdastat(XXh)  lvl(0..63)  freq  vol(1..15)
            string[] stringSeparators = new string[] { "\r", "\n" };
            string[] subs = msg.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            short nrec; //N приемника
            try
            {
                lbfm1lev.Text = "по умолчанию";
                lbfm2lev.Text = "по умолчанию";
                lbfm3lev.Text = "по умолчанию";
                lbfm4lev.Text = "по умолчанию";
                lbfm5lev.Text = "по умолчанию";
                lbfm6lev.Text = "по умолчанию";
                NumericUpDown updnvol;
                for (int i = 0; i < subs.Length; i++)
                {
                    if (subs[i].StartsWith(":."))
                        return;
                    //убрать Ok get:< >
                    int sind = subs[i].IndexOf("fms"); //:< >fmsi . . .
                    if (sind < 0)
                        continue;
                    subs[i] = subs[i].Substring(sind);
                    if (subs[i].Length < 4)
                        continue;
                    // MyParentForm.MsgToCon("len:" + subs[i].Length + " >" + subs[i]);
                    if (!Int16.TryParse(subs[i].Substring(3, 1), out nrec)) //номер от 1
                        continue;
                    string[] argv = subs[i].Split((string[])null,
                        StringSplitOptions.RemoveEmptyEntries); // сепаратор ' '
                    if (argv.Length <= 5) //с учетом "fms1 "
                        continue;
                    if (nrec == 1)
                    {
                        updnvol = UpDownVol1;
                        lbfm1lev.Text = "принято с кодека";
                    }
                    else if (nrec == 2)
                    {
                        updnvol = UpDownVol2;
                        lbfm2lev.Text = "принято с кодека";
                    }
                    else if (nrec == 3)
                    {
                        updnvol = UpDownVol3;
                        lbfm3lev.Text = "принято с кодека";
                    }
                    else if (nrec == 4)
                    {
                        updnvol = UpDownVol4;
                        lbfm4lev.Text = "принято с кодека";
                    }
                    else if (nrec == 5)
                    {
                        updnvol = UpDownVol5;
                        lbfm5lev.Text = "принято с кодека";
                    }
                    else if (nrec == 6)
                    {
                        updnvol = UpDownVol6;
                        lbfm6lev.Text = "принято с кодека";
                    }
                    else
                        continue;
                    ShowStat(updnvol, argv[5]);
                    if (nrec == 6)
                        return;
                }
            }
            catch (Exception e)
            {
                MyParentForm.MsgToCon("FMedit.Parsefms.Exception " + e.Message);
            }
        }
        private void btAddStat1_Click(object sender, EventArgs e)
        {
            paStat1.Enabled = true;
        }
        private void btAddStat2_Click(object sender, EventArgs e)
        {
            paStat2.Enabled = true;
        }
        private void btAddStat3_Click(object sender, EventArgs e)
        {
            paStat3.Enabled = true;
        }
        private void btAddStat4_Click(object sender, EventArgs e)
        {
            paStat4.Enabled = true;
        }
        private void btAddStat5_Click(object sender, EventArgs e)
        {
            paStat5.Enabled = true;
        }
        private void btAddStat6_Click(object sender, EventArgs e)
        {
            paStat6.Enabled = true;
        }
        void ShowSets(TextBox tbstat, TextBox tbfreq, string freq, string station)
        { //станция и частота <station1> <freq1> ...
            tbstat.Text = station.Trim();
            tbfreq.Text = freq;
        }
        void ShowStat(NumericUpDown updnvol, string par)
        { // Текущий уровень приема
            int pos = par.IndexOf("v");
            if (pos >= 0)
            {
                //lbVol.Visible = true;
                //UpDnVol.Visible = true;
                int v = 0;
                int.TryParse(par.Substring(pos + 1), out v);
                if (v <= updnvol.Maximum && v >= updnvol.Minimum)
                    updnvol.Value = v;
            }
            else
            {
                //lbVol.Visible = false;
                //updnvol.Visible = false;
            }
        }
        /*      void ShowStat(string msg)
                {   //"fmsi . . ."
                    if (edit_mode) //в режиме редактирования пробрасываем
                        return;
                    if (skip_ack) //пропустить 1 отклик
                    {
                        skip_ack = false;
                        return;
                    }
                    //разрешить панель приемника и отобразить данные
                    if (a.Length < 5) //не хватает параметров
                        return;
                    this.Enabled = true;
                    //параметры: для 2-х каналов 
                    //  err_code: 0-Ok, 1-addr_wr_err(нет ACK на запись адреса) [2-dt_err для команд записи регистров]
                    //  rda_stat: b7..5=RDSR_STC_SF  b4=StereoIndicator  b1=current_channel_is_station  b0=FM_ready
                    //  rda_lvl: 0..63=RSS logarithmic scale
                    //  read_chn: b9..0=Chan (updated after a Tune or Seek operation)
                    //          Band=2:Freq=50kHz(Space)*Chan + (76MHz(Band2=76-108) или 65MHz(Band3=65-76)
                    // a[offs]:errcode  a[offs+1]:rdastat(XXh)  a[offs+2]:lvl  a[offs+3]:freq
                    // если режим редактирования станции: не обновлять имя станции и частоту 
                } */

        string cmd_fm = "";
        bool MakeCmd()
        {
            string nm;
            TextBox tbfreq;
            NumericUpDown updnvol;
            cmd_fm = "";
            for (int nrec = 1; nrec <= 6; nrec++) //N приемника
            {
                if (nrec == 1)
                {
                    if (!paStat1.Enabled)
                        continue;
                    nm = tbStatname1.Text;
                    tbfreq = tbFreq1;
                    updnvol = UpDownVol1;
                }
                else if (nrec == 2)
                {
                    if (!paStat2.Enabled)
                        continue;
                    nm = tbStatname2.Text;
                    tbfreq = tbFreq2;
                    updnvol = UpDownVol2;
                }
                else if (nrec == 3)
                {
                    if (!paStat3.Enabled)
                        continue;
                    nm = tbStatname3.Text;
                    tbfreq = tbFreq3;
                    updnvol = UpDownVol3;
                }
                else if (nrec == 4)
                {
                    if (!paStat4.Enabled)
                        continue;
                    nm = tbStatname4.Text;
                    tbfreq = tbFreq4;
                    updnvol = UpDownVol4;
                }
                else if (nrec == 5)
                {
                    //if (!paStat5.Enabled)
                    //    continue;
                    nm = tbStatname5.Text;
                    tbfreq = tbFreq5;
                    updnvol = UpDownVol5;
                }
                else if (nrec == 6)
                {
                    //if (!paStat6.Enabled)
                    //    continue;
                    nm = tbStatname6.Text;
                    tbfreq = tbFreq6;
                    updnvol = UpDownVol6;
                }
                else
                    continue;
                //2016: настройка Fdirect, freq= 65MHz..108MHz
                if (tbfreq.Text != "")
                {
                    float f = 65F;
                    tbfreq.Text = tbfreq.Text.Replace('.', ',');
                    float.TryParse(tbfreq.Text, out f);
                    if (f < 65F)
                        tbfreq.Text = 65F.ToString("F2");
                    else if (f > 108F)
                        tbfreq.Text = 108F.ToString("F2");
                }
                //if (tbfreq.Text != "") //??
                {
#if REG_VOLUME
                    //if (/*updnvol.Visible && */updnvol.Enabled)
                    {
                        cmd_fm += "set fmt" + nrec.ToString() + " " + tbfreq.Text + "v" + updnvol.Value.ToString() + " " + nm + ";";
                    }
                    //else
#endif
                    //    cmd_fm += "set fmt" + nrec.ToString()+" " + tbfreq.Text+" " + nm + ";";
                    // MyParentForm.MsgToCon("cmd_fm: " + cmd_fm);
                }
            }
            return true;
        }
        /*        //Отправить команду настройки приемника
                public void SendCmdTune(int nrec, string freq, string station)
                {
                    Timer1.Stop(); //2сек не запрашивать состояние приемников
                    Command: "set fmt" + nrec.ToString() + " " + freq + " " + station);
                    Timer1.Start();
                } */
        //
        private void btFMCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btFMApply_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Применить изменения настроек FM_приемников."
                + "Вы уверены ?", "Подтвердите", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                MakeCmd();
                MyParentForm.SendMsg(cmd_fm + "\r");
                //res = MessageBox.Show("\nЧтобы изменения вступили в силу,\n"
                //   + "необходимо перезапустить БПР\n" + "Перезапустить сейчас ?\n", "",
                //   MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
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