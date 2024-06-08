using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Infastructure.BasicResults
{
    public class BasicResult
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; }


        public BasicResult(string message)
        {
            StatusCode = HttpStatusCode.BadRequest;
            Message = message;
        }

        public BasicResult(HttpStatusCode code, string message)
        {
            StatusCode = code;
            Message = message;
        }

        public BasicResult()
        {
            StatusCode = HttpStatusCode.OK;
        }
    }

}
