using GalaxyInvanders.Game_Objects;
using GalaxyInvanders.Screens;
using GalaxyInvanders.Screens.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GalaxyInvanders
{
    public class GalaxyInvanders : Game
    {
        readonly GraphicsDeviceManager _graphics;

        public GalaxyInvanders()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsMouseVisible = true;
        }

        //protected override void Initialize()
        //{
        //    base.Initialize();
        //}

        protected override void LoadContent()
        {
            Commons.Game = this;
            Commons.Content = Content;
            Commons.GraphicsDevice = _graphics.GraphicsDevice;
            Commons.SpriteBatch = new SpriteBatch(_graphics.GraphicsDevice);

            ScreenManager.AddScreen(new MainMenuScreen());
            ScreenManager.AddScreen(new GameScreen());
            ScreenManager.ActivateScreenByName("MainMenuScreen");
            ScreenManager.Initialize();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            ScreenManager.Update(gameTime);
            InputManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            ScreenManager.Draw(gameTime);

            base.Draw(gameTime);
        }

    }
}