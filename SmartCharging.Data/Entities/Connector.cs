namespace SmartCharging.Data.Entities
{
    public class Connector : BaseEntity
    {
        public long Id { get; set; }
        public double MaxCurrent { get; set; }

        public long ChargeStationId { get; set; }
        public ChargeStation ChargeStation { get; set; }
    }
}
