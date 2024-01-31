using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace TCPclient
{
    public partial class LoadingForm : Form
    {
        EditCodecForm editCodecForm = null;
        string path;
        string fileName;
        ListRemoteState state;
        public LoadingForm(EditCodecForm editCodecForm, ListRemoteState state, string fileName, string output, string input ="")
        {
            InitializeComponent();
            this.editCodecForm = editCodecForm;
            this.path = output;
            tbFrom.Text = output; tbTo.Text = input;
            this.state = state;
            this.fileName = fileName;
            UpdateUI(state);
        }

        private void UpdateUI(ListRemoteState state)
        {
            switch (state)
            {
                case ListRemoteState.Show:
                    {
                        Text = "Загрузка";
                        btCPCancel.Text = "Прервать";
                        btCPApply.Visible = false;
                        progressBar1.Visible = true;
                        label2.Visible = false;
                        tbTo.Visible = false;
                        editCodecForm.CdcOptionSflOpenrRqst(path);
                        break;
                    }
                case ListRemoteState.Edit :
                    {
                        Text = "Загрузка";
                        btCPCancel.Text = "Прервать";
                        btCPApply.Visible = false;
                        progressBar1.Visible = true;
                        label2.Visible = false;
                        tbTo.Visible = false;
                        editCodecForm.CdcOptionSflOpenrRqst(path);
                        break;
                    }
                case ListRemoteState.Copy:
                    {
                        Text = "Копирование";
                        btCPApply.Text = "Копировать";
                        break;
                    }
                case ListRemoteState.Transfer:
                    {
                        Text = "Перемещение";
                        btCPApply.Text = "Перенестить";
                        break;
                    }
            }
        }

        private void btCPCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btCPApply_Click(object sender, EventArgs e)
        {
            btCPApply.Enabled = false;
            progressBar1.Visible = true;
            editCodecForm.CdcOptionSflOpenrRqst(path);
        }

        public void DownloadProgress(int readed, int total)
        {
            int percent = Convert.ToInt32((readed * 100) / total);
            Invoke((MethodInvoker)(() =>
            {

                this.progressBar1.Value = percent;
                if (percent == 100)
                    Close();
            }
            ));
        }
    }
}
