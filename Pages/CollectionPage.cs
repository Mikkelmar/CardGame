using CardGame.Managers;
using CardGame.Managers.GameManagers;
using CardGame.Objects;
using CardGame.Objects.Collection;
using CardGame.PanimaionSystem;
using Engine;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;


namespace CardGame.Pages
{
    public class CollectionPage : Page
    {
        public CollectionPage() : base(1) { }

        public Camera cam = new Camera(new Vector2(0, 0));
        public OrthographicCamera _camera;
        public bool gamePaused = false;
        public ObjectManager objectManager = new ObjectManager();
        public MouseManager mouseManager = new MouseManager();
        public CardManager cardManager;
        public QueueManager queueManager = new QueueManager();
        public CollectionManager collectionManager;
        public DeckSlotSelectionPage deckSlotSelectionPage;
        public DeckManager deckManager = new DeckManager();
        public int activeDeckSlot = 0;
        public override void Init(Game1 g)
        {
            cardManager = new CardManager();

            deckSlotSelectionPage = new DeckSlotSelectionPage(g);
            objectManager.Add(deckSlotSelectionPage, g);

            //EditDeck(g);


        }
        public void EditDeck(Game1 g, Managers.Deck deck)
        {
            objectManager.Clear(g);
            mouseManager.Clear();
            collectionManager = new CollectionManager();
            collectionManager.Init(g, deck);
            //C: \Users\mikke\source\repos\CardGame\Cards\cards.json
            objectManager.Add(new Button("Share deck", (_g) => 
                ClipboardHelper.SetText(g.collectionPage.collectionManager.deckBuilder.printCommaSeparatedCardList()),
                scale: 2f)
            { X = 4000, Y = 2500, Width = 300, Height = 180 }, g);

            objectManager.Add(new Button("Load deck", (_g) =>
                g.collectionPage.collectionManager.deckBuilder.SetDeck(g, cardManager.LoadDeckFromString(ClipboardHelper.GetText())),
                scale: 2f)
            { X = 4400, Y = 2500, Width = 300, Height = 180 }, g);

            
            
            //objectManager.Add(new Button("Load deck", (_g) =>
            //  ClipboardHelper.SetText(g.collectionPage.collectionManager.deckBuilder.d))
            //{ X = 4000, Y = 1160, Width = 200, Height = 180 }, g);

            objectManager.Add(new OnlyCanAddButton()
            { X = 4000, Y = 960, Width = 800, Height = 180 }, g);

            objectManager.Add(new Button("<--", (_g) => g.collectionPage.collectionManager.newPage(_g, -1))
            { X = 4000, Y = 1160, Width = 380, Height = 140 }, g);
            objectManager.Add(new Button("-->", (_g) => g.collectionPage.collectionManager.newPage(_g, 1)) 
            { X = 4420, Y = 1160, Width = 380, Height = 140 }, g);
            objectManager.Add(new Button("Back", (_g)  => g.collectionPage.GoBack(_g)
                ) { X = 4000, Y = 2100, Width = 400, Height = 200 }, g);
            objectManager.Add(new Button("Battle!", (_g) => {
                string deck = _g.collectionPage.collectionManager.deckBuilder.printCommaSeparatedCardList();
                g.pageManer.setActivePage(_g.gameBoard, _g);

                g.gameBoard.setDeck(_g, _g.gameBoard.gameHandler.player1, deck);
                g.gameBoard.gameHandler.StartGame(_g);
            })
            { X = 4500, Y = 2100, Width = 400, Height = 200 }, g);

            objectManager.Add(new Button("Up", (_g) => collectionManager.deckDiplay.Scroll(g, 100), 2f)
            { X = 880, Y = 2500, Width = 120, Height = 120 }, g);
            objectManager.Add(new Button("Down", (_g) => collectionManager.deckDiplay.Scroll(g, -100), 2f)
            { X = 880, Y = 2660, Width = 120, Height = 120 }, g);


        }
        public void GoBack(Game1 g)
        {
            objectManager.Clear(g);
            collectionManager.Destroy(g);
            deckSlotSelectionPage = new DeckSlotSelectionPage(g);
            objectManager.Add(deckSlotSelectionPage, g);
        }
        public override void Draw(Game1 g)
        {
            objectManager.Draw(g);
            queueManager.Draw(g);
        }

        public override void DrawUI(Game1 g)
        {
        }

        public override void Load(Game1 g)
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
        public override void Update(GameTime gt, Game1 g)
        {
            mouseManager.Update(gt, g);
            objectManager.Update(gt, g);
            queueManager.Update(gt, g);
        }
    }
}
