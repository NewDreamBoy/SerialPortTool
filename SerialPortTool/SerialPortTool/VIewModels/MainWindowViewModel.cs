using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SerialPortTool.Views;
using System.Windows.Controls;

namespace SerialPortTool.VIewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private UserControl _userControl;

        public MainWindowViewModel()
        {
            UserControl = new SerialDebugControl();
        }

        [RelayCommand]
        public void SelectionChanged(int index)
        {
            switch (index)
            {
                case 0:
                    UserControl = new SerialDebugControl();
                    break;

                case 1:
                    UserControl = new MySettingsControl();
                    break;
            }
        }
    }
}