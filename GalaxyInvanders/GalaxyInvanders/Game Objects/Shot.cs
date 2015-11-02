using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GalaxyInvanders.Game_Objects
{
    class Shot : Object
    {
        private GoTo _direction;
        private const float Velocity = 8f;

        public Shot(Vector2 position, GoTo direction, Texture2D model)
            : base(position, model, 1)
        {
            _direction = direction;
        }
        public void Move(float elpT)
        {
            switch (_direction)
            {
                case GoTo.Up:
                    MoveUp(Velocity);
                    break;
                case GoTo.Down:
                    MoveDown(Velocity);
                    break;
                case GoTo.LeftUp:
                    MoveUp(Velocity);
                    MoveLeft(Velocity);
                    break;
                case GoTo.RightUp:
                    MoveUp(Velocity);
                    MoveRight(Velocity);
                    break;
                case GoTo.LeftDown:
                    MoveDown(Velocity);
                    MoveLeft(Velocity);
                    break;
                case GoTo.RightDown:
                    MoveDown(Velocity);
                    MoveRight(Velocity);
                    break;
                case GoTo.Stop:
                    UpdateFrame(elpT);
                    break;
            }
        }
        public void DrawShot()
        {
            Commons.SpriteBatch.Draw(model, position, sprRec, Color.White);
        }
        public bool OutOfMap()
        {
            if (position.X <= 0 || position.Y <= 0 || position.X >= Commons.GraphicsDevice.Viewport.Width || position.Y >= Commons.GraphicsDevice.Viewport.Height)
                return true;
            return false;
        }
        public bool IsHit(Object obj)
        {
            if (frames == 1)
                if (position.X < obj.Position.X + (obj.Model.Width / obj.Frames) && position.X + (model.Width / frames) > obj.Position.X
                    && position.Y < obj.Position.Y + obj.Model.Height && position.Y + model.Height > obj.Position.Y)
                {
                    model = Commons.Content.Load<Texture2D>("gameprocess/hit1");
                    ChangeFrame(6, model);
                    _direction = GoTo.Stop;
                    return true;
                }
            return false;
        }
    }
     enum GoTo
    {
        Up,
        Down,
        LeftUp,
        RightUp,
        LeftDown,
        RightDown,
        Stop
    }
}
