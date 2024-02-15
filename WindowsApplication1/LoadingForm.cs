using System;
using System.Windows.Forms;
using log4net;

namespace TCPclient
{
    public partial class LoadingForm : Form
    {
        private EditCodecForm editCodecForm = null;
        private string path;
        private ActionState state;

        public LoadingForm(EditCodecForm editCodecForm, ActionState state, string output, string input = "")
        {
            InitializeComponent();
            this.editCodecForm = editCodecForm;
            this.path = output;
            this.state = state;
            tbFrom.Text = output;
            tbTo.Text = input;
            UpdateUI(state);
        }

        private void UpdateUI(ActionState state)
        {
            switch (state)
            {
                case ActionState.RemoteShow:
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
                case ActionState.RemoteEdit:
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
                case ActionState.RemoteCopy:
                    {
                        Text = "Копирование";
                        btCPApply.Text = "Копировать";
                        break;
                    }
                case ActionState.LocalCopy:
                    {
                        Text = "Копирование";
                        btCPApply.Text = "Копировать";
                        break;
                    }
                case ActionState.RemoteTransfer:
                    {
                        Text = "Перемещение";
                        btCPApply.Text = "Перенести";
                        break;
                    }
                case ActionState.LocalTransfer:
                    {
                        Text = "Перемещение";
                        btCPApply.Text = "Перенести";
                        break;
                    }
            }
        }

        private void btCPCancel_Click(object sender, EventArgs e)
        {
            if (progressBar1.Visible)
            {
                editCodecForm.ActionState = ActionState.Stop;
                editCodecForm.SendMsg("sfl abort");
            }
            else
            {
                editCodecForm.ActionState = ActionState.Inaction;
                Close();
            }
        }

        private void btCPApply_Click(object sender, EventArgs e)
        {
            btCPApply.Enabled = false;
            progressBar1.Visible = true;
            if (state == ActionState.RemoteTransfer || state == ActionState.RemoteCopy)
                editCodecForm.CdcOptionSflOpenrRqst(path);
            else
            {
                //TODO
            }
        }

        private static ILog logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void DownloadProgress(int readed, int total)
        {
            int percent = Convert.ToInt32((readed * 100) / total);
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() =>
                        {
                            this.progressBar1.Value = percent;
                            if (percent == 100)
                                Close();
                        }
                    ));
            }
            else
            {
                this.progressBar1.Value = percent;
                if (percent == 100)
                    Close();
            }
        }
    }
}