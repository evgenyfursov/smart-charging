using SmartCharging.Data.DbContext;
using SmartCharging.Data.Entities;
using SmartCharging.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SmartCharging.Data.Repositories
{
    public class ChargeStationRepository : IChargeStationRepository
    {
        // todo moved to config
        private const int MaxConnectorCount = 5;

        private readonly IDbContext _context;

        public ChargeStationRepository(IDbContext context)
        {
            _context = context;
        }

        public int Create(long groupId, string name, IEnumerable<double> connectorMaxCurrents)
        {
            if (string.IsNullOrEmpty(name) || connectorMaxCurrents == null ||
                !connectorMaxCurrents.Any() || connectorMaxCurrents.Count() > MaxConnectorCount)
                throw new LogicException($"Invalid input data.");

            var currentId = 1;
            var item = new ChargeStation() 
            { 
                Name = name,
                GroupId = groupId,
                Connectors = connectorMaxCurrents.Select(x => new Connector()
                {
                    Id = currentId++,
                    MaxCurrent = x
                }).ToList()
            };
            _context.Repository<ChargeStation>().Add(item);
            return _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var chargeStation = _context.Repository<ChargeStation>().Query().FirstOrDefault(x => x.Id == id);
            if (chargeStation == null)
                throw new LogicException($"Charge Station not found (Id = {id}).");

            _context.Repository<ChargeStation>().Remove(chargeStation);
            _context.SaveChanges();
        }

        public IEnumerable<ChargeStation> Get()
        {
            return _context.Repository<ChargeStation>().Query().ToList();
        }

        public int Update(long id, string name)
        {
            var chargeStation = _context.Repository<ChargeStation>().Query().FirstOrDefault(x => x.Id == id);
            if (chargeStation == null)
                throw new LogicException($"Charge Station not found (Id = {id}).");

            if (!string.IsNullOrEmpty(name))
            {
                chargeStation.Name = name;
            }
            return _context.SaveChanges();
        }
    }
}
