using SerialPortTool.VIewModels;
using System.Windows;

namespace SerialPortTool.Views
{
    /// <summary>
    /// SaveConfigurationWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SaveConfigurationWindow : Window
    {
        public SaveConfigurationWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if(DataContext is SaveConfigurationViewModel vm)
            {
                // 订阅关闭请求事件
                vm.RequestClose += () => Close();
            }
        }
    }
}