namespace UseMediatR.MediatRModel
{
    /// <summary>
    /// 为用户注册请求 DTO
    /// </summary>
    /// <param name="Email"></param>
    public record UserRegistrationRequest(string Email);
}
