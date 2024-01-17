namespace TCPclient
{
    partial class ICEcast
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
            this.components = new System.ComponentModel.Container();
            this.btICEcastApply = new System.Windows.Forms.Button();
            this.btICEcastCancel = new System.Windows.Forms.Button();
            this.paStat1 = new System.Windows.Forms.Panel();
            this.cbURL1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbStat1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbURL2 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbStat2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbURL3 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbStat3 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbURL4 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tbStat4 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.paStat1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btICEcastApply
            // 
            this.btICEcastApply.Location = new System.Drawing.Point(588, 304);
            this.btICEcastApply.Name = "btICEcastApply";
            this.btICEcastApply.Size = new System.Drawing.Size(75, 22);
            this.btICEcastApply.TabIndex = 58;
            this.btICEcastApply.Text = "Применить";
            this.btICEcastApply.UseVisualStyleBackColor = true;
            this.btICEcastApply.Click += new System.EventHandler(this.btICEcastApply_Click);
            // 
            // btICEcastCancel
            // 
            this.btICEcastCancel.Location = new System.Drawing.Point(507, 304);
            this.btICEcastCancel.Name = "btICEcastCancel";
            this.btICEcastCancel.Size = new System.Drawing.Size(75, 22);
            this.btICEcastCancel.TabIndex = 57;
            this.btICEcastCancel.Text = "Отмена";
            this.btICEcastCancel.UseVisualStyleBackColor = true;
            this.btICEcastCancel.Click += new System.EventHandler(this.btICEcastCancel_Click);
            // 
            // paStat1
            // 
            this.paStat1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paStat1.Controls.Add(this.cbURL1);
            this.paStat1.Controls.Add(this.label3);
            this.paStat1.Controls.Add(this.label2);
            this.paStat1.Controls.Add(this.tbStat1);
            this.paStat1.Controls.Add(this.label1);
            this.paStat1.Location = new System.Drawing.Point(4, 7);
            this.paStat1.Name = "paStat1";
            this.paStat1.Size = new System.Drawing.Size(592, 47);
            this.paStat1.TabIndex = 59;
            // 
            // cbURL1
            // 
            this.cbURL1.FormattingEnabled = true;
            this.cbURL1.Location = new System.Drawing.Point(213, 20);
            this.cbURL1.Name = "cbURL1";
            this.cbURL1.Size = new System.Drawing.Size(374, 21);
            this.cbURL1.TabIndex = 5;
            this.cbURL1.SelectionChangeCommitted += new System.EventHandler(this.cbURL1_SelChangeCommit);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(225, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "URL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(117, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Имя Станции";
            // 
            // tbStat1
            // 
            this.tbStat1.Location = new System.Drawing.Point(3, 20);
            this.tbStat1.Name = "tbStat1";
            this.tbStat1.Size = new System.Drawing.Size(193, 20);
            this.tbStat1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(4, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Станция 1";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cbURL2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.tbStat2);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Location = new System.Drawing.Point(4, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 47);
            this.panel1.TabIndex = 60;
            // 
            // cbURL2
            // 
            this.cbURL2.FormattingEnabled = true;
            this.cbURL2.Location = new System.Drawing.Point(213, 20);
            this.cbURL2.Name = "cbURL2";
            this.cbURL2.Size = new System.Drawing.Size(374, 21);
            this.cbURL2.TabIndex = 5;
            this.cbURL2.SelectionChangeCommitted += new System.EventHandler(this.cbURL2_SelChangeCommit);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(225, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "URL";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(117, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Имя Станции";
            // 
            // tbStat2
            // 
            this.tbStat2.Location = new System.Drawing.Point(3, 20);
            this.tbStat2.Name = "tbStat2";
            this.tbStat2.Size = new System.Drawing.Size(193, 20);
            this.tbStat2.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(4, 2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "Станция 2";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.cbURL3);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.tbStat3);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Location = new System.Drawing.Point(4, 103);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(592, 47);
            this.panel2.TabIndex = 61;
            // 
            // cbURL3
            // 
            this.cbURL3.FormattingEnabled = true;
            this.cbURL3.Location = new System.Drawing.Point(213, 20);
            this.cbURL3.Name = "cbURL3";
            this.cbURL3.Size = new System.Drawing.Size(374, 21);
            this.cbURL3.TabIndex = 5;
            this.cbURL3.SelectionChangeCommitted += new System.EventHandler(this.cbURL3_SelChangeCommit);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(225, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "URL";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(117, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Имя Станции";
            // 
            // tbStat3
            // 
            this.tbStat3.Location = new System.Drawing.Point(3, 20);
            this.tbStat3.Name = "tbStat3";
            this.tbStat3.Size = new System.Drawing.Size(193, 20);
            this.tbStat3.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(4, 2);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 15);
            this.label9.TabIndex = 0;
            this.label9.Text = "Станция 3";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.cbURL4);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.tbStat4);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Location = new System.Drawing.Point(4, 151);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(592, 47);
            this.panel3.TabIndex = 62;
            // 
            // cbURL4
            // 
            this.cbURL4.FormattingEnabled = true;
            this.cbURL4.Location = new System.Drawing.Point(213, 20);
            this.cbURL4.Name = "cbURL4";
            this.cbURL4.Size = new System.Drawing.Size(374, 21);
            this.cbURL4.TabIndex = 5;
            this.cbURL4.SelectionChangeCommitted += new System.EventHandler(this.cbURL4_SelChangeCommit);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(225, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "URL";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(117, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "Имя Станции";
            // 
            // tbStat4
            // 
            this.tbStat4.Location = new System.Drawing.Point(3, 20);
            this.tbStat4.Name = "tbStat4";
            this.tbStat4.Size = new System.Drawing.Size(193, 20);
            this.tbStat4.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(4, 2);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(76, 15);
            this.label12.TabIndex = 0;
            this.label12.Text = "Станция 4";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ICEcast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 329);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.paStat1);
            this.Controls.Add(this.btICEcastApply);
            this.Controls.Add(this.btICEcastCancel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ICEcast";
            this.Text = "Потоковое радио";
            this.paStat1.ResumeLayout(false);
            this.paStat1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btICEcastApply;
        private System.Windows.Forms.Button btICEcastCancel;
        private System.Windows.Forms.Panel paStat1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbStat1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbURL1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbURL2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbStat2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbURL3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbStat3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cbURL4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbStat4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Timer timer1;
    }
}