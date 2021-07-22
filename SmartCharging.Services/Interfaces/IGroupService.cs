using SmartCharging.Contract.Services.Contracts;
using System.Collections.Generic;

namespace SmartCharging.Implementation
{
    public interface IGroupService
    {
        IEnumerable<GroupContract> Get();
        int Create(string name, double capacity);
        int Update(long id, string name, double? capacity);
        void Delete(long id);
    }
}