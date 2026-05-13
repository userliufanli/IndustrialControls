namespace IndustrialControls.Controls.Communication
{
    /// <summary>
    /// 通讯状态枚举
    /// </summary>
    public enum CommunicationState
    {
        /// <summary>未连接</summary>
        Disconnected,
        /// <summary>连接中</summary>
        Connecting,
        /// <summary>已连接</summary>
        Connected,
        /// <summary>错误</summary>
        Error
    }
}
