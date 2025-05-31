using DemoClassProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using 序列化协议性能对比Benchmarks.Model;

namespace 序列化协议性能对比Benchmarks
{
    public static class TestData
    {
        public static readonly IReadOnlyList<DemoClass> Origin = Enumerable.Range(0, 10000).Select(i =>
        {
            return new DemoClass
            {
                P1 = i,
                P2 = i % 2 == 0,
                P3 = $"Hello World {i}",
                P4 = i,
                P5 = i,
                Subs =
                       [
                new() {P1 = i, P2 = i % 2 == 0, P3 = $"Hello World {i}", P4 = i, P5 = i,},
                    new() {P1 = i, P2 = i % 2 == 0, P3 = $"Hello World {i}", P4 = i, P5 = i,},
                    new() {P1 = i, P2 = i % 2 == 0, P3 = $"Hello World {i}", P4 = i, P5 = i,},
                    new() {P1 = i, P2 = i % 2 == 0, P3 = $"Hello World {i}", P4 = i, P5 = i,},
                       ]
            };
        }).ToArray();

        public static readonly DemoClassArrayProto OriginProto = InitializeOriginProto();

        private static DemoClassArrayProto InitializeOriginProto()
        {
            var originProto = new DemoClassArrayProto();
            for (int i = 0; i < Origin.Count; i++)
            {
                originProto.DemoClass.Add(DemoClassProto.DemoClassProto.Parser.ParseJson(JsonSerializer.Serialize(Origin[i])));
            }
            return originProto;
        }
    }
}
