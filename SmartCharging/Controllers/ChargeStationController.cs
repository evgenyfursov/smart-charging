using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartCharging.Contract.Services.Contracts;
using SmartCharging.Contract.Services.Contracts.ChargeStation;
using SmartCharging.Implementation;
using System.Collections.Generic;

namespace SmartCharging.Controllers
{
    [ApiController]
    public class ChargeStationController : ControllerBase
    {
        private readonly ILogger<GroupController> _logger;
        private readonly IChargeStationService _chargeStationService;

        public ChargeStationController(ILogger<GroupController> logger,
            IChargeStationService chargeStationService)
        {
            _logger = logger;
            _chargeStationService = chargeStationService;
        }

        [HttpGet]
        [Route("charge-station")]
        public IEnumerable<ChargeStationContract> Get()
        {
            return _chargeStationService.Get();
        }

        [HttpPost]
        [Route("charge-station")]
        public ActionResult Create(ChargeStationCreateRequest model)
        {
            return Ok(_chargeStationService.Create(model.GroupId, model.Name, model.ConnectorMaxCurrents));
        }

        [HttpPut]
        [Route("charge-station/{id:long}")]
        public ActionResult Update(long id, ChargeStationUpdateRequest model)
        {
            return Ok(_chargeStationService.Update(id, model.Name));
        }

        [HttpDelete]
        [Route("charge-station/{id:long}")]
        public ActionResult Delete(long id)
        {
            _chargeStationService.Delete(id);
            return Ok();
        }
    }
}
