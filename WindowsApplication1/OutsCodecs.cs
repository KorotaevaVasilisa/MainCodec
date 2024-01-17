using System;
using System.Windows.Forms;

namespace TCPclient
{
    public partial class OutsCodecs : Form
    {
        EditCodecForm MyParentForm = null;
        public OutsCodecs(EditCodecForm form)
        {
            InitializeComponent();
            this.MyParentForm = form;
            this.DialogResult = DialogResult.Cancel;
            FillDestList();
            Parse(MyParentForm.rmsg_program);
            //Getstat();
            Repaint();
        }
        public void Repaint()
        {
        }
        /*        string[] nameChanalTransOut = new string[] { 
                    "Отключено", 
                    "1-ый канал", "2-ой канал", "3-ий канал", "4-ый канал", 
                    "5-ый канал", "6-ой канал", "7-ой канал", "8-ой канал",
                    "1-ый вход кодера", "2-ой вход кодера", "3-ий вход кодера", "4-ый вход кодера",
                    "5-ый вход кодера", "6-ой вход кодера"
                };
                string[] nameChanalTransOutAdd = new string[] { 
                    "Фонограмма 1", "Фонограмма 2", "Фонограмма 3", "Фонограмма 4", 
                    "Фонограмма 5", "Фонограмма 6", "Фонограмма 7", "Фонограмма 8",
                    "Фонограмма 9",
                    "1-ый выход кодера",
                    "Генератор 1", "Генератор 2"
                };
                string[] nameChanalTransICE = new string[] { 
                     "1-я потоковая станция", "2-я потоковая станция",
                     "3-я потоковая станция", "4-я потоковая станция"
               };
                string[] paramChanalTransOut = new string[] { 
                    "gr", 
                    "p1", "p2", "p3", "p4", 
                    "p5", "p6", "p7", "p8", 
                    "i1", "i2", "i3", "i4", 
                    "i5", "i6", 
                    "m1", "m2", "m3", "m4", 
                    "m5", "m6", "m7", "m8", "m9",
                    "o1",
                    "g1", "g2",
                    "s1", "s2", "s3", "s4" //ICEcast
                }; */
        string[] NameTrans = new string[] {
            "Отключено",
            "1-ый канал", "2-ой канал", "3-ий канал", "4-ый канал",
            "5-ый канал", "6-ой канал", "7-ой канал", "8-ой канал",
            "1-ый вход кодера", "2-ой вход кодера", "3-ий вход кодера", "4-ый вход кодера",
            "5-ый вход кодера", "6-ой вход кодера", "7-ой вход кодера", "8-ой вход кодера",
            "1-я потоковая станция", "2-я потоковая станция",
            "3-я потоковая станция", "4-я потоковая станция"
        };
        string[] ParamTrans = new string[] {
            "gr",
            "p1", "p2", "p3", "p4",
            "p5", "p6", "p7", "p8",
            "i1", "i2", "i3", "i4", "i5", "i6", "i7", "i8",//06.2020- добавил 7-8  
            "s1", "s2", "s3", "s4" //ICEcast
        };
        //
        string[] args = new string[22];
        public void Parse(string msg)
        {
            /* get list:
            : i1 gr gr
            : i2 gr gr
            : i3 gr gr
            : i4 gr gr
            : o1 p1
            : o2 gr
            : o3 p2
            : o4 p3
            CD: 
             --Выходы декодеров подачи программ
             (мб Отключено, Канал 1..8, Потоковая станция 1..4, Вход кодера 1..6)
            cdc Ok get: o1: p1 //p1: 1-й канал
            cdc Ok get: o2: gr //gr: Отключено
            cdc Ok get: o3: p2
            cdc Ok get: o4: p3
            CS:
             --Входы кодеров
             (мб Оператор, Потоковая станция 1..4)
            cdc Ok get: i4: p8 gr //gr: Оператор
            cdc Ok get: i2: p2 m2 //m2: Фонограмма2
             --Выходы декодеров
             (мб Отключено, Вход кодера 1..8)
            cdc Ok get: o1: gr //gr: Отключено
            cdc Ok get: o2: gr
            cdc Ok get: o3: gr
            cdc Ok get: o4: gr */
            string[] stringSeparators = new string[] { "\r", "\n" };
            string[] subs = msg.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < args.Length; i++)
                args[i] = "";
            short nout; //N выхода
            string dest = "";
            ComboBox cbout;
            try
            {
                for (int i = 0; i < subs.Length; i++)
                {
                    //убрать Ok get:< >
                    //if (subs[i].StartsWith("Ok get: "))
                    //    subs[i] = subs[i].Substring(8);
                    int indx = subs[i].IndexOf("Ok get: o");
                    if (indx >= 0)
                        subs[i] = subs[i].Substring(indx + 8);
                    else
                        continue;
                    //int sind = subs[i].IndexOf("Ok get: ");
                    //if (sind < 0)
                    //    continue;
                    //subs[i] = subs[i].Substring(sind + 3);
                    // MyParentForm.MsgToCon("ProgramEdit.Parse.i" + i.ToString() + ">" + subs[i]);
                    if (subs[i].Length < 5)
                        continue;
                    if (!Int16.TryParse(subs[i].Substring(1, 1), out nout)) //номер выхода от 1
                        continue;
                    subs[i] = subs[i].Replace(':', ' ');
                    string[] argv = subs[i].Split((string[])null,
                        StringSplitOptions.RemoveEmptyEntries); // сепаратор ' '
                    if (argv.Length < 2)
                        continue;
                    dest = argv[1];
                    // MyParentForm.MsgToCon("ProgramEdit.Parse nout:" + nout.ToString() + ">" + dest);
                    if (nout == 1)
                    {
                        cbout = cbOut1;
                    }
                    else if (nout == 2)
                    {
                        cbout = cbOut2;
                    }
                    else if (nout == 3)
                    {
                        cbout = cbOut3;
                    }
                    else if (nout == 4)
                    {
                        cbout = cbOut4;
                    }
                    else
                        continue;
                    ShowOut(cbout, dest);
                }
            }
            catch (Exception e)
            {
                MyParentForm.MsgToCon("ProgramEdit.Parse.Exception " + e.Message);
            }
        }
        void ShowOut(ComboBox cbout, string dest)
        {
            int nameind = -1;
            for (int i = 0; i < ParamTrans.Length; i++)
            {
                if (dest == ParamTrans[i])
                {
                    nameind = i;
                    cbout.SelectedIndex = i;
                    break;
                }
            }
            // MyParentForm.MsgToCon("ProgramEdit.ShowOut nmind:" + nameind.ToString() + ">" + NameTrans[nameind]);
            if (nameind >= 0)
                cbout.Text = NameTrans[nameind];
            else
            {
                cbout.SelectedIndex = -1;
                if (dest == "g1")
                    cbout.Text = "Генератор 1";
                else if (dest == "g2")
                    cbout.Text = "Генератор 2";
                else
                    cbout.Text = dest;
            }
        }
        private void FillDestList()
        {
            cbOut1.Items.Clear();
            cbOut2.Items.Clear();
            cbOut3.Items.Clear();
            cbOut4.Items.Clear();
            for (int i = 0; i < NameTrans.Length; i++)
            {
                if (NameTrans[i] == null)
                    break;
                cbOut1.Items.Add(NameTrans[i]);
                cbOut2.Items.Add(NameTrans[i]);
                cbOut3.Items.Add(NameTrans[i]);
                cbOut4.Items.Add(NameTrans[i]);
            }
        }
        string cmd_prog = "";
        bool MakeCmd()
        {
            cmd_prog = "";
            for (int indout = 0; indout < 4; indout++) //ind 0..3->Nвыхода 1..4
            {
                if (indout == 0)
                {
                    if (cbOut1.SelectedIndex >= 0)
                    {
                        cmd_prog += "set o1 " + ParamTrans[cbOut1.SelectedIndex] + ";";
                    }
                }
                else if (indout == 1)
                {
                    if (cbOut2.SelectedIndex >= 0)
                    {
                        cmd_prog += "set o2 " + ParamTrans[cbOut2.SelectedIndex] + ";";
                    }
                }
                else if (indout == 2)
                {
                    if (cbOut3.SelectedIndex >= 0)
                    {
                        cmd_prog += "set o3 " + ParamTrans[cbOut3.SelectedIndex] + ";";
                    }
                }
                else if (indout == 3)
                {
                    if (cbOut4.SelectedIndex >= 0)
                    {
                        cmd_prog += "set o4 " + ParamTrans[cbOut4.SelectedIndex] + ";";
                    }
                }
                else
                    continue;
            }
            // MyParentForm.MsgToCon("ProgramEdit.MakeCmd: " + cmd_prog);
            return true;
        }
        private void btProgramCancel_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.Cancel;
            Close();
        }
        private void btProgramApply_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Применить изменения настроек программ вещания."
                + "Вы уверены ?", "Подтвердите", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                MakeCmd();
                MyParentForm.SendMsg(cmd_prog + "\r");
                //res = MessageBox.Show("\nЧтобы изменения вступили в силу,\n"
                //   + "необходимо перезапустить БПР\n" + "Перезапустить сейчас ?\n", "",
                //   MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (res == DialogResult.Yes)
                //    MyParentForm.SendMsg("system reboot\r");
                this.DialogResult = DialogResult.OK;
                //Close();
            }
            Close();
        }
    }
}