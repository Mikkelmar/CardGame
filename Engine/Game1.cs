using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using Engine;
using CardGame.Pages;
using CardGame.Graphics;
using CardGame.Managers;
using CardGame.Engine;

namespace CardGame
{
    public class Game1 : Game
    {
        // page
        //public PageManager pageManager { get; private set; } = new PageManager();
        public GameBoard gameBoard;
        public CollectionPage collectionPage;
        public PageManager pageManer = new PageManager();
        public SoundManager soundManager;
        //public SaveManager saveManager = new SaveManager();
        //public SoundManager soundManager = new SoundManager();
        //public LevelMap levelMap;
        public OrthographicCamera gameCamera;
        public float gameSpeed = 1f;
        public static bool devMode = true;
        public bool isClient = true;

        public Page game => pageManer.getActivePage();
        public Game1()
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Drawing.Initialize(this);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();

            //Window.ClientBounds = new Rectangle(0, 0, Drawing.WINDOW_WIDTH, Drawing.WINDOW_HEIGHT);
            var viewportadapter = new BoxingViewportAdapter(Window, GraphicsDevice, 1280*4,720*4);
            gameCamera = new OrthographicCamera(viewportadapter);
            gameCamera.Zoom = gameBoard.cam.Zoom;

            pageManer.addPage(collectionPage);
            pageManer.addPage(gameBoard);
            pageManer.setActivePage(collectionPage, this);

            // window
            IsFixedTimeStep = false;
            Window.Title = Drawing.TITLE;
            Window.AllowUserResizing = true;

            Drawing.graphics.PreferredBackBufferWidth = 1280;
            Drawing.graphics.PreferredBackBufferHeight = 720;

            // Apply the changes
            Drawing.graphics.ApplyChanges();


        }

        protected override void LoadContent()
        {
            Textures.Load(this);

            gameBoard = new GameBoard();
            collectionPage = new CollectionPage();

            soundManager = new SoundManager();
            SoundLoader.LoadSounds(soundManager, Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            Page activePage = pageManer.getActivePage();
            activePage.Update(gameTime, this);

            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            Page activePage = pageManer.getActivePage();
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            //Drawing._spriteBatch.Begin(SpriteSortMode.BackToFront, transformMatrix: gameCamera.GetViewMatrix());//, null, SamplerState.PointClamp);
            Drawing._spriteBatch.Begin(
                sortMode: SpriteSortMode.BackToFront,
                blendState: BlendState.AlphaBlend,
                samplerState: SamplerState.LinearClamp,
                transformMatrix: gameCamera.GetViewMatrix());

            base.Draw(gameTime);
            activePage.Draw(this);
            Drawing._spriteBatch.End();

        }
      
    }
}
