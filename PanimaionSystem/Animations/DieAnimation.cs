using CardGame.Cards;
using CardGame.Cards.PanimaionSystem;
using CardGame.Graphics;
using CardGame.Objects;
using CardGame.Objects.Cards;
using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using static CardGame.Objects.Cards.MinionCard;

namespace CardGame.PanimaionSystem.Animations
{
    public class DieAnimation : ObjectAnimation
    {
        protected Sprite summonCard;
        private CardBoard_Actor objectToDie;
        private float alphaValue = 0f;
        private bool IsAnimating = true;
        public DieAnimation(CardBoard_Actor actor, float speed=2f) : base(speed)
        {
            objectToDie = actor;
            Width = 400;
            Height = 400;
            depth = 0.00000000001f;
            this.summonCard = new Sprite(Textures.DieCard);
            SetPosition(actor.X, actor.Y);
        }
        public override void Update(GameTime gt, Game1 g)
        {
            X = objectToDie.X;
            Y = objectToDie.Y;
            if (IsAnimating)
            {
                elapsedTime += (float)gt.ElapsedGameTime.TotalSeconds;

                // Calculate the normalized time (0 to 1)
                float t = MathHelper.Clamp(elapsedTime / duration, 0f, 1f);

         

                // Interpolate position and scale
                //X = MathHelper.Lerp(startPos.X, endPos.X, t);
                //Y = MathHelper.Lerp(startPos.Y, endPos.Y, t);
                alphaValue = MathHelper.Lerp(0f, 1f, t);
                
                // Check if the animation is complete
                if (t >= 1f)
                {
                    IsAnimating = false;
                }
            }
            base.Update(gt, g);
        }
        public override void Start(Game1 g)
        {
            base.Start(g);
        }
        public override void Draw(Game1 g)
        {
            summonCard.Draw(position, Width, Height, layerDepth: depth, alpha: alphaValue);
        }
        
    }
}
