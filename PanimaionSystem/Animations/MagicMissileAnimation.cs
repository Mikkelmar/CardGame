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
    public class MagicMissileAnimation : ObjectAnimation
    {
        protected Sprite bullet;
        private bool IsAnimating = true;
        private Card source, target;
        private Vector2 startPos, endPos, controlPos, previousPos;
        private Random random = new Random();
        private float controlPointOffset = 1000f; // Offset for the control point

        private int frameWidth = 100;
        private int frameHeight = 100;
        private int totalFrames = 35; // Total number of frames in the sprite sheet
        private int startupFrames = 8; // Number of startup frames
        private float timePerFrame = 0.04f; // Time to display each frame
        private float frameElapsedTime = 0;
        private int currentFrame = 0;

        public MagicMissileAnimation(Card source, Card target, Sprite texture, float duration = 2f) : base(duration)
        {
            this.source = source;
            this.target = target;
            Width = 100;
            Height = 100;
            depth = 0.00000000001f;
            this.bullet = texture;
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

                // Calculate the normalized time (0 to 1)
                float t = MathHelper.Clamp(elapsedTime / duration, 0f, 1f);

                // Apply exponential function for speedup
                float exponentialT = t * t;

                // Smooth quadratic Bezier curve interpolation
                Vector2 p0 = startPos;
                Vector2 p1 = controlPos;
                Vector2 p2 = endPos;

                float u = 1 - exponentialT;
                Vector2 pos = (u * u * p0) + (2 * u * exponentialT * p1) + (exponentialT * exponentialT * p2);

                X = pos.X;
                Y = pos.Y;

                // Calculate direction and rotation
                Vector2 direction = pos - currentPosition;
                float rotation = (float)Math.Atan2(direction.Y, direction.X) + MathHelper.PiOver2 + MathHelper.Pi; // Adjusting rotation by 180 degrees (π radians)

                // Update previous position
                previousPos = currentPosition;

                // Check if the animation is complete
                if (t >= 1f)
                {
                    IsAnimating = false;
                }
            }
            base.Update(gt, g);
        }
        protected override void AnimationFinished(Game1 g)
        {
            base.AnimationFinished(g);
            g.soundManager.PlaySound("SpellHit");
        }
        public override void Start(Game1 g)
        {
            g.soundManager.PlaySound("iceball", volume: 0.6f);
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

            // Generate a random control point off to one side with significant offset
            float offsetX = (float)(random.NextDouble() * 0.5 + 0.5) * controlPointOffset * (random.Next(2) == 0 ? 1 : -1); // Random value between -controlPointOffset and +controlPointOffset
            float offsetY = (float)(random.NextDouble() * 0.5 + 0.5) * controlPointOffset * (random.Next(2) == 0 ? 1 : -1); // Random value between -controlPointOffset and +controlPointOffset
            controlPos = new Vector2((startPos.X + endPos.X) / 2 + offsetX, (startPos.Y + endPos.Y) / 2 + offsetY);

            base.Start(g);
        }

        public override void Draw(Game1 g)
        {
            // Calculate the source rectangle for the current frame
            int row = currentFrame / 7; // Assuming 7 columns in the sprite sheet
            int column = currentFrame % 7;
            Rectangle sourceRectangle = new Rectangle(column * frameWidth, row * frameHeight, frameWidth, frameHeight);

            // Calculate direction and rotation
            Vector2 direction = new Vector2(X - previousPos.X, Y - previousPos.Y);
            float rotation = (float)Math.Atan2(direction.Y, direction.X) + MathHelper.PiOver2 + MathHelper.Pi; // Adjusting rotation by 180 degrees (π radians)

            // Offset the draw position slightly in the opposite direction of movement
            float offsetMagnitude = 200f; // Adjust this value as needed
            Vector2 offset = Vector2.Normalize(direction) * offsetMagnitude;

            bullet.Draw(new Vector2(X - offset.X, Y - offset.Y), Width * 8, Height * 8, layerDepth: depth, sourceRectangle: sourceRectangle, rotation: rotation, drawCenter: true);
        }
    }
}
