using System;

namespace Client.Model
{
    public interface IMovable
    {
        event Action<int> CurrentMove;
        void Move(int maxX, int minX,int maxY, int minY);
        void MoveLeft(int speed);
        void MoveRight(int speed);
        void MoveUp(int speed);
        void MoveDown(int speed);
    }
}
