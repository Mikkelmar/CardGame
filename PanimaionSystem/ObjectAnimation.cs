using CardGame.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.Cards.PanimaionSystem
{

    public abstract class ObjectAnimation : GameObject, IQueueable
    {
        protected float duration;
        protected float elapsedTime;
        public bool haveStarted = false;
        public ObjectAnimation (float duration) : base ()
        {
            this.duration = duration;
            this.elapsedTime = 0f;
        }

        public bool IsComplete => elapsedTime >= duration;

        bool IQueueable.haveStarted => haveStarted;

        public override void Update(GameTime gameTime, Game1 g)
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
            haveStarted = true;
        }

        public override void Destroy(Game1 g)
        {
        }

        public override void Draw(Game1 g)
        {
            
        }

        public override void Init(Game1 g)
        {
        }
    }
}
