using CardGame.HeroPowers;
using CardGame.Managers.GameManagers;
using CardGame.Objects.Cards;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.Objects
{
    public class VisualPlayer
    {

        public VisualBoard visualBoard;
        public VisualHand visualHand;
        public Player belongToPlayer;
        public HeroActor heroActor;
        public HeroPower_Actor heroPowerActor;
        public VisualPlayer(Game1 g, Player belongToPlayer)
        {
            this.belongToPlayer = belongToPlayer;
            visualBoard = new VisualBoard(belongToPlayer);
            visualHand = new VisualHand(belongToPlayer);

            if(g.gameBoard.isPlayer == null)
            {
                return;
            }
            if (belongToPlayer.id == g.gameBoard.isPlayer.id)
            {
                visualHand.BaseHandY = 2850;
                visualHand.HideHandY = 3350;

                visualBoard.Y = 1200;
            }
            else
            {
                visualHand.BaseHandY = 100;
                visualHand.HideHandY = -420;

                visualBoard.Y = 680; 

            }
        }
        public void Init(Game1 g)
        {
            g.gameBoard.objectManager.Add(visualBoard, g);
            g.gameBoard.objectManager.Add(visualHand, g);

            heroActor = new HeroActor(g, belongToPlayer.Hero);
            g.gameBoard.objectManager.Add(heroActor, g);

        }

        public void AddCardToBoard(Game1 g, Card card, int atPos = -1)
        {
            visualBoard.AddCard(g, card, atPos);

        }
        public void setHeroPower(Game1 g, HeroPower newHeroPower)
        {
            if (heroPowerActor != null)
            {
                g.gameBoard.objectManager.Remove(heroPowerActor, g);
            }
            belongToPlayer.heroPower = newHeroPower;
            heroPowerActor = new HeroPower_Actor(g, belongToPlayer.heroPower);
            g.gameBoard.objectManager.Add(heroPowerActor, g);
        }

        public void RemoveCardFromBoard(Game1 g, Card card)
        {
            visualBoard.RemoveCard(g, card);
        }
    }
}
