using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace TCPclient
{
    public partial class OpenFile : Form
    {
        EditCodecForm MyParentForm = null;
        string sFile = "";
        public OpenFile(EditCodecForm form, string pathFile, bool readOnly)
        {
            InitializeComponent();
            this.MyParentForm = form;
            this.DialogResult = DialogResult.Cancel;
            ReadFile(pathFile, readOnly);
            sFile = pathFile;
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
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            richTextBox1.ReadOnly = readOnly;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            System.IO.File.WriteAllText(sFile, richTextBox1.Text);
            MessageBox.Show("Файл сохранен");
        }
    }
}
