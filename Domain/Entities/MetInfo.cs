namespace Rems.Domain.Entities
{
    public class MetInfo
    {

        public int MetInfoId { get; set; }

        public int? MetStationId { get; set; }

        public string Variable { get; set; }

        public string Value { get; set; }


        public virtual MetStation MetStation { get; set; }

    }
}
