using CardGame.Objects;
using CardGame.Objects.Cards;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGame.Managers
{
    public class ActionHistoryManager : GameObject
    {
        private List<DisplayActionObject> actionHistory;

        public ActionHistoryManager()
        {
            actionHistory = new List<DisplayActionObject>();
            X = 300;
            Y = 500;
        }
        public override void Draw(Game1 g)
        {
            
        }
        public void AddAction(DisplayActionObject action, Game1 g)
        {
            Card historyCopied = g.gameBoard.cardManager.copyCard(action.actionCard);
            historyCopied.belongToPlayer = action.actionCard.belongToPlayer;
            action.actionCard = historyCopied;
            actionHistory.Insert(0, action);
            g.gameBoard.objectManager.Add(action, g);
            updateMaxHistory(g);
        }
        private void updateMaxHistory(Game1 g)
        {
            double currentSpace = 0;

            for (int i = 0; i < actionHistory.Count; i++)
            {
                if (actionHistory[i].small)
                {
                    currentSpace += 0.5;
                }
                else
                {
                    currentSpace += 1;
                }

                if (currentSpace > 7)
                {
                    // Remove all items from the beginning to the current index (inclusive)
                    foreach (DisplayActionObject actionObject in actionHistory.GetRange(i, actionHistory.Count - i))
                    {
                        g.gameBoard.objectManager.Remove(actionObject, g);
                    }
                    actionHistory.RemoveRange(i, actionHistory.Count-i);
                    break;
                }
            }

            float yPos = Y;
            foreach (var action in actionHistory)
            {
                float _width = 200;
                float _height = _width;
                if (action.small)
                {
                    _height *= 0.5f;
                }
                action.X = X;
                action.Y = yPos;
                action.Width = _width;
                action.Height = _height;
                yPos += _height + 25;
            }
        }
      

        // Methods to log specific actions
        public void LogPlayCard(Card card, Game1 g)
        {
            var actionObject = new DisplayActionObject();
            actionObject.actionCard = card;
            AddAction(actionObject, g);
        }

        public void LogUseHeroPower(string heroPower, List<Card> targets)
        {
            string description = $"Player uses hero power {heroPower}";
            if (targets != null && targets.Count > 0)
            {
                description += " targeting " + string.Join(", ", targets.Select(t => t.Name));
            }
        }

        public void LogAttackWithCard(Game1 g, Card attacker, Card target)
        {
            var actionObject = new DisplayActionObject();
            actionObject.small = true;
            actionObject.type = DisplayActionObject.Types.Attack;
            actionObject.actionCard = attacker;
            AddAction(actionObject, g);
        }

        public void LogCardEffectTrigger(Game1 g, Card card)
        {
            var actionObject = new DisplayActionObject();
            actionObject.small = true;
            actionObject.type = DisplayActionObject.Types.Effect;
            actionObject.actionCard = card;
            AddAction(actionObject, g);
        }

        public override void Destroy(Game1 g)
        {
        }

        public override void Update(GameTime gt, Game1 g)
        {
        }

        

        public override void Init(Game1 g)
        {
        }
    }

}
