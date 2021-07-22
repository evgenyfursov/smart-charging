using SmartCharging.Contract.Services.Contracts;
using System.Collections.Generic;

namespace SmartCharging.Implementation
{
    public interface IConnectorService
    {
        IEnumerable<ConnectorContract> Get();
        int Create(long chargeStationId, double maxCurrent);
        int Update(long id, double maxCurrent);
        void Delete(long id);
    }
}