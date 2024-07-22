using CardGame.Cards;
using CardGame.Cards.PanimaionSystem;
using CardGame.Graphics;
using CardGame.HeroPowers;
using CardGame.Managers;
using CardGame.Objects.Cards;
using CardGame.PanimaionSystem;
using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using static CardGame.Objects.Cards.MinionCard;

namespace CardGame.Objects
{
    public class HeroPower_Actor : GameObject, Clickable, Targeter, HoverLisner
    {
        public HeroPower heroPower;
        private HeroPowerHoverDisplay heroPowerHoverDisplay;
        private static Sprite manaIcon = new Sprite(Textures.Mana);
        private static Sprite usedHeroPower = new Sprite(Textures.usedHeroPower);
        
        protected static Sprite OnBoard_art = new Sprite(Textures.Minion_Board_Icon), cardback = new Sprite(Textures.cardback);

        //public float baseX, baseY, baseWidth, baseHeight;

        public HeroPower_Actor(Game1 g, HeroPower heroPower) : base(0, 0, 100, 100)
        {
            this.heroPower = heroPower;
            Width = 300;
            Height = 300;
            depth = 0.001f;

            X = ((Drawing.WINDOW_WIDTH - Width) / 2)+600;
            if (heroPower.belongToPlayer.id == g.gameBoard.isPlayer.id)
            {
                Y = 1700;
            }
            else
            {
                //For single player reasson cannot be Y = 300;
                Y = 300;
            }
        }
        public override void Destroy(Game1 g)
        {
            g.gameBoard.mouseManager.Remove(this);
            g.gameBoard.mouseManager.RemoveHover(this);
        }

        protected  void Activated(Game1 g)
        {
            if (g.gameBoard.gameHandler.activeOptionSelection)
            {
                return;
            }
            g.gameBoard.mouseManager.stopClick = true;
            
            //The player tries to attack with this card
            if (!heroPower.canUse(g))
            {
                System.Diagnostics.Debug.WriteLine("cannot use!");
                return;
            }

            if (g.gameBoard.gameHandler.ActivePlayer != heroPower.belongToPlayer)
            {
                return;
            }
            if (heroPower.requireTargets)
            {
                g.gameBoard.gameHandler.SelectingTarget = true;
                g.gameBoard.gameHandler.targeter = this;
            }
            else
            {
                if (heroPower.belongToPlayer == g.gameBoard.isPlayer)
                {
                    g.gameBoard.networkHandler.SendTargetCardWithHeroPowerMessage(heroPower.belongToPlayer.id, "");
                }
            }

            
        }
        public void GiveTarget(Game1 g, Card_Actor targetActor)
        {
            Card targetCard = targetActor.card;
            if (heroPower.isValidTarget(g, targetCard))
            {
                g.gameBoard.gameHandler.SelectingTarget = false;
                g.gameBoard.gameHandler.targeter = null;
                //heroPower.getTarget(g, (MinionCard)targetCard);
                if (heroPower.belongToPlayer == g.gameBoard.isPlayer)
                {
                    g.gameBoard.networkHandler.SendTargetCardWithHeroPowerMessage(heroPower.belongToPlayer.id, targetCard.UniqueID);
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Not valid target");
            }
        }
        public virtual void Clicked(float x, float y, Game1 g)
        {
            if (GetHitbox().Contains(x, y))
            {
                if (g.gameBoard.CanPlay())
                {
                    Activated(g);
                }

                    
            }
        }

        public override void Init(Game1 g)
        {
            g.gameBoard.mouseManager.Add(this);
            g.gameBoard.mouseManager.AddHover(this);
        }

        public override void Update(GameTime gt, Game1 g){
            //X = baseX;
            //Y = baseY;
            //Width = baseWidth;
            //Height = baseHeight;
        }
        public override void Draw(Game1 g)
        {
            float resize = Width / 300;
            if (heroPower.canUse(g) && heroPower.belongToPlayer == g.gameBoard.gameHandler.ActivePlayer)
            {
                Drawing.FillRect(new Rectangle((int)X - 20, (int)Y - 20, (int)Width + 40, (int)Height + 40), Color.Green, depth * 1.2f, g);
            }
            OnBoard_art.Draw(new Vector2(X, Y), Width, Height, layerDepth: depth*0.5f);
            if (!heroPower.haveUsedThisTurn || heroPower.canUse(g))
            {
                heroPower.cardArt.Draw(new Vector2(X, Y), Width, Height, layerDepth: depth);
            }
            else
            {
                usedHeroPower.Draw(new Vector2(X, Y), Width, Height, layerDepth: depth);
            }
            

            Color manaColor = Color.White;
            if (heroPower.CurrentCost < heroPower.BaseCost)
            {
                manaColor = Color.Green;
            }
            else if (heroPower.CurrentCost > heroPower.BaseCost)
            {
                manaColor = Color.Red;
            }
            float manaSize = 200;
            manaIcon.Draw(new Vector2(X +(Width/2)- (manaSize/2), Y + Height- manaSize/2), manaSize * resize, manaSize * resize, layerDepth: depth * 0.2f);
            Drawing.DrawText(
                heroPower.CurrentCost.ToString(), 
                X + (Width / 2),
                Y + Height -(60*resize), 
                scale: 4 * resize, 
                layerDepth: depth * 0.1f, border: true, color: manaColor, drawCenter: true);
            
        }
        public static void DrawFullHeroPower(Game1 g, HeroPower _hp, Vector2 pos, float size, float depth)
        {

            size = size / 540;
            float _Width = 540*size;
            float _Height = 840* size;

            OnBoard_art.Draw(pos, _Width, _Width, layerDepth: depth * 0.02f);
            _hp.cardArt.Draw(pos, _Width, _Width, layerDepth: depth * 0.1f);
            Drawing.FillRect(new Rectangle((int)pos.X,(int)pos.Y, (int)_Width, (int)_Height), Color.CadetBlue, depth, g);
            //Drawing.DrawText(heroPower.Text, X + Width / 2, Y + Width + 30, layerDepth: depth * 0.1f, color: Color.Black, drawCenter: true, scale: 2f);
            int _y = 0;
            float fontSize = 2f*size;
            foreach (string text in TextHandler.FitText(_hp.Text, 420 * size, fontSize))
            {
                Drawing.DrawFormattedText(text, new Vector2(pos.X + _Width / 2, pos.Y + 40 + ((_Width + _y * 60))), scale: fontSize, layerDepth: depth * 0.1f, color: Color.Black, drawCenter: true);
                _y++;
            }
        }
        public Vector2 GetSourcePos(Game1 g)
        {
            return GetPosCenter();
        }

        public virtual string GetTargetText(Game1 g)
        {
            return "HERO PAWA";
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
            heroPowerHoverDisplay = new HeroPowerHoverDisplay(heroPower);
            heroPowerHoverDisplay.X = X + Width + 50;
            heroPowerHoverDisplay.Y = Y -200;
            g.game.getObjectManager().Add(heroPowerHoverDisplay, g);
        }
        protected virtual void TriggerOffHovered(Game1 g)
        {
            if (heroPowerHoverDisplay != null)
            {
                g.game.getObjectManager().Remove(heroPowerHoverDisplay, g);
            }
        }

    }
}
