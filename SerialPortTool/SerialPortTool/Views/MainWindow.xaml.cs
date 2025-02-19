using SerialPortTool.VIewModels;
using System.Windows;
using SerialPortTool.Core;

namespace SerialPortTool.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
            this.Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var configItems = ApplicationDataSaveService.Instance.GetConfig();
            // 异步加载配置项
            await ConfigurationManager.Instance.GetConfigurationItemsAsync(configItems);
        }
    }
}