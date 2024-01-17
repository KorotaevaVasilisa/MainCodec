namespace TCPclient
{
    partial class OutsCodecs
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
            this.btProgramApply = new System.Windows.Forms.Button();
            this.btProgramCancel = new System.Windows.Forms.Button();
            this.cbOut1 = new System.Windows.Forms.ComboBox();
            this.cbOut2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbOut4 = new System.Windows.Forms.ComboBox();
            this.cbOut3 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btProgramApply
            // 
            this.btProgramApply.Location = new System.Drawing.Point(397, 206);
            this.btProgramApply.Name = "btProgramApply";
            this.btProgramApply.Size = new System.Drawing.Size(75, 22);
            this.btProgramApply.TabIndex = 60;
            this.btProgramApply.Text = "Применить";
            this.btProgramApply.UseVisualStyleBackColor = true;
            this.btProgramApply.Click += new System.EventHandler(this.btProgramApply_Click);
            // 
            // btProgramCancel
            // 
            this.btProgramCancel.Location = new System.Drawing.Point(316, 206);
            this.btProgramCancel.Name = "btProgramCancel";
            this.btProgramCancel.Size = new System.Drawing.Size(75, 22);
            this.btProgramCancel.TabIndex = 59;
            this.btProgramCancel.Text = "Отмена";
            this.btProgramCancel.UseVisualStyleBackColor = true;
            this.btProgramCancel.Click += new System.EventHandler(this.btProgramCancel_Click);
            // 
            // cbOut1
            // 
            this.cbOut1.FormattingEnabled = true;
            this.cbOut1.Location = new System.Drawing.Point(79, 28);
            this.cbOut1.MaxDropDownItems = 19;
            this.cbOut1.Name = "cbOut1";
            this.cbOut1.Size = new System.Drawing.Size(147, 21);
            this.cbOut1.TabIndex = 61;
            // 
            // cbOut2
            // 
            this.cbOut2.FormattingEnabled = true;
            this.cbOut2.Location = new System.Drawing.Point(79, 53);
            this.cbOut2.MaxDropDownItems = 19;
            this.cbOut2.Name = "cbOut2";
            this.cbOut2.Size = new System.Drawing.Size(147, 21);
            this.cbOut2.TabIndex = 62;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 15);
            this.label1.TabIndex = 63;
            this.label1.Text = "Выход Декодера";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 64;
            this.label2.Text = "Выход 1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 65;
            this.label3.Text = "Выход 2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 69;
            this.label4.Text = "Выход 4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 68;
            this.label5.Text = "Выход 3";
            // 
            // cbOut4
            // 
            this.cbOut4.FormattingEnabled = true;
            this.cbOut4.Location = new System.Drawing.Point(79, 104);
            this.cbOut4.MaxDropDownItems = 19;
            this.cbOut4.Name = "cbOut4";
            this.cbOut4.Size = new System.Drawing.Size(147, 21);
            this.cbOut4.TabIndex = 67;
            // 
            // cbOut3
            // 
            this.cbOut3.FormattingEnabled = true;
            this.cbOut3.Location = new System.Drawing.Point(79, 78);
            this.cbOut3.MaxDropDownItems = 19;
            this.cbOut3.Name = "cbOut3";
            this.cbOut3.Size = new System.Drawing.Size(147, 21);
            this.cbOut3.TabIndex = 66;
            // 
            // ProgramEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 229);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbOut4);
            this.Controls.Add(this.cbOut3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbOut2);
            this.Controls.Add(this.cbOut1);
            this.Controls.Add(this.btProgramApply);
            this.Controls.Add(this.btProgramCancel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgramEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Назначение программ вещания";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btProgramApply;
        private System.Windows.Forms.Button btProgramCancel;
        private System.Windows.Forms.ComboBox cbOut1;
        private System.Windows.Forms.ComboBox cbOut2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbOut4;
        private System.Windows.Forms.ComboBox cbOut3;
    }
}