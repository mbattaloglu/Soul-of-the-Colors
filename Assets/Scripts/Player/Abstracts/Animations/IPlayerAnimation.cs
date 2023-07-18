namespace Game.Player.Abstracts.Animations
{
    public interface IPlayerAnimation
    {
        void OnWalkStarted();
        void OnWalkStopped();
        void OnRunStarted();
        void OnRunStopped();
        void OnCrouchStarted();
        void OnCrouchStopped();
        void OnJumpStarted();
        void OnJumpStopped();
    }
}