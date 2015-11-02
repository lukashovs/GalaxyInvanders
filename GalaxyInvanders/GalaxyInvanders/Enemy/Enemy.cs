using System.Collections.Generic;
using GalaxyInvanders.Game_Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GalaxyInvanders.Enemy
{
    class Enemy : Object
    {
        private List<Shot> shot;
        private int _hp;
        public Enemy(int hp, Texture2D model, int frames)
            : base(new Vector2(Commons.GraphicsDevice.Viewport.Width, 100f), model, frames)
        {
            _hp = hp;
            shot = new List<Shot>();
        }
        public void Draw()
         {
             DrawObj();
             foreach (var s in shot)
             {
                 s.DrawShot();
             }
         }
        public void Update(GameTime time)
        {
            UpdateFrame((float)time.ElapsedGameTime.TotalSeconds);
        }
        public void Hit(int damage) { _hp -= damage; }
        public int Hp { get { return _hp; } set { _hp = value; } }
    }
}
