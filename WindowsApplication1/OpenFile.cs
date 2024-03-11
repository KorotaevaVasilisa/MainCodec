using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace TCPclient
{
    public partial class OpenFile : Form
    {
        EditCodecForm editCodecForm = null;
        string path = "";

        ActionStateEnum state;

        public OpenFile(EditCodecForm form, ActionStateEnum state, string fileName, string information, string path)
        {
            InitializeComponent();
            this.editCodecForm = form;
            this.state = state;
            this.path = path;
            Text = fileName;
            richTextBox1.Text = information;
            UpdateUI(state);
        }

        private void UpdateUI(ActionStateEnum state)
        {
            switch (state)
            {
                case ActionStateEnum.RemoteShow:
                    {
                        richTextBox1.ReadOnly = true;
                        btnSave.Enabled = false;
                        break;
                    }
                    case ActionStateEnum.LocalShow:
                    {
                        richTextBox1.ReadOnly = true;
                        btnSave.Enabled = false;
                        break;
                    }
            }
        }

        private void ReadFile(string pathFile, bool readOnly)
        {
            try
            {
                //               Encoding encoding = Encoding.GetEncoding("windows-1251");
                string fileText = System.IO.File.ReadAllText(pathFile, Encoding.UTF8);
                richTextBox1.Text = fileText;

            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message);
            }
            richTextBox1.ReadOnly = readOnly;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
               
            switch (state)
            {
                case ActionStateEnum.LocalEdit:
                    {
                        try
                        {
                            System.IO.File.WriteAllText(path, richTextBox1.Text);
                            editCodecForm.UpdateData();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        break;
                    }
                case ActionStateEnum.RemoteEdit:
                    {
                        //editCodecForm.EditSaveFile(richTextBox1.Text, path);
                        break;
                    }
            }
            Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
