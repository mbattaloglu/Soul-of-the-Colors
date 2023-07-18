using Game.Player.Abstracts.Inputs;
using Game.InputActions;
using UnityEngine;

namespace Game.Player.Concreates.Inputs
{
    public class ComputerInput : IPlayerInput
    {
        private PlayerInputAction playerInputAction;
        public PlayerInputAction.FootActions footActions { get; private set; }

        public ComputerInput()
        {
            playerInputAction = new PlayerInputAction();
            footActions = playerInputAction.Foot;

            playerInputAction.Enable();
        }

        public float Horizontal => footActions.Move.ReadValue<Vector2>().x;
    }
}