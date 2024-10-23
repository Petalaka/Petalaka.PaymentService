using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Petalaka.Payment.API.Base
{
    [ApiController]
    [Route("api/payment-service/[controller]")]
    [ServiceFilter(typeof(ValidateModelStateAttribute))]
    public abstract class BaseController : ControllerBase
    {
    }
}

