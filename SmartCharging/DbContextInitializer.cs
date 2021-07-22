using SmartCharging.Data.DbContext;
using SmartCharging.Data.Entities;

namespace SmartCharging.API
{
    public static class DbContextInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Repository<Connector>().Add(new Connector { Id = 1, MaxCurrent = 10, ChargeStationId = 1 });
            context.Repository<Connector>().Add(new Connector { Id = 2, MaxCurrent = 11, ChargeStationId = 2 });
            context.Repository<Connector>().Add(new Connector { Id = 3, MaxCurrent = 12, ChargeStationId = 1 });

            context.Repository<ChargeStation>().Add(new ChargeStation { Id = 1, Name = "cs1", GroupId = 1 });
            context.Repository<ChargeStation>().Add(new ChargeStation { Id = 2, Name = "cs2", GroupId = 1 });

            context.Repository<Group>().Add(new Group { Id = 1, Name = "gr1", Capacity = 100 });
            context.Repository<Group>().Add(new Group { Id = 2, Name = "gr2", Capacity = 120 });
            context.Repository<Group>().Add(new Group { Id = 3, Name = "gr3", Capacity = 140 });
            
            context.SaveChanges();
        }
    }
}
