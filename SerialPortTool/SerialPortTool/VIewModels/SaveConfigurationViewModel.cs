using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SerialPortTool.Core;
using SerialPortTool.Models;
using System.Windows.Media;

namespace SerialPortTool.VIewModels
{
    public partial class SaveConfigurationViewModel : ObservableRecipient
    {
        #region 配置参数

        [ObservableProperty] private SerialPortConfigSaver _serialPortConfigSaver;
        [ObservableProperty] private string _selectedSendDataFormat;
        [ObservableProperty] private string _selectedReceiveDataFormat;
        [ObservableProperty] private Brush _myImageBrush;
        [ObservableProperty] private bool _isDeleteButtonVisible;

        RefreshConfigMessage RefreshConfigMessage => new RefreshConfigMessage();

        #endregion 配置参数

        #region 事件

        /// <summary>
        /// 关闭窗口事件
        /// </summary>
        public event Action? RequestClose;

        #endregion 事件

        private SerialPortController SerialPortController => SerialPortController.Instance;

        public SaveConfigurationViewModel(SerialPortConfigSaver serialPortConfigSaver)
        {
            SerialPortConfigSaver = serialPortConfigSaver;
            SelectedSendDataFormat = serialPortConfigSaver.ConnectionParameters.SendFormat.ToString();
            SelectedReceiveDataFormat = serialPortConfigSaver.ConnectionParameters.ReceiveFormat.ToString();
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        [RelayCommand]
        public void SaveConfiguration()
        {
            try
            {
                ApplicationDataSaveService.Instance.SaveConfig(SerialPortConfigSaver);
                RequestClose?.Invoke();
                //发送刷新界面通知
                StrongReferenceMessenger.Default.Send(RefreshConfigMessage);
                SerialPortController.NotifyMainWindow(0, "保存配置成功");
            }
            catch (Exception ex)
            {
                SerialPortController.NotifyMainWindow(3, "保存配置时程序发送异常，保存失败");
                throw;
            }
        }

        /// <summary>
        /// 删除配置
        /// </summary>
        [RelayCommand]
        public void DeleteConfiguration(SerialPortConfigSaver serialPortConfigSaver)
        {
            ApplicationDataSaveService.Instance.DeleteConfig(serialPortConfigSaver);
            RequestClose?.Invoke();
            //发送刷新界面通知
            StrongReferenceMessenger.Default.Send(RefreshConfigMessage);
            SerialPortController.NotifyMainWindow(0, "删除配置成功");
        }

        /// <summary>
        /// 选择图片
        /// </summary>
        /// <param name="uri"></param>
        [RelayCommand]
        public void SelectedImage(Uri uri)
        {
            SerialPortConfigSaver.CoverImagePath = uri.LocalPath;
        }
    }
}