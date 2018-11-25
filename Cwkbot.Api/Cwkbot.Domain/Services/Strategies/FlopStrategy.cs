using Cwkbot.Domain.Interfaces;
using Cwkbot.Domain.Models;
using Cwkbot.Domain.Models.Actions;
using Cwkbot.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cwkbot.Domain.Services.Strategies
{
    public class FlopStrategy : IPokerStrategy
    {
        private HandInfo _handInfo;
        public FlopStrategy(HandInfo handInfo)
        {
            _handInfo = handInfo;
        }
        public IPokerAction Evaluate()
        {
            List<Card> allCards = new List<Card>();
            foreach (var card in _handInfo.YourCards)
                allCards.Add(card);
            foreach (var card in _handInfo.TableCards)
                allCards.Add(card);
            var isTwoPairs = HandUtil.IsTwoPairs(allCards);
            if (isTwoPairs.Item1)
            {
                Raise raise = new Raise();
                raise.Chips = HandUtil.CalculateBetMargin(_handInfo, 15);
            }
            var isPair = HandUtil.IsPair(allCards);
            if (isPair.Item2 > 17)
                return new Call();
            return new Check();
        }
    }
}
