using System;
using System.Windows.Forms;

namespace TCPclient
{
    public partial class Sentfiles : Form
    {
        EditCodecForm MyParentForm = null;
        string passw_dflt = "kgv1957slon";
        string iphost = "";
        public Sentfiles(EditCodecForm form, string ipcdc)
        {
            InitializeComponent();
            this.MyParentForm = form;
            this.DialogResult = DialogResult.Cancel;
            //Repaint();
            tbPassw.Text = passw_dflt;
            iphost = ipcdc;
        }

        private void cbPassw_ChckChngd(object sender, EventArgs e)
        {
            if (cbPassw.Checked)
            {
                tbPassw.Text = passw_dflt;
            }
            else
            {
                tbPassw.Text = "";
            }
        }
        string cmd_sf = "";

        bool MakeCmd()
        {
            cmd_sf = "";
            return true;
        }
        private void btSentFCancel_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.Cancel;
            Close();
        }
        private void btSentFApply_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Переслать файл на БПР " + iphost
                + "\rВы уверены ?", "Подтвердите", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                MakeCmd();
                MyParentForm.SendMsg(cmd_sf + "\r");
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
        //
        private void btGetFile_Click(object sender, EventArgs e)
        {
            return; //dbg
            //Удалить предыдущий архив
            // del  codec
            // Забрать codec с кодека
            //pscp -pw tbPassw.Text root@192.168.1.29:/temas/codec/codec ./ >a.done
            DialogResult res = MessageBox.Show("Забрать файл прошивки с БПР " + iphost
                + "\rВы уверены ?", "Подтвердите", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                string cmd = "pscp -pw " + tbPassw.Text + " root@" + iphost + ":/temas/codec/codec ./ >a.done";
                MyParentForm.SendMsg(cmd + "\r");
            }
        }
        private void btPutFile_Click(object sender, EventArgs e)
        {
            return; //dbg
                    //rem Создать командный файл a.tmp для удаления на кодеке (обязательно)
                    //echo mv /temas/codec/codec /temas/codec/codec.res;exit >a.tmp
                    //rem Переименовать в .res
                    //plink -batch -m a.tmp -pw testing123 root@192.168.1.29
                    //rem Передать codec на кодека
                    //pscp -pw testing123 ./codec root@192.168.1.29:/temas/codec/codec >a.done
            DialogResult res = MessageBox.Show("Переслать файл прошивки на БПР " + iphost
                + "\rВы уверены ?", "Подтвердите", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                string cmd = "echo mv /temas/codec/codec /temas/codec/codec.res;exit >a.tmp";
                MyParentForm.SendMsg(cmd + "\r");
                cmd = "plink -batch -m " + "a.tmp -pw " + tbPassw.Text + " root@" + iphost;
                MyParentForm.SendMsg(cmd + "\r");
                cmd = "pscp -pw" + tbPassw.Text + "./codec root@" + iphost + ":/temas/codec/codec >a.done";
                MyParentForm.SendMsg(cmd + "\r");
            }
        }
        private void btGetFiles_Click(object sender, EventArgs e)
        {
            return; //dbg
                    //rem Удалить предыдущий архив
                    //del  codec.tar
                    //rem Для временного создания архива используется каталог /dev/shm
                    //rem Создать командный файл a.tmp для создания архива codec.tar
                    //echo tar -cf /dev/shm/codec.tar /temas/codec/*.*;exit >a.tmp
                    //rem Подключиться и выполнить a.tmp на кодеке, статистика при необходимости писать в a.done
                    //plink -batch -m a.tmp -pw testing123 root@192.168.1.29 >>a.done
                    //rem Забрать архив codec.tar с кодека
                    //pscp -pw testing123 root@192.168.1.29:/dev/shm/codec.tar ./ >>a.done
                    //rem Создать командный файл a.tmp для удаления архива на кодеке (не обязательно)
                    //echo rm /dev/shm/*.tar;exit >a.tmp
                    //rem Удалить
                    //plink -batch -m a.tmp -pw testing123 root@192.168.1.29
            string cmd = "del codec.tar"; //? где удалить?на компьютере или кодеке?
            MyParentForm.SendMsg(cmd + "\r");
            cmd = "echo tar -cf /dev/shm/codec.tar /temas/codec/*.*exit >a.tmp";
            MyParentForm.SendMsg(cmd + "\r");
            cmd = "plink -batch -m a.tmp -pw " + tbPassw.Text + " root@" + iphost + " >a.done";
            MyParentForm.SendMsg(cmd + "\r");
            cmd = "pscp -pw" + tbPassw.Text + "./codec.tar root@" + iphost + ":/dev/shm/codec.tar >a.done";
            MyParentForm.SendMsg(cmd + "\r");
            cmd = "echo rm /dev/shm/*.tar;exit >a.tmp";
            MyParentForm.SendMsg(cmd + "\r");
            cmd = "plink -batch -m a.tmp -pw " + tbPassw.Text + " root@" + iphost;
            MyParentForm.SendMsg(cmd + "\r");
        }
        private void btPutFiles_Click(object sender, EventArgs e)
        {
            return; //dbg
                    //rem Для временного создания архива используется каталог /dev/shm
                    //rem Забрать архив codec.tar с Арма
                    //pscp -pw testing123 ./codec.tar root@192.168.1.29:/dev/shm/codec.tar >a.done
                    //rem Создать командный файл a.tmp для распаковки архива codec.tar
                    //echo tar -xf /dev/shm/codec.tar -C /;exit >a.tmp
                    //rem Подключиться и выполнить a.tmp на кодеке, статистика при необходимости писать в a.done
                    //plink -batch -m a.tmp -pw testing123 root@192.168.1.29 >>a.done
            string cmd = "pscp -pw" + tbPassw.Text + "./codec.tar root@" + iphost + ":/dev/shm/codec.tar >a.done";
            MyParentForm.SendMsg(cmd + "\r");
            cmd = "echo tar -xf /dev/shm/codec.tar -C /;exit >a.tmp";
            MyParentForm.SendMsg(cmd + "\r");
            cmd = "plink -batch -m " + "a.tmp -pw " + tbPassw.Text + " root@" + iphost;
            MyParentForm.SendMsg(cmd + "\r");
        }

        private void btReboot_Click(object sender, EventArgs e)
        {
            return; //dbg
            DialogResult res = MessageBox.Show("Перезапуск БПР " + iphost
                + "\rВы уверены ?", "Подтвердите", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                //echo reboot;exit >pli.tmp
                //plink -batch -m pli.tmp -pw testing123 root@192.168.1.29
                string cmd = "echo reboot; exit >pli.tmp";
                MyParentForm.SendMsg(cmd + "\r");
                cmd = "plink -batch -m" + "pli.tmp -pw " + tbPassw.Text + " root@" + iphost;
                MyParentForm.SendMsg(cmd + "\r");
            }
        }
    }
}
