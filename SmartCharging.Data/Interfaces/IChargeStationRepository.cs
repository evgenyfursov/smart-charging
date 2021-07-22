using SmartCharging.Data.Entities;
using System.Collections.Generic;

namespace SmartCharging.Data.Interfaces
{
    public interface IChargeStationRepository
    {
        IEnumerable<ChargeStation> Get();
        int Create(long groupId, string name, IEnumerable<double> connectorMaxCurrents);
        int Update(long id, string name);
        void Delete(long id);
    }
}
