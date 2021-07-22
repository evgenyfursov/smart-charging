using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartCharging.Contract.Services.Contracts;
using SmartCharging.Contract.Services.Contracts.Group;
using SmartCharging.Implementation;
using System.Collections.Generic;

namespace SmartCharging.Controllers
{
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly ILogger<GroupController> _logger;
        private readonly IGroupService _groupService;

        public GroupController(ILogger<GroupController> logger,
            IGroupService groupService)
        {
            _logger = logger;
            _groupService = groupService;
        }

        [HttpGet]
        [Route("groups")]
        public IEnumerable<GroupContract> Get()
        {
            return _groupService.Get();
        }

        [HttpPost]
        [Route("groups")]
        public ActionResult Create([FromBody] GroupCreateRequest model)
        {
            return Ok(_groupService.Create(model.Name, model.Capacity));
        }

        [HttpPut]
        [Route("groups/{id:long}")]
        public ActionResult Update(long id, GroupUpdateRequest model)
        {
            return Ok(_groupService.Update(id, model.Name, model.Capacity));
        }

        [HttpDelete]
        [Route("groups/{id:long}")]
        public ActionResult Delete(long id)
        {
            _groupService.Delete(id);
            return Ok();
        }
    }
}
