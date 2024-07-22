using CardGame.Graphics;
using CardGame.Managers;
using CardGame.Objects.Cards;
using CardGame.Shared.GameLogic.HeroPowers;
using Engine;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CardGame.Objects.Collection
{
    public class DeckSlot : GameObject, Clickable
    {
        public int Index { get; private set; }
        private Deck deck;
        private Sprite hero;
        public DeckSlot(int index, Deck deck)
        {
            this.deck = deck;
            Index = index;
            hero = HeroPowerManager.getHeroFromHeroPower(deck.HeroPower).sprite;
        }

        public override void Draw(Game1 g)
        {
            int textureWidth = hero.Texture.Width;
            int textureHeight = hero.Texture.Height;

            // Calculate the scaling factor to maintain the aspect ratio
            float scaleFactor = (float)Height/Width;

            // Define the source rectangle to draw the full width of the texture and a proportional part of its height
            Rectangle sourceRectangle = new Rectangle(0, 0, textureWidth, (int)(textureHeight * scaleFactor));

            // Draw the specific part of the texture using hero.Draw
            hero.Draw(
                position,
                Width,
                Height,
                layerDepth: depth,
                sourceRectangle: sourceRectangle
            );

            // Draw a gray rectangle overlay
            Drawing.FillRect(GetHitbox(), Color.Gray, 0.1f, g);

            // Draw the text
            string text = TextHandler.GetTextThatFits(deck.Name, Width - 20, 3.4f);
            Drawing.DrawText(text, X + 10, Y + 10, border: true, scale: 3.4f, layerDepth: depth*0.1f);
        }



        public void Clicked(float x, float y, Game1 g)
        {
            if (GetHitbox().Contains((int)x, (int)y))
            {
                g.collectionPage.activeDeckSlot = Index;
                g.collectionPage.EditDeck(g, deck);
            }
        }
        public void goTobattle(Game1 g)
        {
            string deck = g.collectionPage.collectionManager.deckBuilder.printCommaSeparatedCardList();
            g.pageManer.setActivePage(g.gameBoard, g);

            g.gameBoard.setDeck(g, g.gameBoard.gameHandler.player1, deck);
            g.gameBoard.gameHandler.StartGame(g);
        }

        public override void Destroy(Game1 g)
        {
            g.collectionPage.mouseManager.Remove(this);
        }

        public override void Update(GameTime gt, Game1 g)
        {
        }


        public override void Init(Game1 g)
        {
            g.collectionPage.mouseManager.Add(this);
        }
    }
}
