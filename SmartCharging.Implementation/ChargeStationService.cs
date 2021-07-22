using SmartCharging.Contract.Services.Contracts;
using SmartCharging.Data.Entities;
using SmartCharging.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SmartCharging.Implementation
{
    public class ChargeStationService : IChargeStationService
    {
        private readonly IChargeStationRepository _chargeStationRepository;

        public ChargeStationService(IChargeStationRepository chargeStationRepository)
        {
            _chargeStationRepository = chargeStationRepository;
        }

        public IEnumerable<ChargeStationContract> Get()
        {
            return ToGroupContract(_chargeStationRepository.Get());
        }

        public int Create(long groupId, string name, IEnumerable<double> connectorMaxCurrents)
        {
            return _chargeStationRepository.Create(groupId, name, connectorMaxCurrents);
        }

        public void Delete(long id)
        {
            _chargeStationRepository.Delete(id);
        }

        public int Update(long id, string name)
        {
            return _chargeStationRepository.Update(id, name);
        }

        // todo create converter or mapper
        private IEnumerable<ChargeStationContract> ToGroupContract(IEnumerable<ChargeStation> stations)
        {
            return stations?.Select(x => new ChargeStationContract
            {
                Id = x.Id,
                Name = x.Name,
                Connectors = x.Connectors?.Select(y => new ConnectorContract
                {
                    Id = y.Id,
                    MaxCurrent = y.MaxCurrent
                })
            });
        }
    }
}
