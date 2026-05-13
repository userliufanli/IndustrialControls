namespace IndustrialControls.Demo.Pages
{
    partial class CommunicationTestForm
    {
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>

        #region Windows 窗体设计器生成的代码

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommunicationTestForm));
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.grpClients = new System.Windows.Forms.GroupBox();
            this.lstClients = new System.Windows.Forms.ListBox();
            this.btnRefreshClients = new System.Windows.Forms.Button();
            this.commControl = new IndustrialControls.Controls.Communication.CommunicationControl();
            this.pnlSend = new System.Windows.Forms.Panel();
            this.btnSendToClient = new System.Windows.Forms.Button();
            this.btnSendToAll = new System.Windows.Forms.Button();
            this.btnSendHex = new System.Windows.Forms.Button();
            this.btnSendString = new System.Windows.Forms.Button();
            this.cboTargetClient = new System.Windows.Forms.ComboBox();
            this.txtSendData = new System.Windows.Forms.TextBox();
            this.lblSendData = new System.Windows.Forms.Label();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.grpStats = new System.Windows.Forms.GroupBox();
            this.btnResetStats = new System.Windows.Forms.Button();
            this.lblClientCount = new System.Windows.Forms.Label();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.lblClientCountTitle = new System.Windows.Forms.Label();
            this.lblErrorCount = new System.Windows.Forms.Label();
            this.lblErrorTitle = new System.Windows.Forms.Label();
            this.lblBytesReceived = new System.Windows.Forms.Label();
            this.lblRecvTitle = new System.Windows.Forms.Label();
            this.lblBytesSent = new System.Windows.Forms.Label();
            this.lblSentTitle = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.lblStateTitle = new System.Windows.Forms.Label();
            this.splitReceive = new System.Windows.Forms.SplitContainer();
            this.splitReceiveTop = new System.Windows.Forms.SplitContainer();
            this.grpStringReceived = new System.Windows.Forms.GroupBox();
            this.txtStringReceived = new System.Windows.Forms.TextBox();
            this.grpFrameReceived = new System.Windows.Forms.GroupBox();
            this.txtFrameReceived = new System.Windows.Forms.TextBox();
            this.grpHexReceived = new System.Windows.Forms.GroupBox();
            this.txtHexReceived = new System.Windows.Forms.TextBox();
            this.pnlLeft.SuspendLayout();
            this.grpClients.SuspendLayout();
            this.pnlSend.SuspendLayout();
            this.pnlStatus.SuspendLayout();
            this.grpStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitReceive)).BeginInit();
            this.splitReceive.Panel1.SuspendLayout();
            this.splitReceive.Panel2.SuspendLayout();
            this.splitReceive.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitReceiveTop)).BeginInit();
            this.splitReceiveTop.Panel1.SuspendLayout();
            this.splitReceiveTop.Panel2.SuspendLayout();
            this.splitReceiveTop.SuspendLayout();
            this.grpStringReceived.SuspendLayout();
            this.grpFrameReceived.SuspendLayout();
            this.grpHexReceived.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.grpClients);
            this.pnlLeft.Controls.Add(this.commControl);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(6);
            this.pnlLeft.Size = new System.Drawing.Size(245, 723);
            this.pnlLeft.TabIndex = 0;
            // 
            // grpClients
            // 
            this.grpClients.Controls.Add(this.lstClients);
            this.grpClients.Controls.Add(this.btnRefreshClients);
            this.grpClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpClients.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grpClients.Location = new System.Drawing.Point(6, 232);
            this.grpClients.Name = "grpClients";
            this.grpClients.Padding = new System.Windows.Forms.Padding(6);
            this.grpClients.Size = new System.Drawing.Size(233, 485);
            this.grpClients.TabIndex = 1;
            this.grpClients.TabStop = false;
            this.grpClients.Text = "已连接客户端:";
            // 
            // lstClients
            // 
            this.lstClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstClients.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lstClients.FormattingEnabled = true;
            this.lstClients.ItemHeight = 15;
            this.lstClients.Location = new System.Drawing.Point(6, 22);
            this.lstClients.Name = "lstClients";
            this.lstClients.Size = new System.Drawing.Size(221, 427);
            this.lstClients.TabIndex = 0;
            // 
            // btnRefreshClients
            // 
            this.btnRefreshClients.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.btnRefreshClients.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnRefreshClients.FlatAppearance.BorderSize = 0;
            this.btnRefreshClients.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshClients.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRefreshClients.ForeColor = System.Drawing.Color.White;
            this.btnRefreshClients.Location = new System.Drawing.Point(6, 449);
            this.btnRefreshClients.Name = "btnRefreshClients";
            this.btnRefreshClients.Size = new System.Drawing.Size(221, 30);
            this.btnRefreshClients.TabIndex = 1;
            this.btnRefreshClients.Text = "刷新列表";
            this.btnRefreshClients.UseVisualStyleBackColor = false;
            this.btnRefreshClients.Click += new System.EventHandler(this.BtnRefreshClients_Click);
            // 
            // commControl
            // 
            this.commControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.commControl.DataEncoding = ((System.Text.Encoding)(resources.GetObject("commControl.DataEncoding")));
            this.commControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.commControl.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.commControl.FrameDelimiter = new byte[] {
        ((byte)(13)),
        ((byte)(10))};
            this.commControl.Location = new System.Drawing.Point(6, 6);
            this.commControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.commControl.MaximumSize = new System.Drawing.Size(233, 226);
            this.commControl.MinimumSize = new System.Drawing.Size(233, 226);
            this.commControl.Name = "commControl";
            this.commControl.SerialPortName = "COM3";
            this.commControl.Size = new System.Drawing.Size(233, 226);
            this.commControl.TabIndex = 0;
            // 
            // pnlSend
            // 
            this.pnlSend.Controls.Add(this.btnSendToClient);
            this.pnlSend.Controls.Add(this.btnSendToAll);
            this.pnlSend.Controls.Add(this.btnSendHex);
            this.pnlSend.Controls.Add(this.btnSendString);
            this.pnlSend.Controls.Add(this.cboTargetClient);
            this.pnlSend.Controls.Add(this.txtSendData);
            this.pnlSend.Controls.Add(this.lblSendData);
            this.pnlSend.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSend.Location = new System.Drawing.Point(245, 0);
            this.pnlSend.Name = "pnlSend";
            this.pnlSend.Padding = new System.Windows.Forms.Padding(8);
            this.pnlSend.Size = new System.Drawing.Size(640, 80);
            this.pnlSend.TabIndex = 1;
            // 
            // btnSendToClient
            // 
            this.btnSendToClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnSendToClient.Enabled = false;
            this.btnSendToClient.FlatAppearance.BorderSize = 0;
            this.btnSendToClient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendToClient.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSendToClient.ForeColor = System.Drawing.Color.White;
            this.btnSendToClient.Location = new System.Drawing.Point(548, 42);
            this.btnSendToClient.Name = "btnSendToClient";
            this.btnSendToClient.Size = new System.Drawing.Size(80, 28);
            this.btnSendToClient.TabIndex = 6;
            this.btnSendToClient.Text = "定向发送";
            this.btnSendToClient.UseVisualStyleBackColor = false;
            this.btnSendToClient.Click += new System.EventHandler(this.BtnSendToClient_Click);
            // 
            // btnSendToAll
            // 
            this.btnSendToAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.btnSendToAll.Enabled = false;
            this.btnSendToAll.FlatAppearance.BorderSize = 0;
            this.btnSendToAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendToAll.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSendToAll.ForeColor = System.Drawing.Color.White;
            this.btnSendToAll.Location = new System.Drawing.Point(462, 42);
            this.btnSendToAll.Name = "btnSendToAll";
            this.btnSendToAll.Size = new System.Drawing.Size(80, 28);
            this.btnSendToAll.TabIndex = 5;
            this.btnSendToAll.Text = "广播全部";
            this.btnSendToAll.UseVisualStyleBackColor = false;
            this.btnSendToAll.Click += new System.EventHandler(this.BtnSendToAll_Click);
            // 
            // btnSendHex
            // 
            this.btnSendHex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnSendHex.Enabled = false;
            this.btnSendHex.FlatAppearance.BorderSize = 0;
            this.btnSendHex.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendHex.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSendHex.ForeColor = System.Drawing.Color.White;
            this.btnSendHex.Location = new System.Drawing.Point(170, 42);
            this.btnSendHex.Name = "btnSendHex";
            this.btnSendHex.Size = new System.Drawing.Size(80, 28);
            this.btnSendHex.TabIndex = 4;
            this.btnSendHex.Text = "发送HEX";
            this.btnSendHex.UseVisualStyleBackColor = false;
            this.btnSendHex.Click += new System.EventHandler(this.BtnSendHex_Click);
            // 
            // btnSendString
            // 
            this.btnSendString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnSendString.Enabled = false;
            this.btnSendString.FlatAppearance.BorderSize = 0;
            this.btnSendString.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendString.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSendString.ForeColor = System.Drawing.Color.White;
            this.btnSendString.Location = new System.Drawing.Point(80, 42);
            this.btnSendString.Name = "btnSendString";
            this.btnSendString.Size = new System.Drawing.Size(84, 28);
            this.btnSendString.TabIndex = 3;
            this.btnSendString.Text = "发送字符串";
            this.btnSendString.UseVisualStyleBackColor = false;
            this.btnSendString.Click += new System.EventHandler(this.BtnSendString_Click);
            // 
            // cboTargetClient
            // 
            this.cboTargetClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTargetClient.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboTargetClient.FormattingEnabled = true;
            this.cboTargetClient.Location = new System.Drawing.Point(296, 46);
            this.cboTargetClient.Name = "cboTargetClient";
            this.cboTargetClient.Size = new System.Drawing.Size(160, 23);
            this.cboTargetClient.TabIndex = 2;
            // 
            // txtSendData
            // 
            this.txtSendData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSendData.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtSendData.Location = new System.Drawing.Point(80, 11);
            this.txtSendData.Name = "txtSendData";
            this.txtSendData.Size = new System.Drawing.Size(548, 22);
            this.txtSendData.TabIndex = 1;
            // 
            // lblSendData
            // 
            this.lblSendData.AutoSize = true;
            this.lblSendData.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSendData.Location = new System.Drawing.Point(11, 14);
            this.lblSendData.Name = "lblSendData";
            this.lblSendData.Size = new System.Drawing.Size(72, 15);
            this.lblSendData.TabIndex = 0;
            this.lblSendData.Text = "发送数据：";
            // 
            // pnlStatus
            // 
            this.pnlStatus.Controls.Add(this.grpStats);
            this.pnlStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStatus.Location = new System.Drawing.Point(245, 653);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Padding = new System.Windows.Forms.Padding(4);
            this.pnlStatus.Size = new System.Drawing.Size(640, 70);
            this.pnlStatus.TabIndex = 3;
            // 
            // grpStats
            // 
            this.grpStats.Controls.Add(this.btnResetStats);
            this.grpStats.Controls.Add(this.lblClientCount);
            this.grpStats.Controls.Add(this.btnClearAll);
            this.grpStats.Controls.Add(this.lblClientCountTitle);
            this.grpStats.Controls.Add(this.lblErrorCount);
            this.grpStats.Controls.Add(this.lblErrorTitle);
            this.grpStats.Controls.Add(this.lblBytesReceived);
            this.grpStats.Controls.Add(this.lblRecvTitle);
            this.grpStats.Controls.Add(this.lblBytesSent);
            this.grpStats.Controls.Add(this.lblSentTitle);
            this.grpStats.Controls.Add(this.lblState);
            this.grpStats.Controls.Add(this.lblStateTitle);
            this.grpStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpStats.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grpStats.Location = new System.Drawing.Point(4, 4);
            this.grpStats.Name = "grpStats";
            this.grpStats.Size = new System.Drawing.Size(632, 62);
            this.grpStats.TabIndex = 0;
            this.grpStats.TabStop = false;
            this.grpStats.Text = "通讯统计";
            // 
            // btnResetStats
            // 
            this.btnResetStats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.btnResetStats.FlatAppearance.BorderSize = 0;
            this.btnResetStats.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetStats.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnResetStats.ForeColor = System.Drawing.Color.White;
            this.btnResetStats.Location = new System.Drawing.Point(544, 24);
            this.btnResetStats.Name = "btnResetStats";
            this.btnResetStats.Size = new System.Drawing.Size(72, 28);
            this.btnResetStats.TabIndex = 10;
            this.btnResetStats.Text = "重置统计";
            this.btnResetStats.UseVisualStyleBackColor = false;
            this.btnResetStats.Click += new System.EventHandler(this.BtnResetStats_Click);
            // 
            // lblClientCount
            // 
            this.lblClientCount.AutoSize = true;
            this.lblClientCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblClientCount.Location = new System.Drawing.Point(452, 30);
            this.lblClientCount.Name = "lblClientCount";
            this.lblClientCount.Size = new System.Drawing.Size(13, 15);
            this.lblClientCount.TabIndex = 9;
            this.lblClientCount.Text = "0";
            // 
            // btnClearAll
            // 
            this.btnClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.btnClearAll.FlatAppearance.BorderSize = 0;
            this.btnClearAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearAll.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClearAll.ForeColor = System.Drawing.Color.White;
            this.btnClearAll.Location = new System.Drawing.Point(468, 24);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(70, 28);
            this.btnClearAll.TabIndex = 11;
            this.btnClearAll.Text = "清空日志";
            this.btnClearAll.UseVisualStyleBackColor = false;
            this.btnClearAll.Click += new System.EventHandler(this.BtnClearAll_Click);
            // 
            // lblClientCountTitle
            // 
            this.lblClientCountTitle.AutoSize = true;
            this.lblClientCountTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblClientCountTitle.Location = new System.Drawing.Point(402, 30);
            this.lblClientCountTitle.Name = "lblClientCountTitle";
            this.lblClientCountTitle.Size = new System.Drawing.Size(49, 15);
            this.lblClientCountTitle.TabIndex = 8;
            this.lblClientCountTitle.Text = "客户端:";
            // 
            // lblErrorCount
            // 
            this.lblErrorCount.AutoSize = true;
            this.lblErrorCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblErrorCount.Location = new System.Drawing.Point(383, 30);
            this.lblErrorCount.Name = "lblErrorCount";
            this.lblErrorCount.Size = new System.Drawing.Size(13, 15);
            this.lblErrorCount.TabIndex = 7;
            this.lblErrorCount.Text = "0";
            // 
            // lblErrorTitle
            // 
            this.lblErrorTitle.AutoSize = true;
            this.lblErrorTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblErrorTitle.Location = new System.Drawing.Point(345, 30);
            this.lblErrorTitle.Name = "lblErrorTitle";
            this.lblErrorTitle.Size = new System.Drawing.Size(36, 15);
            this.lblErrorTitle.TabIndex = 6;
            this.lblErrorTitle.Text = "错误:";
            // 
            // lblBytesReceived
            // 
            this.lblBytesReceived.AutoSize = true;
            this.lblBytesReceived.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBytesReceived.Location = new System.Drawing.Point(264, 30);
            this.lblBytesReceived.Name = "lblBytesReceived";
            this.lblBytesReceived.Size = new System.Drawing.Size(75, 15);
            this.lblBytesReceived.TabIndex = 5;
            this.lblBytesReceived.Text = "0 字节 / 0 帧";
            // 
            // lblRecvTitle
            // 
            this.lblRecvTitle.AutoSize = true;
            this.lblRecvTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRecvTitle.Location = new System.Drawing.Point(226, 30);
            this.lblRecvTitle.Name = "lblRecvTitle";
            this.lblRecvTitle.Size = new System.Drawing.Size(36, 15);
            this.lblRecvTitle.TabIndex = 4;
            this.lblRecvTitle.Text = "接收:";
            // 
            // lblBytesSent
            // 
            this.lblBytesSent.AutoSize = true;
            this.lblBytesSent.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBytesSent.Location = new System.Drawing.Point(145, 30);
            this.lblBytesSent.Name = "lblBytesSent";
            this.lblBytesSent.Size = new System.Drawing.Size(75, 15);
            this.lblBytesSent.TabIndex = 3;
            this.lblBytesSent.Text = "0 字节 / 0 帧";
            // 
            // lblSentTitle
            // 
            this.lblSentTitle.AutoSize = true;
            this.lblSentTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSentTitle.Location = new System.Drawing.Point(107, 30);
            this.lblSentTitle.Name = "lblSentTitle";
            this.lblSentTitle.Size = new System.Drawing.Size(36, 15);
            this.lblSentTitle.TabIndex = 2;
            this.lblSentTitle.Text = "发送:";
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblState.ForeColor = System.Drawing.Color.Red;
            this.lblState.Location = new System.Drawing.Point(55, 30);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(46, 15);
            this.lblState.TabIndex = 1;
            this.lblState.Text = "未连接";
            // 
            // lblStateTitle
            // 
            this.lblStateTitle.AutoSize = true;
            this.lblStateTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStateTitle.Location = new System.Drawing.Point(17, 30);
            this.lblStateTitle.Name = "lblStateTitle";
            this.lblStateTitle.Size = new System.Drawing.Size(36, 15);
            this.lblStateTitle.TabIndex = 0;
            this.lblStateTitle.Text = "状态:";
            // 
            // splitReceive
            // 
            this.splitReceive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitReceive.Location = new System.Drawing.Point(245, 80);
            this.splitReceive.Name = "splitReceive";
            this.splitReceive.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitReceive.Panel1
            // 
            this.splitReceive.Panel1.Controls.Add(this.splitReceiveTop);
            // 
            // splitReceive.Panel2
            // 
            this.splitReceive.Panel2.Controls.Add(this.grpHexReceived);
            this.splitReceive.Size = new System.Drawing.Size(640, 573);
            this.splitReceive.SplitterDistance = 449;
            this.splitReceive.TabIndex = 2;
            // 
            // splitReceiveTop
            // 
            this.splitReceiveTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitReceiveTop.Location = new System.Drawing.Point(0, 0);
            this.splitReceiveTop.Name = "splitReceiveTop";
            // 
            // splitReceiveTop.Panel1
            // 
            this.splitReceiveTop.Panel1.Controls.Add(this.grpStringReceived);
            // 
            // splitReceiveTop.Panel2
            // 
            this.splitReceiveTop.Panel2.Controls.Add(this.grpFrameReceived);
            this.splitReceiveTop.Size = new System.Drawing.Size(640, 449);
            this.splitReceiveTop.SplitterDistance = 160;
            this.splitReceiveTop.TabIndex = 0;
            // 
            // grpStringReceived
            // 
            this.grpStringReceived.Controls.Add(this.txtStringReceived);
            this.grpStringReceived.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpStringReceived.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grpStringReceived.Location = new System.Drawing.Point(0, 0);
            this.grpStringReceived.Name = "grpStringReceived";
            this.grpStringReceived.Padding = new System.Windows.Forms.Padding(4);
            this.grpStringReceived.Size = new System.Drawing.Size(160, 449);
            this.grpStringReceived.TabIndex = 0;
            this.grpStringReceived.TabStop = false;
            this.grpStringReceived.Text = "字符串接收";
            // 
            // txtStringReceived
            // 
            this.txtStringReceived.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.txtStringReceived.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStringReceived.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtStringReceived.Location = new System.Drawing.Point(4, 20);
            this.txtStringReceived.Multiline = true;
            this.txtStringReceived.Name = "txtStringReceived";
            this.txtStringReceived.ReadOnly = true;
            this.txtStringReceived.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStringReceived.Size = new System.Drawing.Size(152, 425);
            this.txtStringReceived.TabIndex = 0;
            // 
            // grpFrameReceived
            // 
            this.grpFrameReceived.Controls.Add(this.txtFrameReceived);
            this.grpFrameReceived.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpFrameReceived.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grpFrameReceived.Location = new System.Drawing.Point(0, 0);
            this.grpFrameReceived.Name = "grpFrameReceived";
            this.grpFrameReceived.Padding = new System.Windows.Forms.Padding(4);
            this.grpFrameReceived.Size = new System.Drawing.Size(476, 449);
            this.grpFrameReceived.TabIndex = 0;
            this.grpFrameReceived.TabStop = false;
            this.grpFrameReceived.Text = "帧数据接收";
            // 
            // txtFrameReceived
            // 
            this.txtFrameReceived.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.txtFrameReceived.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFrameReceived.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtFrameReceived.Location = new System.Drawing.Point(4, 20);
            this.txtFrameReceived.Multiline = true;
            this.txtFrameReceived.Name = "txtFrameReceived";
            this.txtFrameReceived.ReadOnly = true;
            this.txtFrameReceived.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFrameReceived.Size = new System.Drawing.Size(468, 425);
            this.txtFrameReceived.TabIndex = 0;
            // 
            // grpHexReceived
            // 
            this.grpHexReceived.Controls.Add(this.txtHexReceived);
            this.grpHexReceived.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpHexReceived.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grpHexReceived.Location = new System.Drawing.Point(0, 0);
            this.grpHexReceived.Name = "grpHexReceived";
            this.grpHexReceived.Padding = new System.Windows.Forms.Padding(4);
            this.grpHexReceived.Size = new System.Drawing.Size(640, 120);
            this.grpHexReceived.TabIndex = 0;
            this.grpHexReceived.TabStop = false;
            this.grpHexReceived.Text = "原始数据 (HEX)";
            // 
            // txtHexReceived
            // 
            this.txtHexReceived.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.txtHexReceived.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHexReceived.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtHexReceived.Location = new System.Drawing.Point(4, 20);
            this.txtHexReceived.Multiline = true;
            this.txtHexReceived.Name = "txtHexReceived";
            this.txtHexReceived.ReadOnly = true;
            this.txtHexReceived.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHexReceived.Size = new System.Drawing.Size(632, 96);
            this.txtHexReceived.TabIndex = 0;
            // 
            // CommunicationTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.splitReceive);
            this.Controls.Add(this.pnlStatus);
            this.Controls.Add(this.pnlSend);
            this.Controls.Add(this.pnlLeft);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "CommunicationTestForm";
            this.Size = new System.Drawing.Size(885, 723);
            this.pnlLeft.ResumeLayout(false);
            this.grpClients.ResumeLayout(false);
            this.pnlSend.ResumeLayout(false);
            this.pnlSend.PerformLayout();
            this.pnlStatus.ResumeLayout(false);
            this.grpStats.ResumeLayout(false);
            this.grpStats.PerformLayout();
            this.splitReceive.Panel1.ResumeLayout(false);
            this.splitReceive.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitReceive)).EndInit();
            this.splitReceive.ResumeLayout(false);
            this.splitReceiveTop.Panel1.ResumeLayout(false);
            this.splitReceiveTop.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitReceiveTop)).EndInit();
            this.splitReceiveTop.ResumeLayout(false);
            this.grpStringReceived.ResumeLayout(false);
            this.grpStringReceived.PerformLayout();
            this.grpFrameReceived.ResumeLayout(false);
            this.grpFrameReceived.PerformLayout();
            this.grpHexReceived.ResumeLayout(false);
            this.grpHexReceived.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.GroupBox grpClients;
        private System.Windows.Forms.ListBox lstClients;
        private System.Windows.Forms.Button btnRefreshClients;
        private IndustrialControls.Controls.Communication.CommunicationControl commControl;
        private System.Windows.Forms.Panel pnlSend;
        private System.Windows.Forms.Button btnSendToClient;
        private System.Windows.Forms.Button btnSendToAll;
        private System.Windows.Forms.Button btnSendHex;
        private System.Windows.Forms.Button btnSendString;
        private System.Windows.Forms.ComboBox cboTargetClient;
        private System.Windows.Forms.TextBox txtSendData;
        private System.Windows.Forms.Label lblSendData;
        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.GroupBox grpStats;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.Button btnResetStats;
        private System.Windows.Forms.Label lblClientCount;
        private System.Windows.Forms.Label lblClientCountTitle;
        private System.Windows.Forms.Label lblErrorCount;
        private System.Windows.Forms.Label lblErrorTitle;
        private System.Windows.Forms.Label lblBytesReceived;
        private System.Windows.Forms.Label lblRecvTitle;
        private System.Windows.Forms.Label lblBytesSent;
        private System.Windows.Forms.Label lblSentTitle;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Label lblStateTitle;
        private System.Windows.Forms.SplitContainer splitReceive;
        private System.Windows.Forms.SplitContainer splitReceiveTop;
        private System.Windows.Forms.GroupBox grpStringReceived;
        private System.Windows.Forms.TextBox txtStringReceived;
        private System.Windows.Forms.GroupBox grpFrameReceived;
        private System.Windows.Forms.TextBox txtFrameReceived;
        private System.Windows.Forms.GroupBox grpHexReceived;
        private System.Windows.Forms.TextBox txtHexReceived;
    }
}
