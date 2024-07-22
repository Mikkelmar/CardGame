using CardGame.Managers;
using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.Objects
{
    public class EndTurnButton : GameObject, Clickable
    {
        public void Clicked(float x, float y, Game1 g)
        {
            if (!g.gameBoard.CanPlay())
            {
                return;
            }

            if (this.GetHitbox().Contains(x, y))
            {
                if(g.gameBoard.gameHandler.ActivePlayer == g.gameBoard.isPlayer 
                    && !g.gameBoard.gameHandler.activeOptionSelection 
                    && g.gameBoard.gameHandler.player1.DoneMuligan && g.gameBoard.gameHandler.player2.DoneMuligan)
                {
                    g.gameBoard.networkHandler.SendEndTurnMessage(g.gameBoard.gameHandler.ActivePlayer.id.ToString());
                }
                
                //g.gameBoard.gameHandler.EndTurn(g);
            }
        }

        public override void Destroy(Game1 g)
        {
            g.gameBoard.mouseManager.Remove(this);
        }

        public override void Draw(Game1 g)
        {
            
            if (g.gameBoard.gameHandler.ActivePlayer == g.gameBoard.isPlayer
                    && !g.gameBoard.gameHandler.activeOptionSelection
                    && g.gameBoard.gameHandler.player1.DoneMuligan && g.gameBoard.gameHandler.player2.DoneMuligan)
            {
                Drawing.FillRect(GetHitbox(), Color.Green, depth+ depth, g);
                Drawing.DrawText("End turn", X + Width / 2, Y, layerDepth: depth, color: Color.White, drawCenter: true, border: true, scale: 4f);

            }
            else
            {
                Drawing.FillRect(GetHitbox(), Color.Gray, depth, g);
            }
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
