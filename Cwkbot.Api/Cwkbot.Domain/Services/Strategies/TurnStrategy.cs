using Cwkbot.Domain.Interfaces;
using Cwkbot.Domain.Models;
using Cwkbot.Domain.Models.Actions;
using Cwkbot.Domain.Utils;
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
            if (isThreeOfAKind.Item1)
            {
                if (betAvailable != null)
                {
                    if (isThreeOfAKind.Item2 >= 36)
                    {
                        bet.Chips = HandUtil.CalculateBetMargin(_handInfo, 10);
                        if (bet.Chips > myPlayer.Chips)
                            bet.Chips = myPlayer.Chips / 4;
                    }
                }
            }

            var isStraight = HandUtil.IsStraight(allCards);
            if (isStraight)
            {
                if (betAvailable != null)
                {
                    bet.Chips = myPlayer.Chips / 6;
                }
            }

            var isFlush = HandUtil.IsFlush(allCards);
            if (isFlush.Item1)
            {
                if (isFlush.Item2 > 12)
                {
                    if (raiseAvailable != null)
                        raise.Chips = myPlayer.Chips;
                    if (betAvailable != null)
                        bet.Chips = myPlayer.Chips;
                }
                else if (isFlush.Item2 <= 12 && isFlush.Item2 > 9)
                {                       
                    if (betAvailable != null)
                    {
                        bet.Chips = HandUtil.CalculateBetMargin(_handInfo, 10);
                        if (bet.Chips > myPlayer.Chips / 10)
                            bet.Chips = myPlayer.Chips / 10;
                    }     
                }
            }
            var isFourOfAKind = HandUtil.IsFourOfAKind(allCards);
            if (isFourOfAKind)
            {
                if (raiseAvailable != null)
                    raise.Chips = myPlayer.Chips;
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
            if (callAvailable != null && (isThreeOfAKind.Item1 == true
                || isFlush.Item1 == true
                || isStraight == true
                || isFullHouse == true))
                return call;
            else if (checkAvailable != null)
                return check;
            else
                return new Fold();
        }
    }
}
