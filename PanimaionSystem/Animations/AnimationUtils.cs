using CardGame.Objects;
using CardGame.Objects.Cards;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CardGame.PanimaionSystem.Animations
{
    public static class AnimationUtils
    {
        public static GameObject getCardActor(Game1 g, Card card)
        {
            if(card == null)
            {
                Debug.WriteLine("Warning trying to get cardActor but card is null");
                return null;
            }
            //TODO: can be improved
            Vector2 pos = new Vector2(0, 0);
            if (g.gameBoard.gameHandler.player1.Hero == card)
            {
                return g.gameBoard.gameInterface.vPlayer1.heroActor;
            }
            else if (g.gameBoard.gameHandler.player2.Hero == card)
            {
                return g.gameBoard.gameInterface.vPlayer2.heroActor;
            }
            GameObject go;
            go = g.gameBoard.gameInterface.vPlayer1.visualHand.getCardActor(card);
            if (go != null)
            {
                return go;
            }
            go = g.gameBoard.gameInterface.vPlayer2.visualHand.getCardActor(card);
            if (go != null)
            {
                return go;
            }
            go = g.gameBoard.gameInterface.vPlayer1.visualBoard.getCardActor(g, card);
            if (go != null)
            {
                return go;
            }
            go = g.gameBoard.gameInterface.vPlayer2.visualBoard.getCardActor(g, card);
            if (go != null)
            {
                return go;
            }

            return null;
        }
    }
}
