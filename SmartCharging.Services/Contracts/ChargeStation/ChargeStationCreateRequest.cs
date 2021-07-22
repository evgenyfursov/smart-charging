using System.Collections.Generic;

namespace SmartCharging.Contract.Services.Contracts.ChargeStation
{
    public class ChargeStationCreateRequest
    {
        public string Name { get; set; }
        public long GroupId { get; set; }
        public IEnumerable<double> ConnectorMaxCurrents { get; set; }
    }
}