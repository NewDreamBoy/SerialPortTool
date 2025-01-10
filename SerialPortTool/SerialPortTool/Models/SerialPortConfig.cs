namespace SerialPortTool.Models
{
    /// <summary>
    /// 串口配置
    /// </summary>
    public class SerialPortConfig
    {
        /// <summary>
        /// COM口
        /// </summary>
        public List<string> PortNames { get; set; }

        /// <summary>
        /// 波特率
        /// </summary>
        public List<int> BaudRate { get; set; } = [9600, 19200, 38400, 57600, 115200];

        /// <summary>
        /// 数据位
        /// </summary>
        public List<int> DataBits { get; set; } = [7, 8];

        /// <summary>
        /// 停止位
        /// </summary>
        public List<int> StopBits { get; set; } = [1, 2];

        /// <summary>
        /// 校验位
        /// </summary>
        public List<string> Parity { get; set; } = ["None", "Odd", "Even"];

        /// <summary>
        /// 发送数据格式
        /// </summary>
        public List<string> SendDataFormat { get; set; } = ["Text","Hex"];

        /// <summary>
        /// 接收数据格式
        /// </summary>
        public List<string> ReceiveDataFormat { get; set; } = ["Text","Hex"];
    }
}