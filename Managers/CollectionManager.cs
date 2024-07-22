using CardGame.HeroPowers;
using CardGame.Managers;
using CardGame.Objects.Cards;
using CardGame.Objects.Collection;
using CardGame.Shared.GameLogic.HeroPowers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CardGame.Objects
{
    public class CollectionManager
    {
        List<CollectionPageDisplay> pages = new List<CollectionPageDisplay>();
        List<CollectionHeroPowerDisplay> heroPages = new List<CollectionHeroPowerDisplay>();
        public DeckBuilder deckBuilder;
        public DeckDisplay deckDiplay;
        List<SetPageButton> gotoFactionPageButtons = new List<SetPageButton>();
        public SearchBar searchBar;
        private DeckNameField deckNameField;
        public int PageCount => pages.Count + heroPages.Count;
        public int PageNumber = 0;
        private Predicate<Card> currentFilter = card => true;
        private Predicate<Card> currentCostFilter = card => true;
        public int filterManaCurrent = -1;
        public bool filterByCanAdd = false;

        public void Init(Game1 g, Deck deck)
        {
            //Hero powers
            heroPages.Add(new CollectionHeroPowerDisplay(
                new List<HeroPower>() {
                    new FireBlast(null),
                    new DrawHeroPower(null),
                    new ArmourHeroPower(null),
                    new SummonDamgedHeroPower(null),
                    new NatureHeal(null),
                    new DamageHeroPower(null),
                    new EquipDaggersPower(null),
                    new AddSkeletonPower(null),
                }
                ));

            deckBuilder = new DeckBuilder();
            DeckBuilder.DeckUpdate += deckUpdate;


            searchBar = new SearchBar()
            {
                X = 4000,
                Y = 750,
                Width = 800,
                Height = 190
            };
            g.collectionPage.objectManager.Add(searchBar, g);

            deckNameField = new DeckNameField(deck.Name)
            {
                X = 100,
                Y = 20,
                Width = 700,
                Height = 100
            };
            g.collectionPage.objectManager.Add(deckNameField, g);
            
            LoadPages(g);

            if (deck != null)
            {
                deckBuilder.SetDeck(g, g.collectionPage.cardManager.LoadDeckFromString(deck.DeckContents));
                deckBuilder.SetHeroPower(g, HeroPowerManager.getHeroPower(deck.HeroPower));
                deckBuilder.SetName(g, deck.Name);
            }
            deckDiplay = new DeckDisplay();
            g.collectionPage.objectManager.Add(deckDiplay, g);
            deckDiplay.updateDeck(g, deckBuilder.getDeck());
        }

        public void LoadPages(Game1 g)
        {
            // Close existing pages
            CloseAllPages(g);
            foreach (SetPageButton factionButton in gotoFactionPageButtons)
            {
                g.collectionPage.objectManager.Remove(factionButton, g);
            }
            gotoFactionPageButtons.Clear();
            pages.Clear();

            foreach (Card.Faction faction in Enum.GetValues(typeof(Card.Faction)))
            {
                List<Card> cards;
                if (faction == Card.Faction.None)
                {
                    continue;
                }
                else
                {
                    cards = g.collectionPage.cardManager.getAllFactionCards(faction);
                }

                cards = cards.FindAll(currentFilter);
                if(filterManaCurrent != -1)
                {
                    cards = cards.FindAll(currentCostFilter);
                }
                if (filterByCanAdd)
                {
                    cards = cards.FindAll((c) => g.collectionPage.collectionManager.deckBuilder.CanAddCard(c));
                }
                
                cards = cards.OrderBy(c => c.BaseCost).ThenBy(c => c.Name).ToList();

                int cardsPerPage = 15;
                int numberOfPages = (cards.Count + cardsPerPage - 1) / cardsPerPage;
                if(numberOfPages > 0)
                {
                    gotoFactionPageButtons.Add(new SetPageButton(pages.Count, pages.Count+ numberOfPages-1, faction) { 
                        X = 900 +150* gotoFactionPageButtons.Count,
                        Y = 20,
                        Width = 130,
                        Height = 130
                    });
                }
                for (int i = 0; i < numberOfPages; i++)
                {
                    int startIndex = i * cardsPerPage;
                    List<Card> pageCards = cards.GetRange(startIndex, Math.Min(cardsPerPage, cards.Count - startIndex));
                    CollectionPageDisplay currentPage = new CollectionPageDisplay(pageCards);
                    pages.Add(currentPage);
                }
            }
            

            // Load the first page
            if (pages.Count > 0)
            {
                PageNumber = Math.Min(PageNumber, pages.Count - 1);
                pages[PageNumber].loadPage(g);
            }
            else
            {
                PageNumber = 0;
                heroPages[PageNumber].loadPage(g);
            }
            foreach(SetPageButton factionButton in gotoFactionPageButtons)
            {
                g.collectionPage.objectManager.Add(factionButton, g);
            }
        }

        public void CloseAllPages(Game1 g)
        {
            foreach (var page in pages)
            {
                page.closePage(g);
            }

            foreach (var heroPage in heroPages)
            {
                heroPage.closePage(g);
            }

            
        }
        public void setPage(Game1 g, int newPageId)
        {
            if (newPageId == PageNumber)
            {
                return;
            }
            if (PageNumber < pages.Count)
            {
                pages[PageNumber].closePage(g);
            }
            else
            {
                heroPages[PageNumber - pages.Count].closePage(g);
            }

            if (newPageId < pages.Count)
            {
                PageNumber = newPageId;
                pages[PageNumber].loadPage(g);
            }
            else
            {
                PageNumber = newPageId;
                heroPages[PageNumber - pages.Count].loadPage(g);
            }
        }

        public void newPage(Game1 g, int i)
        {
            int newPageId = Math.Min(Math.Max(0, PageNumber + i), PageCount - 1);
            setPage(g, newPageId);
        }

        public void Destroy(Game1 g)
        {
            foreach (CollectionPageDisplay p in pages)
            {
                g.collectionPage.objectManager.Remove(p, g);
            }
            if (deckNameField != null)
            {
                g.collectionPage.objectManager.Remove(deckNameField, g);
            }
            if (searchBar != null)
            {
                g.collectionPage.objectManager.Remove(searchBar, g);
            }
            if (deckDiplay != null)
            {
                g.collectionPage.objectManager.Remove(deckDiplay, g);
            }
            DeckBuilder.DeckUpdate -= deckUpdate;
        }

    
    public void filterByMana(Game1 g, int number)
    {
        if(filterManaCurrent == number)
        {
            filterManaCurrent = -1;
            currentCostFilter = (c) => true;
        }
        else
        {
            filterManaCurrent = number;
            if (number == 7)
            {
                currentCostFilter = (c) => c.BaseCost >= number;
            }
            else
            {
                currentCostFilter = (c) => c.BaseCost == number;
            }
        }
        LoadPages(g);
    }

    public void filterByText(Game1 g, string text)
    {
        string lowerText = text.ToLower();

        UpdateFilter((c) => {
            string allText = "";
            allText += c.getText(g);
            allText += c.Name;
            allText += c.grade;
            allText += c.cardType;
            allText += "cost:" + c.BaseCost;
            allText += "mana:" + c.BaseCost;
            if (c is MinionCard)
            {
                allText += ((MinionCard)c).tribe;
                allText += "attack:" + ((MinionCard)c).BaseAttack;
                allText += "health:" + ((MinionCard)c).BaseHealth;
            }

            // Remove HTML-like tags
            string plainText = Regex.Replace(allText, "<.*?>", string.Empty);

            // Perform case-insensitive search
            return plainText.ToLower().Contains(lowerText);
        }, g);
    }   

    private void deckUpdate(Game1 g, Dictionary<Card, int> newDeck)
    {
        if (filterByCanAdd)
        {
            LoadPages(g);
        }
    }
    public void UpdateFilter(Predicate<Card> newFilter, Game1 g)
        {
            currentFilter = newFilter;
            LoadPages(g);
        }
    }
}
