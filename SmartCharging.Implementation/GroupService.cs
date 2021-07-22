using SmartCharging.Contract.Services.Contracts;
using SmartCharging.Data.Entities;
using SmartCharging.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SmartCharging.Implementation
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public IEnumerable<GroupContract> Get()
        {
            return ToGroupContract(_groupRepository.Get());
        }

        public int Create(string name, double capacity)
        {
            return _groupRepository.Create(name, capacity);
        }

        public void Delete(long id)
        {
            _groupRepository.Delete(id);
        }

        public int Update(long id, string name, double? capacity)
        {
            return _groupRepository.Update(id, name, capacity);
        }

        // todo create converter or mapper
        private IEnumerable<GroupContract> ToGroupContract(IEnumerable<Group> groups)
        {
            return groups?.Select(x => new GroupContract
            {
                Id = x.Id,
                Name = x.Name,
                Capacity = x.Capacity,
                Stations = x.Stations?.Select(y => new ChargeStationContract
                {
                    Id = y.Id,
                    Name = y.Name,
                    Connectors = y.Connectors?.Select(z => new ConnectorContract
                    {
                        Id = z.Id,
                        MaxCurrent = z.MaxCurrent
                    })
                })
            });
        }
    }
}
