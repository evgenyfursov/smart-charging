using System.Collections.Generic;

namespace SmartCharging.Data.Entities
{
    public class ChargeStation : BaseEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public long GroupId { get; set; }
        public Group Group { get; set; }
        public IEnumerable<Connector> Connectors { get; set; }
    }
}
