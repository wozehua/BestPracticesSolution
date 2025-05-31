using Microsoft.VisualBasic;

namespace CommonUtils
{
    /// <summary>
    /// Task extensions.    
    /// 
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        ///  超时取消任务异步操作
        ///  应用场景：
        ///  网络请求超时，需要取消请求，释放资源
        ///  数据库连接超时，需要释放资源
        ///  业务超时，需要取消业务操作，释放资源
        /// </summary>
        /// <param name="task"></param>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        /// <exception cref="TimeoutException"></exception>
        public static async Task TimeoutAfter(this Task task, TimeSpan timeout)
        {
            using var cts = new CancellationTokenSource();
            var completedTask = await Task.WhenAny(task, Task.Delay(timeout, cts.Token));
            if (completedTask == task)
            {
                await cts.CancelAsync();
                await task;
            }
            else
            {
                throw new TimeoutException("操作超时");
            }
        }
        /// <summary>
        /// 超时取消任务异步操作 返回结果
        /// 应用场景：
        /// 网络请求超时，需要取消请求，释放资源
        /// 数据库连接超时，需要释放资源
        /// 业务超时，需要取消业务操作，释放资源
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="task"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        /// <exception cref="TimeoutException"></exception>
        public static async Task<TResult> TimeoutAfter<TResult>(this Task<TResult> task, TimeSpan timeout)
        {
            using var cts = new CancellationTokenSource();
            var completedTask = await Task.WhenAny(task, Task.Delay(timeout, cts.Token));
            if (completedTask == task)
            {
                await cts.CancelAsync();
                return await task;
            }
            else
            {
                throw new TimeoutException("操作超时");
            }
        }
    }
}
