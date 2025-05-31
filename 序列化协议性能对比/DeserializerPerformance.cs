using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using CommonUtils.SerializeUtils;
using DemoClassProto;
using MemoryPack.Compression;
using MemoryPack;
using MessagePack;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsTCPIP;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using 序列化协议性能对比Benchmarks.Model;

namespace 序列化协议性能对比Benchmarks
{
    [GcForce(true)]
    [RPlotExporter]
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public static class DeserializerPerformance
    {
        private static readonly string RawJson = SerializerUtils.TextJsonSerializer(TestData.Origin);
        private static readonly byte[] RawGoogleProtobuf = SerializerUtils.GoogleProtobuf(TestData.OriginProto);
        private static readonly byte[] RawMsgPack = SerializerUtils.MessagePack(TestData.Origin);
        private static readonly byte[] RawMsgPackLz4B = SerializerUtils.MessagePackLz4Block(TestData.Origin);
        private static readonly byte[] RawMsgPackLz4BA = SerializerUtils.MessagePackLz4BlockArray(TestData.Origin);
        private static readonly byte[] RawMemoryPack = SerializerUtils.MemoryPack(TestData.Origin);
        private static readonly byte[] RawMemoryPackBrotli = SerializerUtils.MemoryPackBrotli(TestData.Origin);

        [Benchmark(Baseline = true)]
        public static DemoClass[] TextJsonDeserialize()
        {
            return JsonSerializer.Deserialize<DemoClass[]>(RawJson)!;
        }

        [Benchmark]
        public static DemoClassArrayProto GoogleProtobuf()
        {
            return DemoClassArrayProto.Parser.ParseFrom(RawGoogleProtobuf);
        }

        [Benchmark]
        public static DemoClass[] ProtobufDotNet()
        {
            using var stream = new MemoryStream(RawGoogleProtobuf);
            return Serializer.Deserialize<DemoClass[]>(stream);
        }
        [Benchmark]
        public static DemoClass[] MessagePack()
        {
            return MessagePackSerializer.Deserialize<DemoClass[]>(RawMsgPack);
        }

        [Benchmark]
        public static DemoClass[] MessagePackLz4Block()
        {
            return MessagePackSerializer.Deserialize<DemoClass[]>(RawMsgPackLz4B, SerializerUtils.MpLz4BOptions);
        }

        [Benchmark]
        public static DemoClass[] MessagePackLz4BlockArray()
        {
            return MessagePackSerializer.Deserialize<DemoClass[]>(RawMsgPackLz4BA, SerializerUtils.MpLz4BaOptions);
        }

        [Benchmark]
        public static DemoClass[] MemoryPack()
        {
            return MemoryPackSerializer.Deserialize<DemoClass[]>(RawMemoryPack)!;
        }

        [Benchmark]
        public static DemoClass[] MemoryPackBrotli()
        {
            using var decompressor = new BrotliDecompressor();
            var decompressedBuffer = decompressor.Decompress(RawMemoryPackBrotli);
            return MemoryPackSerializer.Deserialize<DemoClass[]>(decompressedBuffer)!;
        }

    }
}
