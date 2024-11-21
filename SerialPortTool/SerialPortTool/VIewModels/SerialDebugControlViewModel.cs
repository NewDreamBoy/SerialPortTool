using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HandyControl.Controls;
using SerialPortTool.Core;
using SerialPortTool.Models;
using System.IO.Ports;
using System.Windows;

namespace SerialPortTool.VIewModels
{
    public partial class SerialDebugControlViewModel : ObservableRecipient
    {
        #region 业务核心

        public SerialPortConfig SerialPortConfig { get; set; } = new SerialPortConfig();

        public SerialPortController SerialPortController => SerialPortController.Instance;

        public SerialPortStatusInfo StatusInfo;

        #endregion 业务核心

        #region 串口参数

        private readonly SerialConnectionParameters _serialConnectionParameters = new SerialConnectionParameters();

        [ObservableProperty] private List<string> _PortNames;
        [ObservableProperty] private List<int> _baudRate;
        [ObservableProperty] private List<int> _dataBits;
        [ObservableProperty] private List<int> _stopBits;
        [ObservableProperty] private List<string> _parity;
        [ObservableProperty] private List<string> _sendDataFormat;
        [ObservableProperty] private List<string> _receiveDataFormat;

        #endregion 串口参数

        #region 串口参数选择

        [ObservableProperty] private string _selectedPortName;
        [ObservableProperty] private int _selectedBaudRate;
        [ObservableProperty] private int _selectedDataBits;
        [ObservableProperty] private int _selectedStopBits;
        [ObservableProperty] private string _selectedParity;
        [ObservableProperty] private string _selectedSendDataFormat;
        [ObservableProperty] private string _selectedReceiveDataFormat;

        #endregion 串口参数选择

        #region View视图绑定相关

        [ObservableProperty]
        private string _textBoxReceiveArea;

        [ObservableProperty]
        private string _textBoxSendArea;

        #endregion View视图绑定相关

        public SerialDebugControlViewModel()
        {
            InitSerialParameter();
            StatusInfo = new SerialPortStatusInfo();
        }

        public void InitSerialParameter()
        {
            PortNames = SerialPortController.GetPortName().ToList();
            BaudRate = SerialPortConfig.BaudRate;
            DataBits = SerialPortConfig.DataBits;
            StopBits = SerialPortConfig.StopBits;
            Parity = SerialPortConfig.Parity;
            SendDataFormat = SerialPortConfig.SendDataFormat;
            ReceiveDataFormat = SerialPortConfig.ReceiveDataFormat;

            SelectedBaudRate = BaudRate[0];  //默认波特率9600
            SelectedDataBits = DataBits[1];  //默认数据位8
            SelectedStopBits = StopBits[0];  //停止位
            SelectedParity = Parity[0];      //校验位
            SelectedSendDataFormat = SendDataFormat[0];  //默认发送数据格式
            SelectedReceiveDataFormat = ReceiveDataFormat[0]; //默认接收数据格式
        }

        /// <summary>
        /// 连接串口
        /// </summary>
        [RelayCommand]
        public void OpenSerialConnection()
        {
            if (SelectedPortName == null || string.IsNullOrEmpty(SelectedPortName))
            {
                SerialPortController.NotifyMainWindow(1, "请选项要连接的串口设备");
                return;
            }
            _serialConnectionParameters.PortName = SelectedPortName;
            _serialConnectionParameters.BaudRate = SelectedBaudRate;
            _serialConnectionParameters.DataBits = SelectedDataBits;
            _serialConnectionParameters.Parity = SelectedParity;
            _serialConnectionParameters.StopBits = SelectedStopBits;
            if (Enum.TryParse(SelectedSendDataFormat, out SendFormat sendFormat))
            {
                _serialConnectionParameters.SendFormat = sendFormat;
            }
            else
            {
                SerialPortController.NotifyMainWindow(1, "程序异常：发送数据格式转换失败错误");
            }
            if (Enum.TryParse(SelectedReceiveDataFormat, out ReceiveFormat receiveFormat))
            {
                _serialConnectionParameters.ReceiveFormat = receiveFormat;
            }
            else
            {
                SerialPortController.NotifyMainWindow(1, "程序异常：接收数据格式转换失败错误");
            }

            if (SerialPortController.OpenPort(_serialConnectionParameters))
            {
                SerialPortController.SerialPort.DataReceived += SerialPortDataReceived;
            }
        }

        /// <summary>
        /// 串口数据接收事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var data = SerialPortController.Instance.GetSerialPortBytes(_serialConnectionParameters.ReceiveFormat);
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                TextBoxReceiveArea = data.ToString();
            }));
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        [RelayCommand]
        public void CloseSerialConnection()
        {
            SerialPortController.ClosePort();
            StatusInfo.StatusCode = 0;
            StatusInfo.StatusInfo = "关闭串口成功";
            StrongReferenceMessenger.Default.Send(StatusInfo);
            SerialPortController.SerialPort.DataReceived -= SerialPortDataReceived;
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        [RelayCommand]
        public void SendData()
        {
            SerialPortController.SendData(TextBoxSendArea, _serialConnectionParameters.SendFormat);
        }

        /// <summary>
        /// 清空接收区
        /// </summary>
        [RelayCommand]
        public void ClearReceiveAreaContent()
        {
            SerialPortController.CloseMessage();
            TextBoxReceiveArea = string.Empty;
        }

        /// <summary>
        /// 清空发送区
        /// </summary>
        [RelayCommand]
        public void ClearSendAreaContent()
        {
            TextBoxSendArea = string.Empty;
        }
    }
}