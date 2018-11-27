using Cwkbot.Domain.Interfaces;
using Cwkbot.Domain.Models;
using Cwkbot.Domain.Models.Actions;
using Cwkbot.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cwkbot.Domain.Services.Strategies
{
    public class RiverStrategy : IPokerStrategy
    {
        private HandInfo _handInfo;
        public RiverStrategy(HandInfo handInfo)
        {
            _handInfo = handInfo;
        }
        public IPokerAction Evaluate()
        {
            var actions = HandUtil.GetPokerActions(_handInfo);
            var myPlayer = _handInfo.Players.Find(p => p.Username == "cwkbot");
            var raiseAvailable = actions.Find(a => a.Action == "raise");
            var betAvailable = actions.Find(a => a.Action == "bet");
            var callAvailable = actions.Find(a => a.Action == "call");
            var checkAvailable = actions.Find(a => a.Action == "check");
            Raise raise = new Raise();
            Bet bet = new Bet();
            Call call = new Call();
            Check check = new Check();
            List<Card> allCards = new List<Card>();
            foreach (var card in _handInfo.YourCards)
                allCards.Add(card);
            foreach (var card in _handInfo.TableCards)
                allCards.Add(card);

            var isTwoPairs = HandUtil.IsTwoPairs(allCards);
            var isPair = HandUtil.IsPair(allCards);
            var isThreeOfAKind = HandUtil.IsThreeOfAKind(allCards);
            var isStraight = HandUtil.IsStraight(allCards);

            var isFlush = HandUtil.IsFlush(allCards);
            if (isFlush.Item1)
            {
                if (isFlush.Item2 >= 11)
                {
                    if (betAvailable != null)
                        bet.Chips = myPlayer.Chips;
                }
            }
            var isFourOfAKind = HandUtil.IsFourOfAKind(allCards);
            if (isFourOfAKind)
            {
                if (betAvailable != null)
                    bet.Chips = myPlayer.Chips;
            }
            var isFullHouse = HandUtil.IsFullHouse(allCards);
            if (isFullHouse)
            {
                if (betAvailable != null)
                    bet.Chips = myPlayer.Chips;
            }

            if (raise.Chips > 0)
                return raise;
            else if (bet.Chips > 0)
                return bet;
            else if (callAvailable != null && (isFlush.Item1 == true
                || isStraight == true
                || isFourOfAKind == true
                || isFullHouse == true))
                return call;
            else if (checkAvailable != null)
                return check;
            else
                return new Fold();
        }
    }
}
