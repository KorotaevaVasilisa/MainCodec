using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TCPclient
{
    public partial class CreateFolderForm : Form
    {
        EditCodecForm parentForm;
        bool isLocal;
        public CreateFolderForm(EditCodecForm owner, bool isLocal)
        {
            InitializeComponent();
            this.parentForm = owner;
            this.isLocal = isLocal;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            parentForm.CreateFolder(isLocal, textBox1.Text);
            Close();
        }
    }
}
