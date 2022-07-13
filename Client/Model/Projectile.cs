using Client.Model.Directions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    public class Projectile : IMovable
    {
        public Projectile(Position pos, string id)
        {
            Pos = pos;
        }

        public event Action<int> CurrentMove;
        public Position Pos { get; set; }
        public IDirection Direction { get; private set; }


        public void Move(int maxX, int minX, int maxY, int minY)
        {
            throw new NotImplementedException();
        }

        public void MoveDown(int speed)
        {
            throw new NotImplementedException();
        }

        public void MoveLeft(int speed)
        {
            throw new NotImplementedException();
        }

        public void MoveRight(int speed)
        {
            throw new NotImplementedException();
        }

        public void MoveUp(int speed)
        {
            throw new NotImplementedException();
        }
    }
}
