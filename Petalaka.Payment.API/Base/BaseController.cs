using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Petalaka.Payment.API.Base
{
    [ApiController]
    [Route("api/pet-store-service/[controller]")]
    [ServiceFilter(typeof(ValidateModelStateAttribute))]
    public abstract class BaseController : ControllerBase
    {
    }
}

