using _Project.Scripts.Low.Input;
using Fusion;
using UnityEngine;

namespace _Project.Scripts.GameEntities.PlayerAvatar
{
    public class PlayerAvatar : NetworkBehaviour
    {
        [SerializeField] private PlayerAvatarHP playerAvatarHp;
        [SerializeField] private PlayerAvatarLevelController playerAvatarLevelController;
        [SerializeField] private PlayerAvatarMovementController playerAvatarMovementController;

        private PlayerInstance _playerInstance;
        private InputHandler _inputHandler;

        public bool IsInitialized { get; private set; } = false;

        public PlayerAvatarStates States { get; private set; }
    
        public void Initialize(PlayerInstance playerInstance, InputHandler inputHandler)
        {
            _playerInstance = playerInstance;
            _inputHandler = inputHandler;
        
            States = new PlayerAvatarStates();
            playerAvatarMovementController.Initialize(_inputHandler);
            playerAvatarHp.Initialize(playerInstance, States);
            playerAvatarLevelController.Initialize(States);
            
            IsInitialized = true;
        }

        public void AddExp(int amount)
        {
            playerAvatarLevelController.AddExp(amount);
        }

        public void AddHp(float amount)
        {
            playerAvatarHp.RPC_AddHP(amount);
        }
    }
}
