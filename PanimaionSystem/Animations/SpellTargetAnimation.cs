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
    public class SpellTargetAnimation : ObjectAnimation
    {
        protected Sprite spell;
        private bool IsAnimating = true;
        private Card source;
        private Vector2 startPos;

        private int frameWidth = 256;
        private int frameHeight = 256;
        private int totalFrames = 16; // Total number of frames in the sprite sheet
        private float timePerFrame; // Time to display each frame
        private float frameElapsedTime = 0;
        private int currentFrame = 0;

        public SpellTargetAnimation(Card source, Sprite texture, float duration = 1f, float scale=1f) : base(duration)
        {
            this.source = source;
            Width = 120* scale;
            Height = 120* scale;
            if(source is PlayerCard)
            {
                Width = 250* scale;
                Height = 250 * scale;
            }
            depth = 0.00000000001f;
            this.spell = texture;
            timePerFrame = duration / totalFrames;
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
                    currentFrame++;

                    // Stop animating if we've reached the last frame
                    if (currentFrame >= totalFrames)
                    {
                        IsAnimating = false;
                    }
                }
            }
            base.Update(gt, g);
        }

        public override void Start(Game1 g)
        {
            // Initialize start position
            if (AnimationUtils.getCardActor(g, source) == null)
            {
                startPos = g.gameBoard.gameInterface.getPlayer(source.belongToPlayer).heroActor.GetPosCenter();
            }
            else
            {
                startPos = AnimationUtils.getCardActor(g, source).GetPosCenter();
            }

            base.Start(g);
        }

        public override void Draw(Game1 g)
        {
            if (!IsAnimating) return;

            // Calculate the source rectangle for the current frame
            int row = currentFrame / 8; // Assuming 8 columns in the sprite sheet
            int column = currentFrame % 8;
            Rectangle sourceRectangle = new Rectangle(column * frameWidth, row * frameHeight, frameWidth, frameHeight);

            spell.Draw(new Vector2(startPos.X, startPos.Y), Width * 8, Height * 8, layerDepth: depth, sourceRectangle: sourceRectangle, drawCenter: true);
        }
    }
}
