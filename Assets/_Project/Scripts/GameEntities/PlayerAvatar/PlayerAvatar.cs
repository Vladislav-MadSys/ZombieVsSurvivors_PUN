using _Project.Scripts.Low.Input;
using Fusion;
using UnityEngine;

namespace _Project.Scripts.GameEntities.PlayerAvatar
{
    public class PlayerAvatar : NetworkBehaviour
    {
        [SerializeField] private PlayerAvatarHP playerAvatarHp;
        [SerializeField] private PlayerAvatarMovementController playerAvatarMovementController;

        private PlayerInstance _playerInstance;
        private InputHandler _inputHandler;

        public bool IsInitialized { get; private set; } = false;

        public PlayerAvatarStates States { get; private set; }
    
        public void Initialize(PlayerInstance playerInstance, InputHandler inputHandler)
        {
            _playerInstance = playerInstance;
            _inputHandler = inputHandler;
        
            playerAvatarMovementController.Initialize(_inputHandler);
            playerAvatarHp.Initialize(_playerInstance);
            States = new PlayerAvatarStates();
            
            IsInitialized = true;
        }
    }
}
