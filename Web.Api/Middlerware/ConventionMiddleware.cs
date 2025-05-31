using System;

namespace Web.Api.Middlerware
{
    /// <summary>
    /// 按约定处理请求中间件（By Convention）
    /// </summary>
    public class ConventionMiddleware
    {
        private readonly RequestDelegate _next;

        public ConventionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //执行请求前操作
            await _next(context);
            //执行请求后操作

        }
    }
}
