namespace SmartCharging.Contract.Services.Contracts.Group
{
    public class GroupUpdateRequest
    {
        public string Name { get; set; }
        public double? Capacity { get; set; }
    }
}