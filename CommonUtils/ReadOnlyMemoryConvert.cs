using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtils
{
    public static class ReadOnlyMemoryConvert
    {
        /// <summary>
        /// 将 ReadOnlyMemory 转换为 byte 数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] ReadOnlyMemoryToByteArray<T>(ReadOnlyMemory<T> input) where T : struct
        {
            return MemoryMarshal.AsBytes(input.Span).ToArray();
        }
    }
}
