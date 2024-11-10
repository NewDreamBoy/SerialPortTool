using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HandyControl.Controls;
using HandyControl.Tools.Extension;
using SerialPortTool.Views;
using System.Windows.Controls;
using System.Windows.Interop;
using static System.Net.Mime.MediaTypeNames;

namespace SerialPortTool.VIewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly SerialDebugControl _serialDebugControl = new SerialDebugControl();
        private readonly MySettingsControl _settingsControl = new MySettingsControl();

        [ObservableProperty]
        private UserControl _userControl;

        [ObservableProperty]
        private string _infoText;


        public MainWindowViewModel()
        {
            UserControl = _serialDebugControl;
            StrongReferenceMessenger.Default.Register<MainWindowViewModel, string>(this, (r, m) => r.InfoText = m);
        }

        [RelayCommand]
        public void SelectionChanged(int index)
        {
            UserControl = index switch
            {
                0 => _serialDebugControl,
                1 => _settingsControl
            };
        }
    }
}