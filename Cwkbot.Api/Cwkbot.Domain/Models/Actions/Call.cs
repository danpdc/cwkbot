using Cwkbot.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cwkbot.Domain.Models.Actions
{
    public class Call : IPokerAction
    {
        public string Action { get; set; }
    }
}
