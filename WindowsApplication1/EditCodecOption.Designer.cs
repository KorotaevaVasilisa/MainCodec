namespace TCPclient
{
    partial class EditCodecForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditCodecForm));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timerTstConnect = new System.Windows.Forms.Timer(this.components);
            this.timerFindCdc = new System.Windows.Forms.Timer(this.components);
            this.my_serial = new System.IO.Ports.SerialPort(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transferStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tSStatLbBaud = new System.Windows.Forms.ToolStripStatusLabel();
            this.tSStatLbLen = new System.Windows.Forms.ToolStripStatusLabel();
            this.tSStatLbPrty = new System.Windows.Forms.ToolStripStatusLabel();
            this.tSStatLbStop = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tSStatLbPinStat = new System.Windows.Forms.ToolStripStatusLabel();
            this.statStrip = new System.Windows.Forms.StatusStrip();
            this.btOpen = new System.Windows.Forms.Button();
            this.lbBaud = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btLogEnbl = new System.Windows.Forms.Button();
            this.UpDnNport = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.panCOM = new System.Windows.Forms.Panel();
            this.ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.btLoadFile = new System.Windows.Forms.Button();
            this.btCdcOptionRqst = new System.Windows.Forms.Button();
            this.lbRegistrStat = new System.Windows.Forms.Label();
            this.btFindCodecs = new System.Windows.Forms.Button();
            this.pbFindCodecs = new System.Windows.Forms.ProgressBar();
            this.tbCodecs = new System.Windows.Forms.TextBox();
            this.btListCdcs = new System.Windows.Forms.Button();
            this.rButtTCP = new System.Windows.Forms.RadioButton();
            this.rButtCOM = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.btConnect = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbIPcdc = new System.Windows.Forms.TextBox();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbIPcomp = new System.Windows.Forms.TextBox();
            this.panIP = new System.Windows.Forms.Panel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.paNavigation = new System.Windows.Forms.Panel();
            this.updateButton = new System.Windows.Forms.Button();
            this.textRemotePath = new System.Windows.Forms.TextBox();
            this.listViewRemote = new System.Windows.Forms.ListView();
            this.remoteName = new System.Windows.Forms.ColumnHeader();
            this.remoteSize = new System.Windows.Forms.ColumnHeader();
            this.remoteDate = new System.Windows.Forms.ColumnHeader();
            this.remoteAtr = new System.Windows.Forms.ColumnHeader();
            this.listViewLocal = new System.Windows.Forms.ListView();
            this.ColumnName = new System.Windows.Forms.ColumnHeader();
            this.ColumnSize = new System.Windows.Forms.ColumnHeader();
            this.ColumnDate = new System.Windows.Forms.ColumnHeader();
            this.ColumnAtr = new System.Windows.Forms.ColumnHeader();
            this.textLocalPath = new System.Windows.Forms.TextBox();
            this.comboBoxDriveInfo = new System.Windows.Forms.ComboBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.paConsole = new System.Windows.Forms.Panel();
            this.btNavigation = new System.Windows.Forms.Button();
            this.btConsolePause = new System.Windows.Forms.Button();
            this.btPing = new System.Windows.Forms.Button();
            this.btClearCon = new System.Windows.Forms.Button();
            this.btSentCmd = new System.Windows.Forms.Button();
            this.cbTextToSent = new System.Windows.Forms.ComboBox();
            this.ConsoleText = new System.Windows.Forms.TextBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.contextMenuStrip1.SuspendLayout();
            this.statStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDnNport)).BeginInit();
            this.panCOM.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panIP.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.paNavigation.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.paConsole.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // timerTstConnect
            // 
            this.timerTstConnect.Interval = 4000;
            this.timerTstConnect.Tick += new System.EventHandler(this.timerTstConnect_Tick);
            // 
            // timerFindCdc
            // 
            this.timerFindCdc.Interval = 200;
            this.timerFindCdc.Tick += new System.EventHandler(this.timerFindCdc_Tick);
            // 
            // my_serial
            // 
            this.my_serial.BaudRate = 115200;
            this.my_serial.PortName = "COM";
            this.my_serial.ReadTimeout = 5;
            this.my_serial.RtsEnable = true;
            this.my_serial.WriteTimeout = 2000;
            this.my_serial.ErrorReceived += new System.IO.Ports.SerialErrorReceivedEventHandler(this.myserial_ErrRec);
            this.my_serial.PinChanged += new System.IO.Ports.SerialPinChangedEventHandler(this.myserial_PinChngd);
            this.my_serial.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.myserial_DtRec);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.showStripMenuItem, this.editStripMenuItem, this.copyToolStripMenuItem, this.transferStripMenuItem, this.createStripMenuItem, this.deleteStripMenuItem });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(205, 148);
            // 
            // showStripMenuItem
            // 
            this.showStripMenuItem.Name = "showStripMenuItem";
            this.showStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.showStripMenuItem.Size = new System.Drawing.Size(204, 24);
            this.showStripMenuItem.Text = "Смотреть";
            this.showStripMenuItem.Click += new System.EventHandler(this.showStripMenuItem_Click);
            // 
            // editStripMenuItem
            // 
            this.editStripMenuItem.Name = "editStripMenuItem";
            this.editStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.editStripMenuItem.Size = new System.Drawing.Size(204, 24);
            this.editStripMenuItem.Text = "Редактировать";
            this.editStripMenuItem.Click += new System.EventHandler(this.editStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(204, 24);
            this.copyToolStripMenuItem.Text = "Копировать";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // transferStripMenuItem
            // 
            this.transferStripMenuItem.Name = "transferStripMenuItem";
            this.transferStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.transferStripMenuItem.Size = new System.Drawing.Size(204, 24);
            this.transferStripMenuItem.Text = "Перенести";
            this.transferStripMenuItem.Click += new System.EventHandler(this.transferStripMenuItem_Click);
            // 
            // createStripMenuItem
            // 
            this.createStripMenuItem.Name = "createStripMenuItem";
            this.createStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.createStripMenuItem.Size = new System.Drawing.Size(204, 24);
            this.createStripMenuItem.Text = "Создать папку";
            this.createStripMenuItem.Click += new System.EventHandler(this.createStripMenuItem_Click);
            // 
            // deleteStripMenuItem
            // 
            this.deleteStripMenuItem.Name = "deleteStripMenuItem";
            this.deleteStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.deleteStripMenuItem.Size = new System.Drawing.Size(204, 24);
            this.deleteStripMenuItem.Text = "Удалить";
            this.deleteStripMenuItem.Click += new System.EventHandler(this.deleteStripMenuItem_Click);
            // 
            // tSStatLbBaud
            // 
            this.tSStatLbBaud.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tSStatLbBaud.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.tSStatLbBaud.Name = "tSStatLbBaud";
            this.tSStatLbBaud.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tSStatLbBaud.Size = new System.Drawing.Size(102, 25);
            this.tSStatLbBaud.Text = "tSStatLbBaud";
            // 
            // tSStatLbLen
            // 
            this.tSStatLbLen.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tSStatLbLen.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.tSStatLbLen.Name = "tSStatLbLen";
            this.tSStatLbLen.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tSStatLbLen.Size = new System.Drawing.Size(91, 25);
            this.tSStatLbLen.Text = "tSStatLbLen";
            // 
            // tSStatLbPrty
            // 
            this.tSStatLbPrty.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tSStatLbPrty.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.tSStatLbPrty.Name = "tSStatLbPrty";
            this.tSStatLbPrty.Size = new System.Drawing.Size(93, 25);
            this.tSStatLbPrty.Text = "tSStatLbPrty";
            // 
            // tSStatLbStop
            // 
            this.tSStatLbStop.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tSStatLbStop.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.tSStatLbStop.Name = "tSStatLbStop";
            this.tSStatLbStop.Size = new System.Drawing.Size(99, 25);
            this.tSStatLbStop.Text = "tSStatLbStop";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(99, 25);
            this.toolStripStatusLabel1.Text = "Port Status ->";
            // 
            // tSStatLbPinStat
            // 
            this.tSStatLbPinStat.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tSStatLbPinStat.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.tSStatLbPinStat.Name = "tSStatLbPinStat";
            this.tSStatLbPinStat.Size = new System.Drawing.Size(324, 25);
            this.tSStatLbPinStat.Text = "RTShandshake permit CD stat DSR stat CTS stat";
            // 
            // statStrip
            // 
            this.statStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.tSStatLbBaud, this.tSStatLbLen, this.tSStatLbPrty, this.tSStatLbStop, this.toolStripStatusLabel1, this.tSStatLbPinStat });
            this.statStrip.Location = new System.Drawing.Point(0, 925);
            this.statStrip.Name = "statStrip";
            this.statStrip.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statStrip.Size = new System.Drawing.Size(1132, 30);
            this.statStrip.TabIndex = 68;
            this.statStrip.Visible = false;
            // 
            // btOpen
            // 
            this.btOpen.BackColor = System.Drawing.Color.Tomato;
            this.btOpen.Location = new System.Drawing.Point(4, 75);
            this.btOpen.Margin = new System.Windows.Forms.Padding(4);
            this.btOpen.MinimumSize = new System.Drawing.Size(147, 28);
            this.btOpen.Name = "btOpen";
            this.btOpen.Size = new System.Drawing.Size(172, 28);
            this.btOpen.TabIndex = 64;
            this.btOpen.Text = "Открыть/Закрыть";
            this.btOpen.UseVisualStyleBackColor = false;
            this.btOpen.Click += new System.EventHandler(this.btOpen_Click);
            // 
            // lbBaud
            // 
            this.lbBaud.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbBaud.FormattingEnabled = true;
            this.lbBaud.IntegralHeight = false;
            this.lbBaud.ItemHeight = 20;
            this.lbBaud.Items.AddRange(new object[] { "115200", "57600", "38400", "19200", "9600", "4800", "2400", "1200", "600" });
            this.lbBaud.Location = new System.Drawing.Point(239, 7);
            this.lbBaud.Margin = new System.Windows.Forms.Padding(4);
            this.lbBaud.Name = "lbBaud";
            this.lbBaud.ScrollAlwaysVisible = true;
            this.lbBaud.Size = new System.Drawing.Size(99, 24);
            this.lbBaud.TabIndex = 66;
            this.lbBaud.SelectedIndexChanged += new System.EventHandler(this.lbBaud_SelIndChgd);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(9, 14);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 20);
            this.label3.TabIndex = 63;
            this.label3.Text = "Порт";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // btLogEnbl
            // 
            this.btLogEnbl.Location = new System.Drawing.Point(336, 75);
            this.btLogEnbl.Margin = new System.Windows.Forms.Padding(4);
            this.btLogEnbl.MinimumSize = new System.Drawing.Size(100, 28);
            this.btLogEnbl.Name = "btLogEnbl";
            this.btLogEnbl.Size = new System.Drawing.Size(100, 28);
            this.btLogEnbl.TabIndex = 69;
            this.btLogEnbl.Text = "Протокол";
            this.btLogEnbl.Click += new System.EventHandler(this.btLogEnbl_Click);
            // 
            // UpDnNport
            // 
            this.UpDnNport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UpDnNport.Location = new System.Drawing.Point(73, 9);
            this.UpDnNport.Margin = new System.Windows.Forms.Padding(4);
            this.UpDnNport.Maximum = new decimal(new int[] { 40, 0, 0, 0 });
            this.UpDnNport.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.UpDnNport.Name = "UpDnNport";
            this.UpDnNport.Size = new System.Drawing.Size(53, 23);
            this.UpDnNport.TabIndex = 62;
            this.UpDnNport.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.UpDnNport.Value = new decimal(new int[] { 1, 0, 0, 0 });
            this.UpDnNport.ValueChanged += new System.EventHandler(this.Nport_chngd);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(144, 11);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 20);
            this.label2.TabIndex = 65;
            this.label2.Text = "Скорость";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // panCOM
            // 
            this.panCOM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panCOM.Controls.Add(this.label2);
            this.panCOM.Controls.Add(this.UpDnNport);
            this.panCOM.Controls.Add(this.btLogEnbl);
            this.panCOM.Controls.Add(this.label3);
            this.panCOM.Controls.Add(this.lbBaud);
            this.panCOM.Controls.Add(this.btOpen);
            this.panCOM.Location = new System.Drawing.Point(485, 42);
            this.panCOM.Margin = new System.Windows.Forms.Padding(4);
            this.panCOM.Name = "panCOM";
            this.panCOM.Size = new System.Drawing.Size(447, 110);
            this.panCOM.TabIndex = 73;
            this.panCOM.Visible = false;
            // 
            // ToolStripMenuItem1
            // 
            this.ToolStripMenuItem1.Name = "ToolStripMenuItem1";
            this.ToolStripMenuItem1.Size = new System.Drawing.Size(129, 24);
            this.ToolStripMenuItem1.Text = "Настройка сети";
            this.ToolStripMenuItem1.Click += new System.EventHandler(this.MenuNet_Click);
            // 
            // ToolStripMenuItem2
            // 
            this.ToolStripMenuItem2.Name = "ToolStripMenuItem2";
            this.ToolStripMenuItem2.Size = new System.Drawing.Size(154, 24);
            this.ToolStripMenuItem2.Text = "Настройка FM_прм";
            this.ToolStripMenuItem2.Click += new System.EventHandler(this.MenuFM_Click);
            // 
            // ToolStripMenuItem3
            // 
            this.ToolStripMenuItem3.Name = "ToolStripMenuItem3";
            this.ToolStripMenuItem3.Size = new System.Drawing.Size(144, 24);
            this.ToolStripMenuItem3.Text = "Потоковое радио";
            this.ToolStripMenuItem3.Click += new System.EventHandler(this.MenuStreamRadio_Click);
            // 
            // ToolStripMenuItem4
            // 
            this.ToolStripMenuItem4.Name = "ToolStripMenuItem4";
            this.ToolStripMenuItem4.Size = new System.Drawing.Size(145, 24);
            this.ToolStripMenuItem4.Text = "Выходы декодера";
            this.ToolStripMenuItem4.Click += new System.EventHandler(this.MenuProgram_Click);
            // 
            // ToolStripMenuItem5
            // 
            this.ToolStripMenuItem5.Name = "ToolStripMenuItem5";
            this.ToolStripMenuItem5.Size = new System.Drawing.Size(80, 24);
            this.ToolStripMenuItem5.Text = "Консоль";
            this.ToolStripMenuItem5.Click += new System.EventHandler(this.MenuConsole_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(96, 24);
            this.toolStripMenuItem6.Text = "Статистика";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.MenuStat_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(154, 24);
            this.toolStripMenuItem7.Text = "Пересылка файлов";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.MenuSentfile_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.ToolStripMenuItem1, this.ToolStripMenuItem2, this.ToolStripMenuItem3, this.ToolStripMenuItem4, this.ToolStripMenuItem5, this.toolStripMenuItem6, this.toolStripMenuItem7 });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1132, 28);
            this.menuStrip1.TabIndex = 55;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // btLoadFile
            // 
            this.btLoadFile.Location = new System.Drawing.Point(264, 39);
            this.btLoadFile.Margin = new System.Windows.Forms.Padding(4);
            this.btLoadFile.Name = "btLoadFile";
            this.btLoadFile.Size = new System.Drawing.Size(163, 28);
            this.btLoadFile.TabIndex = 25;
            this.btLoadFile.Text = "Выбрать Файл";
            this.btLoadFile.UseVisualStyleBackColor = true;
            this.btLoadFile.Visible = false;
            // 
            // btCdcOptionRqst
            // 
            this.btCdcOptionRqst.Location = new System.Drawing.Point(955, 102);
            this.btCdcOptionRqst.Margin = new System.Windows.Forms.Padding(4);
            this.btCdcOptionRqst.Name = "btCdcOptionRqst";
            this.btCdcOptionRqst.Size = new System.Drawing.Size(115, 47);
            this.btCdcOptionRqst.TabIndex = 31;
            this.btCdcOptionRqst.Text = "Запрос настроек";
            this.btCdcOptionRqst.UseVisualStyleBackColor = true;
            this.btCdcOptionRqst.Click += new System.EventHandler(this.btCdcOptionRqst_Click);
            // 
            // lbRegistrStat
            // 
            this.lbRegistrStat.AutoSize = true;
            this.lbRegistrStat.BackColor = System.Drawing.SystemColors.Control;
            this.lbRegistrStat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbRegistrStat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbRegistrStat.Location = new System.Drawing.Point(64, 43);
            this.lbRegistrStat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbRegistrStat.Name = "lbRegistrStat";
            this.lbRegistrStat.Size = new System.Drawing.Size(133, 22);
            this.lbRegistrStat.TabIndex = 57;
            this.lbRegistrStat.Text = "Не подключен";
            // 
            // btFindCodecs
            // 
            this.btFindCodecs.Location = new System.Drawing.Point(7, 87);
            this.btFindCodecs.Margin = new System.Windows.Forms.Padding(4);
            this.btFindCodecs.Name = "btFindCodecs";
            this.btFindCodecs.Size = new System.Drawing.Size(163, 28);
            this.btFindCodecs.TabIndex = 58;
            this.btFindCodecs.Text = "Найти кодеки";
            this.btFindCodecs.UseVisualStyleBackColor = true;
            this.btFindCodecs.Click += new System.EventHandler(this.btFindCodecs_Click);
            // 
            // pbFindCodecs
            // 
            this.pbFindCodecs.ForeColor = System.Drawing.Color.Green;
            this.pbFindCodecs.Location = new System.Drawing.Point(177, 87);
            this.pbFindCodecs.Margin = new System.Windows.Forms.Padding(4);
            this.pbFindCodecs.Maximum = 255;
            this.pbFindCodecs.Minimum = 1;
            this.pbFindCodecs.Name = "pbFindCodecs";
            this.pbFindCodecs.Size = new System.Drawing.Size(291, 28);
            this.pbFindCodecs.Step = 1;
            this.pbFindCodecs.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbFindCodecs.TabIndex = 59;
            this.pbFindCodecs.Value = 1;
            this.pbFindCodecs.Visible = false;
            // 
            // tbCodecs
            // 
            this.tbCodecs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbCodecs.ForeColor = System.Drawing.Color.Green;
            this.tbCodecs.Location = new System.Drawing.Point(7, 150);
            this.tbCodecs.Margin = new System.Windows.Forms.Padding(4);
            this.tbCodecs.Multiline = true;
            this.tbCodecs.Name = "tbCodecs";
            this.tbCodecs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbCodecs.Size = new System.Drawing.Size(275, 114);
            this.tbCodecs.TabIndex = 60;
            this.tbCodecs.Visible = false;
            // 
            // btListCdcs
            // 
            this.btListCdcs.Location = new System.Drawing.Point(7, 121);
            this.btListCdcs.Margin = new System.Windows.Forms.Padding(4);
            this.btListCdcs.Name = "btListCdcs";
            this.btListCdcs.Size = new System.Drawing.Size(196, 28);
            this.btListCdcs.TabIndex = 61;
            this.btListCdcs.Text = "Скрыть/показать список";
            this.btListCdcs.UseVisualStyleBackColor = true;
            this.btListCdcs.Visible = false;
            this.btListCdcs.Click += new System.EventHandler(this.btListCdcs_Click);
            // 
            // rButtTCP
            // 
            this.rButtTCP.AutoSize = true;
            this.rButtTCP.Checked = true;
            this.rButtTCP.Location = new System.Drawing.Point(968, 46);
            this.rButtTCP.Margin = new System.Windows.Forms.Padding(4);
            this.rButtTCP.Name = "rButtTCP";
            this.rButtTCP.Size = new System.Drawing.Size(77, 20);
            this.rButtTCP.TabIndex = 70;
            this.rButtTCP.TabStop = true;
            this.rButtTCP.Text = "Ethernet";
            this.rButtTCP.UseVisualStyleBackColor = true;
            this.rButtTCP.Click += new System.EventHandler(this.rbEther_Click);
            // 
            // rButtCOM
            // 
            this.rButtCOM.AutoSize = true;
            this.rButtCOM.Location = new System.Drawing.Point(968, 73);
            this.rButtCOM.Margin = new System.Windows.Forms.Padding(4);
            this.rButtCOM.Name = "rButtCOM";
            this.rButtCOM.Size = new System.Drawing.Size(93, 20);
            this.rButtCOM.TabIndex = 71;
            this.rButtCOM.Text = "COM-порт";
            this.rButtCOM.UseVisualStyleBackColor = true;
            this.rButtCOM.Click += new System.EventHandler(this.rbCOM_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(12, 43);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 20);
            this.label4.TabIndex = 74;
            this.label4.Text = "Сеть";
            // 
            // btConnect
            // 
            this.btConnect.Location = new System.Drawing.Point(8, 73);
            this.btConnect.Margin = new System.Windows.Forms.Padding(4);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(148, 28);
            this.btConnect.TabIndex = 20;
            this.btConnect.Text = "Подключиться";
            this.btConnect.UseVisualStyleBackColor = true;
            this.btConnect.Click += new System.EventHandler(this.btConnect_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(47, 4);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 16);
            this.label8.TabIndex = 54;
            this.label8.Text = "Порт";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(351, 32);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 16);
            this.label7.TabIndex = 55;
            this.label7.Text = "Кодек";
            // 
            // tbIPcdc
            // 
            this.tbIPcdc.AcceptsTab = true;
            this.tbIPcdc.Location = new System.Drawing.Point(196, 27);
            this.tbIPcdc.Margin = new System.Windows.Forms.Padding(4);
            this.tbIPcdc.Name = "tbIPcdc";
            this.tbIPcdc.Size = new System.Drawing.Size(143, 22);
            this.tbIPcdc.TabIndex = 56;
            this.tbIPcdc.Text = "192.168.1.106";
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(43, 27);
            this.tbPort.Margin = new System.Windows.Forms.Padding(4);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(112, 22);
            this.tbPort.TabIndex = 57;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(204, 5);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 16);
            this.label6.TabIndex = 58;
            this.label6.Text = "IP";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(351, 64);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 59;
            this.label1.Text = "Компьютер";
            // 
            // tbIPcomp
            // 
            this.tbIPcomp.Enabled = false;
            this.tbIPcomp.Location = new System.Drawing.Point(196, 60);
            this.tbIPcomp.Margin = new System.Windows.Forms.Padding(4);
            this.tbIPcomp.Name = "tbIPcomp";
            this.tbIPcomp.Size = new System.Drawing.Size(143, 22);
            this.tbIPcomp.TabIndex = 60;
            // 
            // panIP
            // 
            this.panIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panIP.Controls.Add(this.tbIPcomp);
            this.panIP.Controls.Add(this.label1);
            this.panIP.Controls.Add(this.label6);
            this.panIP.Controls.Add(this.tbPort);
            this.panIP.Controls.Add(this.tbIPcdc);
            this.panIP.Controls.Add(this.label7);
            this.panIP.Controls.Add(this.label8);
            this.panIP.Controls.Add(this.btConnect);
            this.panIP.Location = new System.Drawing.Point(485, 43);
            this.panIP.Margin = new System.Windows.Forms.Padding(4);
            this.panIP.Name = "panIP";
            this.panIP.Size = new System.Drawing.Size(447, 109);
            this.panIP.TabIndex = 54;
            // 
            // tabPage2
            // 
            this.tabPage2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage2.Controls.Add(this.paNavigation);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1132, 532);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Навигатор";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // paNavigation
            // 
            this.paNavigation.Controls.Add(this.updateButton);
            this.paNavigation.Controls.Add(this.textRemotePath);
            this.paNavigation.Controls.Add(this.listViewRemote);
            this.paNavigation.Controls.Add(this.listViewLocal);
            this.paNavigation.Controls.Add(this.textLocalPath);
            this.paNavigation.Controls.Add(this.comboBoxDriveInfo);
            this.paNavigation.Location = new System.Drawing.Point(0, 0);
            this.paNavigation.Name = "paNavigation";
            this.paNavigation.Size = new System.Drawing.Size(1120, 815);
            this.paNavigation.TabIndex = 0;
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(984, 24);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(131, 23);
            this.updateButton.TabIndex = 7;
            this.updateButton.Text = "Обновить";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // textRemotePath
            // 
            this.textRemotePath.Location = new System.Drawing.Point(614, 53);
            this.textRemotePath.Name = "textRemotePath";
            this.textRemotePath.Size = new System.Drawing.Size(596, 22);
            this.textRemotePath.TabIndex = 6;
            this.textRemotePath.Text = "/temas/codec/";
            this.textRemotePath.TextChanged += new System.EventHandler(this.textRemotePath_TextChanged);
            // 
            // listViewRemote
            // 
            this.listViewRemote.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.remoteName, this.remoteSize, this.remoteDate, this.remoteAtr });
            this.listViewRemote.FullRowSelect = true;
            this.listViewRemote.HideSelection = false;
            this.listViewRemote.Location = new System.Drawing.Point(614, 85);
            this.listViewRemote.Name = "listViewRemote";
            this.listViewRemote.Size = new System.Drawing.Size(517, 409);
            this.listViewRemote.TabIndex = 5;
            this.listViewRemote.UseCompatibleStateImageBehavior = false;
            this.listViewRemote.View = System.Windows.Forms.View.Details;
            this.listViewRemote.DoubleClick += new System.EventHandler(this.listViewRemote_DoubleClick);
            // 
            // remoteName
            // 
            this.remoteName.Text = "Имя";
            this.remoteName.Width = 200;
            // 
            // remoteSize
            // 
            this.remoteSize.Text = "Размер";
            this.remoteSize.Width = 100;
            // 
            // remoteDate
            // 
            this.remoteDate.Text = "Дата";
            this.remoteDate.Width = 140;
            // 
            // remoteAtr
            // 
            this.remoteAtr.Text = "Атрибут";
            // 
            // listViewLocal
            // 
            this.listViewLocal.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.ColumnName, this.ColumnSize, this.ColumnDate, this.ColumnAtr });
            this.listViewLocal.ContextMenuStrip = this.contextMenuStrip1;
            this.listViewLocal.FullRowSelect = true;
            this.listViewLocal.HideSelection = false;
            this.listViewLocal.Location = new System.Drawing.Point(11, 85);
            this.listViewLocal.Name = "listViewLocal";
            this.listViewLocal.Size = new System.Drawing.Size(596, 409);
            this.listViewLocal.TabIndex = 5;
            this.listViewLocal.UseCompatibleStateImageBehavior = false;
            this.listViewLocal.View = System.Windows.Forms.View.Details;
            this.listViewLocal.DoubleClick += new System.EventHandler(this.listView_DoubleClick);
            this.listViewLocal.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listViewLocal_MouseUp);
            // 
            // ColumnName
            // 
            this.ColumnName.Text = "Имя";
            this.ColumnName.Width = 180;
            // 
            // ColumnSize
            // 
            this.ColumnSize.Text = "Размер";
            this.ColumnSize.Width = 100;
            // 
            // ColumnDate
            // 
            this.ColumnDate.Text = "Дата";
            this.ColumnDate.Width = 140;
            // 
            // ColumnAtr
            // 
            this.ColumnAtr.Text = "Атрибут";
            // 
            // textLocalPath
            // 
            this.textLocalPath.Location = new System.Drawing.Point(11, 53);
            this.textLocalPath.Name = "textLocalPath";
            this.textLocalPath.Size = new System.Drawing.Size(596, 22);
            this.textLocalPath.TabIndex = 1;
            // 
            // comboBoxDriveInfo
            // 
            this.comboBoxDriveInfo.FormattingEnabled = true;
            this.comboBoxDriveInfo.Location = new System.Drawing.Point(11, 15);
            this.comboBoxDriveInfo.Name = "comboBoxDriveInfo";
            this.comboBoxDriveInfo.Size = new System.Drawing.Size(121, 24);
            this.comboBoxDriveInfo.TabIndex = 0;
            this.comboBoxDriveInfo.SelectedIndexChanged += new System.EventHandler(this.comboBoxDriveInfo_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage1.Controls.Add(this.paConsole);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1132, 532);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Консоль";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // paConsole
            // 
            this.paConsole.Controls.Add(this.btNavigation);
            this.paConsole.Controls.Add(this.btConsolePause);
            this.paConsole.Controls.Add(this.btPing);
            this.paConsole.Controls.Add(this.btClearCon);
            this.paConsole.Controls.Add(this.btSentCmd);
            this.paConsole.Controls.Add(this.cbTextToSent);
            this.paConsole.Controls.Add(this.ConsoleText);
            this.paConsole.Location = new System.Drawing.Point(0, 0);
            this.paConsole.Margin = new System.Windows.Forms.Padding(4);
            this.paConsole.Name = "paConsole";
            this.paConsole.Size = new System.Drawing.Size(1120, 530);
            this.paConsole.TabIndex = 57;
            // 
            // btNavigation
            // 
            this.btNavigation.Location = new System.Drawing.Point(1019, 24);
            this.btNavigation.Name = "btNavigation";
            this.btNavigation.Size = new System.Drawing.Size(0, 0);
            this.btNavigation.TabIndex = 34;
            this.btNavigation.Text = "Навигатор";
            this.btNavigation.UseVisualStyleBackColor = true;
            // 
            // btConsolePause
            // 
            this.btConsolePause.Location = new System.Drawing.Point(1023, 421);
            this.btConsolePause.Margin = new System.Windows.Forms.Padding(4);
            this.btConsolePause.Name = "btConsolePause";
            this.btConsolePause.Size = new System.Drawing.Size(93, 28);
            this.btConsolePause.TabIndex = 33;
            this.btConsolePause.Text = "Пауза";
            this.btConsolePause.UseVisualStyleBackColor = true;
            this.btConsolePause.Click += new System.EventHandler(this.btConsolePause_Click);
            // 
            // btPing
            // 
            this.btPing.Location = new System.Drawing.Point(1023, 379);
            this.btPing.Margin = new System.Windows.Forms.Padding(4);
            this.btPing.Name = "btPing";
            this.btPing.Size = new System.Drawing.Size(69, 28);
            this.btPing.TabIndex = 32;
            this.btPing.Text = "Ping";
            this.btPing.UseVisualStyleBackColor = true;
            this.btPing.Click += new System.EventHandler(this.btPing_Click);
            // 
            // btClearCon
            // 
            this.btClearCon.Location = new System.Drawing.Point(1023, 457);
            this.btClearCon.Margin = new System.Windows.Forms.Padding(4);
            this.btClearCon.Name = "btClearCon";
            this.btClearCon.Size = new System.Drawing.Size(93, 28);
            this.btClearCon.TabIndex = 31;
            this.btClearCon.Text = "Очистить";
            this.btClearCon.UseVisualStyleBackColor = true;
            this.btClearCon.Click += new System.EventHandler(this.btClearCon_Click);
            // 
            // btSentCmd
            // 
            this.btSentCmd.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btSentCmd.Location = new System.Drawing.Point(1024, 495);
            this.btSentCmd.Margin = new System.Windows.Forms.Padding(4);
            this.btSentCmd.Name = "btSentCmd";
            this.btSentCmd.Size = new System.Drawing.Size(93, 28);
            this.btSentCmd.TabIndex = 25;
            this.btSentCmd.Text = "Передать";
            this.btSentCmd.UseVisualStyleBackColor = false;
            this.btSentCmd.Click += new System.EventHandler(this.btSentCmd_Click);
            // 
            // cbTextToSent
            // 
            this.cbTextToSent.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbTextToSent.FormattingEnabled = true;
            this.cbTextToSent.Location = new System.Drawing.Point(4, 495);
            this.cbTextToSent.Margin = new System.Windows.Forms.Padding(4);
            this.cbTextToSent.MaxDropDownItems = 12;
            this.cbTextToSent.Name = "cbTextToSent";
            this.cbTextToSent.Size = new System.Drawing.Size(1008, 26);
            this.cbTextToSent.TabIndex = 24;
            this.cbTextToSent.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Transmit_KeyUp);
            // 
            // ConsoleText
            // 
            this.ConsoleText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ConsoleText.Location = new System.Drawing.Point(4, 4);
            this.ConsoleText.Margin = new System.Windows.Forms.Padding(4);
            this.ConsoleText.Multiline = true;
            this.ConsoleText.Name = "ConsoleText";
            this.ConsoleText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ConsoleText.Size = new System.Drawing.Size(1008, 479);
            this.ConsoleText.TabIndex = 1;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Location = new System.Drawing.Point(0, 160);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1140, 561);
            this.tabControl.TabIndex = 75;
            this.tabControl.Visible = false;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // EditCodecForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1132, 955);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panIP);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.rButtCOM);
            this.Controls.Add(this.rButtTCP);
            this.Controls.Add(this.btListCdcs);
            this.Controls.Add(this.tbCodecs);
            this.Controls.Add(this.pbFindCodecs);
            this.Controls.Add(this.btFindCodecs);
            this.Controls.Add(this.lbRegistrStat);
            this.Controls.Add(this.btCdcOptionRqst);
            this.Controls.Add(this.btLoadFile);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panCOM);
            this.Controls.Add(this.statStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(120, 120);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "EditCodecForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Настройка кодека";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ClosedForm);
            this.Click += new System.EventHandler(this.EditCodecForm_Click);
            this.contextMenuStrip1.ResumeLayout(false);
            this.statStrip.ResumeLayout(false);
            this.statStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDnNport)).EndInit();
            this.panCOM.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panIP.ResumeLayout(false);
            this.panIP.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.paNavigation.ResumeLayout(false);
            this.paNavigation.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.paConsole.ResumeLayout(false);
            this.paConsole.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ToolStripMenuItem transferStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem editStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem showStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

        #endregion

        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btLoadFile;
        private System.Windows.Forms.Button btCdcOptionRqst;
        private System.Windows.Forms.Panel panIP;
        private System.Windows.Forms.TextBox tbIPcomp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.TextBox tbIPcdc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem3;
        private System.Windows.Forms.Label lbRegistrStat;
        private System.Windows.Forms.Timer timerTstConnect;
        private System.Windows.Forms.Button btFindCodecs;
        private System.Windows.Forms.ProgressBar pbFindCodecs;
        private System.Windows.Forms.TextBox tbCodecs;
        private System.Windows.Forms.Button btListCdcs;
        private System.Windows.Forms.Timer timerFindCdc;
        private System.IO.Ports.SerialPort my_serial;
        private System.Windows.Forms.StatusStrip statStrip;
        private System.Windows.Forms.ToolStripStatusLabel tSStatLbBaud;
        private System.Windows.Forms.ToolStripStatusLabel tSStatLbLen;
        private System.Windows.Forms.ToolStripStatusLabel tSStatLbPrty;
        private System.Windows.Forms.ToolStripStatusLabel tSStatLbStop;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tSStatLbPinStat;
        private System.Windows.Forms.RadioButton rButtTCP;
        private System.Windows.Forms.RadioButton rButtCOM;
        private System.Windows.Forms.Panel panCOM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown UpDnNport;
        private System.Windows.Forms.Button btLogEnbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lbBaud;
        private System.Windows.Forms.Button btOpen;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel paConsole;
        private System.Windows.Forms.Button btNavigation;
        private System.Windows.Forms.Button btConsolePause;
        private System.Windows.Forms.Button btPing;
        private System.Windows.Forms.Button btClearCon;
        private System.Windows.Forms.Button btSentCmd;
        private System.Windows.Forms.ComboBox cbTextToSent;
        private System.Windows.Forms.TextBox ConsoleText;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel paNavigation;
        private System.Windows.Forms.ComboBox comboBoxDriveInfo;
        private System.Windows.Forms.TextBox textLocalPath;
        private System.Windows.Forms.ListView listViewLocal;
        private System.Windows.Forms.ColumnHeader ColumnName;
        private System.Windows.Forms.ColumnHeader ColumnSize;
        private System.Windows.Forms.ColumnHeader ColumnDate;
        private System.Windows.Forms.ColumnHeader ColumnAtr;
        private System.Windows.Forms.ListView listViewRemote;
        private System.Windows.Forms.ColumnHeader remoteName;
        private System.Windows.Forms.ColumnHeader remoteSize;
        private System.Windows.Forms.ColumnHeader remoteDate;
        private System.Windows.Forms.TextBox textRemotePath;
        private System.Windows.Forms.ColumnHeader remoteAtr;
        private System.Windows.Forms.Button updateButton;
    }
}

