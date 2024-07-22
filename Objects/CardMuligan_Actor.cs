using CardGame.Graphics;
using CardGame.Managers;
using CardGame.Shared.GameLogic;
using CardGame.Objects.Cards;
using Engine;
using Microsoft.Xna.Framework;

namespace CardGame.Objects
{
    public class CardMuligan_Actor : CardDiscover_Actor
    {
        public bool keep = true;
        private Muligan muligan;
        private static Sprite cross = new Sprite(Textures.X);
        public CardMuligan_Actor(Card card, Muligan muligan) : base(card, null)
        {
            this.muligan = muligan;
        }

        protected override void Activated(Game1 g)
        {
            //The player tries to active discover option
            g.gameBoard.mouseManager.stopClick = true;
            PlayTheCard(g);
        }
        protected override void PlayTheCard(Game1 g)
        {

            g.soundManager.PlaySound("addToHand");
            keep = !keep;
            muligan.setKeep(card, keep);

        }
        public override void Draw(Game1 g)
        {
            if (!keep)
            {
                //Drawing.FillRect(new Rectangle((int)X - 50, (int)Y + 200, (int)Width + 70,  60), Color.Red, depth*0.2f, g);
                cross.Draw(position, Width, Height, depth * 0.002f);
            }
            Drawing.FillRect(new Rectangle((int)X - 50, (int)Y - 40, (int)Width + 70, (int)Height + 50), Color.Green, depth * 1.2f, g);
            drawCard(g, X, Y, card, depth, drawManaCost: true, drawBaseStats: false, size: (int)Width);
        }
    }
}
