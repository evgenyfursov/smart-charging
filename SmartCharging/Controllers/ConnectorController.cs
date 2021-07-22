using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartCharging.Contract.Services.Contracts;
using SmartCharging.Contract.Services.Contracts.Connector;
using SmartCharging.Implementation;
using System.Collections.Generic;

namespace SmartCharging.Controllers
{
    [ApiController]
    public class ConnectorController : ControllerBase
    {
        private readonly ILogger<GroupController> _logger;
        private readonly IConnectorService _connectorService;

        public ConnectorController(ILogger<GroupController> logger,
            IConnectorService connectorService)
        {
            _logger = logger;
            _connectorService = connectorService;
        }

        [HttpGet]
        [Route("connectors")]
        public IEnumerable<ConnectorContract> Get()
        {
            return _connectorService.Get();
        }

        [HttpPost]
        [Route("connectors")]
        public ActionResult Create(ConnectorCreateRequest model)
        {
            return Ok(_connectorService.Create(model.ChargeStationId, model.MaxCurrent));
        }

        [HttpPut]
        [Route("connectors/{id:long}")]
        public ActionResult Update(long id, ConnectorUpdateRequest model)
        {
            return Ok(_connectorService.Update(id, model.MaxCurrent));
        }

        [HttpDelete]
        [Route("connectors/{id:long}")]
        public ActionResult Delete(long id)
        {
            _connectorService.Delete(id);
            return Ok();
        }
    }
}
