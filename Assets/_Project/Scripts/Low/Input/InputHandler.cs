using System;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Low.Input
{
    public class InputHandler : IInitializable, IDisposable, ITickable
    {
        public Vector2 MoveInput { get; private set; }
    
        private PlayerInput _playerInput;

        [Inject]
        public void Inject(PlayerInput playerInput)
        {
            _playerInput = playerInput;
        }

        public void Initialize()
        {
            _playerInput.Enable();
        }
        
        public void Dispose()
        {
            _playerInput.Disable();
        }
            
        public void Tick()
        {
            MoveInput = _playerInput.Player.Move.ReadValue<Vector2>();
        }
    }
}
