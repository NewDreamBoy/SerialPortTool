namespace SerialPortTool.Core
{
    /// <summary>
    /// 文本转换管理器
    /// </summary>
    public static class TextConversionManager
    {
        /// <summary>
        /// 将十六进制字符串转换为字节数组
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static byte[] HexStringToByteArray(string text)
        {
            var hexValuesSplit = text.Split(' ');
            byte[] bytes = new byte[hexValuesSplit.Length];
            for (int i = 0; i < hexValuesSplit.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexValuesSplit[i], 16);
            }
            return bytes;
        }
    }
}