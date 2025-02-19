using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SerialPortTool.Core;
using SerialPortTool.Models;

namespace SerialPortTool.VIewModels
{
    public partial class MySettingsControlViewModel : ObservableObject
    {
        [ObservableProperty] public List<ConfigurationItem> _configurationItems;

        [ObservableProperty] public string _title;

        public MySettingsControlViewModel()
        {
            Title = "配置管理1";
        }

        [RelayCommand]
        public void ShowSettings(ConfigurationItem item)
        {
            var config = ConfigurationManager.Instance.GetConfigurationItems(item.ConfigurationName);
        }
    }
}