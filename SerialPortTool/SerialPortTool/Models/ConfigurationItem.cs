using System.Windows.Media.Imaging;

namespace SerialPortTool.Models
{
    /// <summary>
    /// 配置项
    /// </summary>
    public class ConfigurationItem
    {
        /// <summary>
        /// 配置名称
        /// </summary>
        public string ConfigurationName { get; set; }

        /// <summary>
        /// 封面图
        /// </summary>
        public BitmapImage CoverImage { get; set; }
    }
}