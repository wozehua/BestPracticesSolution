using System.Runtime.CompilerServices;

namespace 使用结构体代替类Benchmarks
{
    public static class FixedHelper
    {

        /// <summary>
        /// MethodImplOptions.AggressiveInlining 是一个属性，用于指示编译器在调用该方法时尽可能地将其内联展开。内联展开是一种优化技术，可以减少方法调用的开销，提高性能。
        /// unsafe 关键字允许在 C# 中使用指针和直接内存访问，这通常用于性能优化或与底层系统交互。在这种情况下，unsafe 代码块允许我们直接操作内存，从而提高性能。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="dest"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void SetTo(this string str, char* dest)
        {

            fixed (char* ptr = str)
            {
                Unsafe.CopyBlock(dest, ptr, (uint)(Unsafe.SizeOf<char>() * str?.Length ?? 0));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe bool SpanEquals(this string str, char* dest, int length)
        {
            return new Span<char>(dest, length).SequenceEqual(str.AsSpan());
        }
    }
}
