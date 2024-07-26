using CardGame.Cards;
using CardGame.Cards.PanimaionSystem;
using CardGame.Graphics;
using CardGame.Managers;
using CardGame.Objects.Cards;
using CardGame.Objects.MiscObjects;
using CardGame.PanimaionSystem.Animations;
using CardGame.PanimaionSystem.GameObjectAnimation;
using Engine;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;

namespace CardGame.Objects
{
    public class CardBoard_Actor : Card_Actor, LeftRelease
    {
        protected static Sprite powerShield = new Sprite(Textures.powerShield);
        protected static Sprite taunt = new Sprite(Textures.taunt);
        protected static Sprite poison = new Sprite(Textures.poison);
        protected static Sprite triggerEffect = new Sprite(Textures.triggerEffect);
        protected static Sprite skull = new Sprite(Textures.skull);
        protected static Sprite aura = new Sprite(Textures.aura);
        protected static Sprite frozenMinion = new Sprite(Textures.frozenMinion);
        protected static Sprite lifesteal = new Sprite(Textures.lifesteal);
        
        protected int cardsAttack, cardsHealth, cardsBaseAttack, cardsBaseHealth, cardsExtraHealth;

        private List<GameObjectAnimation> animationEffects = new List<GameObjectAnimation>();
        public bool inAnimation = false, isAttacking = false;
        private Vector2 BasePos, LastBasePos;
        private float elapsedTime = 0, duration = 0.2f;//duration how look it takes to repos
        private CardHoverDisplay hoverDisplay;
        public bool showHoverDisplay = true;

