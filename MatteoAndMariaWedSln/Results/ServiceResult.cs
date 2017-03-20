using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MatteoAndMariaWedSln.Results
{
    public class ServiceResult
    {
        public bool Esito { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public object Content { get; set; }

        public ServiceResult(bool esito, string message)
        {
            this.Esito = esito;
            this.Message = message;
        }

        public ServiceResult(bool esito, string message, Exception exc)
        {
            this.Esito = esito;
            this.Message = message;
            this.Exception = exc;
        }

        public ServiceResult(bool esito, string message, object content)
        {
            this.Esito = esito;
            this.Message = message;
            this.Content = content;
        }
    }
}