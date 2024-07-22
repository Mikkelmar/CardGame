using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.Graphics
{
    public class Sprite
    {
        public Texture2D Texture { get; private set; }
        public Sprite(Texture2D texture)
        {
            Texture = texture;
        }

        public void Draw(Vector2 pos, float width, float height, float layerDepth = 0.01f, float scale = 1, float alpha = 1f, Rectangle? sourceRectangle=null, SpriteEffects spriteEffects = SpriteEffects.None, float rotation = 0.0f, bool drawCenter = false)
        {
            Vector2 _scale = new Vector2(
                    width / Texture.Width,
                    height / Texture.Height) * scale;
            Rectangle _sourceRectangle;
            if (sourceRectangle == null)
            {
                _sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
            }
            else
            {
                _sourceRectangle = (Rectangle)sourceRectangle;
                _scale = new Vector2(width / (float)_sourceRectangle.Width, height / (float)_sourceRectangle.Height) * scale;
            }

            Vector2 origin;
            if (drawCenter)
            {
                origin = new Vector2(_sourceRectangle.Width / 2, _sourceRectangle.Height / 2);
            }
            else
            {
                origin = Vector2.Zero;
            }

            Drawing._spriteBatch.Draw(
                texture: Texture,
                position: pos,
                layerDepth: layerDepth,
                scale: _scale,
                sourceRectangle: _sourceRectangle,
                origin: origin,
                rotation: rotation,
                color: Color.White * alpha,
                effects: spriteEffects
            );
        }

    }
}
