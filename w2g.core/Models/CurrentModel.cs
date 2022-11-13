using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w2g.core.Models
{
    public class CurrentModel : Base.Request
    {
        public string? Url { get; set; }
        public string? Name { get; set; }
        public int? Seconds { get; set; }

        public CurrentModel()
        {
            
        }
    }
}
