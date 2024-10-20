using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petalaka.Payment.Core.Utils
{
    public class CoreHelper
    {
        public static DateTimeOffset SystemTimeNow => DateTimeParsing.ConvertToUtcPlus7(DateTimeOffset.Now);
        public static IConfiguration GetRootAppSettings => ReadConfiguration.ReadBasePathAppSettings();
        //public static IConfiguration GetDbDesignTimeAppSettings => ReadConfiguration.ReadDbDesignTimeAppSettings();
    }
}
