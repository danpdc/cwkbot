using Cwkbot.Domain.Enums;
using Cwkbot.Domain.Interfaces;
using Cwkbot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cwkbot.Domain.Services.Strategies
{
    public  class PokerStrategyFactory : IPokerStrategyFactory
    {
        private HandInfo _handInfo;
        public PokerStrategyFactory(HandInfo handInfo)
        {
            _handInfo = handInfo;
        }
        public IPokerStrategy GetStrategy()
        {
            switch (_handInfo.Stage)
            {
                case HandStage.Preflop:
                    return new PreFlopStrategy(_handInfo);
                case HandStage.Flop:
                    return new FlopStrategy(_handInfo);
                case HandStage.Turn:
                    return new TurnStrategy(_handInfo);
                case HandStage.River:
                    return new RiverStrategy(_handInfo);
                default:
                    return null;
            }
        }
    }
}
