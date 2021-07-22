using SmartCharging.Data.Repositories;

namespace SmartCharging.UnitTests
{
    public abstract class BaseTests
    {
        protected readonly FakeDbContext _fakeDbContext;
        protected readonly GroupRepository _groupRepository;
        protected readonly ChargeStationRepository _chargeStationRepository;
        protected readonly ConnectorRepository _connectorRepository;

        protected BaseTests()
        {
            _fakeDbContext = new FakeDbContext();
            _groupRepository = new GroupRepository(_fakeDbContext);
            _chargeStationRepository = new ChargeStationRepository(_fakeDbContext);
            _connectorRepository = new ConnectorRepository(_fakeDbContext);
        }
    }
}
