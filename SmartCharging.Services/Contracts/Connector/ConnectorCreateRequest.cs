namespace SmartCharging.Contract.Services.Contracts.Connector
{
    public class ConnectorCreateRequest
    {
        public long ChargeStationId { get; set; }
        public double MaxCurrent { get; set; }
    }
}