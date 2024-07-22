using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.Cards.PanimaionSystem
{
    public class GameAction : IQueueable
    {
        private Action<Game1> effect;
        private bool isComplete;

        public GameAction(Action<Game1> effect)
        {
            this.effect = effect;
            this.isComplete = false;
        }

        public bool IsComplete => isComplete;

        public bool haveStarted => true;

        public void Update(GameTime gameTime, Game1 g)
        {
            // Actions are instantaneous in this example
            if (!isComplete)
            {
                effect(g); // Pass the appropriate Game1 instance
                isComplete = true;
            }
        }

        public void Start(Game1 g)
        {
            isComplete = false;
        }
    }
}
