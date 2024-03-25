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
        int encodingCode = 0;

        ActionStateEnum state;

        public OpenFile(EditCodecForm form, ActionStateEnum state, string fileName, string information, string path)
        {
            InitializeComponent();

            this.editCodecForm = form;
            this.state = state;
            this.path = path;
            Text = fileName;
            cbCod.SelectedIndex = encodingCode;
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

        private void saveButton_Click(object sender, EventArgs e)
        {

                        try
                        {
                            /*
                            System.IO.File.WriteAllText(path, richTextBox1.Text);
                            editCodecForm.UpdateData(); */
                            FileStream fs = new FileStream(path, FileMode.Create);
                            StreamWriter sw;
                            switch (cbCod.SelectedIndex)
                            {
                                case 0:
                                    sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding("windows-1251"), 1024);
                                    break;
                                case 1:
                                    sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding("koi8-r"), 1024);
                                    break;
                                case 2:
                                default:
                                    sw = new StreamWriter(fs);
                                    break;
                            }
                            sw.WriteLine(richTextBox1.Text);
                            sw.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }                                     
        Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            FileInfo info = new FileInfo(path);
            StreamReader fin;
            switch (cbCod.SelectedIndex)
            {
                case 0:
                    fin = new StreamReader(path, System.Text.Encoding.GetEncoding("windows-1251"), false);
                    break;
                case 1:
                    fin = new StreamReader(path, System.Text.Encoding.GetEncoding("koi8-r"), false);
                    break; // koi8-r 20866
                case 2:
                default:
                    fin = new StreamReader(path);
                    break;
            }
            StringBuilder sText = new StringBuilder((int)info.Length);
            string s;
            while ((s = fin.ReadLine()) != null)
                sText.Append(s + '\n');
            fin.Close();
            richTextBox1.Text = sText.ToString();
        }
    }
}
