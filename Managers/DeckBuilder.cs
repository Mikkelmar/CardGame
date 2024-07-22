using CardGame.HeroPowers;
using CardGame.Objects;
using CardGame.Objects.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGame.Managers
{
    public class DeckBuilder
    {

        public static event Action<Game1, Dictionary<Card, int>> DeckUpdate;

        private Dictionary<string, List<Card>> cards;

        public HeroPower heroPower;
        public string Name = "[Deck Name]";
        public DeckBuilder()
        {
            cards = new Dictionary<string, List<Card>>();
            heroPower = new FireBlast(null);
        }
        public Dictionary<Card, int> getDeck()
        {
            Dictionary<Card, int> deck = new Dictionary<Card, int>();
            foreach (List<Card> lc in cards.Values)
            {
                deck.Add(lc[0], lc.Count);
            }
            return deck;
        }
        public void SetDeck(Game1 g, List<Card> _cards)
        {
            foreach(Card card in _cards)
            {
                if (cards.ContainsKey(card.ID))
                {
                    cards[card.ID].Add(card);
                }
                else
                {
                    cards[card.ID] = new List<Card> { card };
                }
            }
            SortCardsByBaseCost();
            DeckUpdate?.Invoke(g, getDeck());
        }
        internal int getHeroPower()
        {
            if(heroPower != null)
            {
                return heroPower.ID;
            }
            return 0;
        }
        public void SetName(Game1 g, string name)
        {
            Name = name;
            saveDeck(g);
        }
        public void SetHeroPower(Game1 g, HeroPower heroPower)
        {
            this.heroPower = heroPower;
            DeckUpdate?.Invoke(g, getDeck());
            saveDeck(g);
        }
        public bool AddCard(Game1 g, Card card)
        {
            if (CanAddCard(card))
            {
                if (cards.ContainsKey(card.ID))
                {
                    cards[card.ID].Add(card);
                }
                else
                {
                    cards[card.ID] = new List<Card> { card };
                }
                SortCardsByBaseCost();
                DeckUpdate?.Invoke(g, getDeck());
                saveDeck(g);
                return true;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Cannot add "+card.ID);
                //Console.WriteLine("Cannot add more than 2 cards with the same Id.");

                return false;
            }
        }
        public bool RemoveCard(Game1 g, Card card)
        {
            if (cards.ContainsKey(card.ID))
            {
                var cardList = cards[card.ID];
                if (cardList.Count > 0)
                {
                    cardList.Remove(card);
                    if (cardList.Count == 0)
                    {
                        cards.Remove(card.ID);
                    }
                    DeckUpdate?.Invoke(g, getDeck());
                    saveDeck(g);
                    return true;
                }
            }
            System.Diagnostics.Debug.WriteLine("Cannot remove " + card.ID);
            return false;
        }
        
        public void saveDeck(Game1 g)
        {
            
            Deck _deck = new Deck()
            {
                Name = Name,
                HeroPower = getHeroPower(),
                DeckContents = printCommaSeparatedCardList()
            };
            g.collectionPage.deckManager.SaveDeck($"deck_{g.collectionPage.activeDeckSlot}.json", _deck);
        }

        public int GetCardCount(string id)
        {
            if (cards.ContainsKey(id))
            {
                return cards[id].Count;
            }
            return 0;
        }
        public string printCommaSeparatedCardList()
        {
            var allCards = cards.Values.SelectMany(cardList => cardList);
            string cards_ = string.Join(", ", allCards);
            System.Diagnostics.Debug.WriteLine(cards_);
            return cards_;
        }

        public void SortCardsByBaseCost()
        {
            var sorted = cards.OrderBy(entry => entry.Value.First().BaseCost)
                              .ToDictionary(entry => entry.Key, entry => entry.Value);
            cards = sorted;
        }
        public bool CanAddCard(Card card)
        {
            // Rule 1: No more than 30 cards in total in the deck
            int totalCards = cards.Values.Sum(cardList => cardList.Count);
            if (totalCards >= 30)
            {
                return false;
            }

            // Rule 2: No more than two of any card
            if (cards.ContainsKey(card.ID) && cards[card.ID].Count >= 2)
            {
                return false;
            }

            // Rule 3: Only one copy of each Card.Grade.Legendary card
            if (card.grade == Card.Grade.Legendary && cards.Values.SelectMany(c => c).Any(c => c.grade == Card.Grade.Legendary && c.ID == card.ID))
            {
                return false;
            }

            // Calculate current counts and required counts by faction
            var currentCounts = GetCardCountByFaction();
            var requiredCounts = GetRequiredCardCountByFaction();

            // Temporarily add the card to the current count to check if it would be valid
            if (currentCounts.ContainsKey(card.faction))
            {
                currentCounts[card.faction]++;
            }
            else
            {
                currentCounts[card.faction] = 1;
            }

            // Temporarily add the card to the required count if it's a power card
            if (card.PowerCard)
            {
                requiredCounts[card.faction] = 15;
            }
            else if (currentCounts[card.faction] < 5)
            {
                requiredCounts[card.faction] = 5;
            }

            // Ensure that adding this card does not exceed the 30 card limit with the required counts
            int requiredTotal = requiredCounts.Values.Sum();
            if (requiredTotal > 30)
            {
                return false;
            }

            // Ensure no normal cards from other factions if two power card factions are present
            var powerCardFactions = requiredCounts.Where(rc => rc.Value == 15).Select(rc => rc.Key).ToList();
            if (powerCardFactions.Count == 2 && !powerCardFactions.Contains(card.faction))
            {
                return false;
            }

            return true;
        }

        public Dictionary<Card.Faction, int> GetCardCountByFaction()
        {
            return cards.Values.SelectMany(c => c)
                               .GroupBy(c => c.faction)
                               .ToDictionary(g => g.Key, g => g.Count());
        }

        public Dictionary<Card.Faction, int> GetRequiredCardCountByFaction()
        {
            var requiredCounts = new Dictionary<Card.Faction, int>();
            var currentCounts = GetCardCountByFaction();

            // Determine the required counts based on the current cards
            foreach (var faction in Enum.GetValues(typeof(Card.Faction)).Cast<Card.Faction>())
            {
                int currentCount = currentCounts.ContainsKey(faction) ? currentCounts[faction] : 0;

                // Check if there are any power cards in this faction
                bool hasPowerCard = cards.Values.SelectMany(c => c).Any(c => c.faction == faction && c.PowerCard);

                if (hasPowerCard)
                {
                    requiredCounts[faction] = 15;
                }
                else if (currentCount > 0)
                {
                    requiredCounts[faction] = 5;
                }
                else
                {
                    requiredCounts[faction] = 0;
                }
            }

            return requiredCounts;
        }

        public int GetTotalCardCount()
        {
            return cards.Values.Sum(cardList => cardList.Count);
        }


    }
}
