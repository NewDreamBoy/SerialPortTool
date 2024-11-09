using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SerialPortTool.Core;
using SerialPortTool.Models;

namespace SerialPortTool.VIewModels
{
    public partial class SerialDebugControlViewModel : ObservableObject
    {
        public SerialPortConfig SerialPortConfig { get; set; } = new SerialPortConfig();

        private readonly SerialConnectionParameters _serialConnectionParameters = new SerialConnectionParameters();
        private SerialPortController SerialPortController => SerialPortController.Instance;

        [ObservableProperty] private List<string> _PortNames;
        [ObservableProperty] private List<int> _baudRate;
        [ObservableProperty] private List<int> _dataBits;
        [ObservableProperty] private List<int> _stopBits;
        [ObservableProperty] private List<string> _parity;

        [ObservableProperty] private string _selectedPortNames;
        [ObservableProperty] private int _selectedBaudRate;
        [ObservableProperty] private int _selectedDataBits;
        [ObservableProperty] private int _selectedStopBits;
        [ObservableProperty] private string _selectedParity;

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


        [RelayCommand]
        public void OpenSerialConnection()
        {
            //if (SerialPortController.IsSerialPortOpen())
            //{
            //    //TODO 弹出串口已经打开的提示信息
            //    return;
            //}

   

            _serialConnectionParameters.PortName = SelectedPortNames;
            _serialConnectionParameters.BaudRate = SelectedBaudRate;
            _serialConnectionParameters.DataBits = SelectedDataBits;
            _serialConnectionParameters.Parity = SelectedParity;
            _serialConnectionParameters.StopBits = SelectedStopBits;
            if (SerialPortController.OpenPort(_serialConnectionParameters))
            {

            }


        }

        [RelayCommand]
        public void CloseSerialConnection()
        {

        }

        [RelayCommand]
        public void ToggleSerialConnection()
        {
        
        }
    }
}