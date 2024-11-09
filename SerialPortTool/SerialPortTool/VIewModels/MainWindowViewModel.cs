using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using SerialPortTool.Views;
using System.Windows.Controls;

namespace SerialPortTool.VIewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly SerialDebugControl _serialDebugControl = new SerialDebugControl();
        private readonly MySettingsControl _settingsControl = new MySettingsControl();

        [ObservableProperty]
        private UserControl _userControl;

        public MainWindowViewModel()
        {
            UserControl = _serialDebugControl;
        }

        [RelayCommand]
        public void SelectionChanged(int index)
        {
            UserControl = index switch
            {
                0 => _serialDebugControl,
                1 => _settingsControl
            };
            Growl.Success("切换成功");
        }
    }
}