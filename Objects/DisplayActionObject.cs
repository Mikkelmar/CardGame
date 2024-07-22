using CardGame.Graphics;
using CardGame.Managers;
using CardGame.Objects.Cards;
using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.Objects
{
    public class DisplayActionObject : GameObject, HoverLisner
    {

        protected static Sprite triggerEffect = new Sprite(Textures.triggerEffect);
        protected static Sprite attack = new Sprite(Textures.Attack);
        public string ActionDescription { get; set; }
        public Card actionCard;
        public bool small = false;
        public enum Types { None, Effect, Attack }
        public Types type = Types.None;
        private CardHoverDisplay hoverDisplay;

        // Method to render the action (to be implemented based on your rendering logic)


        protected void TriggerHovered(Game1 g)
        {
            
            hoverDisplay = new CardHoverDisplay(actionCard);
            hoverDisplay.X = X + Width + 50;
            hoverDisplay.Y = Y - 200;
            g.gameBoard.objectManager.Add(hoverDisplay, g);


        }
        protected void TriggerOffHovered(Game1 g)
        {
            
            g.gameBoard.objectManager.Remove(hoverDisplay, g);
            
        }

        public override void Destroy(Game1 g)
        {
            if (hoverDisplay != null)
            {
                g.gameBoard.objectManager.Remove(hoverDisplay, g);
            }
            g.game.getMouseManager().RemoveHover(this);
        }

        public override void Update(GameTime gt, Game1 g)
        {
        }

        public override void Draw(Game1 g)
        {
            int zoom = actionCard.cardArt.Texture.Width / 4;
            int zoomY = actionCard.cardArt.Texture.Height / 4;
            int offset = 0;
            if (small)
            {
                zoomY = actionCard.cardArt.Texture.Height / 8;
                offset = actionCard.cardArt.Texture.Height / 4;
            }
            actionCard.cardArt.Draw(new Vector2(X, Y), Width, Height, layerDepth: 0.006f,
                sourceRectangle: new Rectangle(zoom, zoomY + offset, zoom * 2, zoomY * 2));
            Sprite effect = null;
            if (type == Types.Effect)
            {
                effect = triggerEffect;
            }
            else if(type == Types.Attack)
            {
                effect = attack;
            }
            if (type != Types.None)
            {
                float size = Height;
                effect.Draw(new Vector2(X + Width - size / 2, Y), size, size, layerDepth: 0.005f);
            }
            Color color = Color.Green;
            if(actionCard.belongToPlayer != g.gameBoard.isPlayer)
            {
                color = Color.Red;
            }
            int boarderSize = 10;
            Rectangle boarder = GetHitbox();
            boarder.X -= boarderSize;
            boarder.Y -= boarderSize;
            boarder.Width += 2 * boarderSize;
            boarder.Height += 2 * boarderSize;
            Drawing.FillRect(boarder, color, 0.02f, g);
        }

        public override void Init(Game1 g)
        {
            g.game.getMouseManager().AddHover(this);
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
    }
}
