using SerialPortTool.Models;
using System.IO.Ports;

namespace SerialPortTool.Core
{
    /// <summary>
    /// 串口控制器
    /// </summary>
    public class SerialPortController
    {
        private static readonly Lazy<SerialPortController> _instance =
            new Lazy<SerialPortController>(() => new SerialPortController());

        public static SerialPortController Instance => _instance.Value;

        private SerialPort SerialPort { get; set; }

        private SerialPortController() { }


        /// <summary>
        /// 打开串口
        /// </summary>
        /// <returns></returns>
        public bool OpenPort(SerialConnectionParameters serialConnectionParameters)
        {
            try
            {
                SerialPort = new SerialPort(serialConnectionParameters.PortName)
                {
                    BaudRate = serialConnectionParameters.BaudRate,
                    DataBits = serialConnectionParameters.DataBits,
                    StopBits = ToStopBits(serialConnectionParameters.StopBits),
                    Parity = ToParity(serialConnectionParameters.Parity),
                };

                SerialPort.Open();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }


        public string[] GetPortName()
        {
            return SerialPort.GetPortNames();
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <returns></returns>
        public bool ClosePort()
        {
            return true;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <returns></returns>
        public bool SendData()
        {
            return true;
        }

        public bool IsSerialPortOpen()
        {
            return SerialPort != null && SerialPort.IsOpen;
        }

        /// <summary>
        /// 解析校验位
        /// </summary>
        /// <param name="parity"></param>
        /// <returns></returns>
        public Parity ToParity(string parity)
        {
            return Enum.TryParse(parity, true, out Parity parsedParity) ? parsedParity : Parity.None;
        }

        /// <summary>
        /// 解析停止位
        /// </summary>
        /// <param name="stopBits"></param>
        /// <returns></returns>
        public StopBits ToStopBits(int stopBits)
        {
            return stopBits switch
            {
                1 => StopBits.One,
                2 => StopBits.Two,
                _ => StopBits.None
            };
        }
    }
}