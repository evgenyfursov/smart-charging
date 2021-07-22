using SmartCharging.Data;
using SmartCharging.Data.Entities;
using SmartCharging.UnitTests.Helpers;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SmartCharging.UnitTests
{
    public class ConnectorRepositoryTests : BaseTests
    {
        public ConnectorRepositoryTests()
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
        public void Getting_all_connectors()
        {
            var expected = new List<Connector>() { 
                new Connector { Id = 1, MaxCurrent = 50, ChargeStationId = 1 },
                new Connector { Id = 2, MaxCurrent = 61, ChargeStationId = 1 },
                new Connector { Id = 1, MaxCurrent = 51, ChargeStationId = 2 },
                new Connector { Id = 1, MaxCurrent = 41, ChargeStationId = 3 }};

            var actual = _connectorRepository.Get();

            Assert.NotNull(actual);
            Assert.Equal(expected.Count(), actual.Count());
            Assert.Equal(expected, actual, new ObjectComparer<Connector>());
        }

        [Fact]
        public void Creating_new_connector()
        {
            var connector = new Connector { Id = 1, ChargeStationId = 1, MaxCurrent = 12 };

            _connectorRepository.Create(connector.ChargeStationId, connector.MaxCurrent);

            var actual = _fakeDbContext.Repository<Connector>().Query().LastOrDefault();
            Assert.NotNull(actual);
            Assert.Equal(connector, actual, new ObjectComparer<Connector>());
        }

        [Fact]
        public void Creating_new_connector_with_max_current_0_throw_exception()
        {
            Assert.Throws<LogicException>(() =>
                _connectorRepository.Create(1, 0));
        }

        [Fact]
        public void Creating_new_connector_with_capacity_less_han_sum_of_connectors_amps()
        {
            Assert.Throws<LogicException>(() =>
                _connectorRepository.Create(1, 121));
        }

        [Fact]
        public void Updating_connector()
        {
            var connector = new Connector { Id = 2, MaxCurrent = 12, ChargeStationId = 1 };

            _connectorRepository.Update(connector.Id, connector.MaxCurrent);

            var actual = _fakeDbContext.Repository<Connector>().Query().FirstOrDefault(x => x.Id == connector.Id);
            Assert.NotNull(actual);
            Assert.Equal(connector, actual, new ObjectComparer<Connector>());
        }

        [Fact]
        public void Updating_connector_with_capacity_0_throw_exception()
        {
            Assert.Throws<LogicException>(() =>
                _connectorRepository.Update(1, 0));
        }

        [Fact]
        public void Deleting_connector()
        {
            _connectorRepository.Delete(1);

            var connectors = _fakeDbContext.Repository<Connector>().Query().ToArray();
            var deletedConnectors = connectors.FirstOrDefault(x => x.Id == 1 && x.ChargeStationId == 1);

            Assert.Null(deletedConnectors);
            Assert.Equal(3, connectors.Count());
        }

        [Fact]
        public void Deleting_connector_with_incorrect_id_throw_exception()
        {
            Assert.Throws<LogicException>(() =>
                _connectorRepository.Delete(7));
        }

    }
}
