﻿using SerialPortTool.Models;

namespace SerialPortTool.Core.Interface
{
    /// <summary>
    /// 应用数据保存服务接口
    /// </summary>
    public interface IApplicationDataSaveService
    {
        /// <summary>
        /// 初始化
        /// </summary>
        void InitializeAppDataSaveFile();

        /// <summary>
        /// 保存配置文件
        /// </summary>
        void SaveConfig(SerialPortConfigSaver configSaver);

        /// <summary>
        /// 删除配置
        /// </summary>
        /// <param name="configSaver"></param>
        void DeleteConfig(SerialPortConfigSaver configSaver);

        /// <summary>
        /// 获取配置文件
        /// </summary>
        List<SerialPortConfigSaver> GetConfig();

        /// <summary>
        /// 保存串口参数到Json文件
        /// </summary>
        /// <returns></returns>
        string SaveSerialPortParamsToJson(SerialPortConfigSaver configSaver);
    }
}