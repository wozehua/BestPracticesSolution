// See https://aka.ms/new-console-template for more information

#define Seria 
#define DeSeria
using BenchmarkDotNet.Running;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using DemoClassProto;
using Google.Protobuf;
using MemoryPack;
using MemoryPack.Compression;
using MessagePack;
using ProtoBuf;
using 序列化协议性能对比Benchmarks;


#if Seria
BenchmarkRunner.Run(typeof(SerializerPerformance));
#endif
#if DeSeria
BenchmarkRunner.Run(typeof(DeserializerPerformance));
#endif
Console.WriteLine($"TextJson:{Encoding.UTF8.GetBytes(SerializerPerformance.TextJsonSerializer()).Length / 1024.0:F}KB");
Console.WriteLine($"ProtoBufDotNet:{SerializerPerformance.ProtoBufDotNet() / 1024.0:F}KB");
Console.WriteLine($"GoogleProtobuf:{SerializerPerformance.GoogleProtobuf() / 1024.0:F}KB");
Console.WriteLine($"MessagePack:{SerializerPerformance.MessagePack() / 1024.0:F}KB");
Console.WriteLine($"MessagePackLz4Block:{SerializerPerformance.MessagePackLz4Block() / 1024.0:F}KB");
Console.WriteLine($"MessagePackLz4BlockArray:{SerializerPerformance.MessagePackLz4BlockArray() / 1024.0:F}KB");
Console.WriteLine($"MemoryPack:{SerializerPerformance.MemoryPack() / 1024.0:F}KB");
Console.WriteLine($"MemoryPackBrotli:{SerializerPerformance.MemoryPackBrotli() / 1024.0:F}KB");



