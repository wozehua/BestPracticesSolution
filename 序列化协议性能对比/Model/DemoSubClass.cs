using MemoryPack;
using MessagePack;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 序列化协议性能对比Benchmarks.Model
{
    [MemoryPackable]
    [MessagePackObject]
    [ProtoContract]
    public partial class DemoSubClass
    {
        [Key(0)]
        [ProtoMember(1)]
        public int P1 { get; set; }
        [Key(1)]
        [ProtoMember(2)]
        public bool P2 { get; set; }
        [Key(2)]
        [ProtoMember(3)]
        public string P3 { get; set; } = null!;
        [Key(3)]
        [ProtoMember(4)]
        public double P4 { get; set; }

        [Key(4)]
        [ProtoMember(5)]
        public long P5 { get; set; }
    }
}
