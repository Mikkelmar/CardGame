using CardGame.Managers;
using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Text;

namespace CardGame.Objects
{
    public class SearchBar : GameObject, Clickable
    {
        private StringBuilder inputText;
        private bool isActive;

        public SearchBar()
        {
            inputText = new StringBuilder();
            isActive = false;
            depth = 0.00000001f;
        }

        public void Clicked(float x, float y, Game1 g)
        {
            if (GetHitbox().Contains((int)x, (int)y))
            {
                isActive = true;
            }
            else
            {
                isActive = false;
            }
        }

        public override void Destroy(Game1 g)
        {
            g.Window.TextInput -= TextInputHandler;
            g.gameBoard.mouseManager.Remove(this);
        }

        public override void Draw(Game1 g)
        {
            if (isActive)
            {
                Drawing.FillRect(GetHitbox(), Color.White, depth, g);
            }
            else
            {
                Drawing.FillRect(GetHitbox(), Color.Gray, depth, g);
            }

            Drawing.DrawText(inputText.ToString(), X + 10, Y + 10, color: Color.Black, layerDepth: depth * 0.1f, scale: 3f);
            if (inputText.ToString().Equals(""))
            {
                Drawing.DrawText("Search...", X + 10, Y + 10, color: Color.Black*0.7f, layerDepth: depth * 0.1f, scale: 2.7f);
            }
        }

        public override void Init(Game1 g)
        {
            g.collectionPage.mouseManager.Add(this);
            g.Window.TextInput += TextInputHandler;
        }

        public override void Update(GameTime gt, Game1 g)
        {
            if (isActive)
            {
                KeyboardState keyboardState = Keyboard.GetState();
                if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    PerformSearch(g);
                    isActive = false;
                }
            }
        }

        private void TextInputHandler(object sender, TextInputEventArgs e)
        {
            if (isActive)
            {
                if (e.Character == '\b' && inputText.Length > 0) // Handle backspace
                {
                    inputText.Remove(inputText.Length - 1, 1);
                }
                else if (!char.IsControl(e.Character)) // Ignore control characters
                {
                    inputText.Append(e.Character);
                }
            }
        }

        private void PerformSearch(Game1 g)
        {
            string searchText = inputText.ToString();
            // Implement the search logic here
            g.collectionPage.collectionManager.filterByText(g, searchText);
        }
    }
}
