using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SerialPortTool.Core;
using SerialPortTool.Core.Interface;
using SerialPortTool.Models;
using SerialPortTool.Views;
using System.Windows.Controls;

namespace SerialPortTool.VIewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly SerialDebugControl _serialDebugControl = new();
        private readonly MySettingsControl _settingsControl = new();

        [ObservableProperty]
        private UserControl _userControl;

        [ObservableProperty]
        private string? _infoText;

        public MainWindowViewModel()
        {
            UserControl = _serialDebugControl;
            StrongReferenceMessenger.Default.Register<MainWindowViewModel, SerialPortStatusInfo>(this, (r, m) => r.InfoText = m.StatusInfo);
            ApplicationDataSaveService.Instance.InitializeAppDataSaveFile();

            ApplicationDataSaveService.Instance.GetConfig();

        }

        [RelayCommand]
        public void SelectionChanged(int index)
        {
            UserControl = index switch
            {
                0 => _serialDebugControl,
                1 => _settingsControl,
                _ => throw new ArgumentOutOfRangeException(nameof(index), index, null)
            };
        }
    }
}