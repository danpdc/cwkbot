using Cwkbot.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cwkbot.Domain.Models.Actions
{
    public class Call : IPokerAction
    {
        private const string CALL = "call";
        public Call()
        {
            Action = CALL;
        }
        public string Action { get;  private set; }
    }
}
