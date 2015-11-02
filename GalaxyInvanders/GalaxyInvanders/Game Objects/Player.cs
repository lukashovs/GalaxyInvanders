using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GalaxyInvanders.Game_Objects
{
    class Player : Object
    {
        private List<Shot> shot;
        private int _hp;
        private const int HpMax = 5;
        private const float Velocity = 7f;
        private bool _keypressLeft, _keypressRight, _keypressF;
        private int _holder;
        private const int HolderMax = 20;
        private int _damage;

        public Player()
            :base(new Vector2(350f, 400f), Commons.Content.Load<Texture2D>(@"player/front"), 4)
        {
            _hp = HpMax;
            _keypressLeft = _keypressRight = _keypressF = false;
            _holder = HolderMax;
            shot = new List<Shot>();
            _damage = 1;
        }
        public void Update(GameTime time, List<Enemy.Enemy> enemy)
        {
            UpdateFrame((float)time.ElapsedGameTime.TotalSeconds);
            MoveShots(time);
            RemoveShots(enemy);  // удалени снарядов, вышедших за пределы карты или попали в противника
            Move();
            Fire(time);
        }
        public void Draw()
        {
            DrawObj();  // отрисовываем игрока
            foreach (var s in shot)
            {
                s.DrawShot();
            }
        }

        public int HP {
            get { return _hp; }
            set { }
        }



        private void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                MoveUp(Velocity);
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                MoveDown(Velocity);
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                MoveLeft(Velocity);
                if (_keypressLeft)
                {
                    ChangeFrame(3, Commons.Content.Load<Texture2D>(@"player/left"));
                    model = Commons.Content.Load<Texture2D>(@"player/left");
                    _keypressLeft = false;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                MoveRight(Velocity);
                if (_keypressRight)
                {
                    ChangeFrame(3, Commons.Content.Load<Texture2D>(@"player/right"));
                    model = Commons.Content.Load<Texture2D>(@"player/right");
                    _keypressRight = false;
                }
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Left))
            {
                _keypressLeft = true;
                if (_keypressRight)
                    if (frames != 4)
                    {
                        ChangeFrame(4, Commons.Content.Load<Texture2D>(@"player/front"));
                        model = Commons.Content.Load<Texture2D>(@"player/front");
                    }
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Right))
            {
                _keypressRight = true;
                if (_keypressLeft)
                    if (frames != 4)
                    {
                        ChangeFrame(4, Commons.Content.Load<Texture2D>(@"player/front"));
                        model = Commons.Content.Load<Texture2D>(@"player/front");
                    }
            }
        }
        private void Fire(GameTime time)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                if (_keypressF)
                {
                    if (_holder > 0)
                    {
                        shot.Add(new Shot(new Vector2(position.X + ((model.Width / frames)/2 - 3), position.Y), GoTo.Up, Commons.Content.Load<Texture2D>(@"player/shot")));
                        _holder--;
                        _keypressF = false;
                    }
                }
            }
            if (Keyboard.GetState().IsKeyUp(Keys.F))
                _keypressF = true;
        }
        private void RemoveShots(List<Enemy.Enemy> enemy)
        {
            for (int i = 0; i < shot.Count; i++)
            {
                if (shot[i].OutOfMap() || shot[i].NumberOfFrame == 5)
                {
                    shot.RemoveAt(i);
                    i--;
                }
                else
                    for (int j = 0; j < enemy.Count; j++)
                    {
                        if (shot[i].IsHit(enemy[j]))
                        {
                            if (enemy[j].Hp <= _damage)
                            {
                                enemy.RemoveAt(j);
                                j--;
                            }
                            else
                                enemy[j].Hit(_damage);
                        }
                    }
            }
        }
        private void MoveShots(GameTime time)
        {
            foreach (var s in shot)
            {
                s.Move((float)time.ElapsedGameTime.TotalSeconds);
            }
        }
    }
}
