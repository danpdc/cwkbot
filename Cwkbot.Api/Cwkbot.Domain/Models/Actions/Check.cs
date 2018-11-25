using Cwkbot.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cwkbot.Domain.Models.Actions
{
    public class Check : IPokerAction
    {
        private const string CHECK = "check";
        public Check()
        {
            Action = CHECK;
        }
        public string Action { get; private set; }
    }
}
