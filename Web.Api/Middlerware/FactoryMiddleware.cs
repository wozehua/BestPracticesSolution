
namespace Web.Api.Middlerware
{
    /// <summary>
    /// 基于工厂模式的中间件
    /// </summary>
    public class FactoryMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(next);
            //请求前操作
            await next(context);
            //请求后操作
        }
    }
}
