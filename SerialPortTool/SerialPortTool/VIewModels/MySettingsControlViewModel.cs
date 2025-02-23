using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
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
            StrongReferenceMessenger.Default.Register<RefreshConfigMessage>(this, async (w,r) => await RefreshInterface());

        }

        [RelayCommand]
        public void ShowSettings(ConfigurationItem item)
        {
            var config = ConfigurationManager.Instance.GetConfigurationItems(item.ConfigurationName);
            var saveConfigurationVm = new SaveConfigurationViewModel(config);
            saveConfigurationVm.IsDeleteButtonVisible = true;
            var saveConfigurationWindow = new SaveConfigurationWindow
            { DataContext = saveConfigurationVm };
            saveConfigurationWindow.Show();
        }

        public async Task RefreshInterface()
        {
            await ConfigurationManager.Instance.GetConfigurationItemsAsync();
            ConfigurationItems = ConfigurationManager.Instance.ConfigurationItems;
        }
    }
}