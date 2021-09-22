using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace e_robot.Application.Exceptions
{
    [Serializable]
    public class HttpStatusException : Exception
    {
        public HttpStatusCode Status { get; private set; }

        public HttpStatusException(HttpStatusCode status, string msg) : base(msg)
        {
            Status = status;
        }
    }
}
