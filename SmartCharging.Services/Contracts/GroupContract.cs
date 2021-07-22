using System.Collections.Generic;

namespace SmartCharging.Contract.Services.Contracts
{
    public class GroupContract
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Capacity { get; set; }

        public IEnumerable<ChargeStationContract> Stations { get; set; }
    }
}
