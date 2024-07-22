using CardGame.Managers;
using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace CardGame.Objects
{
    public class OnlyCanAddButton : GameObject, Clickable
    {
    
        public void Clicked(float x, float y, Game1 g)
        {
            if (this.GetHitbox().Contains(x, y))
            {
                g.collectionPage.collectionManager.filterByCanAdd = !g.collectionPage.collectionManager.filterByCanAdd;
                g.collectionPage.collectionManager.LoadPages(g);
            }
        }

        public override void Destroy(Game1 g)
        {
            g.collectionPage.mouseManager.Remove(this);
        }

        public override void Draw(Game1 g)
        {
            if (g.collectionPage.collectionManager.filterByCanAdd)
            {
                Drawing.FillRect(GetHitbox(), Color.Green, depth + depth, g);
            }
            else
            {
                Drawing.FillRect(GetHitbox(), Color.Gray, depth + depth, g);
            }
            
            Drawing.DrawText("Only show addable cards", 
                X + Width / 2, Y+30, layerDepth: depth, color: Color.White, drawCenter: true, border: true, scale: 2.5f);
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
