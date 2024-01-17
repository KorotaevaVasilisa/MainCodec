using System;
using System.Windows.Forms;

namespace TCPclient
{
    public partial class BitrStat : Form
    {
        EditCodecForm MyParentForm = null;
        public BitrStat(EditCodecForm form)
        {
            InitializeComponent();
            this.MyParentForm = form;
            Parse(/*MyParentForm.rmsg_stat[0],*/ 0);
            Parse(1);
            Parse(2);
            Parse(3);
            ShowTcpu();
        }

        /*        int[] stat = new int[4],
                    bitr = new int[4],
                    packs = new int[4],
                    lost = new int[4];
                string[] ip = new string[4]; */
        public void Parse(/*string msg,*/ int ind)
        {
            string[] stringSeparators = new string[] { " ", "\r", "\n" };
            string[] subs = /*msg*/MyParentForm.rmsg_stat[ind].Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            string stat = "";
            int on = 0;
            for (int i = 0; i < subs.Length && i <= 5; i++)
            {
                if (i == 0)
                    on = Int32.Parse(subs[0]);
                else if (i == 1)
                {
                    stat = subs[1];
                    if (on < 1)
                        //{
                        //    stat += "  Отключено";
                        break;
                    //}
                }
                else if (i == 2)
                    stat += "  " + subs[2] + "_Кбит";
                else if (i == 3)
                    stat += "  Пакетов:" + subs[3];
                else if (i == 4)
                    stat += "  Потерь:" + subs[4];
                else if (i == 5)
                    stat += "   От:" + subs[5];
            }
            if (ind == 0)
                label1.Text = stat;
            else if (ind == 1)
                label2.Text = stat;
            else if (ind == 2)
                label3.Text = stat;
            else if (ind == 3)
                label4.Text = stat;
        }
        public void ShowTcpu()
        {
            lb_tproc.Text = MyParentForm.t_cpu;
        }
    }
}
