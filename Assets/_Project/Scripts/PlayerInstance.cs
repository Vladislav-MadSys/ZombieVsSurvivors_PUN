using _Project.Scripts.Low.Input;
using _Project.Scripts.NetworkSpawners;
using _Project.Scripts.PlayerAvatar;
using Fusion;
using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class PlayerInstance : NetworkBehaviour
    {
        [SerializeField] private GameObject playerAvatarPrefab;

        private PlayerRef _owner;
        private InputHandler _inputHandler;
        
        public void Initialize(InputHandler inputHandler, PlayerRef owner)
        {
            _owner = owner;
            _inputHandler = inputHandler;
        }
        
        private void Start()
        {
            if (Object.HasInputAuthority)
            {
                NetworkObject playerAvatar = Runner.Spawn(playerAvatarPrefab, inputAuthority: _owner);
                AvatarMovementController avatarMovementController = playerAvatar.GetComponent<AvatarMovementController>();
                avatarMovementController.Initialize(_inputHandler);
            }
        }
    }
}
