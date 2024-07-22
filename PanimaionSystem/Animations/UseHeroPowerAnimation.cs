﻿using CardGame.Cards;
using CardGame.Cards.PanimaionSystem;
using CardGame.Graphics;
using CardGame.HeroPowers;
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
    public class UseHeroPowerAnimation : ObjectAnimation
    {
        private HeroPower heroPower;
        private bool IsAnimating = true;
        private Vector2 startPos, endPos;
        private float Scale = 1f, startScale, endScale, drawDur;
        public UseHeroPowerAnimation(HeroPower heroPower, float speed=2f) : base(speed)
        {
            this.heroPower = heroPower;
            startPos = new Vector2(2000,0);
            endPos = new Vector2(300, 300);
            Width = 540;
            Height = 840;
            startScale = 0.5f;
            endScale = 2f;
            drawDur = 0.7f;
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
            base.Start(g);
        }
        public override void Draw(Game1 g)
        {
            HeroPower_Actor.DrawFullHeroPower(g, heroPower, position, Width, depth);
        }
    }
}