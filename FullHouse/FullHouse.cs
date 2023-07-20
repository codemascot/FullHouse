﻿using FullHouse.Interface;
using System.Collections.Concurrent;


namespace FullHouse
{
    public sealed class FullHouse : IFullHouse
    {
        public string[] suits { get; private set; }
        public string[] ranks { get; private set; }
        public HashSet<string> cards { get; private set; }
        public ConcurrentDictionary<string, int> cardRanks { get; private set; }

        public FullHouse()
        {
            suits = new string[] {"S", "H", "D", "C"};
            ranks = new string[] {"A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"};
            cards = new HashSet<string>();
            cardRanks = new ConcurrentDictionary<string, int>();
        }

        private HashSet<string> ValidateCardsInput(string[] cardsToValidate)
        {
            if (cardsToValidate.Count() != 5)
            {
                throw new ArgumentException("Exactly 5 cards required!");
            }

            HashSet<string> validatedCards = cardsToValidate.ToHashSet();

            if (validatedCards.Count != 5)
            {
                throw new ArgumentException("Contains a duplicate card!");
            }

            return validatedCards;
        }

        private string GetCardRank(string card)
        {
            string[] card_suit = card.Split(';');

            if (card_suit.Count() != 2 ||
                Array.IndexOf(suits, card_suit[1]) == -1 ||
                Array.IndexOf(ranks, card_suit[0]) == -1)
            {
                throw new ArgumentException(String.Format("{0} is invalid!", card));
            }

            return card_suit[0];
        }
        
        public string Hand(string[] cardsInput)
        {
            cards = ValidateCardsInput(cardsInput);

            foreach (string card in cards)
            {
                cardRanks.AddOrUpdate(GetCardRank(card), 1, (key, oldVal) => oldVal + 1);
            }

            if (cardRanks.Count != 2)
            {
                return "NEITHER";   
            }

            if (cardRanks.Values.Contains(4))
            {
                return "FOUR_OF_A_KIND";    
            }

            return "FULL_HOUSE";
        }
    }   
}
