using CardGame.Cards;
using CardGame.Cards.PanimaionSystem;
using CardGame.Graphics;
using CardGame.Managers;
using CardGame.Objects.Cards;
using CardGame.PanimaionSystem;
using Engine;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CardGame.Objects
{
    public class CardCollection_Actor : GameObject, Clickable, HoverLisner
    {
        public Card card;
        public float baseX, baseY, baseWidth, baseHeight;
        private bool canAdd = true;
        protected static Sprite armourIcon = new Sprite(Textures.armourIcon);
        protected static Sprite lockIcon = new Sprite(Textures.lockIcon);
        
        public CardCollection_Actor(Card card) : base(0, 0, 100, 100)
        {
            
            this.card = card;
            X = 50;
            Y = 50;
            Width = 540;
            Height = 840;
            depth = 0.001f;
        }
        public override void Destroy(Game1 g)
        {
            g.collectionPage.mouseManager.Remove(this);
            g.collectionPage.mouseManager.RemoveHover(this);
        }
        public void setNewBaseLocation(int x, int y)
        {
            baseX = x;
            baseY = y;
        }
        protected void Activated(Game1 g)
        {
            g.collectionPage.collectionManager.deckBuilder.AddCard(g,card);
        }
        public virtual void Clicked(float x, float y, Game1 g)
        {
            if (GetHitbox().Contains(x, y))
            {
                Activated(g);
            }
        }

        public override void Init(Game1 g)
        {
            g.collectionPage.mouseManager.Add(this);
            g.collectionPage.mouseManager.AddHover(this);

            baseWidth = Width;
            baseHeight = Height;
            canAdd = g.collectionPage.collectionManager.deckBuilder.CanAddCard(card);
            DeckBuilder.DeckUpdate += deckUpdated;
        }
        private void deckUpdated(Game1 g, Dictionary<Card, int> newDeck)
        {
            canAdd = g.collectionPage.collectionManager.deckBuilder.CanAddCard(card);
        }

        public override void Update(GameTime gt, Game1 g){
            //X = baseX;
            //Y = baseY;
            //Width = baseWidth;
            //Height = baseHeight;
        }
        public override void Draw(Game1 g)
        {
            float resize = 1f;
            if (!canAdd)
            {
                //armourIcon.Draw(new Vector2(X + (10 * resize), Y + (100 * resize)), 500 * resize, 500 * resize, layerDepth: depth * 0.02f);
                lockIcon.Draw(new Vector2(X, Y), Width, Height, layerDepth: depth * 0.02f);
            }
            Card_Actor.drawCard(g, X, Y, card, depth, drawBaseStats: true, size: (int)Width);
        }
        protected bool isHovering = false;
        public virtual void Hover(float x, float y, Game1 g)
        {
            if (GetHitbox().Contains(x, y))
            {
                onHover(g);
                isHovering = true;
            }
            else
            {
                offHover(g);
                isHovering = false;
            }
        }
        protected virtual void offHover(Game1 g)
        {
            if (isHovering)
            {
                TriggerOffHovered(g);
            }
        }
        protected virtual void onHover(Game1 g)
        {
            if (!isHovering)
            {
                TriggerHovered(g);
            }
        }
        protected virtual void TriggerHovered(Game1 g)
        {

        }
        protected virtual void TriggerOffHovered(Game1 g)
        {

        }

    }
}
