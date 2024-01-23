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
    public partial class CopyAndTransferWindow : Form
    {
        EditCodecForm editCodecForm = null;
        bool copy = true;
        string fileName;
        public CopyAndTransferWindow(EditCodecForm editCodecForm, bool copy, string output, string input, string fileName)
        {
            InitializeComponent();
            this.editCodecForm = editCodecForm;
            tbFrom.Text = output; tbTo.Text = input;
            this.copy = copy;
            this.fileName = fileName;
            if(!copy )
            {
                Text = "Перемещение";
                btCPApply.Text = "Перенестить";
            }
        }

        private void btCPCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btCPApply_Click(object sender, EventArgs e)
        {
            editCodecForm.CdcOptionSflOpenrRqst(fileName);
            Close();
        }
    }
}
