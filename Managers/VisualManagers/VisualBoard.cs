using CardGame.Cards.PanimaionSystem;
using CardGame.Objects;
using CardGame.Objects.Cards;
using CardGame.PanimaionSystem;
using CardGame.PanimaionSystem.Animations;
using CardGame.PanimaionSystem.GameObjectAnimation;
using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CardGame.Managers.GameManagers
{
    public class VisualBoard : GameObject
    {
        private List<CardBoard_Actor> CardsObjects = new List<CardBoard_Actor>();

        public Weapon_Actor EquipedWeapon;
        public Player belongToPlayer;
        public VisualBoard(Player belongToPlayer)
        {
            this.belongToPlayer = belongToPlayer;
            Width = Drawing.WINDOW_WIDTH;
        }
        public void MoveActor(Game1 g, CardBoard_Actor actor, float x, float y)
        {
            int currentIndex = CardsObjects.IndexOf(actor);
    
            for(int i=0; i < CardsObjects.Count; i++)
            {
                
                if (CardsObjects[i] != actor && x < CardsObjects[i].GetPosCenter().X)
                {
                    if(i != currentIndex)
                    {
                        CardsObjects.Remove(actor);
                        if (i > currentIndex)
                        {
                            i--;
                        }
                        CardsObjects.Insert(i, actor);
                        UpdatePositions();
                    }
                    break;
                }
                if(i+1 == CardsObjects.Count)
                {
                    CardsObjects.Remove(actor);
                    CardsObjects.Add(actor);
                    UpdatePositions();
                }
            }
        }
        public void EquipNewWeapon(Game1 g, WeaponCard weapon)
        {
            if(EquipedWeapon != null)
            {

                RemoveWeaponActor(g);
            }
            EquipedWeapon = new Weapon_Actor(weapon);
            g.gameBoard.objectManager.Add(EquipedWeapon, g);
        }
        public void RemoveWeaponActor(Game1 g)
        {
            g.gameBoard.objectManager.Remove(EquipedWeapon, g);
        }
        public int getIndexOfActor(CardBoard_Actor actor)
        {
            return CardsObjects.IndexOf(actor);
        }
        public int TempHoverRemove(Game1 g, CardBoard_Actor actor)
        {
            int index = CardsObjects.IndexOf(actor);
            CardsObjects.Remove(actor);
            List<CardBoard_Actor> actors = new List<CardBoard_Actor>(CardsObjects);
            foreach (CardBoard_Actor ca in actors)
            {
                ca.showHoverDisplay = true;
            }
            UpdatePositions();
            return index;
        }
        public void TempHoverAdd(Game1 g, CardBoard_Actor actors, int atIndex = -1)
        {
            
            foreach (CardBoard_Actor ca in CardsObjects)
            {
                ca.showHoverDisplay = false;
            }
            if (atIndex != -1)
            {
                CardsObjects.Insert(atIndex, actors);
            }
            else
            {
                CardsObjects.Add(actors);
            }
            UpdatePositions();
            actors.inAnimation = true;
            QueueManager.Enqueue(new SummonAnimation(actors, 0.4f));
            QueueManager.Enqueue(new GameAction((g) => actors.inAnimation = false));
        }
        public void AddCard(Game1 g, Card card, int atIndex=-1)
        {
            CardBoard_Actor doseCardAllreadyExist = getCardActor(g, card);
            if (doseCardAllreadyExist != null)
            {
                foreach (CardBoard_Actor ca in CardsObjects)
                {
                    ca.showHoverDisplay = true;
                }
                doseCardAllreadyExist.Init(g);
                return;
            }
            g.soundManager.PlaySound("summoned");
            CardBoard_Actor new_card_actor = new CardBoard_Actor((MinionCard)card);
            new_card_actor.Init(g);
            if(atIndex != -1)
            {
                CardsObjects.Insert(atIndex, new_card_actor);
            }
            else
            {
                CardsObjects.Add(new_card_actor);
            }
            
            g.gameBoard.queueManager.EnqueueItem(new GameAction((g) => UpdatePositions()));
            Vector2 startPos = getBasePos(new_card_actor);

            new_card_actor.setPosision(startPos.X, startPos.Y);

            new_card_actor.inAnimation = true;
            QueueManager.Enqueue(new SummonAnimation(new_card_actor, 0.5f));
            QueueManager.Enqueue(new GameAction((g) => new_card_actor.inAnimation = false));
            

        }
        public Vector2 getBasePos(CardBoard_Actor ca)
        {
            //400 from board char width
            float minionMargin = 60;
            float offsetWidthX = ((CardsObjects.Count *minionMargin)+400* CardsObjects.Count )/ 2;
            float startX = X + Width / 2;
            float offMinionXset = 0;
            foreach (CardBoard_Actor _ca in CardsObjects)
            {
                if (_ca == ca)
                {
                    return new Vector2(startX - offsetWidthX + offMinionXset, Y);
                }
                offMinionXset += ca.Width + minionMargin;
            }
            return new Vector2(0, 0);
        }
        private void UpdatePositions()
        {
            float minionMargin = 60;
            float offsetWidthX = ((CardsObjects.Count * minionMargin) + 400 * CardsObjects.Count) / 2;
            float startX = X + Width / 2;
            float offMinionXset = 0;
            foreach (CardBoard_Actor ca in CardsObjects)
            {
                ca.updateBaseposision(startX- offsetWidthX+ offMinionXset, Y);
                offMinionXset += ca.Width + minionMargin;
            }
        }


        public CardBoard_Actor getCardActor(Game1 g, Card card)
        {
            foreach (CardBoard_Actor ca in CardsObjects)
            { 
                if(ca.card.UniqueID.Equals(card.UniqueID))
                {
                    return ca;
                }
            }
            if (g.gameBoard.gameInterface.getPlayer(belongToPlayer).heroActor.card.UniqueID.Equals(card.UniqueID))
            {
                return g.gameBoard.gameInterface.getPlayer(belongToPlayer).heroActor;
            }
            return null;
        }

    
        public override void Draw(Game1 g)
        {

            List<CardBoard_Actor> actors = new List<CardBoard_Actor>(CardsObjects);
            foreach (CardBoard_Actor co in actors)
            {
                co.Draw(g);
            }
        }

        public override void Init(Game1 g)
        {
        }

        public override void Update(GameTime gt, Game1 g)
        {

            List<CardBoard_Actor> actors = new List<CardBoard_Actor>(CardsObjects);
            foreach (CardBoard_Actor co in actors)
            {
                co.Update(gt, g);
            }
            if (g.gameBoard.gameInterface.getPlayer(belongToPlayer).heroActor != null)
            {
                g.gameBoard.gameInterface.getPlayer(belongToPlayer).heroActor.Update(gt, g);

            }
        }
        public override void Destroy(Game1 g)
        {
            foreach (CardBoard_Actor card in CardsObjects)
            {
                card.Destroy(g);
            }
        }
        public void RemoveCard(Game1 g, Card card)
        {
            for (int i = CardsObjects.Count - 1; i >= 0; i--)
            {
                if (CardsObjects[i].card.UniqueID.Equals(card.UniqueID))
                {
                    CardBoard_Actor actor = CardsObjects[i];
                    QueueManager.Enqueue(new DieAnimation(actor, 0.5f));
                    QueueManager.Enqueue(new GameAction((g) => {
                        actor.Destroy(g);
                        CardsObjects.Remove(actor);
                        ///hmm??
                        g.gameBoard.queueManager.EnqueueItem(new GameAction((g) => UpdatePositions()));
                    }));
                    
                    return;
                }
            }
        }
    }
}
