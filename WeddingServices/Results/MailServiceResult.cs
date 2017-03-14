using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingServices.Utilities.Enums;

namespace WeddingServices.Results
{
    public class MailServiceResult
    {

        public bool Esito { get; private set; }
        public string Message { get; private set; }
        public ResultEnum Result { get; private set; }
        public MailServiceResult(bool esito, string message, ResultEnum result)
        {
            this.Esito = esito;
            this.Message = message;
            this.Result = result;
        }
    }
}
