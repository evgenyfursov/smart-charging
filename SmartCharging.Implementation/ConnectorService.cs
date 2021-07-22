using SmartCharging.Contract.Services.Contracts;
using SmartCharging.Data.Entities;
using SmartCharging.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SmartCharging.Implementation
{
    public class ConnectorService : IConnectorService
    {
        private readonly IConnectorRepository _connectorRepository;

        public ConnectorService(IConnectorRepository connectorRepository)
        {
            _connectorRepository = connectorRepository;
        }

        public IEnumerable<ConnectorContract> Get()
        {
            return ToGroupContract(_connectorRepository.Get());
        }

        public int Create(long chargeStationId, double maxCurrent)
        {
            return _connectorRepository.Create(chargeStationId, maxCurrent);
        }

        public void Delete(long id)
        {
            _connectorRepository.Delete(id);
        }

        public int Update(long id, double maxCurrent)
        {
            return _connectorRepository.Update(id, maxCurrent);
        }

        // todo create converter or mapper
        private IEnumerable<ConnectorContract> ToGroupContract(IEnumerable<Connector> connectors)
        {
            return connectors?.Select(x => new ConnectorContract
            {
                Id = x.Id,
                MaxCurrent = x.MaxCurrent,
            });
        }
    }
}
