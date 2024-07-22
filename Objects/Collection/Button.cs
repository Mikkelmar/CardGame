using CardGame.Managers;
using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace CardGame.Objects
{
    public class Button : GameObject, Clickable
    {
        protected string text;
        private Action<Game1> action;
        private float scale;
        public Button(string text, Action<Game1> action, float scale=4f)
        {
            this.text = text;
            this.action = action;
            this.scale = scale;
        }
        public void Clicked(float x, float y, Game1 g)
        {
            if (this.GetHitbox().Contains(x, y))
            {
                action(g);
            }
        }

        public override void Destroy(Game1 g)
        {
            g.collectionPage.mouseManager.Remove(this);
        }

        public override void Draw(Game1 g)
        {
            Drawing.FillRect(GetHitbox(), Color.Yellow, depth+ depth, g);
            Drawing.DrawText(text, X + Width / 2, Y, layerDepth: depth, color: Color.White, drawCenter: true, border: true, scale: scale);
        }

        public override void Init(Game1 g)
        {
            g.collectionPage.mouseManager.Add(this);
        }

        public override void Update(GameTime gt, Game1 g)
        {
        }
    }
}
