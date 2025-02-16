using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SerialPortTool.Core;
using SerialPortTool.Models;

namespace SerialPortTool.VIewModels
{
    public partial class SaveConfigurationViewModel : ObservableRecipient
    {
        #region 配置参数

        [ObservableProperty] private SerialPortConfigSaver _serialPortConfigSaver;
        [ObservableProperty] private string _selectedSendDataFormat;
        [ObservableProperty] private string _selectedReceiveDataFormat;

        #endregion 配置参数

        public SaveConfigurationViewModel(SerialPortConfigSaver serialPortConfigSaver)
        {
            SerialPortConfigSaver = serialPortConfigSaver;
            SelectedSendDataFormat = serialPortConfigSaver.ConnectionParameters.SendFormat.ToString();
            SelectedReceiveDataFormat = serialPortConfigSaver.ConnectionParameters.ReceiveFormat.ToString();
        }

        [RelayCommand]
        public void SaveConfiguration()
        {
            ApplicationDataSaveService.Instance.SaveConfig(SerialPortConfigSaver);
        }

        [RelayCommand]
        public void SelectedImage(Uri uri)
        {
            SerialPortConfigSaver.CoverImagePath = uri.LocalPath;
        }
    }
}