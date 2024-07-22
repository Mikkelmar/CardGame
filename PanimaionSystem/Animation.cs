using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.Cards.PanimaionSystem
{

    public class GameAnimation : IQueueable
    {
        private float duration;
        private float elapsedTime;
        private bool _haveStarted = false;
        public bool haveStarted => _haveStarted;
        public GameAnimation(float duration)
        {
            this.duration = duration;
            this.elapsedTime = 0f;
        }

        public bool IsComplete => elapsedTime >= duration;

        public virtual void Update(GameTime gameTime, Game1 g)
        {
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (IsComplete)
            {
                AnimationFinished(g);
            }
        }
        protected virtual void AnimationFinished(Game1 g)
        {

        }

        public virtual void Start(Game1 g)
        {
            elapsedTime = 0f;
            _haveStarted = true;
        }

    }
}
