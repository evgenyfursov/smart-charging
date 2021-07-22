using SmartCharging.Contract.Services.Contracts;
using System.Collections.Generic;

namespace SmartCharging.Implementation
{
    public interface IChargeStationService
    {
        IEnumerable<ChargeStationContract> Get();
        int Create(long groupId, string name, IEnumerable<double> connectorMaxCurrents);
        int Update(long id, string name);
        void Delete(long id);
    }
}