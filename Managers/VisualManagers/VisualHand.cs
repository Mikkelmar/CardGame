using CardGame.Cards.PanimaionSystem;
using CardGame.Objects;
using CardGame.Objects.Cards;
using CardGame.PanimaionSystem;
using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.Managers.GameManagers
{
    public class VisualHand: GameObject, HoverLisner
    {
        private List<CardHand_Actor> CardsObjects = new List<CardHand_Actor>();
        
        private bool isHovering = false;
        public Player belongToPlayer;
        private float scale = 1f;
        public int HideHandY, BaseHandY;
        public VisualHand(Player belongToPlayer)
        {
            this.belongToPlayer = belongToPlayer;
            Width = Drawing.WINDOW_WIDTH;
            Height = 1100;
        }
        
        public void RemoveCard(Game1 g, Card card)
        {
            for (int i = CardsObjects.Count - 1; i >= 0; i--)
            {
                if (CardsObjects[i].card.UniqueID == card.UniqueID)
                {
                    CardHand_Actor actor = CardsObjects[i];

                    QueueManager.Enqueue(new GameAction((g) => {
                        actor.Destroy(g);
                        CardsObjects.Remove(actor);
                    }));
                    return;
                }
            }
        }
        public void AddCard(Game1 g, Card card)
        {
            CardHand_Actor new_card_actor = new CardHand_Actor(card);
            new_card_actor.Init(g);
            QueueManager.Enqueue(new GameAction((g) => {
                CardsObjects.Add(new_card_actor);
                g.soundManager.PlaySound("addToHand");
            }));
        }


        public override void Draw(Game1 g)
        {
            List<CardHand_Actor> actors = new List<CardHand_Actor>(CardsObjects);
            foreach (CardHand_Actor card in actors)
            {
                card.Draw(g);
            }
            //Drawing.FillRect(GetHitbox(), Color.Red, 1f, g);
        }
        public override void Init(Game1 g)
        {
            g.gameBoard.mouseManager.AddHover(this);
        }

        public override void Update(GameTime gt, Game1 g)
        {
            Y = BaseHandY;
            int i = 0;
            float offset = 0;

            float cardMargin = 60;
            float offsetWidthX = ((CardsObjects.Count * cardMargin) + (540 * 0.7f) * CardsObjects.Count) / 2;
            float startX = X + Width / 2;
            float offCardXset = 0;

            //ca.updateBaseposision(startX - offsetWidthX + offCardXset, Y);
            //offCardXset += ca.Width + minionMargin;

            List<CardHand_Actor> actors = new List<CardHand_Actor>(CardsObjects);
            foreach (CardHand_Actor card in actors)
            {
                if (i != 0)
                {
                    offCardXset += CardsObjects[i - 1].Width + cardMargin;
                }
                card.baseX = startX - offsetWidthX + offCardXset;

                card.baseY = Y-card.Height;
                i++;
            }
            foreach (CardHand_Actor card in actors)
            {
                card.Update(gt, g);
            }

        }
        public Vector2 getNewCardPos()
        {
            Y = BaseHandY;
            int i = 0;

            float cardMargin = 60;
            float offsetWidthX = ((CardsObjects.Count * cardMargin) + (540 * 0.7f) * CardsObjects.Count) / 2;
            float startX = X + Width / 2;
            float offCardXset = 0;

            List<CardHand_Actor> actors = new List<CardHand_Actor>(CardsObjects);
            foreach (CardHand_Actor card in actors)
            {
                if (i != 0)
                {
                    offCardXset += CardsObjects[i - 1].Width + cardMargin;
                }
                i++;
            }
            if(CardsObjects.Count != 0)
            {
                offCardXset += CardsObjects[i - 1].Width + cardMargin;
            }
            
            return new Vector2(startX - offsetWidthX + offCardXset, Y-(840*0.7f));
        }

        public override void Destroy(Game1 g)
        {
            foreach (CardHand_Actor card in CardsObjects)
            {
                card.Destroy(g);
            }
        }
        private void TriggerHover()
        {
            if (isHovering)
            {
                scale = 2f;
            }
            else
            {
                scale = 1f;
            }
        }
        public void Hover(float x, float y, Game1 g)
        {
            if(GetHitbox().Contains(x, y))
            {
                if(isHovering != GetHitbox().Contains(x, y))
                {
                    TriggerHover();
                }
                isHovering = true;
                return;
            }
            if (isHovering != GetHitbox().Contains(x, y))
            {
                TriggerHover();
            }
            isHovering = false;
        }
        public CardHand_Actor getCardActor(Card card)
        {
            foreach (CardHand_Actor ca in CardsObjects)
            {
                if (ca.card.UniqueID.Equals(card.UniqueID))
                {
                    return ca;
                }
            }
            return null;
        }
    }
}
