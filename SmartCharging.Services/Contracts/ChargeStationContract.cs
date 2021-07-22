using System.Collections.Generic;

namespace SmartCharging.Contract.Services.Contracts
{
    public class ChargeStationContract
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<ConnectorContract> Connectors { get; set; }
    }
}
