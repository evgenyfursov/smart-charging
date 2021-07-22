using SmartCharging.Data.DbContext;
using SmartCharging.Data.Entities;
using SmartCharging.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SmartCharging.Data.Repositories
{
    public class ConnectorRepository : IConnectorRepository
    {
        private const int MaxConnectorCount = 5;

        private readonly IDbContext _context;

        public ConnectorRepository(IDbContext context)
        {
            _context = context;
        }

        public int Create(long chargeStationId, double maxCurrent)
        {
            if (maxCurrent == 0)
                throw new LogicException($"Max Current cannot be 0.");

            var station = _context.Repository<ChargeStation>().Query()
                .FirstOrDefault(x => x.Id == chargeStationId);
            if (station == null)
                throw new LogicException($"Charge Station not found (Id = {chargeStationId}).");

            var group = _context.Repository<Group>().Query().FirstOrDefault(x => x.Id == station.GroupId);
            if (group == null)
                throw new LogicException($"Group not found (Id = {station.GroupId}).");

            var groupCapacity = group.Stations?.Sum(x => x.Connectors?.Sum(y => y.MaxCurrent) ?? 0) ?? 0;
            if (groupCapacity + maxCurrent > group.Capacity)
                throw new LogicException($"Group capacity should be greater or equal than sum of connector amps");

            var lastConnectorId = station.Connectors?.Max(x => x.Id) ?? 0; 
            if (lastConnectorId >= MaxConnectorCount)
                throw new LogicException($"Charge Station already has {MaxConnectorCount} connectors.");

            var item = new Connector
            {
                Id = lastConnectorId + 1,
                MaxCurrent = maxCurrent,
                ChargeStationId = chargeStationId
            };
            _context.Repository<Connector>().Add(item);
            return _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var connector = _context.Repository<Connector>().Query().FirstOrDefault(x => x.Id == id);
            if (connector == null)
                throw new LogicException($"Connector not found (Id = {id}).");

            var chargeStation = _context.Repository<ChargeStation>().Query()
                .FirstOrDefault(x => x.Id == connector.ChargeStationId);
            if (chargeStation == null)
                throw new LogicException($"Charge Station not found (Id = {connector.ChargeStationId}).");
            if (chargeStation.Connectors?.Count() <= 1)
                throw new LogicException($"Charge Station should has at least one connector.");

            _context.Repository<Connector>().Remove(connector);
            _context.SaveChanges();
        }

        public IEnumerable<Connector> Get()
        {
            return _context.Repository<Connector>().Query().ToList();
        }

        public int Update(long id, double maxCurrent)
        {
            if (maxCurrent == 0)
                throw new LogicException($"Max Current cannot be 0.");

            var connector = _context.Repository<Connector>().Query().FirstOrDefault(x => x.Id == id);
            if (connector == null)
                throw new LogicException($"Connector not found (Id = {id}).");

            var chargeStation = _context.Repository<ChargeStation>().Query()
                .FirstOrDefault(x => x.Id == connector.ChargeStationId);
            if (chargeStation == null)
                throw new LogicException($"Charge Station not found (Id = {connector.ChargeStationId}).");

            var group = _context.Repository<Group>().Query().FirstOrDefault(x => x.Id == chargeStation.GroupId);
            if (group == null)
                throw new LogicException($"Group not found (Id = {chargeStation.GroupId}).");


            var groupCapacity = group.Stations?.Sum(x => x.Connectors?.Sum(y => y.MaxCurrent) ?? 0) ?? 0;
            var updatedGroupCapacity = groupCapacity - connector.MaxCurrent + maxCurrent;
            if (groupCapacity < updatedGroupCapacity)
                throw new LogicException($"Group capacity should be greater or equal than sum of connector amps");

            connector.MaxCurrent = maxCurrent;
            return _context.SaveChanges();
        }
    }
}
