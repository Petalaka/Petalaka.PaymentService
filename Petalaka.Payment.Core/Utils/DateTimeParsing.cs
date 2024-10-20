using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petalaka.Payment.Core.Utils
{
    public class DateTimeParsing
    {
        public static DateTimeOffset ConvertToUtcPlus7(DateTimeOffset dateTime)
        {
            return dateTime.ToOffset(new TimeSpan(7, 0, 0));
        }
    }
}
