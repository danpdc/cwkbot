using Cwkbot.Domain.Interfaces;
using Cwkbot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cwkbot.Domain.Services.Strategies
{
    public class TurnStrategy : IPokerStrategy
    {
        private HandInfo _handInfo;
        public TurnStrategy(HandInfo handInfo)
        {
            _handInfo = handInfo;
        }
        public IPokerAction Evaluate()
        {
            return null;
        }
    }
}
