using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtils
{
    /// <summary>
    /// Protobuf通用帮助类
    /// </summary>
    public static class ProtobufUtils
    {
        /// <summary>
        /// 将对象实例序列化为字符串（Base64编码格式）
        /// Pb=Protobuf(下同)
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="objectInstance">对象实例</param>
        /// <returns>字符串（Base64编码格式）</returns>
        public static string SerializePbToBase64String<T>(this T objectInstance)
        {
            using var ms = new MemoryStream();
            Serializer.Serialize(ms, objectInstance);
            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
        }

        /// <summary>
        /// 将字符串（Base64编码格式）反序列化为对象实例
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="text">字符串（Base64编码格式）</param>
        /// <returns>对象实例</returns>
        public static T DeserializeBase64StringToPb<T>(this string text)
        {
            var arr = Convert.FromBase64String(text);
            using var ms = new MemoryStream(arr);
            return Serializer.Deserialize<T>(ms);
        }

        /// <summary>
        /// 将字符串反序列化为对象实例
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="text">字符串（Base64编码格式）</param>
        /// <returns>对象实例</returns>
        public static T DeserializeStringToPb<T>(this string text)
        {
            ReadOnlySpan<byte> byteSpan = Encoding.UTF8.GetBytes(text);
            return Serializer.Deserialize<T>(byteSpan);
        }

        /// <summary>
        /// 将对象实例序列化为字节数组——ProtoBuf
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象实例</param>
        /// <returns>字节数组</returns>
        public static byte[] SerializePbToByteArray<T>(this T obj)
        {
            using var ms = new MemoryStream();
            Serializer.Serialize(ms, obj);
            return ms.ToArray();
        }

        /// <summary>
        /// 将字节数组反序列化为对象实例
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="arr">字节数组</param>
        /// <returns></returns>
        public static T DeserializeByteArrayToPb<T>(this byte[] arr)
        {
            using var ms = new MemoryStream(arr);
            return Serializer.Deserialize<T>(ms);
        }

        /// <summary>
        /// 将字节数组反序列化为对象实例
        /// </summary>
        /// <param name="stream">流</param>
        /// <returns></returns>
        public static T DeserializeStreamToPb<T>(this Stream stream)
        {
            return Serializer.Deserialize<T>(stream);
        }

        /// <summary>
        /// 将对象实例序列化为二进制文件
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象实例</param>
        /// <param name="path">文件路径（目录+文件名）</param>
        public static void SerializePbToFile<T>(this T obj, string path)
        {
            using var file = File.Create(path);
            Serializer.Serialize(file, obj);
        }

        /// <summary>
        /// 将二进制文件反序列化为对象实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T DeserializePbFromFile<T>(this string path)
        {
            using var file = File.OpenRead(path);
            return Serializer.Deserialize<T>(file);
        }
    }
}
