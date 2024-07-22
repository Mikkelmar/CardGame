using CardGame.Client;
using CardGame.HeroPowers;
using CardGame.Managers;
using CardGame.Managers.GameManagers;
using CardGame.Objects;
using CardGame.Objects.Cards;
using CardGame.PanimaionSystem;
using Engine;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace CardGame.Pages
{
    public class GameBoard : Page
    {
        
        public NetworkHandler networkHandler;
        public GameBoard() : base(0) {
        }

        public Player isPlayer;

        public Camera cam = new Camera(new Vector2(0, 0));
        public OrthographicCamera _camera;
        public bool gamePaused = false;
        public ObjectManager objectManager = new ObjectManager();
        public MouseManager mouseManager = new MouseManager();
        public GameHandler gameHandler = new GameHandler();
        public ActionHistoryManager actionHistoryManager;
        public GameInterface gameInterface;
        public CardManager cardManager;
        public GameUtils utils = new GameUtils();
        public QueueManager queueManager = new QueueManager();
        private Rectangle playBox = new Rectangle(300, 600, 4500, 1500);
        private Rectangle playBox2 = new Rectangle(1600, 200, 1900, 500);
        //Rectangle playBox3 = new Rectangle(300, 600, 4500, 700);
        public bool devMode = false;
        public bool GameOver = false;
        
        public override void Init(Game1 g)
        {
            cardManager = new CardManager();
            actionHistoryManager = new ActionHistoryManager();
            objectManager.Add(actionHistoryManager, g);


            //C: \Users\mikke\source\repos\CardGame\Cards\cards.json
            gameHandler.Init(g);
            

            gameInterface = new GameInterface();


            networkHandler = new NetworkHandler(g);
        }
        public bool isOnBoard(float x, float y) {
            return playBox.Contains(x, y) || playBox2.Contains(x, y);
        }
        public override void Draw(Game1 g)
        {
            Drawing.FillRect(playBox, Color.LightGreen, 1f, g);
            Drawing.FillRect(playBox2, Color.LightGreen, 1f, g);

            objectManager.Draw(g);
            queueManager.Draw(g);
        }

        public override void DrawUI(Game1 g)
        {
        }

        public override ObjectManager getObjectManager()
        {
            return objectManager;
        }
        public override MouseManager getMouseManager()
        {
            return mouseManager;
        }

        public override void Load(Game1 g)
        {
            
        }
        public void setDeck(Game1 g, Player player, string deck)
        {
            if (deck.Equals(""))
            {
                setDeck(g, player, new List<Card>());
            }
            else
            {
                setDeck(g, player, g.gameBoard.cardManager.LoadDeckFromString(deck));
            }
            
        }
        public void setDeck(Game1 g, Player player, List<Card> deck)
        {
            player.LoadDeck(g, deck);
            //player.setHeroPower(g, new FireBlast(player));
        }
        public override void Update(GameTime gt, Game1 g)
        {
            mouseManager.Update(gt, g);
            objectManager.Update(gt, g);
            queueManager.Update(gt, g);
            networkHandler._gameClient.actionQueue.ExecuteAll();

        }
        public bool CanPlay()
        {
            //Check if game is over/paused or something else
            if (GameOver)
            {
                return false;
            }
            return true;
        }
    }
}
