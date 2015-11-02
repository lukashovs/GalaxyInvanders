using System.Collections.Generic;
using GalaxyInvanders.Game_Objects;
using GalaxyInvanders.Screens.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GalaxyInvanders.Screens
{
    internal class MainMenuScreen : Screen
    {
      private SpriteFont _font;
        List<MenuItem>  _menuItems = new List<MenuItem>();
        Color  _itemNormalColor = Color.White;
        Texture2D _playTexture2D;
        Texture2D _exitTexture2D;
        Texture2D _soundTexture2D;
         
        int _widthFrame;                 
        int _heightFrame;
        int _numberOfFrame;
        float _timePerFrame;
        float _totalElapsed;
        Rectangle _spriteRectangle, _spriteRectangle1; //позиция в кадре
        const int Frames = 3;   // количество кадров в фрейме 
        const int FramePerSec = 5; // скорость анимации

        int _widthFrameSound;
        int _heightFrameSound;
        int _numberOfFrameSound;
        Rectangle _spriteRectangleSound; //позиция в кадре
        const int FramesSound = 2;   // количество кадров в фрейме 

        int _widthFrameExit;
        int _heightFrameExit;
        Rectangle _spriteRectangleExit, _spriteRectangleExit1; //позиция в кадре
        const int FramesExit = 2;   // количество кадров в фрейме 
        Vector2 _positionExit, _positionPlay, _positionSound;
       

        private Song _mainSong;
        private bool _sound;
        
        private Texture2D _backgroundTexture2D;
        
        internal MainMenuScreen()
            : base("MainMenuScreen")
        {
        }

        internal override bool Initialize()
        {
            /**************************************************/
            
             //load Content
            _backgroundTexture2D = Commons.Content.Load<Texture2D>("Menu/menu");
            _font = Commons.Content.Load<SpriteFont>("Fonts/MenuFont");
            _playTexture2D = Commons.Content.Load<Texture2D>("Menu/start");
            _exitTexture2D = Commons.Content.Load<Texture2D>("Menu/exit");
            _soundTexture2D = Commons.Content.Load<Texture2D>("Menu/sound");
           /**************************************************/
           //play
            _heightFrame = _playTexture2D.Height;
            _widthFrame = _playTexture2D.Width / Frames;
            _numberOfFrame = 1;
            _timePerFrame = (float)1 / FramePerSec;
            _spriteRectangle = new Rectangle((_widthFrame) * 0, 0, _widthFrame, _heightFrame);
            _spriteRectangle1 = new Rectangle((_widthFrame) * 2, 0, _widthFrame, _heightFrame);
            _positionPlay =  new Vector2(Commons.GraphicsDevice.Viewport.Width/2 - _playTexture2D.Width/6,
                     Commons.GraphicsDevice.Viewport.Height/2 - _playTexture2D.Height/2 - 15);
            //sound
            _heightFrameSound = _soundTexture2D.Height;
            _widthFrameSound = _soundTexture2D.Width / FramesSound;
            _numberOfFrameSound = 0;
            _spriteRectangleSound = new Rectangle(0, 0, _widthFrameSound, _heightFrameSound);
            _positionSound = new Vector2(143, Commons.GraphicsDevice.Viewport.Height - _exitTexture2D.Height * 3 - 20);

            //exit
            _heightFrameExit = _exitTexture2D.Height;
            _widthFrameExit = _exitTexture2D.Width / FramesExit;
            _spriteRectangleExit = new Rectangle(0, 0, _widthFrameExit, _heightFrameExit);
            _spriteRectangleExit1 = new Rectangle((_widthFrameExit) * 1, 0, _widthFrameExit, _heightFrameExit);
            _positionExit = new Vector2(Commons.GraphicsDevice.Viewport.Width - _exitTexture2D.Width - 98,
                Commons.GraphicsDevice.Viewport.Height - _exitTexture2D.Height*3 - 20);
            
            /**************************************************/
            //Song
            MediaPlayer.IsRepeating = true;
            _mainSong = Commons.Content.Load<Song>("Music/main_theme");
            MediaPlayer.Volume = 0.5f; // Громкость (максимальное 1.0f)
            MediaPlayer.Play(_mainSong);
            _sound = true;
            /**************************************************/
         
             _menuItems.AddRange(new[]
      {
        new MenuItem(
          _exitTexture2D,"Exit",
          _positionExit,
          () => { Commons.Game.Exit(); }, FramesExit),
          
          new MenuItem(
                _playTexture2D,"Play",
               _positionPlay,
                () => { ScreenManager.ActivateScreenByName("GameScreen"); }, 3),
                 new MenuItem(
                _soundTexture2D,"Sound",
                _positionSound,
                () => {_sound = SoundButtonClick(_sound); }, FramesSound)
      });/*
            if (GameScreen.lvlSet > 0)
            menuItems.AddRange(new MenuItem[]
      {
                new MenuItem(
          "Restrat ",
          font,
          new Vector2(
            Commons.GraphicsDevice.Viewport.Width / 2,
            Commons.GraphicsDevice.Viewport.Height * 3/6),
          new Action(() => { GameScreen.lvlSet = 0;
              ScreenManager.ActivateScreenByName("GameScreen"); } ))
          });*/
            return base.Initialize();
        }


        internal override void Update(GameTime gameTime)
        {
           
            ButtonStartAnimation((float) gameTime.ElapsedGameTime.TotalSeconds);
            if (InputManager.IsKeyPress(Keys.Escape))
                Commons.Game.Exit();
          //  for (int i = 0; i < _menuItems.Count; i++)
            foreach (var item in _menuItems)
            {
                if (item.Clicked())
                {
                    if (item.Text.Equals("Sound"))
                    {
                        ButtonSoundAnimation((float)gameTime.ElapsedGameTime.TotalSeconds);
                        item.Action.Invoke();
                        break;
                    }
                    item.Action.Invoke();
                    break;
                }
                   
            }
            base.Update(gameTime);
        }

        internal override void Draw(GameTime gameTime)
        {
            Commons.GraphicsDevice.Clear(Color.Gray);
            Commons.SpriteBatch.Begin();
            Commons.SpriteBatch.Draw(_backgroundTexture2D,
                new Rectangle(0, 0, Commons.GraphicsDevice.Viewport.Width, Commons.GraphicsDevice.Viewport.Height),
                Color.White);
         //   for (int i = 0; i < _menuItems.Count; i++)
                foreach (var item in _menuItems)
                {
                    
           
                switch (item.Text)
                {
                    case "Exit":
                        {
                            Commons.SpriteBatch.Draw(item.SpriteTexture2D,
                                item.Hovered() ? new Vector2(_positionExit.X + 3, _positionExit.Y) : _positionExit,
                                   item.Hovered() ? _spriteRectangleExit1 : _spriteRectangleExit,
                                _itemNormalColor);
                            break;
                        }
                    case "Play":
                        {
                            Commons.SpriteBatch.Draw(item.SpriteTexture2D,
                                item.Hovered() ? new Vector2(_positionPlay.X + 5, _positionPlay.Y) : _positionPlay,
                              item.Hovered() ? _spriteRectangle1 : _spriteRectangle, 
                             _itemNormalColor);
                            break;
                        }
                    case "Sound":
                        {
                            Commons.SpriteBatch.Draw(_soundTexture2D,
                                _positionSound,
                            _spriteRectangleSound,
                                _itemNormalColor);
                            break;
                        }
                }
            }
            Commons.SpriteBatch.End();
            base.Draw(gameTime);
        }

        protected void ButtonStartAnimation(float elpT)
        {
            _totalElapsed += elpT;
            if (_totalElapsed > _timePerFrame)
            {
                if (_numberOfFrame == Frames-2)
                    _numberOfFrame = 0;
                else
                {
                    _numberOfFrame++;
                }
                _spriteRectangle = new Rectangle((_widthFrame) * _numberOfFrame, 0, _widthFrame, _heightFrame);
                _totalElapsed = 0;
            }
        }

        private bool SoundButtonClick(bool sound)
        {
            if (sound)
            {
                MediaPlayer.Pause();
            }
            else
            {
                MediaPlayer.Resume();
            }
            return !sound;
        }

        protected void ButtonSoundAnimation(float elpT)
        {
            _totalElapsed += elpT;
            if (_totalElapsed > 0)
            {
                if (_numberOfFrameSound == FramesSound -1)
                    _numberOfFrameSound = 0;
                else
                {
                    _numberOfFrameSound++;
                }
                _spriteRectangleSound = new Rectangle((_widthFrameSound) * _numberOfFrameSound, 0, _widthFrameSound, _heightFrameSound);
                _totalElapsed = 0;
            }
        }
    }
}