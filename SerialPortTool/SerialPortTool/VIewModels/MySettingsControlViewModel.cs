using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SerialPortTool.Core;
using SerialPortTool.Models;
using SerialPortTool.Views;

namespace SerialPortTool.VIewModels
{
    public partial class MySettingsControlViewModel : ObservableObject
    {
        [ObservableProperty] public List<ConfigurationItem> _configurationItems;

        [ObservableProperty] public string _title;

        public MySettingsControlViewModel()
        {
            Title = "配置管理";
        }

        [RelayCommand]
        public void ShowSettings(ConfigurationItem item)
        {
            var config = ConfigurationManager.Instance.GetConfigurationItems(item.ConfigurationName);
            var saveConfigurationVm = new SaveConfigurationViewModel(config);
            var saveConfigurationWindow = new SaveConfigurationWindow
            { DataContext = saveConfigurationVm };
            saveConfigurationWindow.Show();
        }
    }
}