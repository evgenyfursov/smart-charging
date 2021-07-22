using Microsoft.EntityFrameworkCore;
using SmartCharging.Data.DbContext;
using SmartCharging.Data.Entities;
using SmartCharging.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SmartCharging.Data.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly IDbContext _context;

        public GroupRepository(IDbContext context)
        {
            _context = context;
        }

        public int Create(string name, double capacity)
        {
            if (string.IsNullOrEmpty(name) || capacity == 0)
                throw new LogicException($"Invalid input data.");

            var item = new Group() 
            { 
                Name = name,
                Capacity = capacity 
            };
         
            _context.Repository<Group>().Add(item);
            return _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var group = _context.Repository<Group>().Query().Include(x => x.Stations)
                .ThenInclude(x => x.Connectors)
                .FirstOrDefault(x => x.Id == id);

            if (group == null)
                throw new LogicException($"Group not found (Id = {id}).");

            _context.Repository<Group>().Remove(group);
            _context.SaveChanges();
        }

        public IEnumerable<Group> Get()
        {
            var sd2 = _context.Repository<Group>().Query().Include(x => x.Stations).ThenInclude(x => x.Connectors).ToList();

            return _context.Repository<Group>().Query().ToList();
        }

        public int Update(long id, string name, double? capacity)
        {
            var group = _context.Repository<Group>().Query().FirstOrDefault(x => x.Id == id);

            if (group == null)
                throw new LogicException($"Group not found (Id = {id}).");

            if (!string.IsNullOrEmpty(name))
            {
                group.Name = name;
            }

            if (capacity.HasValue)
            {
                if (capacity == 0)
                    throw new LogicException($"Group capacity cannot be 0 (GroupId = {id}).");

                group.Capacity = capacity.Value;
            } 

            return _context.SaveChanges();
        }
    }
}
