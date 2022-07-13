using Client.Model.Directions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Model
{
    public class Tank : IMovable
    {
        public event Action<int> CurrentMove;
        public Position Pos { get; set; }
        public IDirection Direction { get; private set; }
        public Projectile Projectile { get; private set; }
        public Sprite Sprite { get; private set; }
        public string Id { get; private set; }
        public int Speed { get; private set; }


        public Tank(Position pos, string id, string sprite, IDirection dir, int speed)
        {
            Pos = pos;
            Sprite = new Sprite(new System.Drawing.Bitmap(sprite));
            Id = id;
            Direction = dir;
            Speed = speed;
            Projectile = new Projectile(pos, id, Speed*2);
            CurrentMove = MoveDown;
        }

        public void SetUserInput(Keys key)
        {
            switch (key)
            { 
                case Keys.A:
                    Direction = new LeftDirection();
                    CurrentMove = MoveLeft;
                    break;
                case Keys.D:
                    Direction = new RightDirection();
                    CurrentMove = MoveRight;
                    break;
                case Keys.W:
                    Direction = new UpDirection();
                    CurrentMove = MoveUp;
                    break;
                case Keys.S:
                    Direction = new DownDirection();
                    CurrentMove = MoveDown;
                    break;
                case Keys.Space:
                    Shoot();
                    break;
                default:
                    break;
            }
            Projectile.SetDirection(Direction, Pos.X, Pos.Y);
            Sprite.Rotate(Direction);
            CurrentMove?.Invoke(Speed);

        }

        public void Shoot()
        {
            if(Projectile.CanMove == false)
                Projectile.StartMoveTask();
        }

        #region Moving
        public void Move(int maxX, int maxY)
        {
            if ((Direction is RightDirection) && (Pos.X + Speed >= maxX))
            {
                Direction = new LeftDirection();
                CurrentMove = MoveLeft;
            }
            else if ((Direction is LeftDirection) && (Pos.X + Speed <= 0))
            {
                Direction = new RightDirection();
                CurrentMove = MoveRight;
            }
            else if ((Direction is UpDirection) && (Pos.Y + Speed <= 0))
            {
                Direction = new DownDirection();
                CurrentMove = MoveDown;
            }
            else if ((Direction is DownDirection) && (Pos.Y + Speed + Sprite.Icon.Height >= maxY))
            {
                Direction = new UpDirection();
                CurrentMove = MoveUp;
            }
            Sprite.Rotate(Direction);
            CurrentMove?.Invoke(Speed);
            Projectile.SetDirection(Direction, Pos.X, Pos.Y);
        }

        public void MoveDown(int speed)
        {
            Pos.Y += speed;
        }

        public void MoveLeft(int speed)
        {
            Pos.X -= speed;
        }

        public void MoveRight(int speed)
        {
            Pos.X += speed;
        }

        public void MoveUp(int speed)
        {
            Pos.Y -= speed;
        }
        #endregion
    }
}
