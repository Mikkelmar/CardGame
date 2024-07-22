using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CardGame.Objects;
using System;
using Engine;

namespace CardGame.PanimaionSystem.Animations
{
    public class TextPopup : GameObject
    {
        private string text;
        private float duration; // Duration for which the text stays on screen
        private float elapsed;  // Time elapsed since the text was displayed
        private Color color;
        private float initialScale; // Initial scale value for the text
        private float finalScale;   // Final scale value for the text
        private float initialAlpha; // Initial alpha value for the text
        private float scale;

        public TextPopup(string text, float duration, float x, float y, float scale = 1.5f, Color? color = null) : base(x, y)
        {
            this.text = text;
            this.duration = duration;
            this.elapsed = 0f;
            this.color = color ?? Color.White;
            this.depth = 0.0000000000005f;
            this.initialScale = 0.1f; // Start from a very small scale
            this.finalScale = scale * 10;
            this.initialAlpha = this.color.A / 255f; // Store initial alpha value
        }

        public override void Update(GameTime gt, Game1 g)
        {
            elapsed += (float)gt.ElapsedGameTime.TotalSeconds;

            // Calculate the scale for the first second to create a "popping" effect
            float currentScale = initialScale;
            if (elapsed < 0.2f)
            {
                currentScale = MathHelper.Lerp(initialScale, finalScale, elapsed / 0.2f);
            }
            else
            {
                currentScale = finalScale;
            }

            // Fade out the text after the first second
            float alpha = MathHelper.Lerp(initialAlpha, 0, (elapsed - 1f) / (duration - 1f));
            color = new Color(color.R, color.G, color.B, alpha);

            // Remove the text after duration is over
            if (elapsed >= duration)
            {
                g.gameBoard.objectManager.Remove(this, g); // Assuming Game1 has a method to remove objects
            }

            // Update the scale
            scale = currentScale;
        }

        public override void Draw(Game1 g)
        {
            // Draw text centered on the screen
            Drawing.DrawText(
                text,
                X,
                Y,
                layerDepth: depth,
                color: color,
                scale: scale,
                drawCenter: true,
                border: true
            );
        }

        public override void Destroy(Game1 g)
        {
            // Implementation for destroying the object if needed
        }

        public override void Init(Game1 g)
        {
            // Initialization code if needed
        }
    }
    }
