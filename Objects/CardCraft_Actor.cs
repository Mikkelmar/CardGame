using CardGame.Graphics;
using CardGame.Managers;
using CardGame.Objects.Cards;
using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using static CardGame.Objects.Cards.MinionCard;

namespace CardGame.Objects
{
    public class CardCraft_Actor : CardDiscover_Actor
    {

        public CardCraft_Actor(Card card, Card sourceCard) : base(card, sourceCard)
        {
            drawManaCost = true;
        }

        protected override void Activated(Game1 g)
        {
            //The player tries to active discover option
            PlayTheCard(g);
            //g.gameBoard.gameHandler.StopSelecting(g);
            g.gameBoard.mouseManager.stopClick = true;
        }
        protected override void PlayTheCard(Game1 g)
        {
            //g.gameBoard.gameHandler.PlayCard(g, sourceCard, sourceCard.belongToPlayer);
            g.gameBoard.networkHandler.SendCardSelected(card.UniqueID);
            //((CraftCreator)sourceCard).optionSelected(g, card);
        }

    }
}
