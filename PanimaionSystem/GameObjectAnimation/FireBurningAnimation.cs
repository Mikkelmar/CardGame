using CardGame.Cards.PanimaionSystem;
using CardGame.Graphics;
using CardGame.Objects;
using Engine;
using Microsoft.Xna.Framework;

namespace CardGame.PanimaionSystem.GameObjectAnimation
{
    public class FireBurningAnimation : ObjectAnimation
    {
        protected Sprite spell;
        private Vector2 startPos;

        private int frameWidth = 128;
        private int frameHeight = 128;
        private int totalFrames = 30; // Total number of frames in the sprite sheet
        private float timePerFrame; // Time to display each frame
        private float frameElapsedTime = 0;
        private int currentFrame = 0;

        public FireBurningAnimation(Vector2 startPos, Sprite texture, float duration = 1f) : base(duration)
        {
            this.startPos = startPos;
            Width = 120;
            Height = 120;
            depth = 0.00000000001f;
            this.spell = texture;
            timePerFrame = duration / totalFrames;
        }

        public override void Update(GameTime gt, Game1 g)
        {
            
            frameElapsedTime += (float)gt.ElapsedGameTime.TotalSeconds;

            // Update the current frame based on elapsed time
            if (frameElapsedTime >= timePerFrame)
            {
                frameElapsedTime -= timePerFrame;
                currentFrame++;

                // Stop animating if we've reached the last frame
                    
            }
            
            base.Update(gt, g);
        }

        public override void Start(Game1 g)
        {
            base.Start(g);
        }

        public override void Draw(Game1 g)
        {
            
            // Calculate the source rectangle for the current frame
            int row = currentFrame / 6; // Assuming 8 columns in the sprite sheet
            int column = currentFrame % 6;
            Rectangle sourceRectangle = new Rectangle(column * frameWidth, row * frameHeight, frameWidth, frameHeight);

            spell.Draw(new Vector2(startPos.X, startPos.Y), Width * 8, Height * 8, layerDepth: depth, sourceRectangle: sourceRectangle, drawCenter: true);
        }
    }
}