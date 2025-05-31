using System.Text;
using Microsoft.Extensions.ObjectPool;

//1. 引入创建对象池策略 
var pooledPolicy = new DefaultPooledObjectPolicy<StringBuilder>();

//创建对象池
var objectPool = new DefaultObjectPool<StringBuilder>(pooledPolicy);

//3.从池中获取对象

var stringBuilder = objectPool.Get();

try
{
    //使用对象
    stringBuilder.Append("Hello");
    stringBuilder.Append(" World");
    Console.WriteLine(stringBuilder.ToString());
}
finally
{
    //4.将对象返回到池中
    objectPool.Return(stringBuilder);

}
Console.ReadKey();