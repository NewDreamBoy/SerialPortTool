using SerialPortTool.VIewModels;
using System.Windows.Controls;

namespace SerialPortTool.Views
{
    /// <summary>
    /// MySettingsControl.xaml 的交互逻辑
    /// </summary>
    public partial class MySettingsControl : UserControl
    {
        public MySettingsControl()
        {
            this.DataContext = new MySettingsControlViewModel();
            InitializeComponent();
        }
    }
}