using CommunityToolkit.Mvvm.ComponentModel;
using SerialPortTool.Core;
using SerialPortTool.Models;

namespace SerialPortTool.VIewModels
{
    public partial class MySettingsControlViewModel : ObservableObject
    {
        [ObservableProperty] public List<ConfigurationItem> _ConfigurationItems;

        [ObservableProperty] public string _title;

        public MySettingsControlViewModel()
        {
            ConfigurationItems = ConfigurationManager.Instance.GetConfigurationItems();
            Title = "配置管理";
        }
    }
}