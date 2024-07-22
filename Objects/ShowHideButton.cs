using CardGame.Managers;
using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.Objects
{
    public class ShowHideButton : GameObject, Clickable
    {
        OptionSelectManager source;
        public ShowHideButton(OptionSelectManager source)
        {
            this.source = source;
        }
        public void Clicked(float x, float y, Game1 g)
        {
            if (this.GetHitbox().Contains(x, y))
            {
                source.ToggleHide();
            }
        }

        public override void Destroy(Game1 g)
        {
            g.gameBoard.mouseManager.Remove(this);
        }

        public override void Draw(Game1 g)
        {
            Drawing.FillRect(GetHitbox(), Color.Cyan, depth + depth, g);
            Drawing.DrawText("Hide/Show", X + Width / 2, Y, layerDepth: depth, color: Color.White, drawCenter: true, border: true, scale: 4f);

        }

        public override void Init(Game1 g)
        {
            g.gameBoard.mouseManager.Add(this);
        }

        public override void Update(GameTime gt, Game1 g)
        {
        }
    }
}
