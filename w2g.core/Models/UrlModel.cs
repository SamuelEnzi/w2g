using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w2g.core.Models
{
    public class UrlModel : Base.Request
    {
        public string? Url { get; set; }
        public string? Name { get; set; }

        public UrlModel()
        {

        }
    }
}
