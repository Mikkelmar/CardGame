using CardGame.Objects.Cards;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.Objects.Collection
{
    public class CollectionPageDisplay : GameObject
    {
        List<Card> cards = new List<Card>();
        List<CardCollection_Actor> displayCards = new List<CardCollection_Actor>();
        public CollectionPageDisplay(List<Card> cards)
        {
            this.cards = cards;
            X = 1020;
            Y = 200;
        }
        public override void Destroy(Game1 g)
        {
        }

        public override void Draw(Game1 g)
        {
            
        }
        public void closePage(Game1 g)
        { 
            foreach(CardCollection_Actor displayCards in displayCards)
            {
                g.collectionPage.objectManager.Remove(displayCards, g);
            }
            displayCards.Clear();
        }
        public void loadPage(Game1 g)
        {
            int _cardsInWidth = 5;
            int _cardsInHeight = 3;
            int _cardsmargin = 50;
            
            for (int y = 0; y < _cardsInHeight; y++)
            {
                for (int x = 0; x < _cardsInWidth; x++)
                {
                    if ((y * _cardsInWidth) + x >= cards.Count)
                    {
                        break;
                    }
                    Card card = cards[(y * _cardsInWidth) + x];
                    CardCollection_Actor _newCardDisplay = new CardCollection_Actor(card);
                    _newCardDisplay.X = X + (_newCardDisplay.Width * x + _cardsmargin * x);
                    _newCardDisplay.Y = Y + (_newCardDisplay.Height * y + _cardsmargin * y);
                    displayCards.Add(_newCardDisplay);
                    g.collectionPage.objectManager.Add(_newCardDisplay, g);
                }
            }
        }

        public override void Init(Game1 g)
        {
            
        }

        public override void Update(GameTime gt, Game1 g)
        {
        }
    }
}
