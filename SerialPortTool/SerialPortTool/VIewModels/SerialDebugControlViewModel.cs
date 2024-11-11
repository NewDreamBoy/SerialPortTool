﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HandyControl.Controls;
using SerialPortTool.Core;
using SerialPortTool.Models;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace SerialPortTool.VIewModels
{
    public partial class SerialDebugControlViewModel : ObservableRecipient
    {
        #region 业务核心

        public SerialPortConfig SerialPortConfig { get; set; } = new SerialPortConfig();

        private SerialPortController SerialPortController => SerialPortController.Instance;

        #endregion 业务核心

        #region 串口参数

        private readonly SerialConnectionParameters _serialConnectionParameters = new SerialConnectionParameters();

        [ObservableProperty] private List<string> _PortNames;
        [ObservableProperty] private List<int> _baudRate;
        [ObservableProperty] private List<int> _dataBits;
        [ObservableProperty] private List<int> _stopBits;
        [ObservableProperty] private List<string> _parity;

        #endregion 串口参数

        #region 串口参数选择

        [ObservableProperty] private string _selectedPortName;
        [ObservableProperty] private int _selectedBaudRate;
        [ObservableProperty] private int _selectedDataBits;
        [ObservableProperty] private int _selectedStopBits;
        [ObservableProperty] private string _selectedParity;

        #endregion 串口参数选择

        #region View

        [ObservableProperty]
        private string _textBoxReceiveArea;

        [ObservableProperty]
        private string _textBoxSendArea;

        #endregion View

        private StringBuilder str = new StringBuilder();
        private DispatcherTimer _timer;
        private int maxItems = 1000; // 设置最大显示条数

        public SerialDebugControlViewModel()
        {
            InitSerialParameter();
        }

        public void InitSerialParameter()
        {
            PortNames = SerialPortController.GetPortName().ToList();
            BaudRate = SerialPortConfig.BaudRate;
            DataBits = SerialPortConfig.DataBits;
            StopBits = SerialPortConfig.StopBits;
            Parity = SerialPortConfig.Parity;

            SelectedBaudRate = BaudRate[0];  //默认波特率9600
            SelectedDataBits = DataBits[1];  //默认数据位8
            SelectedStopBits = StopBits[0];  //停止位
            SelectedParity = Parity[0];      //校验位
        }

        /// <summary>
        /// 连接串口
        /// </summary>
        [RelayCommand]
        public void OpenSerialConnection()
        {
            if (SelectedPortName == null || string.IsNullOrEmpty(SelectedPortName))
            {
                Growl.Warning("请选项要连接的串口设备");
                return;
            }
            _serialConnectionParameters.PortName = SelectedPortName;
            _serialConnectionParameters.BaudRate = SelectedBaudRate;
            _serialConnectionParameters.DataBits = SelectedDataBits;
            _serialConnectionParameters.Parity = SelectedParity;
            _serialConnectionParameters.StopBits = SelectedStopBits;

            if (SerialPortController.OpenPort(_serialConnectionParameters))
            {
                StrongReferenceMessenger.Default.Send
               ($"串口{_serialConnectionParameters.PortName}已连接 {_serialConnectionParameters.BaudRate} {_serialConnectionParameters.DataBits} {_serialConnectionParameters.StopBits} {_serialConnectionParameters.Parity}");
                SerialPortController.DataReceived += OnDataReceived;

                _timer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromMilliseconds(100) // 每100ms更新一次
                };
                _timer.Tick += (s, e) => UpdateTextBoxReceiveArea();
                _timer.Start();
            }
        }

        private void OnDataReceived(string data)
        {
            str.Append($"{data}\n");
        }

        private void UpdateTextBoxReceiveArea()
        {
            if (str.Length > 0)
            {
                TextBoxReceiveArea += str.ToString();
                str.Clear();
            }
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        [RelayCommand]
        public void CloseSerialConnection()
        {
            SerialPortController.ClosePort();
            StrongReferenceMessenger.Default.Send("");
            SerialPortController.DataReceived -= OnDataReceived;
        }

        [RelayCommand]
        public void SendData()
        {
            SerialPortController.SendData(TextBoxSendArea);
        }

        /// <summary>
        /// 清空接收区
        /// </summary>
        [RelayCommand]
        public void ClearReceiveAreaContent()
        {
            str.Clear();
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