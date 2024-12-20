﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SerialPortTool.Models;

namespace SerialPortTool.Core
{
    /// <summary>
    /// 配置管理器
    /// </summary>
    public class ConfigurationManager
    {
        public static readonly Lazy<ConfigurationManager> _instance = new Lazy<ConfigurationManager>(() => new ConfigurationManager());

        public static ConfigurationManager Instance => _instance.Value;

        public ConfigurationManager() { }


        /// <summary>
        /// 获取用户所有的配置项
        /// </summary>
        /// <returns></returns>
        public List<ConfigurationItem> GetConfigurationItems()
        {
            List<ConfigurationItem> temodata = new List<ConfigurationItem>()
            {
                new()
                {
                    ConfigurationImage = "Resources/Configuration/Configuration1.png",
                    ConfigurationName = "配置1",
                    ConnectionParameters = null
                },
                new()
                {
                    ConfigurationImage = "Resources/Configuration/Configuration1.png",
                    ConfigurationName = "配置2",
                    ConnectionParameters = null
                },
                new()
                {
                    ConfigurationImage = "Resources/Configuration/Configuration1.png",
                    ConfigurationName = "配置3",
                    ConnectionParameters = null
                },
                new()
                {
                    ConfigurationImage = "Resources/Configuration/Configuration1.png",
                    ConfigurationName = "配置4",
                    ConnectionParameters = null
                },
                new()
                {
                    ConfigurationImage = "Resources/Configuration/Configuration1.png",
                    ConfigurationName = "配置5",
                    ConnectionParameters = null
                },
                new()
                {
                    ConfigurationImage = "Resources/Configuration/Configuration1.png",
                    ConfigurationName = "配置6",
                    ConnectionParameters = null
                },       new()
                {
                    ConfigurationImage = "Resources/Configuration/Configuration1.png",
                    ConfigurationName = "配置7",
                    ConnectionParameters = null
                },
                new()
                {
                    ConfigurationImage = "Resources/Configuration/Configuration1.png",
                    ConfigurationName = "配置8",
                    ConnectionParameters = null
                },
                new()
                {
                    ConfigurationImage = "Resources/Configuration/Configuration1.png",
                    ConfigurationName = "配置9",
                    ConnectionParameters = null
                },
                new()
                {
                    ConfigurationImage = "Resources/Configuration/Configuration1.png",
                    ConfigurationName = "配置10",
                    ConnectionParameters = null
                }
            };
            return temodata;
        }
    }
}
