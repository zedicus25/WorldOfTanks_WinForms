namespace Client.Model
{
    public interface IMovable
    {
        void Move(int maxX, int maxY);
        void MoveLeft();
        void MoveRight();
        void MoveUp();
        void MoveDown();
    }
}
