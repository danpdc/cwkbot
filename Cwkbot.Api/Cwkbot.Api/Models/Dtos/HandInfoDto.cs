using Cwkbot.Domain.Enums;
using Cwkbot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwkbot.Api.Models.Dtos
{
    public class HandInfoDto
    {
        public int SmallBlind { get; set; }
        public int BigBlind { get; set; }
        public List<CardDto> YourCards { get; set; }
        public List<CardDto> TableCards { get; set; }
        public List<PlayerDto> Players { get; set; }

        public HandInfo GenerateHandInfoModel(HandInfoDto dto)
        {
            int smallBlind = dto.SmallBlind;
            int bigBlind = dto.BigBlind;
            List<Card> yourCards = GenerateCardsModel(dto.YourCards);
            List<Card> tableCards = GenerateCardsModel(dto.TableCards);
            List<Player> players = GetPlayerModels(dto.Players);
            return new HandInfo(smallBlind, bigBlind, yourCards, tableCards, players);
        }

        private List<Card> GenerateCardsModel(List<CardDto> listOfCardsDto)
        {
            List<Card> yourCardsModel = new List<Card>();
            foreach (var card in listOfCardsDto)
            {
                Card cardModel = new Card();
                cardModel.Rank = (CardRank)Enum.Parse(typeof(CardRank), card.Rank, true);
                cardModel.Suit = (CardSuit)Enum.Parse(typeof(CardSuit), card.Suit, true);
                yourCardsModel.Add(cardModel);
            }
            return yourCardsModel;
        }

        private List<Player> GetPlayerModels(List<PlayerDto> dtos)
        {
            List<Player> playerModels = new List<Player>();
            foreach (var player in dtos)
            {
                Player newPlayer = player.GetPlayerModel(player);
                playerModels.Add(newPlayer);
            }
            return playerModels;
        }
    }
}
