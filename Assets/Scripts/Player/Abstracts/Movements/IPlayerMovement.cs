namespace Game.Player.Abstracts.Movements
{
    public interface IPlayerMovement
    {
        void Move(float horizontal);
        void Rotate(float horizontal);
        void Crouch(bool isCrouch);
        void Jump();
    }
}