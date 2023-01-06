using System;
using System.Collections.Generic;
using System.Text;

namespace w2g.core.standart.Models.Base
{
    public class Request
    {
        public RequestType RequestType { get; set; } = RequestType.None;
        public Request()
        {

        }
    }
}
