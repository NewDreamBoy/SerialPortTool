using SerialPortTool.Core.Interface;
using SerialPortTool.Models;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace SerialPortTool.Core
{
    public class ApplicationDataSaveService : IApplicationDataSaveService
    {
        #region 单例

        public static readonly Lazy<ApplicationDataSaveService> _instance = new Lazy<ApplicationDataSaveService>(() => new ApplicationDataSaveService());
        public static ApplicationDataSaveService Instance = _instance.Value;

        #endregion 单例

        private string AppDataFolderName { get; } = "SerialPortTool";

        /// <summary>
        /// 配置文件目录
        /// </summary>
        public string ConfigurationDirectory { get; set; }

        /// <summary>
        /// 应用程序数据保存路径
        /// </summary>
        public string AppDataPath { get; set; }

        private JsonSerializerOptions _options;

        public ApplicationDataSaveService()
        {
            _options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = null,
                // 允许中文字符不转义
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs)
            };

            AppDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AppDataFolderName);
            ConfigurationDirectory = Path.Combine(AppDataPath, "SerialPortConfigs");
        }

        /// <summary>
        /// 初始化应用程序数据保存文件
        /// </summary>
        public void InitializeAppDataSaveFile()
        {
            if (!Directory.Exists(AppDataPath)) Directory.CreateDirectory(AppDataPath);
            if (!Directory.Exists(ConfigurationDirectory)) Directory.CreateDirectory(ConfigurationDirectory);
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="configSaver"></param>
        public void SaveConfig(SerialPortConfigSaver configSaver)
        {
            if (configSaver != null)
            {
                //配置文件夹处理
                var newConfigPath = Path.Combine(ConfigurationDirectory, configSaver.ConfigName);
                if (!Directory.Exists(newConfigPath)) Directory.CreateDirectory(newConfigPath);

                //处理图片
                if (!string.IsNullOrEmpty(configSaver.CoverImagePath))
                {
                    var extension = Path.GetExtension(configSaver.CoverImagePath);
                    string newConfigFilePath = Path.Combine(newConfigPath, $"封面图{extension}");
                    File.Copy(configSaver.CoverImagePath, newConfigFilePath, true);
                    //更新图片路劲
                    configSaver.CoverImagePath = newConfigFilePath;
                }

                //创建Json文件夹
                var jsonPath = $"{newConfigPath}\\{configSaver.ConfigName}.Json";
                //配置参数序列化成Json格式字符串
                var configJson = SaveSerialPortParamsToJson(configSaver);
                //写入配置内容到Json
                File.WriteAllText(jsonPath, configJson);
            }
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        public List<SerialPortConfigSaver> GetConfig()
        {
            List<SerialPortConfigSaver> configs = new List<SerialPortConfigSaver>();
            //获取所有配置文件夹
            string[] configFolders = Directory.GetDirectories(ConfigurationDirectory);
            foreach (var folder in configFolders)
            {
                // 获取文件夹中的所有JSON文件
                string[] jsonFiles = Directory.GetFiles(folder, "*.json");
                foreach (var jsonFile in jsonFiles)
                {
                    string jsonContent = File.ReadAllText(jsonFile);
                    var config = JsonSerializer.Deserialize<SerialPortConfigSaver>(jsonContent);
                    if (config != null) configs.Add(config);
                }
            }
            return configs;
        }

        /// <summary>
        /// 将串口参数保存为Json字符串
        /// </summary>
        /// <param name="configSaver"></param>
        /// <returns></returns>
        public string SaveSerialPortParamsToJson(SerialPortConfigSaver configSaver)
        {
            return JsonSerializer.Serialize(configSaver, _options);
        }
    }
}