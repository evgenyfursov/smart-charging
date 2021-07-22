using SmartCharging.Data;
using SmartCharging.Data.Entities;
using SmartCharging.UnitTests.Helpers;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SmartCharging.UnitTests
{
    public class ChargeStationRepositoryTests : BaseTests
    {
        public ChargeStationRepositoryTests()
        {
            _fakeDbContext.Repository<Group>().Add(new Group { Id = 2, Name = "group2", Capacity = 111 });
            _fakeDbContext.Repository<Group>().Add(new Group { Id = 3, Name = "group3", Capacity = 122 });

            _fakeDbContext.Repository<ChargeStation>().Add(new ChargeStation { Id = 1, Name = "station1", GroupId = 2 });
            _fakeDbContext.Repository<ChargeStation>().Add(new ChargeStation { Id = 2, Name = "station2", GroupId = 3 });
            _fakeDbContext.Repository<ChargeStation>().Add(new ChargeStation { Id = 3, Name = "station3", GroupId = 3 });

            _fakeDbContext.Repository<Connector>().Add(new Connector { Id = 1, MaxCurrent = 50, ChargeStationId = 1 });
            _fakeDbContext.Repository<Connector>().Add(new Connector { Id = 2, MaxCurrent = 61, ChargeStationId = 1 });
            _fakeDbContext.Repository<Connector>().Add(new Connector { Id = 1, MaxCurrent = 51, ChargeStationId = 2 });
            _fakeDbContext.Repository<Connector>().Add(new Connector { Id = 1, MaxCurrent = 41, ChargeStationId = 3 });
        }

        [Fact]
        public void Getting_all_stations()
        {
            var expected = new List<ChargeStation>() {
                new ChargeStation { Id = 1, Name = "station1", GroupId = 2 },
                new ChargeStation { Id = 2, Name = "station2", GroupId = 3 },
                new ChargeStation { Id = 3, Name = "station3", GroupId = 3 } };

            var actual = _chargeStationRepository.Get();

            Assert.NotNull(actual);
            Assert.Equal(3, actual.Count());
            Assert.Equal(expected, actual, new ObjectComparer<ChargeStation>());
        }

        [Fact]
        public void Creating_new_station()
        {
            var station = new ChargeStation { Name = "newStation", GroupId = 2, 
                Connectors = (new List<Connector> { new Connector { Id = 1, MaxCurrent = 7 }, new Connector { Id = 2, MaxCurrent = 9 }, new Connector { Id = 3, MaxCurrent = 12 } }).AsEnumerable() };

            _chargeStationRepository.Create(2, station.Name, new List<double> { 7, 9, 12 });

            var actual = _fakeDbContext.Repository<ChargeStation>().Query().LastOrDefault();
            Assert.NotNull(actual);
            Assert.Equal(station.Name, actual.Name);
            Assert.Equal(station.GroupId, actual.GroupId);
            Assert.Equal(station.Connectors, actual.Connectors.ToList(), new ObjectComparer<Connector>());
        }

        [Fact]
        public void Creating_new_station_with_capacity_0_throw_exception()
        {
            Assert.Throws<LogicException>(() =>
                _chargeStationRepository.Create(1, null, new List<double>()));
        }

        [Fact]
        public void Updating_station()
        {
            var station = new ChargeStation { Id = 2, Name = "updatedStationName", GroupId = 3  };

            _chargeStationRepository.Update(station.Id, station.Name);

            var actual = _fakeDbContext.Repository<ChargeStation>().Query().FirstOrDefault(x => x.Id == station.Id);
            Assert.NotNull(actual);
            Assert.Equal(station, actual, new ObjectComparer<ChargeStation>());
        }

        [Fact]
        public void Updating_station_with_invalid_id_throw_exception()
        {
            Assert.Throws<LogicException>(() =>
                _chargeStationRepository.Update(8, "updatedStationName"));
        }

        [Fact]
        public void Deleting_station()
        {
            _chargeStationRepository.Delete(1);

            var stations = _fakeDbContext.Repository<ChargeStation>().Query().ToArray();
            var deletedStation = stations.FirstOrDefault(x => x.Id == 1);

            Assert.Null(deletedStation);
            Assert.Equal(2, stations.Count());
        }

        [Fact]
        public void Deleting_station_with_incorrect_id_throw_exception()
        {
            Assert.Throws<LogicException>(() =>
                _groupRepository.Delete(7));
        }

    }
}
