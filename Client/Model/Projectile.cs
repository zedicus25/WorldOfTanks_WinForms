using Client.Model.Directions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Model
{
    public class Projectile : IMovable
    {
        

        public event Action<int> CurrentMove;
        public Position Pos { get; set; }
        public IDirection Direction { get; private set; }
        public string Id { get; private set; }
        public int Speed { get; private set; }
        public Sprite Sprite { get; private set; }
        public Task MoveTask;
        public bool CanMove { get; private set; }
        public Projectile(Position pos, string id, int speed)
        {
            Pos = new Position(pos.X+17, pos.Y+55);
            Id = id;
            Speed = speed;
            CanMove = false;
            Sprite = new Sprite(new Bitmap("bull.png"));
            MoveTask = new Task(Shoot);
        }

        public void SetDirection(IDirection dir, int x, int y)
        {
            if (dir == null)
                throw new NullReferenceException("Direction was null");

            if (CanMove == false)
            {
                Direction = dir;
                if (Direction is LeftDirection)
                {
                    CurrentMove = MoveLeft;
                    Pos.X = x - 15;
                    Pos.Y = y + 17;
                }
                else if (Direction is RightDirection)
                {
                    CurrentMove = MoveRight;
                    Pos.X = x + 45;
                    Pos.Y = y + 17;
                }
                else if (Direction is DownDirection)
                {
                    CurrentMove = MoveDown;
                    Pos.X = x + 17;
                    Pos.Y = y + 45;
                }
                else if (Direction is UpDirection)
                {
                    CurrentMove = MoveUp;
                    Pos.X = x + 17;
                    Pos.Y = y - 15;
                }
                Sprite.Rotate(Direction);
            }
        }


        public void StartMoveTask()
        {
            MoveTask = new Task(Shoot);
            MoveTask.Start();
        }
        public void Shoot()
        {
            CanMove = true;
            while (CanMove)
            {
                Move(300, 300);
                Thread.Sleep(100);
            }
            CanMove = false;
        }

        public void Move(int maxX, int maxY)
        {
            if (CanMove)
            {
                if ((Direction is RightDirection) && (Pos.X + Speed >= maxX))
                {
                    CanMove = false;
                    return;
                }
                else if ((Direction is LeftDirection) && (Pos.X + Speed <= 0))
                {
                    CanMove = false;
                    return;
                }
                else if ((Direction is UpDirection) && (Pos.Y + Speed <= 0))
                {
                    CanMove = false;
                    return;
                }
                else if ((Direction is DownDirection) && (Pos.Y + Speed >= maxY))
                {
                    CanMove = false;
                    return;
                }
                Sprite.Rotate(Direction);
                CurrentMove?.Invoke(Speed);
            }
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
    }
}
