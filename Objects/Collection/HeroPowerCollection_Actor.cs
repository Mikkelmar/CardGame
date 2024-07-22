using CardGame.Cards;
using CardGame.Cards.PanimaionSystem;
using CardGame.Graphics;
using CardGame.HeroPowers;
using CardGame.Managers;
using CardGame.Objects.Cards;
using CardGame.PanimaionSystem;
using Engine;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CardGame.Objects
{
    public class HeroPowerCollection_Actor : GameObject, Clickable, HoverLisner
    {
        public HeroPower heroPower;
        public float baseX, baseY, baseWidth, baseHeight;
        private bool canAdd = true;
        protected static Sprite OnBoard_art = new Sprite(Textures.Minion_Board_Icon);


        public HeroPowerCollection_Actor(HeroPower heroPower) : base(0, 0, 100, 100)
        {
            
            this.heroPower = heroPower;
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
            g.collectionPage.collectionManager.deckBuilder.SetHeroPower(g, heroPower);
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
            //canAdd = g.collectionPage.collectionManager.deckBuilder.CanAddCard(card);
            //DeckBuilder.DeckUpdate += deckUpdated;
        }
        private void deckUpdated(Game1 g, Dictionary<Card, int> newDeck)
        {
            //canAdd = g.collectionPage.collectionManager.deckBuilder.CanAddCard(card);
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
            if (g.collectionPage.collectionManager.deckBuilder.heroPower.ID == heroPower.ID)
            {
                //armourIcon.Draw(new Vector2(X + (10 * resize), Y + (100 * resize)), 500 * resize, 500 * resize, layerDepth: depth * 0.02f);
                Drawing.FillRect(new Rectangle((int)X - 20, (int)Y - 20, (int)Width + 40, (int)Height + 40), Color.Green, depth * 1.2f, g);

            }
            OnBoard_art.Draw(new Vector2(X, Y), Width, Width, layerDepth: depth * 0.02f);
            heroPower.cardArt.Draw(new Vector2(X, Y), Width, Width, layerDepth: depth * 0.1f);
            Drawing.FillRect(GetHitbox(), Color.CadetBlue, depth, g);
            //Drawing.DrawText(heroPower.Text, X + Width / 2, Y + Width + 30, layerDepth: depth * 0.1f, color: Color.Black, drawCenter: true, scale: 2f);
            int _y = 0;
            float fontSize = 2f;
            foreach (string text in TextHandler.FitText(heroPower.Text, 420 * resize,  fontSize))
            {
                Drawing.DrawFormattedText(text, new Vector2(X + Width / 2, Y + 40+ ((Width + _y*60 ))), scale: fontSize, layerDepth: depth * 0.1f, color: Color.Black, drawCenter: true);
                _y++;
            }
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
