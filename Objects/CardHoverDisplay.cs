using CardGame.Objects.Cards;
using Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.Objects
{
    public class CardHoverDisplay : Card_Actor
    {
        public CardHoverDisplay(Card card) : base(card)
        {

            depth = 0.000000001f;
            hideCardOnOpponentTurn = false;
            drawManaCost = true;
        }

        public override void GiveTarget(Game1 g, Card_Actor targetActor)
        {
        }

        protected override void Activated(Game1 g)
        {
        }
        public override void Draw(Game1 g)
        {
            float yPos = Math.Min(Math.Max(200, Y + Height/2), Drawing.WINDOW_HEIGHT-Height);
            drawCard(g, X, yPos, card, depth, drawManaCost: true, drawBaseStats: true, size: (int)Width);
        }
    }
}
