using SmartCharging.Data.Entities;
using System.Collections.Generic;

namespace SmartCharging.Data.Interfaces
{
    public interface IGroupRepository
    {
        IEnumerable<Group> Get();
        int Create(string name, double capacity);
        int Update(long id, string name, double? capacity); 
        void Delete(long id);
    }
}
