using System;
using System.Text;
using System.Windows.Forms;
using IndustrialControls.Controls.Communication;
using IndustrialControls.Core;
using IndustrialControls.Demo;

namespace IndustrialControls.Demo.Pages
{
    public partial class CommunicationTestForm : UserControl
    {
        private Timer _statsTimer;

        public CommunicationTestForm()
        {
            InitializeComponent();
            DemoUiChrome.ApplyPage(this, compactPadding: true);
            if (!ControlDesignerHelper.IsDesignMode(this))
            {
                SetupEvents();
                SetupStatsTimer();
            }
        }

        private void SetupEvents()
        {
            // 状态事件
            commControl.StateChanged += (s, state) =>
            {
                lblState.Text = state.ToString();
                lblState.ForeColor = state == CommunicationState.Connected
                    ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                bool connected = state == CommunicationState.Connected;
                btnSendString.Enabled = connected;
                btnSendHex.Enabled = connected;
                btnSendToAll.Enabled = connected;
                btnSendToClient.Enabled = connected;
                AppendLog($"[状态] {state}");
            };

            // 字符串接收
            commControl.StringReceived += (s, text) =>
            {
                AppendText(txtStringReceived, $"[{DateTime.Now:HH:mm:ss}] {text}");
            };

            // 原始字节接收
            commControl.DataReceived += (s, data) =>
            {
                string hex = BitConverter.ToString(data).Replace("-", " ");
                AppendText(txtHexReceived, $"[{DateTime.Now:HH:mm:ss}] {hex}");
            };

            // 帧接收
            commControl.FrameReceived += (s, e) =>
            {
                AppendText(txtFrameReceived, $"[{DateTime.Now:HH:mm:ss}] {e.Text}");
            };

            // 客户端连接/断开
            commControl.ClientConnected += (s, e) =>
            {
                AppendLog($"[客户端连接] {e.ClientId} (当前{e.ClientCount}个)");
                RefreshClientList();
            };

            commControl.ClientDisconnected += (s, e) =>
            {
                AppendLog($"[客户端断开] {e.ClientId} (当前{e.ClientCount}个)");
                RefreshClientList();
            };

            // 客户端字符串接收
            commControl.ClientStringReceived += (s, e) =>
            {
                AppendText(txtStringReceived, $"[{DateTime.Now:HH:mm:ss}] [{e.ClientId}] {e.Text}");
            };

            // 错误事件
            commControl.ErrorOccurred += (s, ex) =>
            {
                AppendLog($"[错误] {ex.Message}");
            };
        }

        private void SetupStatsTimer()
        {
            _statsTimer = new Timer { Interval = 500 };
            _statsTimer.Tick += (s, e) => UpdateStats();
            _statsTimer.Start();
        }

        private void UpdateStats()
        {
            lblBytesSent.Text = $"{commControl.BytesSent} 字节 / {commControl.FramesSent} 帧";
            lblBytesReceived.Text = $"{commControl.BytesReceived} 字节 / {commControl.FramesReceived} 帧";
            lblErrorCount.Text = commControl.ErrorCount.ToString();
            lblClientCount.Text = commControl.ClientCount.ToString();
        }

        // 发送字符串
        private void BtnSendString_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSendData.Text)) return;
            try
            {
                commControl.Send(txtSendData.Text);
                AppendLog($"[发送] {txtSendData.Text}");
            }
            catch (Exception ex) { AppendLog($"[发送失败] {ex.Message}"); }
        }

        // 发送HEX
        private void BtnSendHex_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSendData.Text)) return;
            try
            {
                string[] parts = txtSendData.Text.Trim().Split(new[] { ' ', ',', '-' }, StringSplitOptions.RemoveEmptyEntries);
                byte[] data = new byte[parts.Length];
                for (int i = 0; i < parts.Length; i++)
                    data[i] = Convert.ToByte(parts[i], 16);
                commControl.Send(data);
                AppendLog($"[发送HEX] {BitConverter.ToString(data)}");
            }
            catch (FormatException) { AppendLog("[错误] HEX格式错误，示例: 01 03 00 00"); }
            catch (Exception ex) { AppendLog($"[发送失败] {ex.Message}"); }
        }

        // 广播
        private void BtnSendToAll_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSendData.Text)) return;
            try
            {
                commControl.SendToAll(txtSendData.Text);
                AppendLog($"[广播] {txtSendData.Text}");
            }
            catch (Exception ex) { AppendLog($"[广播失败] {ex.Message}"); }
        }

        // 定向发送
        private void BtnSendToClient_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSendData.Text)) return;
            string clientId = cboTargetClient.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(clientId))
            {
                AppendLog("[提示] 请先选择目标客户端");
                return;
            }
            try
            {
                commControl.SendToClient(clientId, txtSendData.Text);
                AppendLog($"[定向发送→{clientId}] {txtSendData.Text}");
            }
            catch (Exception ex) { AppendLog($"[定向发送失败] {ex.Message}"); }
        }

        // 刷新客户端列表
        private void BtnRefreshClients_Click(object sender, EventArgs e)
        {
            RefreshClientList();
        }

        private void RefreshClientList()
        {
            if (InvokeRequired) { Invoke(new Action(RefreshClientList)); return; }
            var clients = commControl.GetConnectedClients();
            lstClients.Items.Clear();
            cboTargetClient.Items.Clear();
            foreach (var c in clients)
            {
                lstClients.Items.Add(c);
                cboTargetClient.Items.Add(c);
            }
            if (cboTargetClient.Items.Count > 0) cboTargetClient.SelectedIndex = 0;
        }

        // 重置统计
        private void BtnResetStats_Click(object sender, EventArgs e)
        {
            commControl.ResetStatistics();
            UpdateStats();
            AppendLog("[统计已重置]");
        }

        // 清空所有
        private void BtnClearAll_Click(object sender, EventArgs e)
        {
            txtStringReceived.Clear();
            txtHexReceived.Clear();
            txtFrameReceived.Clear();
            AppendLog("[日志已清空]");
        }

        // UI辅助
        private void AppendLog(string msg)
        {
            AppendText(txtStringReceived, msg);
        }

        private void AppendText(TextBox tb, string text)
        {
            if (tb.IsDisposed) return;
            if (InvokeRequired) { BeginInvoke(new Action(() => AppendText(tb, text))); return; }
            tb.AppendText(text + Environment.NewLine);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _statsTimer?.Stop();
                _statsTimer?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
