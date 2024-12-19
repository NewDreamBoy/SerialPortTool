using SerialPortTool.VIewModels;
using System.Windows.Controls;

namespace SerialPortTool.Views
{
    /// <summary>
    /// SerialDebugControl.xaml 的交互逻辑
    /// </summary>
    public partial class SerialDebugControl : UserControl
    {
        public SerialDebugControl()
        {
            this.DataContext = new SerialDebugControlViewModel();
            InitializeComponent();
        }
    }
}