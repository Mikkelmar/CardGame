using CardGame.Cards.PanimaionSystem;
using CardGame.HeroPowers;
using CardGame.Managers;
using CardGame.Managers.GameManagers;
using CardGame.Objects;
using CardGame.Objects.Cards;
using CardGame.Pages;
using CardGame.PanimaionSystem;
using CardGame.PanimaionSystem.Animations;
using CardGame.Shared.GameLogic.HeroPowers;
using CardGame.Shared.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CardGame.Client
{
    public class GameClient
    {
        private ClientWebSocket _client;
        private Game1 g;
        public readonly ServerActionQueue actionQueue = new ServerActionQueue();
        public GameClient(Game1 g)
        {
            this.g = g;
        }
        public async Task ConnectAsync(string uri)
        {
            _client = new ClientWebSocket();
            await _client.ConnectAsync(new Uri(uri), CancellationToken.None);
            Console.WriteLine("Connected!");

            _ = ReceiveMessages();
        }

        public async Task SendMessageAsync(string message)
        {
            var bytes = Encoding.UTF8.GetBytes(message);
            var buffer = new ArraySegment<byte>(bytes);
            await _client.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public async Task ReceiveMessages()
        {
            var buffer = new byte[1024 * 4];

            while (_client.State == WebSocketState.Open)
            {
                var result = await _client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Debug.WriteLine("Message received: " + message);
                actionQueue.EnqueueAction(() => HandleMessage(message));
            }
        }
        private Player getPlayer(Game1 g, string id)
        {
            if (id == "1")
            {
                return g.gameBoard.gameHandler.player1;
            }
            return g.gameBoard.gameHandler.player2;
        }
        private void HandleMessage(string message)
        {
            var messageObject = JsonSerializer.Deserialize<JsonElement>(message);
            var messageType = messageObject.GetProperty("type").GetString();
            var options = new JsonSerializerOptions();
            options.Converters.Add(new CustomCardConverter(g.gameBoard.cardManager, g.gameBoard.gameHandler));
            if (messageObject.TryGetProperty("actionSequence", out JsonElement actionSequenceArray))
            {
                Queue<JsonElement> actionSequence = new Queue<JsonElement>();
                foreach (JsonElement element in actionSequenceArray.EnumerateArray())
                {
                    actionSequence.Enqueue(element);
                }
                Debug.WriteLine("ACTION SEQUENCE" + actionSequence);
                // Add the deserialized sequence to the ActionManager
                g.gameBoard.gameHandler.actionManager.AddSequence(actionSequence);
            }

            switch (messageType)
            {
                case "SetPlayer":
                    g.gameBoard.isPlayer = getPlayer(g, messageObject.GetProperty("playerID").GetString());
                    //GameHandler.setRandomSeed(int.Parse(messageObject.GetProperty("randomSeed").ToString()));
                    g.gameBoard.gameInterface.Init(g);
                    g.gameBoard.networkHandler.SendReadyMessage();
                    break;
                case "TargetHeroPowerCard":
                    TargetCardHeroPower(g, messageObject);
                    break;
                case "StartGame":
                    g.gameBoard.gameHandler.StartGame(g);
                    break;
                case "EndTurn":
                    g.gameBoard.gameHandler.EndTurn(g);
                    break;
                case "TargetCard":
                    TargetCard(g, messageObject);
                    break;
                case "CardSelected":
                    CardSelected(g, messageObject);
                    break;
                case "CardOptionSelected":
                    CardOptionSelected(g, messageObject);
                    break; 
                case "AttackMinion":
                    string cardID = messageObject.GetProperty("withCard").ToString();
                    Card card = g.gameBoard.gameHandler.getAllCards().Find(c => c.UniqueID.Equals(cardID));

                    string targetCardID = messageObject.GetProperty("targetCard").ToString();
                    Card targetCard = g.gameBoard.gameHandler.getAllCards().Find(c => c.UniqueID.Equals(targetCardID));

                    //Maybe add this animation elsewhere for making sure it actually attacks
                    CardBoard_Actor target = g.gameBoard.gameInterface.getPlayer(targetCard.belongToPlayer).visualBoard.getCardActor(g, targetCard);
                    if (target != null && (target is CardBoard_Actor))
                    {
                        CardBoard_Actor attackingCardActor = g.gameBoard.gameInterface.getPlayer(card.belongToPlayer).visualBoard.getCardActor(g, card);
                        g.gameBoard.queueManager.EnqueueItem(new AttackAnimation(attackingCardActor, target, 0.7f));
                    }
                    g.gameBoard.gameHandler.MinionAttackMinion(g, (MinionCard)card, (MinionCard)targetCard);

                    g.gameBoard.queueManager.EnqueueItem(new GameAction((g) =>
                        g.gameBoard.actionHistoryManager.LogAttackWithCard(g, card, targetCard)));
                    break;
                case "PlayedCard":
                    ;
                    string _cardID = messageObject.GetProperty("cardID").ToString();
                    Card _card = g.gameBoard.gameHandler.getAllCards().Find(c => c.UniqueID.Equals(_cardID));

                    if (_card.requireTargets) {
                        g.gameBoard.queueManager.EnqueueItem(new GameAction((g) => g.soundManager.PlaySound("playSound")));
                    }
                    else
                    {
                        g.gameBoard.queueManager.EnqueueItem(new GameAction((g) => g.soundManager.PlaySound("playSpell")));
                    }
                    
                    
                    if (_card.belongToPlayer != g.gameBoard.isPlayer)
                    {
                        g.gameBoard.queueManager.EnqueueItem(new PlayCardAnimation(_card));
                    }
                    g.gameBoard.queueManager.EnqueueItem(new GameAction((g) => 
                    g.gameBoard.actionHistoryManager.LogPlayCard(_card, g)));
                    

                    string targetID = messageObject.GetProperty("targetID").ToString();
                    Card _targetCard = null;
                    if (!targetID.Equals("null"))
                    {
                        _targetCard = g.gameBoard.gameHandler.getAllCharacters().Find(c => c.UniqueID.Equals(targetID));
                    }
                    g.gameBoard.gameHandler.PlayCard(
                        g,
                        _card,
                        getPlayer(g, messageObject.GetProperty("playedBy").ToString()),
                        int.Parse(messageObject.GetProperty("posision").ToString()),
                        _targetCard);
                    break;
                case "MuliganKeep":
                    int playerID = messageObject.GetProperty("playerID").GetInt32();
                    List<string> cardsToKeep = JsonSerializer.Deserialize<List<string>>(messageObject.GetProperty("cardsToKeep").GetRawText());
                    g.gameBoard.gameHandler.PlayerAcceptMuligan(g, getPlayer(g, playerID.ToString()), cardsToKeep);

                    break; 
                case "StartingDeck":
                    var _options = new JsonSerializerOptions();
                    _options.Converters.Add(new CustomSimpleCardConverter(g.gameBoard.cardManager, g.gameBoard.gameHandler));
                    List<Card> deck = JsonSerializer.Deserialize<List<Card>>(messageObject.GetProperty("deck").ToString(), _options);
                    string PlayersDeck = messageObject.GetProperty("player").ToString();
                    HeroPower hp = HeroPowerManager.getHeroPower(messageObject.GetProperty("heroPower").GetInt32());
                    
                    if (PlayersDeck == "1")
                    {
                        g.gameBoard.setDeck(g, g.gameBoard.gameHandler.player1, deck);
                        hp.belongToPlayer = g.gameBoard.gameHandler.player1;
                        g.gameBoard.gameHandler.player1.setHeroPower(g, hp);
                        g.gameBoard.gameInterface.vPlayer1.setHeroPower(g, hp);
                        g.gameBoard.gameHandler.player1.setHero(hp.ID);
                    }
                    else
                    {
                        g.gameBoard.setDeck(g, g.gameBoard.gameHandler.player2, deck);
                        hp.belongToPlayer = g.gameBoard.gameHandler.player2;
                        g.gameBoard.gameHandler.player2.setHeroPower(g, hp);
                        g.gameBoard.gameHandler.player2.setHero(hp.ID);
                        g.gameBoard.gameInterface.vPlayer2.setHeroPower(g, hp);
                    }
                    
                    break;
                default:
                    break;
            }
        }

        private void CardOptionSelected(Game1 g, JsonElement messageObject)
        {
            string cardID = messageObject.GetProperty("cardID").ToString();

            Card sourceCard = g.gameBoard.gameHandler.optionSelectManager.SourceCard;
            Card Target = g.gameBoard.gameHandler.optionSelectManager.GetCards().Find(c => c.UniqueID.Equals(cardID));
            g.gameBoard.gameHandler.ActivateCard(g, Target, sourceCard.belongToPlayer);
            g.gameBoard.gameHandler.StopSelecting(g);
        }
        private void CardSelected(Game1 g, JsonElement messageObject)
        {
            string cardID = messageObject.GetProperty("cardID").ToString();

            Card sourceCard = g.gameBoard.gameHandler.optionSelectManager.SourceCard;
            Card Target = g.gameBoard.gameHandler.optionSelectManager.GetCards().Find(c => c.UniqueID.Equals(cardID));
            ((CraftCreator)sourceCard).optionSelected(g, Target);
            g.gameBoard.gameHandler.StopSelecting(g);

        }
        private void TargetCard(Game1 g, JsonElement messageObject)
        {
            string cardID = messageObject.GetProperty("withCard").ToString();
            Card card = g.gameBoard.gameHandler.getAllCards().Find(c => c.UniqueID.Equals(cardID));
            if(card == null)
            {
                //card = g.gameBoard.gameHandler.oldOptions.Find(c => c.UniqueID.Equals(cardID));
            }
            string targetCardID = messageObject.GetProperty("targetCard").ToString();
            Card targetCard = g.gameBoard.gameHandler.getAllCards().Find(c => c.UniqueID.Equals(targetCardID));

            card.getTarget(g, (MinionCard)targetCard);
        }

        private void TargetCardHeroPower(Game1 g, JsonElement messageObject)
        {
            HeroPower hp;
            string player = messageObject.GetProperty("player").ToString();
            if (player == "1")
            {
                hp = g.gameBoard.gameHandler.player1.heroPower;
            }
            else
            {
                hp = g.gameBoard.gameHandler.player2.heroPower;
            }

            if (hp.belongToPlayer != g.gameBoard.isPlayer)
            {
                g.gameBoard.queueManager.EnqueueItem(new UseHeroPowerAnimation(hp));
            }

            string targetCardID = messageObject.GetProperty("targetCard").ToString();
            if(targetCardID != "null" && targetCardID != "")
            {
                Card targetCard = g.gameBoard.gameHandler.getAllCards().Find(c => c.UniqueID.Equals(targetCardID));
                hp.getTarget(g, (MinionCard)targetCard);
            }
            else
            {
                hp.TriggerUse(g);
            }
            
        }

    }
}