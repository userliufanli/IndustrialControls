using System.Windows.Forms;

namespace IndustrialControls.Controls.Communication
{
    partial class CommunicationControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // 取消所有正在进行的异步操作
                _asyncOperationCts?.Cancel();
                _asyncOperationCts?.Dispose();
                _asyncOperationCts = null;

                _reconnectDelayCts?.Cancel();
                _reconnectDelayCts?.Dispose();
                _reconnectDelayCts = null;

                // 取消宿主窗体 Load/Shown 上的早期自动连接订阅
                try { DetachHostFormEarlyAutoConnect(); } catch { }

                // 取消事件订阅（在 _manager Dispose 之前）
                if (_manager != null)
                {
                    _manager.StateChanged -= _onManagerStateChanged;
                    _manager.DataReceived -= _onManagerDataReceived;
                    _manager.ErrorOccurred -= _onManagerErrorOccurred;
                }

                // 清理控件事件
                cboMode.SelectedIndexChanged -= CboMode_SelectedIndexChanged;
                btnConnect.Click -= BtnConnect_Click;
                btnDisconnect.Click -= BtnDisconnect_Click;
                btnSave.Click -= BtnSave_Click;
                pnlIndicator.Paint -= PnlIndicator_Paint;

                // 共享的 ParameterManager 实例由静态池管理，不在此处 Dispose
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlIndicator = new System.Windows.Forms.Panel();
            this.lblState = new System.Windows.Forms.Label();
            this.cboMode = new System.Windows.Forms.ComboBox();
            this.pnlTcpParams = new System.Windows.Forms.Panel();
            this.txtTcpIp = new System.Windows.Forms.TextBox();
            this.nudTcpPort = new System.Windows.Forms.NumericUpDown();
            this.nudTcpTimeout = new System.Windows.Forms.NumericUpDown();
            this.lblMs = new System.Windows.Forms.Label();
            this.pnlTcpServerParams = new System.Windows.Forms.Panel();
            this.txtServerIp = new System.Windows.Forms.TextBox();
            this.nudServerPort = new System.Windows.Forms.NumericUpDown();
            this.pnlSerialParams = new System.Windows.Forms.Panel();
            this.cboSerialPort = new System.Windows.Forms.ComboBox();
            this.cboBaudRate = new System.Windows.Forms.ComboBox();
            this.cboParity = new System.Windows.Forms.ComboBox();
            this.cboDataBits = new System.Windows.Forms.ComboBox();
            this.cboStopBits = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblStats = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlTcpParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTcpPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTcpTimeout)).BeginInit();
            this.pnlTcpServerParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudServerPort)).BeginInit();
            this.pnlSerialParams.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblTitle.Location = new System.Drawing.Point(10, 4);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(63, 15);
            this.lblTitle.TabIndex = 10;
            this.lblTitle.Text = "通讯控件";
            // 
            // pnlIndicator
            // 
            this.pnlIndicator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.pnlIndicator.Location = new System.Drawing.Point(10, 31);
            this.pnlIndicator.Name = "pnlIndicator";
            this.pnlIndicator.Size = new System.Drawing.Size(8, 8);
            this.pnlIndicator.TabIndex = 0;
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblState.Location = new System.Drawing.Point(22, 28);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(49, 15);
            this.lblState.TabIndex = 1;
            this.lblState.Text = "未连接";
            // 
            // cboMode
            // 
            this.cboMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMode.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cboMode.FormattingEnabled = true;
            this.cboMode.Items.AddRange(new object[] {
            "TCP客户端",
            "TCP服务端",
            "串口"});
            this.cboMode.Location = new System.Drawing.Point(10, 50);
            this.cboMode.Name = "cboMode";
            this.cboMode.Size = new System.Drawing.Size(180, 21);
            this.cboMode.TabIndex = 2;
            // 
            // pnlTcpParams
            // 
            this.pnlTcpParams.Controls.Add(this.txtTcpIp);
            this.pnlTcpParams.Controls.Add(this.nudTcpPort);
            this.pnlTcpParams.Controls.Add(this.nudTcpTimeout);
            this.pnlTcpParams.Controls.Add(this.lblMs);
            this.pnlTcpParams.Location = new System.Drawing.Point(10, 78);
            this.pnlTcpParams.Name = "pnlTcpParams";
            this.pnlTcpParams.Size = new System.Drawing.Size(180, 48);
            this.pnlTcpParams.TabIndex = 3;
            // 
            // txtTcpIp
            // 
            this.txtTcpIp.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.txtTcpIp.Location = new System.Drawing.Point(0, 0);
            this.txtTcpIp.Name = "txtTcpIp";
            this.txtTcpIp.Size = new System.Drawing.Size(180, 23);
            this.txtTcpIp.TabIndex = 0;
            this.txtTcpIp.Text = "127.0.0.1";
            // 
            // nudTcpPort
            // 
            this.nudTcpPort.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.nudTcpPort.Location = new System.Drawing.Point(0, 26);
            this.nudTcpPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudTcpPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTcpPort.Name = "nudTcpPort";
            this.nudTcpPort.Size = new System.Drawing.Size(80, 23);
            this.nudTcpPort.TabIndex = 1;
            this.nudTcpPort.Value = new decimal(new int[] {
            502,
            0,
            0,
            0});
            // 
            // nudTcpTimeout
            // 
            this.nudTcpTimeout.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.nudTcpTimeout.Location = new System.Drawing.Point(86, 26);
            this.nudTcpTimeout.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.nudTcpTimeout.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudTcpTimeout.Name = "nudTcpTimeout";
            this.nudTcpTimeout.Size = new System.Drawing.Size(66, 23);
            this.nudTcpTimeout.TabIndex = 2;
            this.nudTcpTimeout.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // lblMs
            // 
            this.lblMs.AutoSize = true;
            this.lblMs.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblMs.Location = new System.Drawing.Point(154, 30);
            this.lblMs.Name = "lblMs";
            this.lblMs.Size = new System.Drawing.Size(21, 13);
            this.lblMs.TabIndex = 3;
            this.lblMs.Text = "ms";
            // 
            // pnlTcpServerParams
            // 
            this.pnlTcpServerParams.Controls.Add(this.txtServerIp);
            this.pnlTcpServerParams.Controls.Add(this.nudServerPort);
            this.pnlTcpServerParams.Location = new System.Drawing.Point(10, 78);
            this.pnlTcpServerParams.Name = "pnlTcpServerParams";
            this.pnlTcpServerParams.Size = new System.Drawing.Size(180, 48);
            this.pnlTcpServerParams.TabIndex = 4;
            // 
            // txtServerIp
            // 
            this.txtServerIp.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.txtServerIp.Location = new System.Drawing.Point(0, 0);
            this.txtServerIp.Name = "txtServerIp";
            this.txtServerIp.Size = new System.Drawing.Size(180, 23);
            this.txtServerIp.TabIndex = 0;
            this.txtServerIp.Text = "0.0.0.0";
            // 
            // nudServerPort
            // 
            this.nudServerPort.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.nudServerPort.Location = new System.Drawing.Point(0, 26);
            this.nudServerPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudServerPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudServerPort.Name = "nudServerPort";
            this.nudServerPort.Size = new System.Drawing.Size(80, 23);
            this.nudServerPort.TabIndex = 1;
            this.nudServerPort.Value = new decimal(new int[] {
            502,
            0,
            0,
            0});
            // 
            // pnlSerialParams
            // 
            this.pnlSerialParams.Controls.Add(this.cboSerialPort);
            this.pnlSerialParams.Controls.Add(this.cboBaudRate);
            this.pnlSerialParams.Controls.Add(this.cboParity);
            this.pnlSerialParams.Controls.Add(this.cboDataBits);
            this.pnlSerialParams.Controls.Add(this.cboStopBits);
            this.pnlSerialParams.Location = new System.Drawing.Point(10, 78);
            this.pnlSerialParams.Name = "pnlSerialParams";
            this.pnlSerialParams.Size = new System.Drawing.Size(180, 48);
            this.pnlSerialParams.TabIndex = 5;
            // 
            // cboSerialPort
            // 
            this.cboSerialPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSerialPort.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cboSerialPort.FormattingEnabled = true;
            this.cboSerialPort.Location = new System.Drawing.Point(0, 0);
            this.cboSerialPort.Name = "cboSerialPort";
            this.cboSerialPort.Size = new System.Drawing.Size(84, 21);
            this.cboSerialPort.TabIndex = 0;
            // 
            // cboBaudRate
            // 
            this.cboBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBaudRate.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cboBaudRate.FormattingEnabled = true;
            this.cboBaudRate.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cboBaudRate.Location = new System.Drawing.Point(90, 0);
            this.cboBaudRate.Name = "cboBaudRate";
            this.cboBaudRate.Size = new System.Drawing.Size(90, 21);
            this.cboBaudRate.TabIndex = 1;
            // 
            // cboParity
            // 
            this.cboParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboParity.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cboParity.FormattingEnabled = true;
            this.cboParity.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even"});
            this.cboParity.Location = new System.Drawing.Point(0, 26);
            this.cboParity.Name = "cboParity";
            this.cboParity.Size = new System.Drawing.Size(60, 21);
            this.cboParity.TabIndex = 2;
            // 
            // cboDataBits
            // 
            this.cboDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDataBits.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cboDataBits.FormattingEnabled = true;
            this.cboDataBits.Items.AddRange(new object[] {
            "7",
            "8"});
            this.cboDataBits.Location = new System.Drawing.Point(66, 26);
            this.cboDataBits.Name = "cboDataBits";
            this.cboDataBits.Size = new System.Drawing.Size(50, 21);
            this.cboDataBits.TabIndex = 3;
            // 
            // cboStopBits
            // 
            this.cboStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStopBits.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cboStopBits.FormattingEnabled = true;
            this.cboStopBits.Items.AddRange(new object[] {
            "1",
            "1.5",
            "2"});
            this.cboStopBits.Location = new System.Drawing.Point(122, 26);
            this.cboStopBits.Name = "cboStopBits";
            this.cboStopBits.Size = new System.Drawing.Size(58, 21);
            this.cboStopBits.TabIndex = 4;
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnConnect.FlatAppearance.BorderSize = 0;
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnConnect.ForeColor = System.Drawing.Color.White;
            this.btnConnect.Location = new System.Drawing.Point(10, 136);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(50, 26);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = false;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnDisconnect.FlatAppearance.BorderSize = 0;
            this.btnDisconnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDisconnect.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnDisconnect.ForeColor = System.Drawing.Color.White;
            this.btnDisconnect.Location = new System.Drawing.Point(63, 136);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(50, 26);
            this.btnDisconnect.TabIndex = 7;
            this.btnDisconnect.Text = "断开";
            this.btnDisconnect.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(116, 136);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 26);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "保存参数";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // lblStats
            // 
            this.lblStats.AutoSize = true;
            this.lblStats.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblStats.Location = new System.Drawing.Point(10, 166);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(59, 15);
            this.lblStats.TabIndex = 9;
            this.lblStats.Text = "TX:0  RX:0";
            // 
            // CommunicationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblStats);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.pnlSerialParams);
            this.Controls.Add(this.pnlTcpServerParams);
            this.Controls.Add(this.pnlTcpParams);
            this.Controls.Add(this.cboMode);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.pnlIndicator);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.MaximumSize = new System.Drawing.Size(200, 196);
            this.MinimumSize = new System.Drawing.Size(200, 196);
            this.Name = "CommunicationControl";
            this.Size = new System.Drawing.Size(200, 196);
            this.pnlTcpParams.ResumeLayout(false);
            this.pnlTcpParams.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTcpPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTcpTimeout)).EndInit();
            this.pnlTcpServerParams.ResumeLayout(false);
            this.pnlTcpServerParams.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudServerPort)).EndInit();
            this.pnlSerialParams.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlIndicator;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.ComboBox cboMode;
        private System.Windows.Forms.Panel pnlTcpParams;
        private System.Windows.Forms.TextBox txtTcpIp;
        private System.Windows.Forms.NumericUpDown nudTcpPort;
        private System.Windows.Forms.NumericUpDown nudTcpTimeout;
        private System.Windows.Forms.Label lblMs;
        private System.Windows.Forms.Panel pnlTcpServerParams;
        private System.Windows.Forms.TextBox txtServerIp;
        private System.Windows.Forms.NumericUpDown nudServerPort;
        private System.Windows.Forms.Panel pnlSerialParams;
        private System.Windows.Forms.ComboBox cboSerialPort;
        private System.Windows.Forms.ComboBox cboBaudRate;
        private System.Windows.Forms.ComboBox cboParity;
        private System.Windows.Forms.ComboBox cboDataBits;
        private System.Windows.Forms.ComboBox cboStopBits;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblTitle;

    }
}
