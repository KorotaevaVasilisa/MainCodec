namespace TCPclient
{
    partial class Sentfiles
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btPutFile = new System.Windows.Forms.Button();
            this.btGetFile = new System.Windows.Forms.Button();
            this.btGetFiles = new System.Windows.Forms.Button();
            this.PutFiles = new System.Windows.Forms.Button();
            this.cbPassw = new System.Windows.Forms.CheckBox();
            this.tbPassw = new System.Windows.Forms.TextBox();
            this.btSntCancel = new System.Windows.Forms.Button();
            this.btSntApply = new System.Windows.Forms.Button();
            this.btReboot = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btPutFile
            // 
            this.btPutFile.Location = new System.Drawing.Point(12, 66);
            this.btPutFile.Name = "btPutFile";
            this.btPutFile.Size = new System.Drawing.Size(221, 23);
            this.btPutFile.TabIndex = 0;
            this.btPutFile.Text = "Переслать файл прошивки на БПР";
            this.btPutFile.UseVisualStyleBackColor = true;
            this.btPutFile.Click += new System.EventHandler(this.btPutFile_Click);
            // 
            // btGetFile
            // 
            this.btGetFile.Location = new System.Drawing.Point(12, 26);
            this.btGetFile.Name = "btGetFile";
            this.btGetFile.Size = new System.Drawing.Size(221, 23);
            this.btGetFile.TabIndex = 1;
            this.btGetFile.Text = "Забрать файл прошивки с БПР";
            this.btGetFile.UseVisualStyleBackColor = true;
            this.btGetFile.Click += new System.EventHandler(this.btGetFile_Click);
            // 
            // btGetFiles
            // 
            this.btGetFiles.Location = new System.Drawing.Point(261, 26);
            this.btGetFiles.Name = "btGetFiles";
            this.btGetFiles.Size = new System.Drawing.Size(221, 23);
            this.btGetFiles.TabIndex = 2;
            this.btGetFiles.Text = "Забрать папку кодека с БПР";
            this.btGetFiles.UseVisualStyleBackColor = true;
            this.btGetFiles.Click += new System.EventHandler(this.btGetFiles_Click);
            // 
            // PutFiles
            // 
            this.PutFiles.Location = new System.Drawing.Point(261, 66);
            this.PutFiles.Name = "PutFiles";
            this.PutFiles.Size = new System.Drawing.Size(221, 23);
            this.PutFiles.TabIndex = 3;
            this.PutFiles.Text = "Переслать папку кодека на БПР";
            this.PutFiles.UseVisualStyleBackColor = true;
            // 
            // cbPassw
            // 
            this.cbPassw.AutoSize = true;
            this.cbPassw.Checked = true;
            this.cbPassw.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPassw.Location = new System.Drawing.Point(12, 108);
            this.cbPassw.Name = "cbPassw";
            this.cbPassw.Size = new System.Drawing.Size(138, 17);
            this.cbPassw.TabIndex = 4;
            this.cbPassw.Text = "Пароль по умолчанию";
            this.cbPassw.UseVisualStyleBackColor = true;
            this.cbPassw.CheckedChanged += new System.EventHandler(this.cbPassw_ChckChngd);
            // 
            // tbPassw
            // 
            this.tbPassw.Location = new System.Drawing.Point(12, 131);
            this.tbPassw.Name = "tbPassw";
            this.tbPassw.Size = new System.Drawing.Size(188, 20);
            this.tbPassw.TabIndex = 5;
            // 
            // btSntCancel
            // 
            this.btSntCancel.Location = new System.Drawing.Point(480, 238);
            this.btSntCancel.Name = "btSntCancel";
            this.btSntCancel.Size = new System.Drawing.Size(88, 23);
            this.btSntCancel.TabIndex = 6;
            this.btSntCancel.Text = "Отменить";
            this.btSntCancel.UseVisualStyleBackColor = true;
            this.btSntCancel.Click += new System.EventHandler(this.btSentFCancel_Click);
            // 
            // btSntApply
            // 
            this.btSntApply.Location = new System.Drawing.Point(355, 238);
            this.btSntApply.Name = "btSntApply";
            this.btSntApply.Size = new System.Drawing.Size(99, 23);
            this.btSntApply.TabIndex = 7;
            this.btSntApply.Text = "Применить";
            this.btSntApply.UseVisualStyleBackColor = true;
            this.btSntApply.Visible = false;
            this.btSntApply.Click += new System.EventHandler(this.btSentFApply_Click);
            // 
            // btReboot
            // 
            this.btReboot.Location = new System.Drawing.Point(16, 188);
            this.btReboot.Name = "btReboot";
            this.btReboot.Size = new System.Drawing.Size(221, 23);
            this.btReboot.TabIndex = 8;
            this.btReboot.Text = "Перезапуск БПР";
            this.btReboot.UseVisualStyleBackColor = true;
            this.btReboot.Click += new System.EventHandler(this.btReboot_Click);
            // 
            // Sentfiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 273);
            this.Controls.Add(this.btReboot);
            this.Controls.Add(this.btSntApply);
            this.Controls.Add(this.btSntCancel);
            this.Controls.Add(this.tbPassw);
            this.Controls.Add(this.cbPassw);
            this.Controls.Add(this.PutFiles);
            this.Controls.Add(this.btGetFiles);
            this.Controls.Add(this.btGetFile);
            this.Controls.Add(this.btPutFile);
            this.Name = "Sentfiles";
            this.Text = "Пересылка файлов";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btPutFile;
        private System.Windows.Forms.Button btGetFile;
        private System.Windows.Forms.Button btGetFiles;
        private System.Windows.Forms.Button PutFiles;
        private System.Windows.Forms.CheckBox cbPassw;
        private System.Windows.Forms.TextBox tbPassw;
        private System.Windows.Forms.Button btSntCancel;
        private System.Windows.Forms.Button btSntApply;
        private System.Windows.Forms.Button btReboot;
    }
}