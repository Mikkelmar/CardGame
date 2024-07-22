using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.Cards.PanimaionSystem
{
    public interface IQueueable
    {
        bool IsComplete { get; }
        bool haveStarted { get; }
        void Update(GameTime gameTime, Game1 g);
        void Start(Game1 g);
    }
}
