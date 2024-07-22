using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.Objects.MiscObjects
{
    public class DamageCounter : GameObject
    {
        GameObject cardToFollow;
        private TimeSpan duration = TimeSpan.FromSeconds(0.7f);
        private TimeSpan elapsedTime;
        private float initialScale = 0.0f;
        private float finalScale = 1.0f;
        private float scale;
        private float alpha = 1.0f; // Full opacity
        private string number;
        private Color color;
        public DamageCounter(GameObject cardToFollow, int number)
        {
            this.cardToFollow = cardToFollow;

            this.number = number.ToString();
            if (number > 0)
            {
                color = Color.Green;
                this.number = "+" + this.number;
            }
            else
            {
                color = Color.Red;
            }
            this.elapsedTime = TimeSpan.Zero;
            depth = 0.00000001f;
        }
        public override void Update(GameTime gameTime, Game1 g)
        {

            SetPosition(cardToFollow.GetPosCenter().X, cardToFollow.GetPosCenter().Y);
            elapsedTime += gameTime.ElapsedGameTime;

            // Calculate the times for each phase
            double popDuration = duration.TotalSeconds * 0.2;
            double stayDuration = duration.TotalSeconds * 0.6;
            double fadeDuration = duration.TotalSeconds * 0.2;

            double totalSeconds = elapsedTime.TotalSeconds;

            // Phase 1: Pop up
            if (totalSeconds <= popDuration)
            {
                scale = MathHelper.Lerp(initialScale, finalScale, (float)(totalSeconds / popDuration));
            }
            // Phase 2: Stay at final scale
            else if (totalSeconds <= popDuration + stayDuration)
            {
                scale = finalScale;
            }
            // Phase 3: Fade out
            else if (totalSeconds <= popDuration + stayDuration + fadeDuration)
            {
                scale = finalScale;
                float fadeProgress = (float)((totalSeconds - (popDuration + stayDuration)) / fadeDuration);
                alpha = MathHelper.Lerp(1.0f, 0.0f, fadeProgress);
            }
            else
            {
                // The animation is complete, you may want to flag this object for removal
                g.gameBoard.objectManager.Remove(this, g);
            }
        }
        public override void Destroy(Game1 g)
        {
        }

        public override void Draw(Game1 g)
        {
            //Drawing.FillRect(GetHitbox(), Color.Orange,);
            Drawing.DrawText(number, X, Y, drawCenter: true, border: true, scale: 6f* scale, layerDepth: depth, color: color);
        }

        public override void Init(Game1 g)
        {
        }

        
    }
}
