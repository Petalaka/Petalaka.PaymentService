
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Petalaka.Payment.Repository.Base
{
    public class BaseResponse<T>
    {
        public int StatusCode { get; set; } = StatusCodes.Status200OK;
        public string? Message { get; set; } = "Successful";
        public T? Data { get; set; }
        public BaseResponse(int statusCode, string? message, T? data)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }
        public BaseResponse()
        {
        }
    }
    public class BaseResponse : BaseResponse<object>
    {
        public BaseResponse(int statusCode, string? message, object? data) : base(statusCode, message, data)
        {
        }
        public BaseResponse(int statusCode, string? message) : base(statusCode, message, null)
        {
        }
    }
}
