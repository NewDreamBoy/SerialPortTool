using CommunityToolkit.Mvvm.Messaging;
using HandyControl.Controls;
using SerialPortTool.Models;
using System.IO.Ports;
using System.Text;
using System.Windows.Interop;

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

        public SerialPort SerialPort { get; set; }

        private StringBuilder message = new StringBuilder();

        private SerialPortStatusInfo SerialPortStatusInfo { get; set; }

        private SerialPortController()
        {
            SerialPortStatusInfo = new SerialPortStatusInfo { StatusCode = 0, StatusInfo = "" };
        }


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
                    NotifyMainWindow(1, "串口连接失败：当前已有连接的串口，如需更换串口请先关闭当前串口！");
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
                NotifyMainWindow(0, "串口连接成功");
            }
            catch (Exception e)
            {
                NotifyMainWindow(2, $"串口连接异常：异常信息：{e.Message}");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取串口缓冲区的数据
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetSerialPortBytes(ReceiveFormat receiveFormat)
        {
            //获取缓冲区中的字节数
            int availableBytes = SerialPort.BytesToRead;
            Byte[] buffer = new Byte[availableBytes];
            if (availableBytes <= 0) return message;
            //读取缓冲区中的数据
            SerialPort.Read(buffer, 0, availableBytes);
            string msg = "";
            msg += $"[{DateTime.Now.ToString("HH:mm:ss.fff")}]  ";
            switch (receiveFormat)
            {
                case ReceiveFormat.Text:
                    msg += Encoding.UTF8.GetString(buffer);
                    break;
                case ReceiveFormat.Hex:
                    msg += BitConverter.ToString(buffer).Replace("-", " ");
                    break;
            }
            message.Append($"{msg}\n");
            return message;
        }

        /// <summary>
        /// 清空StringBuilder里面的数据
        /// </summary>
        public void CloseMessage()
        {
            message.Clear();
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
                CloseMessage();
                SerialPort.Close();
                Growl.Success("串口关闭连接成功");
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <returns></returns>
        public void SendData(string content, SendFormat sendFormat)
        {
            if (!IsSerialPortOpen())
            {
                NotifyMainWindow(1, "当前没有连接的串口，请先连接串口！");
                return;
            }

            switch (sendFormat)
            {
                case SendFormat.Text:
                    SerialPort.Write(content);
                    break;
                case SendFormat.Hex:
                    var bytes = TextConversionManager.HexStringToByteArray(content);
                    SerialPort.Write(bytes, 0, bytes.Length);
                    break;
            }
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

        /// <summary>
        /// 通知主窗口
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        /// <param name="isNotificationPopup"></param>
        public void NotifyMainWindow(int statusCode, string message,bool isNotificationPopup = true)
        {
            SerialPortStatusInfo.StatusCode = (StatusCode)statusCode;
            SerialPortStatusInfo.StatusInfo = message;
            StrongReferenceMessenger.Default.Send(SerialPortStatusInfo);
            if(isNotificationPopup)
            {
                switch (statusCode)
                {
                    case 0:
                        Growl.Success(message);
                        break;
                    case 1:
                        Growl.Warning(message);
                        break;
                    case 2:
                        Growl.Fatal(message);
                        break;
                }
            }
        }
    }
}