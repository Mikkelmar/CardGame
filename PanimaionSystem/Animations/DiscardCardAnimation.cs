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
    public class DiscardCardAnimation : ObjectAnimation
    {
        private Card cardToDraw;
        private bool IsAnimating = true;
        private Vector2 startPos, endPos;
        private float Scale = 1f, startScale, endScale, drawDur;
        public DiscardCardAnimation(Card cardToDraw, float speed, Vector2 startPos, Vector2 endPos) : base(speed)
        {
            this.cardToDraw = cardToDraw;
            this.startPos = startPos;
            this.endPos = endPos;
            Width = 540;
            Height = 840;
            startScale = 1f;
            endScale = 1.6f;
            drawDur = speed;
            depth = 0.00000000001f;
        }
        public override void Update(GameTime gt, Game1 g)
        {
            if (IsAnimating)
            {
                elapsedTime += (float)gt.ElapsedGameTime.TotalSeconds;

                // Calculate the normalized time (0 to 1)
                float t = MathHelper.Clamp(elapsedTime / drawDur, 0f, 1f);

                // Apply an easing function for smooth animation
                t = EaseOutCubic(t);

                // Interpolate position and scale
                X = MathHelper.Lerp(startPos.X, endPos.X, t);
                Y = MathHelper.Lerp(startPos.Y, endPos.Y, t);
                Scale = MathHelper.Lerp(startScale, endScale, t);
                Width = 540 * Scale;
                Height = 840 * Scale;
                // Check if the animation is complete
                if (t >= 1f)
                {
                    IsAnimating = false;
                }
            }
            base.Update(gt, g);
        }
        private float EaseOutCubic(float t)
        {
            return (float)(1 - Math.Pow(1 - t, 5));
        }
        public override void Start(Game1 g)
        {
            base.Start(g);
        }
        public override void Draw(Game1 g)
        {
            DrawCard(g, cardToDraw);
        }
        private void DrawCard(Game1 g, Card card)
        {
            Card_Actor.drawCard(g, X, Y, card, depth, size: (int)Width);
        }
    }
}
