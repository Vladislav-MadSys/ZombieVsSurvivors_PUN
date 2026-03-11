using System;
using _Project.Scripts.GameEntities.PlayerAvatar.Weapon;
using _Project.Scripts.Low.Input;
using Fusion;
using UnityEngine;

namespace _Project.Scripts.GameEntities.PlayerAvatar
{
    public class PlayerAvatar : NetworkBehaviour
    {
        [SerializeField] private PlayerAvatarHP playerAvatarHp;
        [SerializeField] private PlayerAvatarUpgradeManager playerAvatarUpgradeManager;
        [SerializeField] private PlayerAvatarLevelController playerAvatarLevelController;
        [SerializeField] private PlayerAvatarMovementController playerAvatarMovementController;

        [SerializeField] private MachineGun machineGun;

        private InputHandler _inputHandler;

        public PlayerInstance PlayerInstance { get; private set; }
        public bool IsInitialized { get; private set; } = false;

        public PlayerAvatarStates States { get; private set; }
    
        public void Initialize(PlayerInstance playerInstance, InputHandler inputHandler)
        {
            PlayerInstance = playerInstance;
            _inputHandler = inputHandler;
        
            States = new PlayerAvatarStates();
            playerAvatarMovementController.Initialize(_inputHandler, States);
            playerAvatarHp.Initialize(playerInstance, States);
            playerAvatarLevelController.Initialize(States);
            playerAvatarUpgradeManager.Initialize(States);
            machineGun.Initialize(States);
            
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
