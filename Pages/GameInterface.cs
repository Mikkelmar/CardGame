using CardGame.Managers;
using CardGame.Managers.GameManagers;
using CardGame.Objects;
using CardGame.Objects.Cards;
using System.Collections.Generic;

namespace CardGame.Pages
{
    public class GameInterface : RightClickable
    {
        public VisualPlayer vPlayer1;
        public VisualPlayer vPlayer2;
        public List<Card> allCards = new List<Card>();
        public void AddCard(Card card)
        {
            allCards.Add(card);
        }
        public VisualPlayer getPlayer(Player p)
        {
            if(p == vPlayer1.belongToPlayer)
            {
                return vPlayer1;
            }
            return vPlayer2;
        }

        public bool SelectingTarget = false;
        public Targeter targeter = null;

        public void Init(Game1 g)
        {
            g.gameBoard.devMode = false;
            g.gameBoard.mouseManager.AddRight(this);

            vPlayer1 = new VisualPlayer(g, g.gameBoard.gameHandler.player1);
            vPlayer2 = new VisualPlayer(g, g.gameBoard.gameHandler.player2);

            vPlayer1.Init(g);
            vPlayer2.Init(g);

            g.gameBoard.objectManager.Add(new EndTurnButton() { X = 4100, Y = 1200, Width = 400, Height = 200 }, g);
            g.gameBoard.objectManager.Add(new UI(), g);
        }
        public void EndTurn(Game1 g)
        {
         
        }
        public void StartGame(Game1 g)
        {
            
           
        }
        public void ActivateCard(Game1 g, Card card, Player player)
        {
            
        }

        public void PlayCard(Game1 g, Card card, Player player, int atPos = -1)
        {
            getPlayer(player).visualHand.RemoveCard(g, card);
            player.CurrentMana -= card.Cost;
        }

        public void RightClicked(float x, float y, Game1 g)
        {
            SelectingTarget = false;
            targeter = null;
            g.gameBoard.gameHandler.SelectingTarget = false;
            g.gameBoard.gameHandler.targeter = null;
            if (g.gameBoard.gameHandler.optionSelectManager.Peek() != null)
            {
                if (g.gameBoard.gameHandler.optionSelectManager.Peek().hide)
                {
                    g.gameBoard.gameHandler.optionSelectManager.Peek().ToggleHide(g);
                    g.gameBoard.gameHandler.activeOptionSelection = true;
                }
            }
        }
    }
}
