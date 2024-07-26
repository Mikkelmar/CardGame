using CardGame.Objects.Cards;
using CardGame.Shared.Managers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CardGame.Client
{
    public class NetworkHandler
    {
        public GameClient _gameClient;

        public NetworkHandler(Game1 g)
        {
            if (!g.isClient)
            {
                return;
            }
            Debug.WriteLine("Player connecting...");
            _gameClient = new GameClient(g);
            _gameClient.ConnectAsync("ws://51.175.74.234:7000").Wait(); // Adjust the URI as needed
            SendJoinGameMessage(g);
           
        }

        

        public void SendJoinGameMessage(Game1 g)
        {
            var message = new
            {
                type = "JoinGame",
                deck = g.collectionPage.collectionManager.deckBuilder.printCommaSeparatedCardList(),
                heroPower = g.collectionPage.collectionManager.deckBuilder.getHeroPower()
            };
            _gameClient.SendMessageAsync(JsonConvert.SerializeObject(message)).Wait();
        }
        public void SendCardSelected(string uniqueInstanceId)
        {
            var message = new
            {
                type = "CardSelected",
                cardID = uniqueInstanceId,
            };
            _gameClient.SendMessageAsync(JsonConvert.SerializeObject(message)).Wait();
        }
        
        public void SendCardOptionSelected(string uniqueInstanceId, string targetID = "null")
        {
            var message = new
            {
                type = "CardOptionSelected",
                cardID = uniqueInstanceId,
                targetID = targetID,
            };
            _gameClient.SendMessageAsync(JsonConvert.SerializeObject(message)).Wait();
        }
        public void SendPlayCardMessage(string uniqueInstanceId, int posision=-1, string targetInstanceId="null")
        {
            var message = new
            {
                type = "PlayCard",
                cardID = uniqueInstanceId,
                targetID = targetInstanceId,
                posision = posision
            };
            _gameClient.SendMessageAsync(JsonConvert.SerializeObject(message)).Wait();
        }
        internal void SendMuliganConfirmed(List<string> cardsToKeep)
        {
            var message = new
            {
                type = "MuliganKeep",
                cardsToKeep = cardsToKeep,
            };
            _gameClient.SendMessageAsync(JsonConvert.SerializeObject(message)).Wait();
        }
        public void SendAttackWithMinionMessage(string uniqueInstanceId, string targetInstanceId)
        {
            var message = new
            {
                type = "AttackWithMinion",
                cardID = uniqueInstanceId,
                targetCardID = targetInstanceId
            };
            _gameClient.SendMessageAsync(JsonConvert.SerializeObject(message)).Wait();
        }
        public void SendTargetCardWithCardMessage(string uniqueInstanceId, string targetInstanceId)
        {
            var message = new
            {
                type = "TargetCardWithCard",
                cardID = uniqueInstanceId,
                targetCardID = targetInstanceId
            };
            _gameClient.SendMessageAsync(JsonConvert.SerializeObject(message)).Wait();
        }
        public void SendTargetCardWithHeroPowerMessage(int player, string targetInstanceId)
        {
            var message = new
            {
                type = "TargetCardWithHeroPower",
                player = player,
                targetCardID = targetInstanceId
            };
            _gameClient.SendMessageAsync(JsonConvert.SerializeObject(message)).Wait();
        }

        public void SendEndTurnMessage(string playerId)
        {
            var message = new
            {
                type = "EndTurn",
                playerId
            };
            _gameClient.SendMessageAsync(System.Text.Json.JsonSerializer.Serialize(message)).Wait();
        }
        public void SendReadyMessage()
        {
            var message = new
            {
                type = "ReadyToStart",
            };
            _gameClient.SendMessageAsync(System.Text.Json.JsonSerializer.Serialize(message)).Wait();
        }
        public Card CreateCard(Game1 g, Card cardToCreate, Card fromCard)
        {
            Card _createdCard = g.gameBoard.gameHandler.actionManager.createCard(g, cardToCreate);
            return _createdCard;
        }
    }
}