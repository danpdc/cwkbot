﻿using Cwkbot.Api.Models.Dtos;
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
        public PokerwarsController()
        {

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
        public IActionResult Play([FromBody] string info)
        {
            string handInfo = info;
            return Ok("{\"action\": \"fold\"}");
        }
    }
}