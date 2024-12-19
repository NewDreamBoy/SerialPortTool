using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortTool.Models
{
    /// <summary>
    /// 配置项
    /// </summary>
    public class ConfigurationItem
    {
        /// <summary>
        /// 封面图
        /// </summary>
        public string ConfigurationImage { get; set; }
        /// <summary>
        /// 配置名称
        /// </summary>
        public string ConfigurationName { get; set; }
        /// <summary>
        /// 配置内容
        /// </summary>
        public SerialConnectionParameters ConnectionParameters { get; set; }
    }
}
