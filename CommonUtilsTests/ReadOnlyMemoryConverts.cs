using CommonUtils;

namespace CommonUtilsTests
{
    public class ReadOnlyMemoryConvertTests
    {
        /// <summary>
        /// 单元测试 int 类型参数 ReadOnlyMemory 转 byte[]
        /// </summary>
        [Fact]
        public void IntTypeParameterReadOnlyMemoryToByte()
        {
            // Arrange
            var memory = new ReadOnlyMemory<int>([1, 2, 3]);

            // Act
            var bytes = ReadOnlyMemoryConvert.ReadOnlyMemoryToByteArray(memory);

            // Assert
            Assert.Equal(new byte[] { 1, 0, 0, 0, 2, 0, 0, 0, 3, 0, 0, 0 }, bytes);
        }

        /// <summary>
        /// 单元测试 string 类型参数 ReadOnlyMemory 转 byte[]
        /// </summary>
        [Fact]
        public void StringTypeParameterReadOnlyMemoryToByte()
        {
            // Arrange
            var memory = "foo";

            // Act  
            var bytes = ReadOnlyMemoryConvert.ReadOnlyMemoryToByteArray(memory.AsMemory());

            // Assert
            Assert.Equal("66-00-6F-00-6F-00", BitConverter.ToString(bytes));
        }

    }

}
