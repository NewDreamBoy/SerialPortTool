using HandyControl.Controls;
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
    
                if (IsSerialPortOpen())
                {
                    Growl.Warning("当前已有连接的串口，如需更换串口请先关闭当前串口！");
                    return false;
                }
                SerialPort = new SerialPort(serialConnectionParameters.PortName)
                {
                    BaudRate = serialConnectionParameters.BaudRate,
                    DataBits = serialConnectionParameters.DataBits,
                    StopBits = ToStopBits(serialConnectionParameters.StopBits),
                    Parity = ToParity(serialConnectionParameters.Parity),
                };
                SerialPort.Open();
                Growl.Success("串口连接成功");
            }
            catch (Exception e)
            {
                Growl.Fatal($"串口连接异常,异常信息：{e.Message}");
                return false;
            }
            return true;
        }


        /// <summary>
        /// 获取当前设备上可用的串口名称
        /// </summary>
        /// <returns></returns>
        public string[] GetPortName()
        {
            return SerialPort.GetPortNames();
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <returns></returns>
        public void ClosePort()
        {
            if (IsSerialPortOpen())
            {
                SerialPort.Close();
                Growl.Success("串口关闭连接成功");
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <returns></returns>
        public void SendData(string content)
        {
            if (!IsSerialPortOpen()) { Growl.Warning($"当前没有连接的串口，请先连接串口！"); return; }
            SerialPort.Write(content);
        }

        /// <summary>
        /// 检查串口是否打开
        /// </summary>
        /// <returns></returns>
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