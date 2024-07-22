using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.Managers
{
    public interface Clickable
    {
        public void Clicked(float x, float y, Game1 g);
    }
    public interface LeftRelease
    {
        public void LeftReleased(float x, float y, Game1 g);
    }
    public interface RightClickable
    {
        public void RightClicked(float x, float y, Game1 g);
    }
    public interface HoverLisner{
        public void Hover(float x, float y, Game1 g);
    }

}
