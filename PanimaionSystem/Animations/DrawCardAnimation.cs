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
    public class DrawCardAnimation : ObjectAnimation
    {
        private Card cardToDraw;
        private bool IsAnimating = true;
        private Vector2 startPos, endPos;
        private float Scale = 1f, startScale, endScale, drawDur;
        public DrawCardAnimation(Card cardToDraw, float speed=2f) : base(speed)
        {
            this.cardToDraw = cardToDraw;
            startPos = new Vector2(5000,3000);
            endPos = new Vector2(3600, 500);
            Width = 540;
            Height = 840;
            startScale = 0.5f;
            endScale = 2f;
            drawDur = 0.8f;
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
            t--;
            return t * t * t + 1;
        }
        public override void Start(Game1 g)
        {
            g.soundManager.PlaySound("draw");
            base.Start(g);
        }
        public override void Draw(Game1 g)
        {
            DrawCard(g, cardToDraw);
        }
        private void DrawCard(Game1 g, Card card)
        {
            if(g.gameBoard.isPlayer == card.belongToPlayer)
            {
                Card_Actor.drawCard(g, X, Y, card, depth, size: (int)Width);
            }
            else
            {
                Card_Actor.drawACardBack(g, X, Y, depth);
            }

            
        }
    }
}
