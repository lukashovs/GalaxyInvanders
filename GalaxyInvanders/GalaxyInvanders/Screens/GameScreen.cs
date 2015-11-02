using System.Collections.Generic;
using System.Linq;
using GalaxyInvanders.Enemy;
using GalaxyInvanders.Game_Objects;
using GalaxyInvanders.Screens.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GalaxyInvanders.Screens
{
    internal class GameScreen : Screen
    {
        SpriteFont _font;
         List<MenuItem> menuItems = new List<MenuItem>();
        readonly Color _itemNormalColor = Color.White;
        readonly Color _itemHoverColor = Color.Red;


        internal static SpriteFont SpriteFont;
        Player _player;
        AI _ai;
        Texture2D _gameOver;
        
        bool _gamePause; // пауза bool по умолчанию в false
        bool _pausePress ;

        internal GameScreen()
            : base("GameScreen")
        {

        }

        internal override bool Initialize()
        {
            _player = new Player();
            _ai = new AI();
            

            _gamePause = false;
            _gameOver = Commons.Content.Load<Texture2D>("GameProcess/game_over");
            SpriteFont = Commons.Content.Load<SpriteFont>("Fonts/NewSpriteFont");
            _font = Commons.Content.Load<SpriteFont>("Fonts/MenuFont");
            menuItems.AddRange(new[]
            {
                new MenuItem("Menu", _font, new Vector2(
                    Commons.GraphicsDevice.Viewport.Width - _font.MeasureString("Menu").X,
                    _font.MeasureString("Menu").Y / 2),
                () => { ScreenManager.ActivateScreenByName("MainMenuScreen"); }),
            });
            return base.Initialize();
        }
        //internal override void Remove()
        //{
        //    base.Remove();
        //}
        internal override void Update(GameTime gameTime)
        {
            //====================================================> Меню
            if (InputManager.IsKeyPress(Keys.Escape))
                ScreenManager.ActivateScreenByName("MainMenuScreen");

            //for (int i = 0; i < menuItems.Count; i++)
                foreach (var item in menuItems.Where(item => item.Clicked()))
                {
                    item.Action.Invoke();
                    break;
                }
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Commons.Game.Exit();
            //====================================================> end
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (_pausePress)
                {
                    _gamePause = !_gamePause;
                    _pausePress = false;
                }
            }
            else
                _pausePress = true;
            if (!_gamePause)
            {
                _player.Update(gameTime, _ai.Enemy);
                _ai.Update(gameTime);
            }
            base.Update(gameTime);
        }
        internal override void Draw(GameTime gameTime)
        {
            Commons.GraphicsDevice.Clear(Color.Transparent);

            Commons.SpriteBatch.Begin();
            if (_player.HP<=0)  // поставить на хп игрока
            {
                Commons.SpriteBatch.Draw(_gameOver, new Vector2((Commons.GraphicsDevice.Viewport.Width / 2) - (_gameOver.Width / 2), (Commons.GraphicsDevice.Viewport.Height / 2) - (_gameOver.Height / 2)), Color.White);
                //====================================================> Меню
                foreach (MenuItem t in menuItems)
                {
                    Commons.SpriteBatch.DrawString(
                        _font,
                        t.Text,
                        t.Position,
                        t.Hovered() ? _itemHoverColor : _itemNormalColor);
                }
                //====================================================> end    
            }
            else
            {
                _player.Draw();
                _ai.Draw();
                foreach (MenuItem item in menuItems)
                {
                    Commons.SpriteBatch.DrawString(
                        _font,
                        item.Text,
                        item.Position,
                        item.Hovered() ? _itemHoverColor : _itemNormalColor);
                }
                //====================================================> end           
                if (_gamePause)
                    Commons.SpriteBatch.DrawString(_font, "Pause", new Vector2(Commons.GraphicsDevice.Viewport.Width / 2 - 50, 100), Color.Red);
            }
            Commons.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }        
}