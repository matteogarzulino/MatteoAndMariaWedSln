using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingServices.Utilities.Logging
{
    public static class LogUtilities
    {
        public static void WriteToLog(this Exception exception)
        {
            Trace.TraceError("Message: " + exception.Message);
            Trace.TraceError("Stack: " + exception.StackTrace);
            WriteInnerException(exception);
        }

        private static void WriteInnerException(Exception exception)
        {
            if (exception.InnerException != null)
            {
                Trace.TraceError("Inner Exception Message: " + exception.InnerException.Message);
                Trace.TraceError("Inner Exception Stack: " + exception.InnerException.StackTrace);
                WriteInnerException(exception.InnerException);
            }
        }
    }
}
