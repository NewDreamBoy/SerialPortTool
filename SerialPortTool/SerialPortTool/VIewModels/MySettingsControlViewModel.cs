using CommunityToolkit.Mvvm.ComponentModel;
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
            Title = "配置管理";
            ConfigurationItems = ConfigurationManager.Instance.GetConfigurationItems();
        }
    }
}