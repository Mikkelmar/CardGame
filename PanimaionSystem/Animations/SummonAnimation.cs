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
    public class SummonAnimation : ObjectAnimation
    {
        protected Sprite summonCard;
        private bool IsAnimating = true;
        private CardBoard_Actor objectToSummon;
        private float alphaValue = 0f;
        public SummonAnimation(CardBoard_Actor actor, float speed=2f) : base(speed)
        {
            objectToSummon = actor;
            Width = 400;
            Height = 400;
            depth = 0.00000000001f;
            this.summonCard = new Sprite(Textures.summonCard);
            SetPosition(actor.X, actor.Y);
        }
        public override void Update(GameTime gt, Game1 g)
        {
            X = objectToSummon.X;
            Y = objectToSummon.Y;
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
            
            g.soundManager.PlaySound("cardPlace4");
            base.Start(g);
        }
        public override void Draw(Game1 g)
        {
            summonCard.Draw(position, Width, Height, layerDepth: depth, alpha: alphaValue);
        }
        
    }
}
