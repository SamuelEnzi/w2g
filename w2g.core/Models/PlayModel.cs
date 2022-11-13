using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w2g.core.Models
{
    public class PlayModel : Base.Request
    {
        public int Seconds { get; set; }

        public PlayModel()
        {

        }
    }
}