        public CardBoard_Actor(MinionCard card) : base(card)
        {
            this.card = card;

            Width = 400;
            Height = 400;
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
        private void onTakeDamage(Game1 g, int damage, Card source) {
            g.gameBoard.queueManager.EnqueueItem(
                new GameAction((g) => g.gameBoard.objectManager.Add(new DamageCounter(this, -damage)))
            );
        }
        private void onRestoreHealth(Game1 g, int damage, Card source)
        {
            g.gameBoard.queueManager.EnqueueItem(
                new GameAction((g) => g.gameBoard.objectManager.Add(new DamageCounter(this, damage)))
            );
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
            if(g.gameBoard.gameHandler.activeOptionSelection)
            {
                return;
            }
            g.gameBoard.mouseManager.stopClick = true;
            if (g.gameBoard.gameHandler.SelectingTarget)
            {
                g.gameBoard.gameHandler.targeter.GiveTarget(g, this);
            }
            else
            {
                //The player tries to attack with this card
                if (!((MinionCard)card).CanAttackNow)
                {
                    string feedBacktext = "You cannot attack with this minion";
                    if (((MinionCard)card).Frozen)
                    {
                        feedBacktext = "This minion is frozen";
                    }
                    else if (((MinionCard)card).sleeping && !((MinionCard)card).Rush && !((MinionCard)card).Charge)
                    {
                        feedBacktext = "This minion needs a turn to get ready";
                    }
                    else if(((MinionCard)card).haveAttacked)
                    {
                        feedBacktext = "This minion have allready attacked";
                    }

                    TextPopup _textPopup = new TextPopup(
                        feedBacktext,
                        duration: 2f, // Text will stay for 2 seconds
                        x: Drawing.WINDOW_WIDTH / 2,
                        y: Drawing.WINDOW_HEIGHT / 2,
                        scale: .6f,
                        color: Color.White
                    );
                    g.gameBoard.objectManager.Add(_textPopup, g);
                    g.gameBoard.queueManager.EnqueueItem(new GameAction((g) => g.soundManager.PlaySound("playSound")));
                    return;
                }
                g.soundManager.PlaySound("readyToAttack");

                if (g.gameBoard.gameHandler.ActivePlayer == card.belongToPlayer)
                {
                    g.gameBoard.gameHandler.SelectingTarget = true;
                    g.gameBoard.gameHandler.targeter = this;
                }
                
            }
        }
        public override void GiveTarget(Game1 g, Card_Actor targetActor)
        {
            Card targetCard = targetActor.card;
            //Check if can attack
            string feedBackText = "Not a valid target";
            if (!card.belongToPlayer.Board.canAttackTarget(g, card, targetCard))
            {
                if (card.belongToPlayer != g.gameBoard.isPlayer)
                {
                    feedBackText = "This is not your minion";
                }
                else if (card.belongToPlayer == targetCard.belongToPlayer)
                {
                    feedBackText = "Cannot target your own minions";
                }
                else if(!targetCard.belongToPlayer.Board.canAttack(targetCard))
                {
                    feedBackText = "A minion with taunt is in the way";
                }
                else if(targetCard is PlayerCard && ((MinionCard)card).sleeping && !((MinionCard)card).Charge)
                {
                    feedBackText = "Can only attack minions first turn with rush.";
                }
                TextPopup _textPopup = new TextPopup(
                        feedBackText,
                        duration: 2f,
                        x: Drawing.WINDOW_WIDTH / 2,
                        y: Drawing.WINDOW_HEIGHT / 2,
                        scale: .6f,
                        color: Color.White
                    );
                g.gameBoard.objectManager.Add(_textPopup, g);
                g.gameBoard.queueManager.EnqueueItem(new GameAction((g) => g.soundManager.PlaySound("playSound")));
                return;
            }
            //attack
            if (card is MinionCard)
            {
                
                if (card.belongToPlayer == g.gameBoard.isPlayer)
                {
                    g.gameBoard.networkHandler.SendAttackWithMinionMessage(card.UniqueID, targetCard.UniqueID);
                }
                    
                //((MinionCard)card).AttackCharacter(g, (MinionCard)targetCard);
                
            }
            g.gameBoard.gameHandler.SelectingTarget = false;
            g.gameBoard.gameHandler.targeter = null;
        }
        public void LeftReleased(float x, float y, Game1 g)
        {
            return;//Unsure if to keep this function
            if (GetHitbox().Contains(x, y))
            {
                if (g.gameBoard.gameHandler.activeOptionSelection)
                {
                    return;
                }
                g.gameBoard.mouseManager.stopClick = true;
                if (g.gameBoard.gameHandler.SelectingTarget)
                {
                    g.gameBoard.gameHandler.targeter.GiveTarget(g, this);
                }
            }
        }

        public override void Init(Game1 g)
        {
            g.gameBoard.mouseManager.AddLeftRelease(this);
            if (((MinionCard)card).SpellDamage > 0)
            {
                addAnimationEffect(g, GameObjectAnimationList.getSpellDamageAnimation(this));
            }
            if (card is MinionCard)
            {
                ((MinionCard)card).OnTakeDamage += onTakeDamage;
                ((MinionCard)card).OnRestoreHealth += onRestoreHealth;
                
            }
            base.Init(g);
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

            if(card is MinionCard)
            {
                DrawMinionCard(g, (MinionCard)card);
            }
        }
        private void DrawMinionCard(Game1 g, MinionCard cardToDraw)
        {
            float resize = Width / 360;

            if (((MinionCard)card).CanAttackNow && card.belongToPlayer == g.gameBoard.gameHandler.ActivePlayer)
            {
                Drawing.FillRect(new Rectangle((int)X-20, (int)Y - 20, (int)Width + 40, (int)Height + 40), Color.Green, depth * 1.4f, g);
            }
            if (cardToDraw.PowerShield)
            {
                powerShield.Draw(new Vector2(X, Y - (35 * resize)) , Width, Height+(70 * resize), scale: scale, layerDepth: depth*0.8f);
                
            }
            if (cardToDraw.Frozen)
            {
                frozenMinion.Draw(new Vector2(X, Y), Width, Height, scale: scale, layerDepth: depth*0.99f);
            }
            
            if (cardToDraw.Taunt)
            {
                taunt.Draw(new Vector2(X-60, Y - (105 * resize)), Width+120, Height + (180 * resize), scale: scale, layerDepth: depth * 1.5f);

            }
            if (cardToDraw.Poisonus)
            {
                poison.Draw(new Vector2(X + (100 * resize), Y + (245 * resize)), (160 * resize), (160 * resize), scale: scale, layerDepth: depth*0.9f);

            }
            if (cardToDraw.LifeSteal)
            {
                lifesteal.Draw(new Vector2(X + (100 * resize), Y + (245 * resize)), (160 * resize), (160 * resize), scale: scale, layerDepth: depth * 0.9f);

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
            attack.Draw(new Vector2(X-30* resize, Y+248*resize), 150 * resize, 150 * resize, layerDepth: depth * 0.2f);
            Drawing.DrawText(cardsAttack.ToString(), X + (45 * resize), Y + 280 * resize, scale: 3f * resize, layerDepth: depth * 0.1f, border: true, color: attackColor, drawCenter: true);

            //Health
            
            blood.Draw(new Vector2(X + 210*resize, Y +  200*resize), 190 * resize, 190 * resize, layerDepth: depth * 0.2f);
            Color healthColor = Color.White;
            if (cardsHealth < cardsBaseHealth+ cardsExtraHealth)
            {
                healthColor = Color.Red;
            }else if (cardsHealth > cardsBaseHealth)
            {
                healthColor = Color.Green;
            }
            Drawing.DrawText(cardsHealth.ToString(), X +(305*resize), Y + 280 * resize, scale: 3f * resize, layerDepth: depth * 0.1f, border: true, color: healthColor, drawCenter: true);
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
            foreach(GameObjectAnimation animation in animationEffects)
            {
                g.gameBoard.objectManager.Remove(animation, g);
            }
            g.gameBoard.mouseManager.RemoveLeftRelease(this);
            if (card is MinionCard)
            {
                ((MinionCard)card).OnTakeDamage -= onTakeDamage;
                ((MinionCard)card).OnRestoreHealth -= onRestoreHealth;

            }
            base.Destroy(g);
        }
        public void addAnimationEffect(Game1 g, GameObjectAnimation animation)
        {
            if(animationEffects.Find(a => a.ID.Equals(animation.ID)) == null)
            {
                animationEffects.Add(animation);
                g.gameBoard.objectManager.Add(animation, g);
            }
        }
    }
}
