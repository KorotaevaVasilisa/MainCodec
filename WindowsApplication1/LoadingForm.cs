using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using log4net;

namespace TCPclient
{
    public partial class LoadingForm : Form
    {
        private EditCodecForm editCodecForm = null;
        private string path;
        private string inputPath;
        private int sizeFile;
        private ActionStateEnum state;

        public LoadingForm(EditCodecForm editCodecForm, ActionStateEnum state,int sizeFile, string output, string input = "")
        {
            InitializeComponent();
            this.editCodecForm = editCodecForm;
            this.path = output;
            this.state = state;
            tbFrom.Text = output;
            tbTo.Text = input;
            this.sizeFile = sizeFile;
            this.inputPath = input;
            UpdateUI(state);
        }

        private void UpdateUI(ActionStateEnum state)
        {
            switch (state)
            {
                case ActionStateEnum.RemoteShow:
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
                case ActionStateEnum.RemoteEdit:
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
                case ActionStateEnum.RemoteCopy:
                    {
                        Text = "Копирование";
                        btCPApply.Text = "Копировать";
                        break;
                    }
                case ActionStateEnum.LocalCopy:
                    {
                        Text = "Копирование";
                        btCPApply.Text = "Копировать";
                        break;
                    }
                case ActionStateEnum.RemoteTransfer:
                    {
                        Text = "Перемещение";
                        btCPApply.Text = "Перенести";
                        break;
                    }
                case ActionStateEnum.LocalTransfer:
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
                editCodecForm.ActionState = ActionStateEnum.Stop;
                editCodecForm.SendMsg("sfl abort");
            }
            else
            {
                editCodecForm.ActionState = ActionStateEnum.Inaction;
                Close();
            }
        }

        private void btCPApply_Click(object sender, EventArgs e)
        {
            btCPApply.Enabled = false;
            progressBar1.Visible = true;
            if (state == ActionStateEnum.RemoteTransfer || state == ActionStateEnum.RemoteCopy)
                editCodecForm.CdcOptionSflOpenrRqst(path);
            else
            {
                editCodecForm.EditSaveFile(sizeFile, inputPath);
                /*
                try
                {
                    
                    string fileText = System.IO.File.ReadAllText(path, Encoding.UTF8);
                    //var stringsBytes = File.ReadAllLines(path, Encoding.UTF8);
                    if (state == ActionStateEnum.LocalTransfer)
                    {
                        System.IO.FileInfo file = new System.IO.FileInfo(path);
                        if (file.Exists)
                        {
                            file.Delete();
                        }
                    }
                    editCodecForm.EditSaveFile(sizeFile,path);
                    //Close();
                }
                catch( IOException ex)
                {
                    MessageBox.Show(ex.Message);
                }*/
            }
        }

        private static ILog logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        int readed = 0;
        public void DownloadProgress(int package)
        {
                readed += package;
                int percent = Convert.ToInt32((readed * 100) / sizeFile);
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