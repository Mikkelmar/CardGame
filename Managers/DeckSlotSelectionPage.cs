using CardGame.Managers;
using Engine;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CardGame.Objects.Collection
{
    public class DeckSlotSelectionPage : GameObject
    {
        private List<DeckSlot> deckSlots;

        public DeckSlotSelectionPage(Game1 g)
        {
            
            deckSlots = new List<DeckSlot>();
             
        }

        public override void Draw(Game1 g)
        {
            foreach (var slot in deckSlots)
            {
                slot.Draw(g);
            }
        }


   

        public override void Destroy(Game1 g)
        {
        }

        public override void Init(Game1 g)
        {
            int sizeX = 700, sizeY = 400;
            for (int i = 0; i < 15; i++)
            {
                Deck deck = g.collectionPage.deckManager.LoadDeck($"deck_{i}.json");
                deckSlots.Add(new DeckSlot(i, deck)
                {
                    X = 100 + (i % 5) * (sizeX + 100),
                    Y = 100 + (i / 5) * (sizeY + 100 + 100),
                    Width = sizeX,
                    Height = sizeY
                });
            }

            foreach (DeckSlot slot in deckSlots)
            {
                slot.Init(g);
            }
        }

        public override void Update(GameTime gt, Game1 g)
        {
        }
    }

    

    
}
