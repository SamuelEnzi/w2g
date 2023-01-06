namespace w2g.core.standart.Models
{
    public class CurrentModel : Base.Request
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public int? Seconds { get; set; }

        public CurrentModel()
        {

        }
    }
}
