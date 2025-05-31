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
    /// <summary>
    /// 为了防止时间太久忘了，所以直接用中文命名，可以直观看到这个是什么用途
    /// </summary>
    [MemoryPackable]
    [MessagePackObject]
    [ProtoContract]
    public partial class DemoClass
    {
        /// <summary>
        /// 其中Key是MessagePack的属性，ProtoMember是ProtoBuf的属性
        /// 这样看MemoryPack比MessagePack和ProtoBuf都简单一点
        /// MessagePack的序号是从0开始，ProtoBuf的序号是从1开始.
        /// </summary>
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

        [Key(5)]
        [ProtoMember(6)]
#pragma warning disable CA2227 // 集合属性应为只读
        public required ICollection<DemoSubClass> Subs { get; set; }
#pragma warning restore CA2227 // 集合属性应为只读
    }
}
