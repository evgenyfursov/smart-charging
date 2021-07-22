using System.Collections.Generic;

namespace SmartCharging.Data.Entities
{
    public class Group : BaseEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Capacity { get; set; }

        public IEnumerable<ChargeStation> Stations { get; set; }
    }
}
