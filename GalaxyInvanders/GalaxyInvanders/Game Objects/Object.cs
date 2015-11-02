using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GalaxyInvanders.Game_Objects
{
    class Object : Frame
    {
        protected Vector2 position;
        protected Texture2D model;

        public Vector2 Position { get { return position; } set { } }
        public Texture2D Model { get { return model; } set { model = value; } }
        public Object(Vector2 _position, Texture2D _model, int _frames)
            :base(_frames, _model)
        {
            position = _position;
            model = _model;
        }

        public void MoveUp(float _range)
        { position.Y = position.Y > 0f ? position.Y -= _range : position.Y = 0f; }
        public void MoveDown(float _range)
        { position.Y = ((int)(position.Y) + model.Height) < Commons.GraphicsDevice.Viewport.Height ? position.Y += _range : position.Y = (float)(Commons.GraphicsDevice.Viewport.Height - model.Height); }
        public void MoveLeft(float _range)
        { position.X = position.X > 0 ? position.X -= _range : position.X = 0f; }
        public void MoveRight(float _range)
        { position.X = (int)(position.X) + (model.Width / frames) < Commons.GraphicsDevice.Viewport.Width ? position.X += _range : position.X = (float)(Commons.GraphicsDevice.Viewport.Width - (model.Width / frames)); }
        public void DrawObj()
        {
            Commons.SpriteBatch.Draw(model, position, sprRec, Color.White);
        }
    }
}
