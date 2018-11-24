using Cwkbot.Api.Models.Dtos;
using Cwkbot.Domain.Interfaces;
using Cwkbot.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwkbot.Api.Controllers
{
    [Route("pokerwars.io")]
    public class PokerwarsController : Controller
    {
        IPokerService _pokerService;
        public PokerwarsController(IPokerService pokerService)
        {
            _pokerService = pokerService;
        }

        [HttpGet]
        [Route("ping")]
        public IActionResult Ping()
        {
            var pong = new PingDto { Pong = true };
            return Ok(pong);
        }

        [HttpPost]
        [Route("play")]
        public IActionResult Play([FromBody] HandInfo handInfo)
        {
            if (ModelState.IsValid)
            {
                var handEval = _pokerService.GetHandEvaluation(handInfo);
                IPokerAction action = handEval.SuggestedAction;
                return Ok(action);
            }
            else
                return BadRequest();
        }
    }
}
