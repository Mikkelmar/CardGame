using CardGame.Managers;
using CardGame.Managers.GameManagers;
using CardGame.Objects.Cards;
using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CardGame.Objects
{
    public class OptionSelectManager : GameObject
    {

        private List<CardDiscover_Actor> CardsObjects = new List<CardDiscover_Actor>();
        private List<Card> _cards = new List<Card>();
        public bool canCancel = false;
        private bool hide = false;
        private bool canToggleHide = true;
        private Player createdBy;
        private ShowHideButton button;
        private ConfirmMuliganButton muliganButton;
        public Card SourceCard;
        public OptionSelectManager(List<Card> cards, Card fromCard=null, Player fromPlayer=null)
        {
            Debug.WriteLine("NEW OSM");
            if (fromCard!= null) {
                Debug.WriteLine(fromCard.UniqueID +"     cards:     "+ cards);
            }
            
            if (fromPlayer != null)
            {
                canToggleHide=false;
                createdBy = fromPlayer;
            }
            _cards = cards;
            SourceCard = fromCard;
            if (fromCard != null && fromCard is OptionCreator)
            {
                //Må få fikset can craft
                canCancel = false;// ((OptionCreator)fromCard).canCancel();
                foreach (Card card in cards)
                {
                    CardDiscover_Actor new_card_actor = new CardDiscover_Actor(card, fromCard);
                    CardsObjects.Add(new_card_actor);
                }
            }
            else if (fromCard != null && fromCard is CraftCreator)
            {
                canCancel = false;// ((CraftCreator)fromCard).canCancel(); 
                foreach (Card card in cards)
                {
                    CardCraft_Actor new_card_actor = new CardCraft_Actor(card, fromCard);
                    CardsObjects.Add(new_card_actor);
                }
            }
            else
            {
                //fromCard is null handle
                foreach (Card card in cards)
                {
                    CardMuligan_Actor new_card_actor = new CardMuligan_Actor(card, fromPlayer.muligan);
                    CardsObjects.Add(new_card_actor);
                }
            }
        }
        public List<Card> GetCards()
        {
            return _cards;
        }

        public override void Destroy(Game1 g)
        {
            foreach (CardDiscover_Actor card in CardsObjects)
            {
                card.Destroy(g);
            }

            g.gameBoard.objectManager.Remove(button, g);
            g.gameBoard.objectManager.Remove(muliganButton, g);
        }

        public override void Draw(Game1 g)
        {
            if (hide)
            {
                return;
            }
            foreach (CardDiscover_Actor co in CardsObjects)
            {
                co.Draw(g);
            }
        }

        public override void Init(Game1 g)
        {
            int i = 0;
            foreach (CardDiscover_Actor new_card_actor in CardsObjects)
            {
                new_card_actor.Init(g);
                float startX = (Drawing.WINDOW_WIDTH / 2) - ((150 + new_card_actor.Width) * CardsObjects.Count / 2);
                new_card_actor.X = startX + (150 + new_card_actor.Width)* i;
                new_card_actor.Y = 700;
                i++;

                new_card_actor.card.belongToPlayer = g.gameBoard.gameHandler.ActivePlayer;
                new_card_actor.card.currentState = Card.CardState.Hand;
                if (!new_card_actor.card.haveBeenInitiated)
                {
                    new_card_actor.card.InitCard(g);
                }
            }
            if (createdBy != null)
            {
                muliganButton = new ConfirmMuliganButton(createdBy);
                g.gameBoard.objectManager.Add(muliganButton, g);
            }
            if (canToggleHide)
            {
                button = new ShowHideButton(this)
                {
                    X = 3500,
                    Y = 2000,
                    Width = 600,
                    Height = 300
                };
                g.gameBoard.objectManager.Add(button, g);
            }
            
        }
        public void ToggleHide()
        {
            hide = !hide;
        }

        public override void Update(GameTime gt, Game1 g)
        {

        }
    }
}
