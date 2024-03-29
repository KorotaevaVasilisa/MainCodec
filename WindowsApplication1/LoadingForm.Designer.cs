﻿namespace TCPclient
{
    partial class LoadingForm
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
            this.btCPCancel = new System.Windows.Forms.Button();
            this.btCPApply = new System.Windows.Forms.Button();
            this.tbFrom = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbTo = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // btCPCancel
            // 
            this.btCPCancel.Location = new System.Drawing.Point(378, 145);
            this.btCPCancel.Name = "btCPCancel";
            this.btCPCancel.Size = new System.Drawing.Size(100, 25);
            this.btCPCancel.TabIndex = 0;
            this.btCPCancel.Text = "Отмена";
            this.btCPCancel.UseVisualStyleBackColor = true;
            this.btCPCancel.Click += new System.EventHandler(this.btCPCancel_Click);
            // 
            // btCPApply
            // 
            this.btCPApply.Location = new System.Drawing.Point(484, 145);
            this.btCPApply.Name = "btCPApply";
            this.btCPApply.Size = new System.Drawing.Size(121, 25);
            this.btCPApply.TabIndex = 1;
            this.btCPApply.Text = "Копировать";
            this.btCPApply.UseVisualStyleBackColor = true;
            this.btCPApply.Click += new System.EventHandler(this.btCPApply_Click);
            // 
            // tbFrom
            // 
            this.tbFrom.Location = new System.Drawing.Point(76, 35);
            this.tbFrom.Margin = new System.Windows.Forms.Padding(15);
            this.tbFrom.Name = "tbFrom";
            this.tbFrom.ReadOnly = true;
            this.tbFrom.Size = new System.Drawing.Size(529, 22);
            this.tbFrom.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Откуда";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Куда";
            // 
            // tbTo
            // 
            this.tbTo.Location = new System.Drawing.Point(76, 67);
            this.tbTo.Name = "tbTo";
            this.tbTo.ReadOnly = true;
            this.tbTo.Size = new System.Drawing.Size(529, 22);
            this.tbTo.TabIndex = 5;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(18, 104);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(587, 23);
            this.progressBar1.TabIndex = 6;
            this.progressBar1.Visible = false;
            // 
            // LoadingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 182);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.tbTo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbFrom);
            this.Controls.Add(this.btCPApply);
            this.Controls.Add(this.btCPCancel);
            this.MaximizeBox = false;
            this.Name = "LoadingForm";
            this.Text = "Копирование";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btCPCancel;
        private System.Windows.Forms.Button btCPApply;
        private System.Windows.Forms.TextBox tbFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbTo;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}