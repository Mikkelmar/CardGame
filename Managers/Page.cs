using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.Managers
{
    public abstract class Page
    {

        public int id;
        public Page(int id)
        {
            this.id = id;
        }

        //public HudManager hudManager { get; } = new HudManager();
        //public MouseManager mouseManager { get; } = new MouseManager();
        public abstract void Init(Game1 g);
        public abstract void Update(GameTime gt, Game1 g);
        public abstract void Draw(Game1 g);
        public abstract void Load(Game1 g);
        public abstract void DrawUI(Game1 g);
        public abstract ObjectManager getObjectManager();
        public abstract MouseManager getMouseManager();
    }
}
