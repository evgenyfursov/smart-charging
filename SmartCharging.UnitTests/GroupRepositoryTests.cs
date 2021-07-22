using SmartCharging.Data;
using SmartCharging.Data.Entities;
using SmartCharging.UnitTests.Helpers;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SmartCharging.UnitTests
{
    public class GroupRepositoryTests : BaseTests
    {
        public GroupRepositoryTests()
        {
            _fakeDbContext.Repository<Group>().Add(new Group
            {
                Id = 1,
                Name = "group1",
                Capacity = 100,
                Stations = new List<ChargeStation>() 
                    { new ChargeStation { Id = 5, Name = "station5", Connectors = 
                    new List<Connector>() { new Connector { Id = 7, MaxCurrent = 77 } } } }
            });
            _fakeDbContext.Repository<Group>().Add(new Group { Id = 2, Name = "group2", Capacity = 111 });
            _fakeDbContext.Repository<Group>().Add(new Group { Id = 3, Name = "group3", Capacity = 122 });
        }

        [Fact]
        public void Getting_all_groups()
        {
            var expected = new List<Group>() {
                new Group {
                Id = 1,
                Name = "group1",
                Capacity = 100,
                Stations = new List<ChargeStation>()
                    { new ChargeStation { Id = 5, Name = "station5", Connectors =
                    new List<Connector>() { new Connector { Id = 7, MaxCurrent = 77 } } } }
                },
                new Group { Id = 2, Name = "group2", Capacity = 111 },
                new Group { Id = 3, Name = "group3", Capacity = 122 }
            };

            var actual = _groupRepository.Get();

            Assert.NotNull(actual);
            Assert.Equal(3, actual.Count());
            Assert.Equal(expected, actual, new ObjectComparer<Group>());
        }

        [Fact]
        public void Creating_new_group()
        {
            var group = new Group { Name = "newGroup", Capacity = 212 };

            _groupRepository.Create(group.Name, group.Capacity);

            var actual = _fakeDbContext.Repository<Group>().Query().LastOrDefault();
            Assert.NotNull(actual);
            Assert.Equal(group, actual, new ObjectComparer<Group>());
        }

        [Fact]
        public void Creating_new_group_with_capacity_0_throw_exception()
        {
            Assert.Throws<LogicException>(() =>
                _groupRepository.Create("newGroup", 0));
        }

        [Fact]
        public void Updating_group()
        {
            var group = new Group { Id = 2, Name = "updatedGroupName", Capacity = 222 };

            _groupRepository.Update(group.Id, group.Name, group.Capacity);

            var actual = _fakeDbContext.Repository<Group>().Query().FirstOrDefault(x => x.Id == group.Id);
            Assert.NotNull(actual);
            Assert.Equal(group, actual, new ObjectComparer<Group>());
        }

        [Fact]
        public void Updating_group_with_capacity_0_throw_exception()
        {
            Assert.Throws<LogicException>(() =>
                _groupRepository.Update(1, "updatedGroupName", 0));
        }

        [Fact]
        public void Deleting_group()
        {
            _groupRepository.Delete(1);

            var groups = _fakeDbContext.Repository<Group>().Query().ToArray();
            var deletedGroup = groups.FirstOrDefault(x => x.Id == 1);

            Assert.Null(deletedGroup);
            Assert.Equal(2, groups.Count());
        }

        [Fact]
        public void Deleting_group_with_incorrect_id_throw_exception()
        {
            Assert.Throws<LogicException>(() =>
                _groupRepository.Delete(7));
        }

    }
}
