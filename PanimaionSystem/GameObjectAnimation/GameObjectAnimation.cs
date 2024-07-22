using CardGame.Graphics;
using CardGame.Objects;
using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace CardGame.PanimaionSystem.GameObjectAnimation
{
    public class GameObjectAnimation : GameObject
    {
        private Card_Actor actor;
        private Sprite sprite;
        private int frameWidth;
        private int frameHeight;
        private int frameCount;
        private int currentFrame;
        private float frameTime;
        private float elapsedTime, alpha;
        private int textureFrameWIdth;
        public string ID;

        public GameObjectAnimation(Card_Actor actor, Sprite sprite, int frameWidth, int frameHeight, int frameCount, float frameTime, int textureFrameWIdth, float alpha = 1f) : base(actor.X, actor.Y, frameWidth, frameHeight)
        {
            this.actor = actor;
            this.sprite = sprite;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.frameCount = frameCount;
            this.frameTime = frameTime;
            this.currentFrame = 0;
            this.elapsedTime = 0f;
            this.textureFrameWIdth = textureFrameWIdth;
            this.alpha = alpha;
        }

        public override void Destroy(Game1 g)
        {
            // Cleanup logic if needed
        }

        public override void Draw(Game1 g)
        {
            if (actor is CardBoard_Actor && ((CardBoard_Actor)actor).inAnimation)
            {
                return;
            }

            int row = currentFrame / textureFrameWIdth; // Assuming 7 columns in the sprite sheet
            int column = currentFrame % textureFrameWIdth;
            Rectangle sourceRectangle = new Rectangle(column * frameWidth, row * frameHeight, frameWidth, frameHeight);
            //Rectangle sourceRectangle = new Rectangle(frameWidth * currentFrame, 0, frameWidth, frameHeight);
            //Drawing.FillRect(new Rectangle((int)X, (int)Y, 256, 256), Color.Red, depth, g);
            sprite.Draw(position, Width, Height, layerDepth: depth, sourceRectangle: sourceRectangle, drawCenter: true, alpha: alpha);
        }

        public override void Init(Game1 g)
        {
            // Initialization logic if needed
        }

        public override void Update(GameTime gt, Game1 g)
        {

            depth = actor.depth * 0.01f;
            elapsedTime += (float)gt.ElapsedGameTime.TotalSeconds;

            if (elapsedTime >= frameTime)
            {
                currentFrame++;
                if (currentFrame >= frameCount)
                {
                    currentFrame = 0; // Loop back to the first frame
                }
                elapsedTime = 0f;
            }

            // Update position to follow the actor
            this.Width = actor.Width * 1.5f;
            this.Height = actor.Height*1.5f;
            this.X = actor.GetPosCenter().X;
            this.Y = actor.GetPosCenter().Y;
        }
    }
}
