using CommunityToolkit.Mvvm.ComponentModel;
using SerialPortTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

namespace SerialPortTool.VIewModels
{
    public partial class SaveConfigurationViewModel : ObservableRecipient
    {
        #region 配置参数

        [ObservableProperty]
        private SerialConnectionParameters? _parameters;

        [ObservableProperty] private string _selectedSendDataFormat;
        [ObservableProperty] private string _selectedReceiveDataFormat;

        [ObservableProperty] private string _sendCommand;
        [ObservableProperty] private string _configurationNote;

        [ObservableProperty] private Uri? _coverURI;

        #endregion

        public SaveConfigurationViewModel(SerialConnectionParameters? parameters)
        {
            if (parameters == null) return;
            Parameters = parameters;
            SelectedSendDataFormat = Parameters.SendFormat.ToString();
            SelectedReceiveDataFormat = Parameters.ReceiveFormat.ToString();
            CoverURI = null;
        }

        [RelayCommand]
        public void SaveConfiguration()
        {

        }

        [RelayCommand]
        public void SelectedImage(Uri uri)
        {

        }
    }
}