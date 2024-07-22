using CardGame.Cards;
using CardGame.Graphics;
using CardGame.Managers;
using CardGame.Objects.Cards;
using CardGame.Objects.MiscObjects;
using CardGame.PanimaionSystem.Animations;
using Engine;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;

namespace CardGame.Objects
{
    public class Weapon_Actor : Card_Actor
    {
        protected static Sprite poison = new Sprite(Textures.poison);
        protected static Sprite triggerEffect = new Sprite(Textures.triggerEffect);
        protected static Sprite skull = new Sprite(Textures.skull);
        protected static Sprite aura = new Sprite(Textures.aura);
        protected static Sprite armourIcon = new Sprite(Textures.armourIcon);

        protected int cardsAttack, cardsHealth, cardsBaseAttack, cardsBaseHealth, cardsExtraHealth;


        public bool inAnimation = false, isAttacking = false;
        private Vector2 BasePos, LastBasePos;
        private float elapsedTime = 0, duration = 0.2f;//duration how look it takes to repos
        private CardHoverDisplay hoverDisplay;
        public bool showHoverDisplay = true;

        public Weapon_Actor(WeaponCard card) : base(card)
        {
            this.card = card;

            Width = 400;
            Height = 400;
            X = -700 + (Drawing.WINDOW_WIDTH - Width) / 2;
            Y = 300;
        }
        public override void Init(Game1 g)
        {
            base.Init(g);
            if (g.gameBoard.isPlayer.id == card.belongToPlayer.id)
            {
                setPosision(-700 + (Drawing.WINDOW_WIDTH - Width) / 2, 1700);
            }
            else
            {
                setPosision(-700 + (Drawing.WINDOW_WIDTH - Width) / 2, 100);
            }
        }
        protected override void ChangeCardValues(Dictionary<string, int> dict, Game1 g)
        {
            
            cardsAttack = dict["attack"];
            
            cardsHealth = dict["health"];

            cardsExtraHealth = dict["extraHealth"];
            cardsBaseHealth = dict["baseHealth"];
            cardsBaseAttack = dict["baseAttack"];

            base.ChangeCardValues(dict, g);
        }
        public void setPosision(float newX, float newY)
        {
            LastBasePos = new Vector2(newX, newY);
            BasePos = new Vector2(newX, newY);
            X = newX;
            Y = newY;
        }
        public void updateBaseposision(float newX, float newY)
        {
            LastBasePos = new Vector2(X, Y);
            BasePos = new Vector2(newX, newY);
            elapsedTime = 0;
        }
        protected override void TriggerHovered(Game1 g)
        {
            if (showHoverDisplay)
            {
                hoverDisplay = new CardHoverDisplay(card);
                hoverDisplay.X = X + Width + 50;
                hoverDisplay.Y = Y - 200;
                g.gameBoard.objectManager.Add(hoverDisplay, g);
            }
            
            
        }

        protected override void TriggerOffHovered(Game1 g)
        {
            if(hoverDisplay != null)
            {
                g.gameBoard.objectManager.Remove(hoverDisplay, g);
            }
        }
        protected override void Activated(Game1 g)
        {
            
        }
        public override void GiveTarget(Game1 g, Card_Actor targetActor)
        {
            
        }
        

        public override void Update(GameTime gt, Game1 g)
        {
            if (isAttacking)
            {
                return;
            }
            if (elapsedTime <= duration)
            {
                elapsedTime += (float)gt.ElapsedGameTime.TotalSeconds;

                // Calculate the normalized time (0 to 1)
                float t = MathHelper.Clamp(elapsedTime / duration, 0f, 1f);



                // Interpolate position and scale
                X = MathHelper.Lerp(LastBasePos.X, BasePos.X, t);
                Y = MathHelper.Lerp(LastBasePos.Y, BasePos.Y, t);

                // Check if the animation is complete
                if (t >= 1f)
                {
                    //DONE
                }
            }
        }
        public override void Draw(Game1 g)
        {
            if (inAnimation)
            {
                return;
            }
            //Drawing.FillRect(GetHitbox(), Color.Blue, 0.000000001f, g);

            if(card is WeaponCard)
            {
                DrawWeaponCard(g, (WeaponCard)card);
            }
        }
        private void DrawWeaponCard(Game1 g, WeaponCard cardToDraw)
        {
            float resize = Width / 360;
            
            if (cardToDraw.Poisonus)
            {
                poison.Draw(new Vector2(X + (100 * resize), Y + (245 * resize)), (160 * resize), (160 * resize), scale: scale, layerDepth: depth*0.9f);

            }
            if (cardToDraw.TriggerEffect)
            {
                triggerEffect.Draw(new Vector2(X + (100 * resize), Y + (245 * resize)), (160 * resize), (160 * resize), scale: scale, layerDepth: depth * 0.9f);

            }
            if (cardToDraw.DeathRattles.Count > 0)
            {
                skull.Draw(new Vector2(X + (100 * resize), Y + (245 * resize)), (160 * resize), (160 * resize), scale: scale, layerDepth: depth * 0.9f);

            }
            if (cardToDraw.ActiveAura.Count > 0)
            {
                aura.Draw(new Vector2(X + (100 * resize), Y + (245 * resize)), (160 * resize), (160 * resize), scale: scale, layerDepth: depth * 0.9f);

            }
            
            OnBoard_art.Draw(new Vector2(X, Y), Width, Height, scale: scale, layerDepth: depth);
            card.cardArt.Draw(new Vector2(X + (2 * resize), Y + (2 * resize)), Width, Height, layerDepth: depth * 1.1f, scale: scale);

            

            //Attack
            Color attackColor = Color.White;
            if (cardsAttack > cardsBaseAttack)
            {
                attackColor = Color.Green;
            }
            attack.Draw(new Vector2(X-30* resize, Y+240*resize), 150 * resize, 150 * resize, layerDepth: depth * 0.2f);
            Drawing.DrawText(cardsAttack.ToString(), X + (30 * resize), Y + 280 * resize, scale: 3f * resize, layerDepth: depth * 0.1f, border: true, color: attackColor);

            //Health

            armourIcon.Draw(new Vector2(X + 210*resize, Y +  200*resize), 190 * resize, 190 * resize, layerDepth: depth * 0.2f);
            Color healthColor = Color.White;
            if (cardsHealth < cardsBaseHealth+ cardsExtraHealth)
            {
                healthColor = Color.Red;
            }else if (cardsHealth > cardsBaseHealth)
            {
                healthColor = Color.Green;
            }
            Drawing.DrawText(cardsHealth.ToString(), X +(280*resize), Y + 280 * resize, scale: 3f * resize, layerDepth: depth * 0.1f, border: true, color: healthColor);
        }
        public override string GetTargetText(Game1 g)
        {
            return "Attack enemy";
        }
        public override void Destroy(Game1 g)
        {
            if (hoverDisplay != null)
            {
                g.gameBoard.objectManager.Remove(hoverDisplay, g);
            }
            base.Destroy(g);
        }
    }
}
