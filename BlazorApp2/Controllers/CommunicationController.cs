using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp2.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunicationController : ControllerBase
    {
        private readonly DataState _dataState;

        public CommunicationController(DataState dataState)
        {
            _dataState = dataState;
        }

        [HttpGet("ping")]
        public ActionResult helloWorld()
        {
            return new OkObjectResult("pong");
        }

        [HttpPost("update/{guid}")]
        public async Task<IActionResult> updateDashboard(string guid, DataModel data)
        {
            await _dataState.Update(guid, data);

            return new OkResult();
        }
    }
}