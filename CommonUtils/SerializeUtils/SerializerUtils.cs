using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MessagePack;
using MemoryPack;
using MemoryPack.Compression;
using Google.Protobuf;

namespace CommonUtils.SerializeUtils
{
    public static class SerializerUtils
    {
        #region Text.Json 序列化
        /// <summary>
        /// Text.Json 序列化
        /// 加入MethodImpl(MethodImplOptions.AggressiveInlining) 标记，
        /// 提示编译器可以内联优化代码，可以提高性能
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="origin"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string TextJsonSerializer<T>(T origin)
        {
            return JsonSerializer.Serialize(origin);
        }
        #endregion

        #region ProtoBuf 序列化
        /// <summary>
        /// ProtoBuf 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="origin"></param>
        /// <param name="stream"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ProtoBufDotNet<T>(T origin, Stream stream)
        {
            Serializer.Serialize(stream, origin);
        }
        #endregion

        #region MessagePack 序列化
        /// <summary>
        /// MessagePack 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="origin"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] MessagePack<T>(T origin)
        {
            return MessagePackSerializer.Serialize(origin);
        }

        public static readonly MessagePackSerializerOptions MpLz4BOptions = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4Block);

        /// <summary>
        /// MessagePack LZ4Block 压缩序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="origin"></param>
        /// <returns></returns>
        [MethodImpl(methodImplOptions: MethodImplOptions.AggressiveInlining)]
        public static byte[] MessagePackLz4Block<T>(T origin)
        {
            return MessagePackSerializer.Serialize(origin, MpLz4BOptions);
        }

        public static readonly MessagePackSerializerOptions MpLz4BaOptions = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray);

        /// <summary>
        /// MessagePack LZ4BlockArray 压缩序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="origin"></param>
        /// <returns></returns>
        [MethodImpl(methodImplOptions: MethodImplOptions.AggressiveInlining)]
        public static byte[] MessagePackLz4BlockArray<T>(T origin)
        {
            return MessagePackSerializer.Serialize(origin, MpLz4BaOptions);
        }
        #endregion

        #region Memory 序列化

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] MemoryPack<T>(T origin)
        {
            return MemoryPackSerializer.Serialize(origin);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] MemoryPackBrotli<T>(T origin)
        {
            using var compressor = new BrotliCompressor();
            MemoryPackSerializer.Serialize(compressor, origin);
            return compressor.ToArray();
        }

        #endregion

        #region GoogoleProtobuf 序列化
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] GoogleProtobuf<T>(T origin) where T : IMessage<T>
        {
            return origin.ToByteArray();
        }
        #endregion

    }
}
