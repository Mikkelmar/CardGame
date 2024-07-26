using CardGame.Cards;
using CardGame.Cards.PanimaionSystem;
using CardGame.Graphics;
using CardGame.Managers;
using CardGame.Objects.Cards;
using CardGame.PanimaionSystem;
using Engine;
using Microsoft.Xna.Framework;

namespace CardGame.Objects
{
    public class CardDeckDisplay_Actor : GameObject, Clickable, HoverLisner
    {
        public Card card;
        public float baseX, baseY, baseWidth, baseHeight;

        private static Sprite legendaryIcon = new Sprite(Textures.legendary);
        private static Sprite manaIcon = new Sprite(Textures.Mana);
        private static Sprite powerCard = new Sprite(Textures.powerCard);
        
        private int amount;
        private CardHoverDisplay hoverDisplay;
        public CardDeckDisplay_Actor(Card card, int amount) : base(0, 0, 100, 100)
        {
            this.amount = amount;
            this.card = card;
            X = 50;
            Y = 50;
            Width = 540;
            Height = 840;
            depth = 0.001f;
        }
        public override void Destroy(Game1 g)
        {
            TriggerOffHovered(g);
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
            g.collectionPage.collectionManager.deckBuilder.RemoveCard(g,card);
            g.soundManager.PlaySound("draggingCard");
            
            
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
            int textureWidth = card.cardArt.Texture.Width;
            int textureHeight = card.cardArt.Texture.Height;
            
            // Calculate the scaling factor to maintain the aspect ratio
            float scaleFactor = Height / Width;  // This seems to be the intended scale factor based on the original code
            int offset = (int)(textureHeight/5);
            // Define the source rectangle to draw the full width of the texture and a proportional part of its height
            Rectangle sourceRectangle = new Rectangle(0, offset, textureWidth, (int)(textureHeight * scaleFactor));

            // Calculate the destination size while maintaining the aspect ratio
            int sizeX = (int)Width;

            // Draw the specific part of the texture using card.cardArt.Draw
            card.cardArt.Draw(
                position,
                Width,
                Height,
                layerDepth: depth,
                sourceRectangle: sourceRectangle
            );

            //mana
            manaIcon.Draw(new Vector2(position.X - 130, position.Y + 10 * resize), 130 * resize, 130 * resize, layerDepth: depth * 0.2f);
            Drawing.DrawText(card.BaseCost.ToString(), position.X - 70, position.Y + 22 * resize, scale: 3 * resize, layerDepth: depth * 0.1f, border: true, drawCenter: true);

            //Name
            Drawing.DrawText(card.Name, position.X + 10, position.Y + 70 * resize, scale: 2f, layerDepth: depth * 0.1f, color: Color.Gold, drawCenter: false, border: true);
            if (amount > 1)
            {
                Drawing.DrawText(amount.ToString(), position.X + sizeX + 10, position.Y + 20 * resize, scale: 3f, layerDepth: depth * 0.1f, color: Color.Gold, drawCenter: false, border: true);
            }
            if (card.grade == Card.Grade.Legendary)
            {
                legendaryIcon.Draw(new Vector2(position.X + sizeX - 120, position.Y * resize), 150 * resize, 150 * resize, layerDepth: depth * 0.2f);

            }
            if (card.PowerCard)
            {
                powerCard.Draw(new Vector2(position.X + sizeX - 210, position.Y * resize), 130 * resize, 130 * resize, layerDepth: depth * 0.2f);

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
            hoverDisplay = new CardHoverDisplay(card);
            hoverDisplay.X = X + Width + 50;
            hoverDisplay.Y = Y - 200;
            g.game.getObjectManager().Add(hoverDisplay, g);
        }
        protected virtual void TriggerOffHovered(Game1 g)
        {
            if (hoverDisplay != null)
            {
                g.game.getObjectManager().Remove(hoverDisplay, g);
            }
        }

    }
}
