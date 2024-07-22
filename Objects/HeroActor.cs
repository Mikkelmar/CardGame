using CardGame.Cards;
using CardGame.Cards.PanimaionSystem;
using CardGame.Graphics;
using CardGame.Managers;
using CardGame.Managers.GameManagers;
using CardGame.Objects.Cards;
using CardGame.PanimaionSystem;
using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.Objects
{
    public class HeroActor : CardBoard_Actor
    {

        
        protected static Sprite armourIcon = new Sprite(Textures.armourIcon);
        PlayerCard hero;
        public HeroActor(Game1 g, PlayerCard hero) : base(hero)
        {
            this.hero = hero;
            hero.sleeping = false;
            Width = 550;
            Height = 550;
            X = (Drawing.WINDOW_WIDTH - Width) / 2;
            if(g.gameBoard.isPlayer == null)
            {
                return;
            }
            if (hero.belongToPlayer.id == g.gameBoard.isPlayer.id)
            {
                Y = 1700;
            }
            else
            {
                Y = 100;
            }
            setPosision(X, Y);
            
            
        }
        public override void GiveTarget(Game1 g, Card_Actor targetActor)
        {
            Card targetCard = targetActor.card;
            //Check if can attack
            if (card.belongToPlayer == targetCard.belongToPlayer)
            {
                System.Diagnostics.Debug.WriteLine("Cannot target your own minions");
                return;
            }
            if (targetCard.currentState != Card.CardState.Board)
            {
                System.Diagnostics.Debug.WriteLine("Target status is not on the board");
                return;
            }
            if (!targetCard.belongToPlayer.Board.canAttack(targetCard))
            {
                System.Diagnostics.Debug.WriteLine("A minion with taunt is in the way");
                return;
            }
            //attack
            if (card.belongToPlayer == g.gameBoard.isPlayer)
            {
                g.gameBoard.networkHandler.SendAttackWithMinionMessage(card.UniqueID, targetCard.UniqueID);
            }
            g.gameBoard.gameHandler.SelectingTarget = false;
            g.gameBoard.gameHandler.targeter = null;
        }

        protected override void Activated(Game1 g)
        {
            if (g.gameBoard.gameHandler.activeOptionSelection)
            {
                return;
            }
            System.Diagnostics.Debug.WriteLine("Clicked minion " + card.Name);
            g.gameBoard.mouseManager.stopClick = true;

            if (g.gameBoard.gameHandler.SelectingTarget)
            {
                g.gameBoard.gameHandler.targeter.GiveTarget(g, this);
            }
            else
            {
                //The player tries to attack with this card
                if (!((MinionCard)card).CanAttack(g))
                {
                    System.Diagnostics.Debug.WriteLine("You have allready attacked with this minion");
                    return;
                }

                if (g.gameBoard.gameHandler.ActivePlayer == card.belongToPlayer)
                {
                    g.gameBoard.gameHandler.SelectingTarget = true;
                    g.gameBoard.gameHandler.targeter = this;
                }

            }
        }


        public override void Draw(Game1 g)
        {
            float resize  = Width / 550;
            //DRAW
            if (hero.CanAttack(g))
            {
                Drawing.FillRect(new Rectangle(
                    (int)((X-20)* resize),
                    (int)((Y - 20) * resize),
                    (int)((Width+40) * resize),
                    (int)((Height + 40) * resize)), 
                   Color.Green, depth + depth, g);
            }


            float player1offset = 0;
            if (hero.belongToPlayer.id != g.gameBoard.isPlayer.id)
            {
                player1offset = 180;
            }
            //Attack
            if (hero.Attack > 0)
            {

                attack.Draw(new Vector2(X - (60 * resize), Y + (200 * resize+ player1offset)), 190 * resize, 190 * resize, layerDepth: depth * 0.2f);
                Drawing.DrawText(cardsAttack.ToString(), X + (10 * resize), Y + (250 * resize)+ player1offset, scale: 4f * resize, layerDepth: depth * 0.1f, border: true);
            }

            //Health
            Color healthColor = Color.White;
            if (cardsHealth < cardsBaseHealth + cardsExtraHealth)
            {
                healthColor = Color.Red;
            }
            else if (cardsHealth > cardsBaseHealth)
            {
                healthColor = Color.Green;
            }
            
            blood.Draw(new Vector2(X + (390 * resize), Y + ((170 + player1offset) * resize)), 230 * resize, 230 * resize, layerDepth: depth * 0.21f);
            Drawing.DrawText(cardsHealth.ToString(), X + (505* resize), Y + ((250 + player1offset) * resize), scale: 4f * resize, layerDepth: depth * 0.1f, border: true, color: healthColor, drawCenter: true);

            //Armour
            if (hero.Armour > 0)
            {
                armourIcon.Draw(new Vector2(X + (390 * resize), Y + ((370- 320 + player1offset) * resize)), 230 * resize, 230 * resize, layerDepth: depth * 0.2f);
                Drawing.DrawText(hero.Armour.ToString(), X + (505 * resize), Y + ((450 - 335 + player1offset) * resize), scale: 4f * resize, layerDepth: depth * 0.1f, border: true, color: Color.White, drawCenter: true);

            }
            if (hero.Frozen)
            {
                frozenMinion.Draw(new Vector2(X, Y), Width, Height, scale: scale, layerDepth: depth * 0.99f);
            }
            card.cardArt.Draw(new Vector2(X, Y), Width * resize, Height * resize, layerDepth: depth);
        }
        protected override void offHover(Game1 g)
        {
            if (isHovering)
            {
                TriggerOffHovered(g);
            }
        }
        protected override void onHover(Game1 g)
        {
            if (!isHovering)
            {
                TriggerHovered(g);
            }
        }
        protected override void TriggerHovered(Game1 g)
        {

        }
        protected override void TriggerOffHovered(Game1 g)
        {

        }

        public override string GetTargetText(Game1 g)
        {
            return "Attack enemy";
        }

        public override void Update(GameTime gt, Game1 g)
        {
            base.Update(gt, g);
        }
    }
}
