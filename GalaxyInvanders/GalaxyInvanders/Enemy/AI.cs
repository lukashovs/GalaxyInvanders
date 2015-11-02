using System;
using System.Collections.Generic;
using GalaxyInvanders.Game_Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Object = GalaxyInvanders.Game_Objects.Object;

namespace GalaxyInvanders.Enemy
{
    class AI
    {
        private List<Enemy> enemy;
        private Object bonus;
        public List<Enemy> Enemy { get { return enemy; } set { } }
        public AI() 
        {
            enemy = new List<Enemy>();
            enemy.Add(new Enemy(5, Commons.Content.Load<Texture2D>(@"player/front"), 4));
            NewBonus();
        }
        public void Update(GameTime gameTime)
        {
            foreach (var e in enemy)
            {
                e.Update(gameTime);
            }
            Strategy();
            if ((int)(gameTime.TotalGameTime.TotalMilliseconds) % 1000 == 0)
                if (bonus.NumberOfFrame == 0)
                    bonus.UpdateFrame(bonus.TimePerFrame);
                    
        }
        public void Draw()
        {
            foreach (var e in enemy)
            {
                e.Draw();
            }
            if (bonus.NumberOfFrame == 0)
                bonus.DrawObj();
            else
            {
                for (int i = 0; i < Commons.GraphicsDevice.Viewport.Width / (bonus.Model.Width / bonus.Frames)+2; i++)
                {
                    Commons.SpriteBatch.Draw(bonus.Model, new Vector2(bonus.Position.X + ((bonus.Model.Width / bonus.Frames) * i), bonus.Position.Y), bonus.SprRec, Color.White);
                }
            }
        }
        private void NewBonus()
        {
            Random rnd = new Random();
            int @new = rnd.Next(0, 1);
            switch (@new)
            {
                case 0:
                    bonus = new Object(new Vector2(-50f, 300f), Commons.Content.Load<Texture2D>(@"bonus/laser"), 2);
                    break;
            }
        }

        public void Strategy()
        {
            foreach (Enemy tEnemy in Enemy)
            {
                if (tEnemy.Position != new Vector2((float)Commons.GraphicsDevice.Viewport.Width / 2, (float)Commons.GraphicsDevice.Viewport.Height / 2))
                tEnemy.MoveDown(1);
                tEnemy.MoveLeft(1);
            }

        }
    }
}
