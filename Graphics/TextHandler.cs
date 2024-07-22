using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGame.Graphics
{
    public static class TextHandler
    {
        public static float GetFitScale(string text, float width)
        {
            return width/TextHandler.textLength(text);
        }
        public static float textLength(string text)
        {
            return Textures.font_cardo.MeasureString(text).X;
        }
        public static string GetTextThatFits(string text, float width, float scale)
        {
            float accumulatedWidth = 0;
            int lastIndex = 0;

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                float charWidth = Textures.font_cardo.MeasureString(c.ToString()).X * scale;

                if (accumulatedWidth + charWidth > width)
                {
                    break;
                }

                accumulatedWidth += charWidth;
                lastIndex = i;
            }

            return text.Substring(0, lastIndex + 1);
        }
        public static float textHeight(string text)
        {
            return Textures.font_cardo.MeasureString(text).Y;
        }
        public static List<string> FitText(string text, float containerWidth, float fontScale = 1f)
        {
            List<string> lines = new List<string>();

            // Remove <b> tags for length calculation
            string textWithoutTags = RemoveTags(text);

            if (TextHandler.textLength(textWithoutTags) * fontScale > containerWidth)
            {
                string[] entireString = text.Split(" ");
                string line = entireString[0];
                string finalString = line;
                for (int i = 1; i < entireString.Length; i++)
                {
                    line += " " + entireString[i];
                    string lineWithoutTags = RemoveTags(line);

                    if (TextHandler.textLength(lineWithoutTags) * fontScale > containerWidth)
                    {
                        lines.Add(finalString);
                        lines.AddRange(FitText(
                            String.Join(" ", entireString.Skip(i).Take(entireString.Length - i).ToArray()),
                            containerWidth,
                            fontScale));
                        return lines;
                    }
                    finalString = line;
                }
                lines.Add(finalString);
            }
            else
            {
                lines.Add(text);
            }
            return lines;
        }

        private static string RemoveTags(string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(input, @"<.*?>", string.Empty);
        }
    }

}
