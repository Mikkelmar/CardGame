using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using CardGame.Graphics;
using CardGame;
using System.Collections.Generic;

namespace Engine
{
    public static class Drawing
    {
        public static int WINDOW_WIDTH = 2560*2, WINDOW_HEIGHT = 1080*2;


        public static string TITLE = "Card game";
        public static bool vsync = false;

        //Graphics
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch _spriteBatch;
        private static Texture2D rect;

        // frametime
        public static float fps, delta;

        public static void Initialize(Game1 g)
        {
            g.IsFixedTimeStep = false;
            graphics = new GraphicsDeviceManager(g);
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            graphics.SynchronizeWithVerticalRetrace = vsync;
            graphics.HardwareModeSwitch = false;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            _spriteBatch = new SpriteBatch(g.GraphicsDevice);
            g.GraphicsDevice.PresentationParameters.MultiSampleCount = 4; //hmm unsure

        }
        public static void Update(GameTime gt, Game1 g)
        {
            delta = (float)gt.ElapsedGameTime.TotalSeconds;
            fps = (float)(1 / delta);

        }
        public static void DrawText(string text, float x, float y, Game1 g, float layerDepth = 0.0001f, Color? color = null, float scale = 1f)
        {
            DrawText(text, x - g.gameBoard.cam.position.X, y - g.gameBoard.cam.position.Y, layerDepth, color, scale);
        }
        
        public static void DrawText(string text, float x, float y, float layerDepth = 0.0001f, Color? color = null, float scale = 1f, bool border = false, bool drawCenter = false)
        {
            if (drawCenter)
            {
                float length = TextHandler.textLength(text) * scale;
                x -= length / 2;
            }
            if (color == null)
            {
                color = Color.White;
            }
            _spriteBatch.DrawString(Textures.font_cardo, text, new Vector2(x, y), (Color)color, 0.0f, new Vector2(0, 0), scale, SpriteEffects.None, layerDepth);
            if (border)
            {
                _spriteBatch.DrawString(Textures.font_cardo, text, new Vector2(x, y) + new Vector2(2 * scale, 2 * scale), (Color)Color.Black, 0.0f, new Vector2(0, 0), scale, SpriteEffects.None, layerDepth + layerDepth*0.1f);
                _spriteBatch.DrawString(Textures.font_cardo, text, new Vector2(x, y) + new Vector2(-2 * scale, 2 * scale), (Color)Color.Black, 0.0f, new Vector2(0, 0), scale, SpriteEffects.None, layerDepth + layerDepth * 0.1f);
                _spriteBatch.DrawString(Textures.font_cardo, text, new Vector2(x, y) + new Vector2(2 * scale, -2 * scale), (Color)Color.Black, 0.0f, new Vector2(0, 0), scale, SpriteEffects.None, layerDepth + layerDepth * 0.1f);
                _spriteBatch.DrawString(Textures.font_cardo, text, new Vector2(x, y) + new Vector2(-2 * scale, -2 * scale), (Color)Color.Black, 0.0f, new Vector2(0, 0), scale, SpriteEffects.None, layerDepth + layerDepth * 0.1f);
            }
        }
        public static void FillRect(Rectangle bounds, Color col, float depth, Game1 g, float alpha = 1f)
        {
            if (rect == null) { rect = new Texture2D(g.GraphicsDevice, 1, 1); }
            rect.SetData(new[] { Color.White });
            _spriteBatch.Draw(rect, bounds, null, col*alpha, 0, new Vector2(0, 0), SpriteEffects.None, depth);
        }
        public static void DrawLine(Texture2D texture, Vector2 begin, Vector2 end)
        {
            _spriteBatch.Draw(texture, begin, null, Color.White,
                         (float)Math.Atan2(end.Y - begin.Y, end.X - begin.X),
                         new Vector2(0f, (float)texture.Height / 2),
                         new Vector2(Vector2.Distance(begin, end) / texture.Width, 1f),
                         SpriteEffects.None, 0f);
        }

        public static void DrawFormattedText(string text, Vector2 position, float layerDepth = 0.0001f, Color? color = null, float scale = 1f, bool border = false, bool drawCenter = false)
        {
            var parts = ParseFormattedString(text);
            if (drawCenter)
            {
                float length = 0;
                foreach (var part in parts)
                {
                    SpriteFont font = part.IsBold ? Textures.font_robortoBold : Textures.font_cardo;
                    length += font.MeasureString(part.Text).X * scale;
                }
                position.X -= length / 2;
            }
            if (color == null)
            {
                color = Color.White;
            }

            
            Vector2 currentPosition = position;

            foreach (var part in parts)
            {
                SpriteFont font = part.IsBold ? Textures.font_robortoBold : Textures.font_cardo;
                Vector2 size = font.MeasureString(part.Text)*scale;

                _spriteBatch.DrawString(font, part.Text, currentPosition, (Color)color, 0.0f, new Vector2(0, 0), scale, SpriteEffects.None, layerDepth);

                currentPosition.X += size.X;
            }
        }

        private static List<TextPartData> ParseFormattedString(string text)
        {
            var parts = new List<TextPartData>();
            var regex = new System.Text.RegularExpressions.Regex(@"<b>(.*?)<\/b>|([^<]+)");
            var matches = regex.Matches(text);

            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                if (match.Groups[1].Success) // Bold group
                {
                    parts.Add(new TextPartData { Text = match.Groups[1].Value, IsBold = true });
                }
                else if (match.Groups[2].Success) // Regular group
                {
                    parts.Add(new TextPartData { Text = match.Groups[2].Value, IsBold = false });
                }
            }

            return parts;
        }

    }
    public class TextPartData
    {
        public string Text { get; set; }
        public bool IsBold { get; set; }
    }
}
