using CardGame.Graphics;
using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace CardGame.Objects.Collection
{
    public class ManaCrystalCostFilter : Button
    {
        private static Sprite manaIcon = new Sprite(Textures.Mana);
        public ManaCrystalCostFilter(string text, Action<Game1> g) : base(text, g) { }

        public override void Draw(Game1 g)
        {
            manaIcon.Draw(position, Width, Height, layerDepth: 0.02f);
            Drawing.DrawText(text,
                X+60, Y+15, scale: 3f, layerDepth: 0.01f, color: Color.White, drawCenter: true, border: true);
            if (g.collectionPage.collectionManager.filterManaCurrent.ToString().Equals(text[0].ToString()))
            {
                
                Drawing.FillRect(GetHitbox(), col: Color.Green, 0.1f, g, 0.7f);
            }
        }
    }
}
