using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Petalaka.Payment.Core.CustomException
{
    public class CoreException : Exception
    {
        public CoreException(int statusCode, string errorMessage)
            : base(errorMessage)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }

        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }
        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; set; }
    }
}
