using Game.Player.Abstracts.Animations;
using UnityEngine;

namespace Game.Player.Concreates.Animations
{
    public class PlayerAnimation : IPlayerAnimation
    {
        private Animator animator;

        public PlayerAnimation(Animator animator)
        {
            this.animator = animator;
        }

        public void OnCrouchStarted()
        {
            animator.SetBool("Crouch", true);
        }

        public void OnCrouchStopped()
        {
            animator.SetBool("Crouch", false);
        }

        public void OnJumpStarted()
        {
            animator.SetBool("Jump", true);
        }

        public void OnJumpStopped()
        {
            animator.SetBool("Jump", false);
        }

        public void OnRunStarted()
        {
            animator.SetBool("Run", true);
        }

        public void OnRunStopped()
        {
            animator.SetBool("Run", false);
        }

        public void OnWalkStarted()
        {
            animator.SetBool("Walk", true);
        }

        public void OnWalkStopped()
        {
            animator.SetBool("Walk", false);
        }
        
    }
}
