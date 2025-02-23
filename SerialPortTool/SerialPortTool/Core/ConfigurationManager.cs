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

        #endregion 单例

        public List<ConfigurationItem> ConfigurationItems { get; set; }

        private List<SerialPortConfigSaver> _serialPortConfigSavers;

        /// <summary>
        /// 异步获取用户所有的配置项
        /// </summary>
        /// <returns></returns>
        public async Task GetConfigurationItemsAsync()
        {
            List<SerialPortConfigSaver> serialPortConfigSavers = ApplicationDataSaveService.Instance.GetConfig();
            // 初始化集合
            var items = new List<ConfigurationItem>();

            if (serialPortConfigSavers == null)
            {
                ConfigurationItems = items;
                return;
            }

            _serialPortConfigSavers = serialPortConfigSavers;

            // 在后台线程中执行耗时操作
            await Task.Run(() =>
            {
                foreach (var item in _serialPortConfigSavers)
                {
                    ConfigurationItem configurationItem = new ConfigurationItem();
                    if (!string.IsNullOrEmpty(item.CoverImagePath))
                    {
                        // 创建 BitmapImage 并冻结以便跨线程使用
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(item.CoverImagePath, UriKind.RelativeOrAbsolute);
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.EndInit();
                        bitmap.Freeze();
                        configurationItem.CoverImage = bitmap;
                    }
                    configurationItem.ConfigurationName = item.ConfigName;
                    items.Add(configurationItem);
                }
            });

            // 回到 UI 线程更新配置项集合
            ConfigurationItems = items;
        }

        /// <summary>
        /// 获取配置项的详细配置
        /// </summary>
        /// <param name="ConfigurationName">配置名</param>
        public SerialPortConfigSaver GetConfigurationItems(string ConfigurationName)
        {
            return _serialPortConfigSavers.FirstOrDefault(item => item.ConfigName == ConfigurationName);
        }
    }
}