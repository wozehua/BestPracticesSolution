using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using CommonUtils.SerializeUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 序列化协议性能对比Benchmarks
{
    [RPlotExporter]
    [GcForce(true)]
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public static class SerializerPerformance
    {
        [Benchmark(Baseline = true)]
        public static string TextJsonSerializer()
        {
            return SerializerUtils.TextJsonSerializer(TestData.Origin);
        }
        [Benchmark]
        public static long ProtoBufDotNet()
        {
            using var stream = new MemoryStream(409600);
            SerializerUtils.ProtoBufDotNet(TestData.Origin, stream);
            return stream.Length;
        }

        [Benchmark]
        public static long MessagePack()
        {
            var bytes = SerializerUtils.MessagePack(TestData.Origin);
            return bytes.Length;
        }

        [Benchmark]
        public static long MessagePackLz4Block()
        {
            return SerializerUtils.MessagePackLz4Block(TestData.Origin).Length;
        }

        [Benchmark]
        public static long MessagePackLz4BlockArray()
        {
            return SerializerUtils.MessagePackLz4BlockArray(TestData.Origin).Length;
        }

        [Benchmark]
        public static long MemoryPack()
        {
            return SerializerUtils.MemoryPack(TestData.Origin).Length;
        }

        [Benchmark]
        public static long MemoryPackBrotli()
        {
            return SerializerUtils.MemoryPackBrotli(TestData.Origin).Length;
        }

        [Benchmark]
        public static long GoogleProtobuf()
        {
            return SerializerUtils.GoogleProtobuf(TestData.OriginProto).Length;
        }
    }
}
