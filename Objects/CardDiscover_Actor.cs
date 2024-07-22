using CardGame.Graphics;
using CardGame.Managers;
using CardGame.Objects.Cards;
using Engine;
using Microsoft.Xna.Framework;

namespace CardGame.Objects
{
    public class CardDiscover_Actor : Card_Actor
    {
        public Card sourceCard;
        public CardDiscover_Actor(Card card, Card sourceCard) : base(card)
        {
            this.sourceCard = sourceCard;
            depth = 0.00000000000001f;
        }
        
        protected override void Activated(Game1 g)
        {
            //The player tries to active discover option
            if(g.gameBoard.isPlayer != g.gameBoard.gameHandler.ActivePlayer)
            {
                return;
            }
            g.gameBoard.mouseManager.stopClick = true;
            if (card.requireTargets)
            {
                g.gameBoard.gameHandler.SelectingTarget = true;
                g.gameBoard.gameHandler.targeter = this;
                g.gameBoard.gameHandler.optionSelectManager.ToggleHide();
            }
            else
            {
                PlayTheCard(g);
            }
        }
        protected virtual void PlayTheCard(Game1 g)
        {
            //g.gameBoard.gameHandler.PlayCard(g, sourceCard, sourceCard.belongToPlayer);
            g.gameBoard.networkHandler.SendCardOptionSelected(card.UniqueID);
            //g.gameBoard.gameHandler.ActivateCard(g, card, card.belongToPlayer);

        }
        public override void GiveTarget(Game1 g, Card_Actor targetActor)
        {
            Card targetCard = targetActor.card;
            if (card.isValidTarget(g, targetCard))
            {
                card.getTarget(g, (MinionCard)targetCard);
                if (card.belongToPlayer == g.gameBoard.isPlayer)
                {
                    g.gameBoard.networkHandler.SendTargetCardWithCardMessage(card.UniqueID, targetCard.UniqueID);
                }
                PlayTheCard(g);
                g.gameBoard.gameHandler.SelectingTarget = false;
                g.gameBoard.gameHandler.targeter = null;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Not valid target");
            }
        }
        public override void Draw(Game1 g)
        {
            if (g.gameBoard.isPlayer != g.gameBoard.gameHandler.ActivePlayer)
            {
                drawACardBack(g, X, Y - 1000, depth, (int)Width);
            }
            else
            {
                Drawing.FillRect(new Rectangle((int)X - 50, (int)Y - 40, (int)Width + 70, (int)Height + 50), Color.Green, depth * 1.2f, g);
                drawCard(g, X, Y, card, depth, drawManaCost: drawManaCost, drawBaseStats: false, size: (int)Width);
            }
            
        }
    }
}
