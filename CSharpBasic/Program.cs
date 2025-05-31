

var number = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
var memory = new Memory<int>(number);
var span = new Span<int>(number);
Console.WriteLine(memory.Length);
Console.WriteLine(span.Length);