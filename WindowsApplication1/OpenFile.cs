using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace TCPclient
{
    public partial class OpenFile : Form
    {
        EditCodecForm editCodecForm = null;
        string sFile = "";
        bool readOnly;
        public OpenFile(EditCodecForm form, string pathFile, bool readOnly)
        {
            InitializeComponent();
            this.editCodecForm = form;
            this.DialogResult = DialogResult.Cancel;
            ReadFile(pathFile, readOnly);
            sFile = pathFile;
            this.readOnly = readOnly;
            if (readOnly)
                btnSave.Enabled = false;
        }

        public OpenFile(EditCodecForm form, ListRemoteState state, string fileName, string information)
        {
            InitializeComponent();
            this.editCodecForm = form;
            Text = fileName;
            richTextBox1.Text = information;
            if (state == ListRemoteState.Show)
            {
                richTextBox1.ReadOnly = true;
                btnSave.Enabled = false;            
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
            if (!readOnly)
            {
                try
                {
                    System.IO.File.WriteAllText(sFile, richTextBox1.Text);
                    MessageBox.Show("Файл сохранен");
                }catch( Exception ex)
                {
                    MessageBox.Show(ex.Message);
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
