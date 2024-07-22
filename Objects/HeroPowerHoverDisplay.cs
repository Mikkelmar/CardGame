using CardGame.HeroPowers;
using CardGame.Objects.Cards;
using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.Objects
{
    public class HeroPowerHoverDisplay : GameObject
    {
        private HeroPower heroPower;
        public HeroPowerHoverDisplay(HeroPower heroPower) : base()
        {
            this.heroPower = heroPower;
            depth = 0.000000001f;
            X = 50;
            Y = 50;
            Width = 540 * 1.5f;
            Height = 840 * 1.5f;
        }

        public override void Destroy(Game1 g)
        {
        }

        public override void Draw(Game1 g)
        {
            float yPos = Math.Min(Math.Max(200, Y), Drawing.WINDOW_HEIGHT-Height);
            HeroPower_Actor.DrawFullHeroPower(g, heroPower, new Vector2(X, yPos), Width, depth);
        }

        public override void Init(Game1 g)
        {
        }

        public override void Update(GameTime gt, Game1 g)
        {
        }
    }
}
