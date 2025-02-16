using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SerialPortTool.Core;
using SerialPortTool.Models;
using SerialPortTool.Views;
using System.IO.Ports;
using System.Windows;

namespace SerialPortTool.VIewModels
{
    public partial class SerialDebugControlViewModel : ObservableRecipient
    {
        #region 核心业务

        /// <summary>
        /// 串口控制器
        /// </summary>
        private SerialPortController SerialPortController => SerialPortController.Instance;

        #endregion 核心业务

        #region 串口参数

        /// <summary>
        /// 串口选项配置保存类
        /// </summary>
        private readonly SerialConnectionParameters _serialConnectionParameters = new();

        /// <summary>
        /// 串口选项默认参数
        /// </summary>
        [ObservableProperty]
        private SerialPortConfig _defaultSerialPortConfig = new();

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
        }

        /// <summary>
        /// 初始化串口参数
        /// </summary>
        public void InitSerialParameter()
        {
            DefaultSerialPortConfig.PortNames = SerialPortController.GetPortName().ToList();        //获取本机串口设备
            SelectedBaudRate = DefaultSerialPortConfig.BaudRate[0];                                 //默认波特率9600
            SelectedDataBits = DefaultSerialPortConfig.DataBits[1];                                 //默认数据位8
            SelectedStopBits = DefaultSerialPortConfig.StopBits[0];                                 //停止位
            SelectedParity = DefaultSerialPortConfig.Parity[0];                                     //校验位
            SelectedSendDataFormat = DefaultSerialPortConfig.SendDataFormat[0];                     //默认发送数据格式
            SelectedReceiveDataFormat = DefaultSerialPortConfig.ReceiveDataFormat[0];               //默认接收数据格式
        }

        /// <summary>
        /// 连接串口
        /// </summary>
        [RelayCommand]
        public void OpenSerialConnection()
        {
            if (string.IsNullOrEmpty(SelectedPortName))
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
                SerialPortController.NotifyMainWindow(2, "程序异常：发送数据格式转换失败错误");
            }
            if (Enum.TryParse(SelectedReceiveDataFormat, out ReceiveFormat receiveFormat))
            {
                _serialConnectionParameters.ReceiveFormat = receiveFormat;
            }
            else
            {
                SerialPortController.NotifyMainWindow(2, "程序异常：接收数据格式转换失败错误");
            }

            try
            {
                if (SerialPortController.OpenPort(_serialConnectionParameters))
                    SerialPortController.SerialPort!.DataReceived += SerialPortDataReceived;
                else
                    SerialPortController.NotifyMainWindow(1, "打开串口失败");
            }
            catch (Exception e)
            {
                SerialPortController.NotifyMainWindow(2, $"程序异常：打开串口时发生异常：{e.Message}");
                throw;
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
            if (!SerialPortController.Instance.IsSerialPortOpen()) return;
            SerialPortController.ClosePort();
            SerialPortController.NotifyMainWindow(0, "关闭串口成功");
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

        /// <summary>
        /// 打开保存配置界面
        /// </summary>
        [RelayCommand]
        public void OpenSaveConfigurationInterface()
        {
            SerialPortConfigSaver serialPortConfigSaver = new SerialPortConfigSaver();
            serialPortConfigSaver.ConnectionParameters = _serialConnectionParameters;
            var saveConfigurationVm = new SaveConfigurationViewModel(serialPortConfigSaver);
            var saveConfigurationWindow = new SaveConfigurationWindow
            { DataContext = saveConfigurationVm };
            saveConfigurationWindow.Show();
        }
    }
}