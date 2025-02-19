using SerialPortTool.Core;
using SerialPortTool.VIewModels;
using System.Windows;
using System.Windows.Controls;

namespace SerialPortTool.Views
{
    /// <summary>
    /// MySettingsControl.xaml 的交互逻辑
    /// </summary>
    public partial class MySettingsControl : UserControl
    {
        private MySettingsControlViewModel vm;

        public MySettingsControl()
        {
            InitializeComponent();
            vm = new MySettingsControlViewModel();
            this.DataContext = vm;
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            vm.ConfigurationItems = ConfigurationManager.Instance.ConfigurationItems;
        }
    }
}