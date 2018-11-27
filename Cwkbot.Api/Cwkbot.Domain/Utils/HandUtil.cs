using Cwkbot.Domain.Interfaces;
using Cwkbot.Domain.Models;
using Cwkbot.Domain.Models.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cwkbot.Domain.Utils
{
    public static class HandUtil
    {
        public static Tuple<bool, int> IsPair(List<Card> cards)
        {

            var hasPair = cards.GroupBy(card => card.Rank)
                               .Count(group => group.Count() == 2) == 1;
            int sum = 0;
            if (hasPair)
            {
                var paired = cards.GroupBy(card => card.Rank);
                foreach (var key in paired)
                {
                    var keyArray = key.ToArray();
                    if (keyArray.Length == 2)
                        sum = (int)keyArray[0].Rank + (int)keyArray[1].Rank;
                }
            }
            return new Tuple<bool, int>(hasPair, sum);
        }

        public static Tuple<bool, int> IsTwoPairs(List<Card> cards)
        {
            var hasTwoPairs = cards.GroupBy(card => card.Rank)
                                   .Count(group => group.Count() >= 2) == 2;
            var pairs = cards.GroupBy(card => card.Rank);
            int sum = 0;
            foreach (var card in cards)
            {
                sum += (int)card.Rank;
            }
            return new Tuple<bool, int>(hasTwoPairs, sum);
        }

        public static Tuple<bool, int> IsThreeOfAKind(List<Card> cards)
        {
            var hasThreeOfAKind = cards.GroupBy(card => card.Rank)
                                            .Any(group => group.Count() == 3);
            int sum = 0;
            if (hasThreeOfAKind)
            {
                var three = cards.GroupBy(card => card.Rank);
                foreach (var key in three)
                {
                    var keyArray = key.ToArray();
                    if (keyArray.Length == 3)
                        sum = (int)keyArray[0].Rank + (int)keyArray[1].Rank + (int)keyArray[2].Rank;
                }
            }
            return new Tuple<bool, int>(hasThreeOfAKind, sum);
        }

        public static bool IsStraight(List<Card> cards)
        {
            var hasStraight = cards.GroupBy(card => card.Rank)
                                            .Count() == cards.Count()
                                       && cards.Max(card => (int)card.Rank)
                                        - cards.Min(card => (int)card.Rank) == 4;
            return hasStraight;
        }

        public static Tuple<bool, int> IsFlush(List<Card> cards)
        {
            var grouped = cards.GroupBy(card => card.Suit);
            bool hasflush = false;
            int highCard = 0;
            foreach (var group in grouped)
            {
                var groupArray = group.ToArray();
                if (groupArray.Length >= 5)
                {
                    hasflush = true;
                    for (int i = 0; i < groupArray.Length; i++)
                    {
                        if ((int)groupArray[i].Rank > highCard)
                            highCard = (int)groupArray[i].Rank;
                    }
                }
            }
            return new Tuple<bool, int>(hasflush, highCard);
        }

        public static bool IsFourOfAKind(List<Card> cards)
        {
            var hasFourOfAKind = cards.GroupBy(card => card.Rank)
                                            .Any(group => group.Count() == 4);
            return hasFourOfAKind;
        }

        public static bool IsFullHouse(List<Card> cards)
        {
            var hasPair = IsPair(cards);
            var hasThreeOfAKind = IsThreeOfAKind(cards);
            if (hasPair.Item1 == true && hasThreeOfAKind.Item1 == true)
                return true;
            else
                return false;
        }
        public static List<IPokerAction> GetPokerActions(HandInfo hand)
        {
            List<IPokerAction> actions = new List<IPokerAction>();
            actions.Add(new Fold());
            var isCheckPossible = IsCheckPossible(hand.Players);
            if (isCheckPossible)
            {
                actions.Add(new Check());
                actions.Add(new Bet());
            }
            else
            {
                actions.Add(new Call());
                actions.Add(new Raise());
            }


            return actions;
        }

        public static int CalculateBetMargin(HandInfo hand, int percent)
        {
            var myPlayer = hand.Players.Find(p => p.Username == "cwkbot");
            var playingPlayers = hand.Players.Where(p => p.HasFolded == false);
            int neededForCall = 0;
            foreach (var player in playingPlayers)
            {
                if (player.PotContribution > myPlayer.PotContribution)
                    neededForCall = player.PotContribution - myPlayer.PotContribution;
            }
            if (neededForCall == 0)
                return (myPlayer.Chips * percent) / 100;
            else
                return neededForCall + (myPlayer.Chips * 5 / 100);
        }

        private static bool IsCheckPossible(List<Player> players)
        {
            int pot = players[0].PotContribution;
            var playingPlayers = players.Where(p => p.HasFolded == false);
            foreach (var player in playingPlayers)
            {
                if (pot != player.PotContribution)
                    return false;
            }
            return true;
        }
    }
}
