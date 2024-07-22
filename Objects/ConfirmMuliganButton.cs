using CardGame.Managers;
using CardGame.Managers.GameManagers;
using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.Objects
{
    public class ConfirmMuliganButton : GameObject, Clickable
    {
        Player playerMuligan;
        public ConfirmMuliganButton(Player playerMuligan)
        {
            this.playerMuligan = playerMuligan;
            X = 3500;
            Y = 2000;
            Width = 600;
            Height = 300;
        }
        public void Clicked(float x, float y, Game1 g)
        {
            if (this.GetHitbox().Contains(x, y))
            {
                g.gameBoard.networkHandler.SendMuliganConfirmed(playerMuligan.muligan.getWantToKeep());
                g.gameBoard.gameHandler.StopSelecting(g);
            }
        }

        public override void Destroy(Game1 g)
        {
            g.gameBoard.mouseManager.Remove(this);
        }

        public override void Draw(Game1 g)
        {
            Drawing.FillRect(GetHitbox(), Color.DarkGreen, depth+ depth+ depth, g);
            Rectangle r = GetHitbox();
            r.Inflate(-20f, -20f);
            Drawing.FillRect(r, Color.Green, depth + depth, g);
            Drawing.DrawText("Confirm", X + Width / 2, Y+80, layerDepth: depth, color: Color.White, drawCenter: true, border: true, scale: 4f);

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
