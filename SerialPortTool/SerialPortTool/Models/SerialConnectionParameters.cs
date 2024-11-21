namespace SerialPortTool.Models
{
    /// <summary>
    /// 串口连接参数的数据类
    /// 该类保存串行连接的参数
    /// </summary>
    public class SerialConnectionParameters
    {
        public string PortName { get; set; }
        public int BaudRate { get; set; }
        public int DataBits { get; set; }
        public int StopBits { get; set; }
        public string Parity { get; set; }
        public SendFormat SendFormat { get; set; }
        public ReceiveFormat ReceiveFormat { get; set; }

    }

    public enum SendFormat
    {
        Text,
        Hex
    }

    public enum ReceiveFormat
    {
        Text,
        Hex
    }
}