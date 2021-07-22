using SmartCharging.Data.Entities;
using System.Collections.Generic;

namespace SmartCharging.Data.Interfaces
{
    public interface IConnectorRepository
    {
        IEnumerable<Connector> Get();
        int Create(long chargeStationId, double maxCurrent);
        int Update(long id, double maxCurrent); 
        void Delete(long id);
    }
}
