using CardGame.Cards;
using CardGame.Cards.PanimaionSystem;
using CardGame.Graphics;
using CardGame.Objects;
using CardGame.Objects.Cards;
using Engine;
using Microsoft.Xna.Framework;
using System;

namespace CardGame.PanimaionSystem.Animations
{
    public class FireballAnimation : ObjectAnimation
    {
        protected Sprite fireball;
        private bool IsAnimating = true;
        private Card source, target;
        private Vector2 startPos, endPos, previousPos;

        private int frameWidth = 100;
        private int frameHeight = 100;
        private int totalFrames = 61; // Total number of frames in the sprite sheet
        private int startupFrames = 0; // Number of startup frames
        private float timePerFrame = 0.005f; // Time to display each frame
        private float frameElapsedTime = 0;
        private int currentFrame = 0;

        private float stationaryTime; // Time to stay stationary
        private float moveStartTime;  // Time when movement starts

        public FireballAnimation(Card source, Card target, Sprite texture, float duration = 2f) : base(duration)
        {
            this.source = source;
            this.target = target;
            Width = 180;
            Height = 180;
            depth = 0.00000000001f;
            this.fireball = texture;

            stationaryTime = duration * 0.3f; // 15% of total duration
            moveStartTime = stationaryTime;    // Movement starts after stationary time
        }

        public override void Update(GameTime gt, Game1 g)
        {
            if (IsAnimating)
            {
                frameElapsedTime += (float)gt.ElapsedGameTime.TotalSeconds;

                // Update the current frame based on elapsed time
                if (frameElapsedTime >= timePerFrame)
                {
                    frameElapsedTime -= timePerFrame;

                    if (currentFrame < startupFrames)
                    {
                        currentFrame++;
                    }
                    else
                    {
                        currentFrame = startupFrames + ((currentFrame - startupFrames + 1) % (totalFrames - startupFrames));
                    }
                }

                // Store the current position before updating it
                Vector2 currentPosition = new Vector2(X, Y);

                elapsedTime += (float)gt.ElapsedGameTime.TotalSeconds;

                if (elapsedTime > moveStartTime)
                {
                    // Calculate the normalized time (0 to 1) for the movement phase
                    float t = MathHelper.Clamp((elapsedTime - moveStartTime) / (duration - stationaryTime), 0f, 1f);

                    // Apply exponential function for speedup
                    float exponentialT = t * t;

                    // Linear interpolation with increasing speed
                    X = MathHelper.Lerp(startPos.X, endPos.X, exponentialT);
                    Y = MathHelper.Lerp(startPos.Y, endPos.Y, exponentialT);

                    // Calculate direction and rotation
                    Vector2 direction = new Vector2(X - previousPos.X, Y - previousPos.Y);
                    float rotation = (float)Math.Atan2(direction.Y, direction.X) + MathHelper.PiOver2 + MathHelper.Pi; // Adjusting rotation by 180 degrees (π radians)

                    // Update previous position
                    previousPos = currentPosition;

                    // Check if the animation is complete
                    if (t >= 1f)
                    {
                        IsAnimating = false;
                    }
                }
            }
            base.Update(gt, g);
        }
        protected override void AnimationFinished(Game1 g)
        {
            base.AnimationFinished(g);
            g.soundManager.PlaySound("Fire impact");

        }
        public override void Start(Game1 g)
        {
            g.soundManager.PlaySound("shootFireball");
            // Initialize start and end positions
            if (AnimationUtils.getCardActor(g, source) == null)
            {
                startPos = g.gameBoard.gameInterface.getPlayer(source.belongToPlayer).heroActor.GetPosCenter();
            }
            else
            {
                startPos = AnimationUtils.getCardActor(g, source).GetPosCenter();
            }

            endPos = AnimationUtils.getCardActor(g, target).GetPosCenter();

            // Set initial previous position to start position
            previousPos = startPos;

            base.Start(g);
        }

        public override void Draw(Game1 g)
        {
            // Calculate the source rectangle for the current frame
            int row = currentFrame / 8; // Assuming 8 columns in the sprite sheet
            int column = currentFrame % 8;
            Rectangle sourceRectangle = new Rectangle(column * frameWidth, row * frameHeight, frameWidth, frameHeight);

            Vector2 drawPos = new Vector2(X, Y);

            if (elapsedTime <= moveStartTime)
            {
                // If still in stationary phase, draw at start position
                drawPos = startPos;
            }

            // Calculate direction and rotation
            Vector2 direction = new Vector2(X - previousPos.X, Y - previousPos.Y);
            float rotation = (float)Math.Atan2(direction.Y, direction.X) + MathHelper.PiOver2 + MathHelper.Pi; // Adjusting rotation by 180 degrees (π radians)

            // Offset the draw position slightly in the opposite direction of movement
            float offsetMagnitude = 200f; // Adjust this value as needed
            Vector2 offset = Vector2.Normalize(direction) * offsetMagnitude;

            fireball.Draw(new Vector2(drawPos.X - offset.X, drawPos.Y - offset.Y), Width * 8, Height * 8, layerDepth: depth, sourceRectangle: sourceRectangle, rotation: rotation, drawCenter: true);
        }
    }
}
