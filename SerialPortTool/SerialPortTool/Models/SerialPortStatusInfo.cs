namespace SerialPortTool.Models
{
    /// <summary>
    /// 串口状态信息
    /// </summary>
    public class SerialPortStatusInfo
    {
        /// <summary>
        /// 状态信息
        /// </summary>
        public string StatusInfo { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public StatusCode StatusCode { get; set; }
    }
}