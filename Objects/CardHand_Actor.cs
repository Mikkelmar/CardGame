using CardGame.Graphics;
using CardGame.Managers;
using CardGame.Objects.Cards;
using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CardGame.Objects
{
    public class CardHand_Actor : Card_Actor, LeftRelease, RightClickable
    {
        private bool havePlayed = false;
        public bool dragging = false;
        public CardBoard_Actor hoverMinion = null;
        public bool renderCard = true;
        public CardHand_Actor(Card card) : base(card)
        {
            Width = 540 * 0.7f;
            Height = 840 * 0.7f;
            depth = 0.000001f;
            drawManaCost = true;
        }
        public override void Init(Game1 g)
        {
            g.gameBoard.mouseManager.AddRight(this);
            g.gameBoard.mouseManager.AddLeftRelease(this);
            base.Init(g);
        }
        public override void Destroy(Game1 g)
        {
            g.gameBoard.mouseManager.RemoveRight(this);
            g.gameBoard.mouseManager.RemoveLeftRelease(this);
            base.Destroy(g);
        }
        private void releasedAt(Game1 g, float x, float y)
        {
            if (g.gameBoard.isOnBoard(x, y))
            {
                castCard(g);
                g.gameBoard.mouseManager.stopLeftRelease = true;
            }
        }
        public override void Update(GameTime gt, Game1 g) {
            if (dragging)
            {
                Vector2 pos = g.gameBoard.mouseManager.GetMousePos(g);
                X = pos.X;
                Y = pos.Y;
            }
            else
            {
                X = baseX;
                Y = baseY;
                Width = baseWidth;
                Height = baseHeight;//TODO MOVE TO BASE
                base.Update(gt, g);
            }
        }
        private void castCard(Game1 g)
        {
            //The player tries to play the card
            if (havePlayed) { return; }
            if (!card.CanBePlayed)//|| g.gameBoard.gameHandler.ActivePlayer != card.belongToPlayer)
            { return; }

            
            if (card.requireTargets && card.isAnyValidTargets(g))
            {
                g.gameBoard.gameHandler.SelectingTarget = true;
                g.gameBoard.gameHandler.targeter = this;
            }
            else
            {
                HandlePlayeCard(g);
            }
        }
        private void HandlePlayeCard(Game1 g)
        {
            int playAtPos = -1;
            if (hoverMinion != null)
            {

                
                playAtPos = g.gameBoard.gameInterface.getPlayer(card.belongToPlayer).visualBoard.getIndexOfActor(hoverMinion);
                //g.gameBoard.gameInterface.getPlayer(card.belongToPlayer).visualBoard.TempHoverRemove(g, hoverMinion);

                hoverMinion = null;
            }
            //g.gameBoard.gameHandler.PlayCard(g, card, card.belongToPlayer, playAtPos);
            string targetID = "null";
            if(card.target != null)
            {
                targetID = card.target.UniqueID;
            }
            g.gameBoard.networkHandler.SendPlayCardMessage(card.UniqueID, playAtPos, targetID);
            
            havePlayed = true;
        }
        protected override void Activated(Game1 g)
        {
            if (havePlayed)
            {
                return;
            }
            if (g.gameBoard.gameHandler.activeOptionSelection)
            {
                return;
            }
            if (!card.CanBePlayed)//|| g.gameBoard.gameHandler.ActivePlayer != card.belongToPlayer)
            {
                return;
            }

            g.soundManager.PlaySound("draggingCard");
            g.gameBoard.mouseManager.stopClick = true;
            dragging = true;
            
        }
        public override void GiveTarget(Game1 g, Card_Actor targetActor)
        {
            Card targetCard = targetActor.card;
            if (card.isValidTarget(g, targetCard))
            {
                //if (card.belongToPlayer == g.gameBoard.isPlayer)
                //{
                //Debug.WriteLine(card, card.UniqueID);
                //Debug.WriteLine(targetCard, targetCard.UniqueID);
                card.target = targetCard;
                //g.gameBoard.networkHandler.SendTargetCardWithCardMessage(card.UniqueID, targetCard.UniqueID);
                //}
                //card.getTarget(g, (MinionCard)targetCard);
                HandlePlayeCard(g);
                
                g.gameBoard.gameHandler.SelectingTarget = false;
                g.gameBoard.gameHandler.targeter = null;
                havePlayed = true;
            }
            else
            {
                Debug.WriteLine("Not valid target");
            }
        }
        public override void Draw(Game1 g)
        {
            if (!renderCard)
            {
                return;
            }
            if(g.gameBoard.gameHandler.targeter == this || havePlayed)
            {
                return;
            }
            if (card.CanBePlayed && !havePlayed && card.belongToPlayer == g.gameBoard.gameHandler.ActivePlayer && card.belongToPlayer == g.gameBoard.isPlayer)
            {
                //Orange glow if special conditions are met, and the card can be played
                if(card is SpecialCondition && ((SpecialCondition)card).isConditionFufilled(g))
                {
                    Drawing.FillRect(new Rectangle((int)X - 20, (int)Y - 20, (int)Width + 40, (int)Height + 40), Color.Orange, depth * 1.2f, g);

                }
                else 
                {
                    Drawing.FillRect(new Rectangle((int)X - 20, (int)Y - 20, (int)Width + 40, (int)Height + 40), Color.Green, depth * 1.2f, g);
                }
            }
            base.Draw(g);
        }
        protected override void TriggerHovered(Game1 g)
        {
            
            baseWidth *= 2.3f;
            baseHeight *= 2.3f;

        }
        protected override void TriggerOffHovered(Game1 g)
        {
            //hardcoded based on Width = 540;

            baseWidth = 540*0.7f;
            baseHeight = 840* 0.7f;
        }
        public override void Clicked(float x, float y, Game1 g)
        {
            dragging = false;
            base.Clicked(x, y, g);
        }
        public void LeftReleased(float x, float y, Game1 g)
        {
            if(dragging == true)
            {
                releasedAt(g, x, y);
            }
            dragging = false;
        }
        public override void Hover(float x, float y, Game1 g)
        {
            
            if (dragging && card.requireTargets && !(card is MinionCard))
            {
                if (g.gameBoard.isOnBoard(x, y))
                {
                    castCard(g);
                }
                else if(g.gameBoard.gameHandler.targeter == this){
                    g.gameBoard.gameHandler.targeter = null;
                    g.gameBoard.gameHandler.SelectingTarget = false;
                }
            }
            if (dragging && card is MinionCard)
            {
                if (g.gameBoard.isOnBoard(x, y))
                {
                    triggerMinionHoverBoard(g,x,y);
                }
                else
                {
                    triggerMinionHoverOffBoard(g);
                }
            }
            base.Hover(x, y, g);
        }
        public void RightClicked(float x, float y, Game1 g)
        {
            dragging = false;
            triggerMinionHoverOffBoard(g);
        }
        private void triggerMinionHoverBoard(Game1 g, float x, float y)
        {
            if (hoverMinion == null)
            {
                Vector2 startPos = g.gameBoard.gameInterface.getPlayer(card.belongToPlayer).heroActor.GetPosCenter();
                hoverMinion = new CardBoard_Actor((MinionCard)card)
                {
                    showHoverDisplay = false,
                    X = startPos.X,
                    Y = startPos.Y
                };
                g.gameBoard.gameInterface.getPlayer(card.belongToPlayer).visualBoard.TempHoverAdd(g, hoverMinion);
                //g.gameBoard.objectManager.Add(hoverMinion, g);
                renderCard = false;
            }
            if (hoverMinion != null)
            {
                g.gameBoard.gameInterface.getPlayer(card.belongToPlayer).visualBoard.MoveActor(g, hoverMinion, x, y);
            }
        }
        private void triggerMinionHoverOffBoard(Game1 g)
        {
            if (hoverMinion != null)
            {
                g.gameBoard.gameInterface.getPlayer(card.belongToPlayer).visualBoard.TempHoverRemove(g, hoverMinion);
                //g.gameBoard.objectManager.Remove(hoverMinion, g);
                
                hoverMinion = null;
                renderCard = true;
            }
        }
        public override Vector2 GetSourcePos(Game1 g)
        {
            if (hoverMinion != null)
            {
                return hoverMinion.GetPosCenter();
            }
            return base.GetSourcePos(g);
        }
    }
}
