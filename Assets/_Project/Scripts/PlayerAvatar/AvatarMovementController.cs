using _Project.Scripts.Low.Input;
using Fusion;
using UnityEngine;

namespace _Project.Scripts.PlayerAvatar
{
    public class AvatarMovementController : NetworkBehaviour
    {
        private InputHandler _inputHandler;

        public void Initialize(InputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }

        public override void FixedUpdateNetwork()
        {
            Debug.Log(_inputHandler.MoveInput);
        }
    }
}
