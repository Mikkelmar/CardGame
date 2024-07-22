using CardGame.Objects;
using CardGame.Objects.Cards;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.Managers
{
    public interface Targeter
    {
        //public bool isValidTarget(Game1 g, Card card);
        public void GiveTarget(Game1 g, Card_Actor targetActor);
        public Vector2 GetSourcePos(Game1 g);
        public string GetTargetText(Game1 g);

    }
}
