using System;
namespace w2g.core.standart.Models
{
    public class UrlModel : Base.Request
    {
        public string Url { get; set; }
        public string Name { get; set; }

        public UrlModel()
        {

        }
    }
}
