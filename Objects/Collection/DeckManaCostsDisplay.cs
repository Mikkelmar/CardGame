using CardGame.Graphics;
using CardGame.Objects.Cards;
using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGame.Objects.Collection
{
    public class DeckManaCostsDisplay : GameObject
    {
        public List<ManaCrystalCostFilter> manaCrystal = new List<ManaCrystalCostFilter>();


        Dictionary<int, int> deckCosts = new Dictionary<int, int> {
            {0, 0 },
            {1, 0 },
            {2, 0 },
            {3, 0 },
            {4, 0 },
            {5, 0 },
            {6, 0 },
            {7, 0 },
        };

        public void updateDeck(Game1 g, Dictionary<Card, int> newDeck)
        {
            deckCosts = new Dictionary<int, int> {
                {0, 0 },
                {1, 0 },
                {2, 0 },
                {3, 0 },
                {4, 0 },
                {5, 0 },
                {6, 0 },
                {7, 0 },
            };
            foreach (Card c in newDeck.Keys)
            {
                if(c.BaseCost >= 7)
                {
                    deckCosts[7] += newDeck[c];
                }
                else//add handle to ahndle values not in list
                {
                    deckCosts[c.BaseCost] += newDeck[c];
                }
                
            }
        }
        public override void Destroy(Game1 g)
        {
            foreach(ManaCrystalCostFilter cystal in manaCrystal)
            {
                g.collectionPage.objectManager.Remove(cystal, g);
            }
            manaCrystal.Clear();
        }

        public override void Draw(Game1 g)
        {
            for (int i=0;i<8;i++)
            {
                int maxHeight = 550;
                int higestCount = deckCosts.Values.Max();
                if (higestCount != 0)
                {
                    int height = (maxHeight * deckCosts[i] / higestCount);// 10, maxHeight);//Math.Max(1,deckCosts.Values.Sum());
                    Drawing.FillRect(new Rectangle(4000 - 40 + 120 * i, 20 + (maxHeight - height), 80, height), Color.Orange, 0.1f, g);
                }
                    
            }
        }

        public override void Init(Game1 g)
        {
            for (int i = 0; i < 8; i++)
            {
                string number = $"{i}";
                if (i == 7)
                {
                    number = $"{i}+";
                }
                int filterNumber = i;
                ManaCrystalCostFilter crystal = new ManaCrystalCostFilter(number, 
                    (_g) => _g.collectionPage.collectionManager.filterByMana(_g, filterNumber)
                    ) { 
                    X = 4000 - 60 + 120 * i,
                    Y = 600 - 15,
                    Width = 120,
                    Height = 120
                };
                manaCrystal.Add(crystal);
                g.collectionPage.objectManager.Add(crystal, g);

            }
            
        }

        public override void Update(GameTime gt, Game1 g)
        {
        }
    }
}
