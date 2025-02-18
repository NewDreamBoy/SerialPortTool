using SerialPortTool.Models;
using System.Windows.Media.Imaging;

namespace SerialPortTool.Core
{
    /// <summary>
    /// 配置管理器
    /// </summary>
    public class ConfigurationManager
    {
        #region 单例

        public static readonly Lazy<ConfigurationManager> _instance = new Lazy<ConfigurationManager>(() => new ConfigurationManager());
        public static ConfigurationManager Instance => _instance.Value;

        #endregion

        public List<SerialPortConfigSaver> SerialPortConfigSavers { get; set; }
        private List<ConfigurationItem> _configurationItems;

        public ConfigurationManager()
        {
            SerialPortConfigSavers = ApplicationDataSaveService.Instance.GetConfig();
        }

        /// <summary>
        /// 获取用户所有的配置项
        /// </summary>
        /// <returns></returns>
        public List<ConfigurationItem> GetConfigurationItems()
        {
            _configurationItems = new List<ConfigurationItem>();
            if (SerialPortConfigSavers == null) return _configurationItems;
            foreach (var item in SerialPortConfigSavers)
            {
                ConfigurationItem configurationItem = new ConfigurationItem();
                if(!string.IsNullOrEmpty(item.CoverImagePath))
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(item.CoverImagePath, UriKind.RelativeOrAbsolute);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad; 
                    bitmap.EndInit();
                    configurationItem.CoverImage = bitmap;
                }
                configurationItem.ConfigurationName = item.ConfigName;
                _configurationItems.Add(configurationItem);
            }
            return _configurationItems;
        }
    }
}