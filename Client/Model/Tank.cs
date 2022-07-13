using Client.Model.Directions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Projectile = new Projectile(pos, id);
            Sprite = new Sprite(new System.Drawing.Bitmap(sprite));
            Id = id;
            Direction = dir;
            Speed = speed;
            CurrentMove = MoveDown;
        }

        #region Moving
        public void Move(int maxX, int minX, int maxY, int minY)
        {
            if ((Direction is RightDirection) && (Pos.X + Speed >= maxX))
            {
                Direction = new LeftDirection();
                CurrentMove = MoveLeft;
            }
            else if ((Direction is LeftDirection) && (Pos.X + Speed <= minX))
            {
                Direction = new RightDirection();
                CurrentMove = MoveRight;
            }
            else if ((Direction is UpDirection) && (Pos.Y + Speed + Sprite.Icon.Height >= maxY))
            {
                Direction = new DownDirection();
                CurrentMove = MoveDown;
            }
            else if ((Direction is DownDirection) && (Pos.Y + Speed <= minY))
            {
                Direction = new UpDirection();
                CurrentMove = MoveUp;
            }
            Sprite.Rotate(Direction);
            CurrentMove?.Invoke(Speed);
        }

        public void MoveDown(int speed)
        {
            Pos.Y -= speed;
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
            Pos.Y += speed;
        }
        #endregion
    }
}
