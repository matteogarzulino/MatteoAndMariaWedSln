using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingServices.Utilities
{
    public static class ExceptionUtilities
    {
        public static string ToCompleteMessage(this Exception exception)
        {
            return string.Format("Eccezione: \r\n{0}\r\n\tStackTrace:{1}\r\n\tInner:{2}", exception.Message, exception.StackTrace, exception.InnerException != null ? exception.InnerException.Message : string.Empty);
        }
    }
}
