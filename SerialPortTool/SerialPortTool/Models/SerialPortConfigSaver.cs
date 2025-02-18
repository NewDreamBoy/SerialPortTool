namespace SerialPortTool.Models
{
    /// <summary>
    /// 串口配置保存器
    /// </summary>
    public class SerialPortConfigSaver
    {
        /// <summary>
        /// 配置名
        /// </summary>
        public string ConfigName { get; set; }

        /// <summary>
        /// 连接参数
        /// </summary>
        public SerialConnectionParameters ConnectionParameters { get; set; }

        /// <summary>
        /// 封面图名
        /// </summary>
        public string CoverImagePath { get; set; } = "";

        /// <summary>
        /// 发送命令
        /// </summary>
        public string SendCommand { get; set; }

        /// <summary>
        /// 配置备注
        /// </summary>
        public string ConfigRemark { get; set; }
    }
}