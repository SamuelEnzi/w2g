using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w2g.core.Models.Base
{
    public class Request
    {
        public RequestType RequestType { get; set; } = RequestType.None;

        public Request()
        {

        }
    }
}
