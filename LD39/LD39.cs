using LD39.Game1;
using LD39.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD39
{
    public class LD39 : Game
    {
        private static bool _mustQuit, _mustLaunch;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SceneManager sceneManager;
        Level mainLevel;
        MainMenu mainMenu;
        Score scoreMenu;
        Camera camera;
        Texture2D mainMenuGfx, gameOver;

        public LD39()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = Constants.GameHeight,
                PreferredBackBufferWidth = Constants.GameWidth,
                IsFullScreen = Constants.GameWindowed
            };
            IsMouseVisible = Constants.MouseVisible;
            Window.AllowUserResizing = Constants.DebugMode;
            Content.RootDirectory = "Content";
            _mustQuit = false;
            _mustLaunch = false;
        }

        protected override void Initialize()
        {
            sceneManager = new SceneManager(GameState.MainMenu);
            camera = new Camera();
            mainLevel = new Level(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            mainMenu = new MainMenu();
            scoreMenu = new Score(GraphicsDevice);

            camera.ViewportWidth = graphics.PreferredBackBufferWidth;
            camera.ViewportHeight = graphics.PreferredBackBufferHeight;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Ressources.LoadImages(Content);
            Ressources.LoadSounds(Content);
            Ressources.LoadFonts(Content);

            gameOver = Ressources.Images["GameOver"];
            mainMenuGfx = Ressources.Images["MainMenu"];

            mainLevel.Load(Content);
        }

        protected override void UnloadContent(){}

        protected override void Update(GameTime gameTime)
        {
            if (_mustQuit)
                Exit();

            if (_mustLaunch)
            {
                sceneManager.SetGameState(GameState.InGame);
                _mustLaunch = false;
            }

            switch (sceneManager.GetGameState())
            {
                case GameState.MainMenu:
                    mainMenu.Update();
                    camera.Zoom = 1f;
                    camera.Position = camera.InitialPosition;
                    break;
                case GameState.InGame:
                    mainLevel.Update(gameTime);
                    mainLevel.HandleGameOver(sceneManager);
                    camera.Position = mainLevel.GetPlayer().GetPosition();
                    camera.Zoom = 1.5f;
                    break;
                case GameState.Score:
                    scoreMenu.Update();
                    camera.Zoom = 1f;
                    camera.Position = camera.InitialPosition;
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointClamp, transformMatrix:camera.TranslationMatrix);
            switch (sceneManager.GetGameState())
            {
                case GameState.MainMenu:
                    spriteBatch.Draw(mainMenuGfx, new Vector2(0, 0), Color.White);
                    mainMenu.Draw(spriteBatch);
                    break;
                case GameState.InGame:
                    mainLevel.Draw(spriteBatch);
                    break;
                case GameState.Score:
                    spriteBatch.Draw(gameOver, new Vector2(0, 0), Color.White);
                    scoreMenu.Draw(spriteBatch);
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public static void MustQuit()
        {
            _mustQuit = true;
        }

        public static void MustLaunch()
        {
            _mustLaunch = true;
        }
    }
}
