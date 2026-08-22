using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonUtils
{
    // 错误码用枚举（值类型）
    public enum UserError
    {
        NotFound,
        InvalidId,
        Deactivated
    }

    // 通用 Result —— 升级为 record struct（仍然是值类型，零堆分配）
    public readonly record struct Result<TValue, TError> where TError : struct where TValue : class
    {
        private readonly TValue? _value;
        private readonly TError? _error;
        private readonly bool _isSuccess;

        private Result(TValue value) { _value = value; _error = null; _isSuccess = true; }
        private Result(TError error) { _value = default; _error = error; _isSuccess = false; }

        public bool IsSuccess => _isSuccess;
        public TValue Value => _isSuccess ? _value! : throw new InvalidOperationException("失败时不可取值");
        public TError Error => !_isSuccess ? _error!.Value : throw new InvalidOperationException("成功时无错误");

        // 隐式转换保持零开销
        //隐式转换让代码更简洁
        //编译器自动调用 implicit operator 方法，包装成 Result。
        public static implicit operator Result<TValue, TError>(TValue value) => new(value);
        public static implicit operator Result<TValue, TError>(TError error) => new(error);

        // Named alternatives to satisfy CA2225 (provide a method alongside implicit operators)
        public Result<TValue, TError> ToResult(TValue value) => new(value);
        public Result<TValue, TError> FromTError(TError error) => new(error);
    }

    public static class UserService
    {
        public static Result<User, UserError> GetUser(Guid id)
        {
            if (id == Guid.Empty)
                return UserError.InvalidId;  // 隐式转换为 Result
            // 编译器自动调用 implicit operator，包装成 Result

            var user = FindUser(id);
            if (user is null)
                return UserError.NotFound; // 编译器自动包装

            if (user.IsDeactivated)
                return UserError.Deactivated;

            return user;  // 隐式转换为 Result  // 编译器自动包装（注意：User 是引用类型）
        }
        public static User FindUser(Guid id)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                IsDeactivated = false
            };
        }
    }
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsDeactivated { get; set; }
    }
    
}
