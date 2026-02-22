using _Project.Scripts.GameEntities.PlayerAvatar;
using _Project.Scripts.Low.Input;
using _Project.Scripts.NetworkSpawners;
using _Project.Scripts.Session;
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
        private RoomSessionData _roomSessionData;

        [Networked] public NetworkObject PlayerAvatar { get; private set; }

        public void Initialize(InputHandler inputHandler, PlayerRef owner)
        {
            _owner = owner;
            _inputHandler = inputHandler;
        }

        private void Start() 
        {
            if (Object.HasInputAuthority)
            {
                _roomSessionData = GameSceneContainer.Instance.RoomSessionData;
                PlayerAvatar = Runner.Spawn(playerAvatarPrefab, inputAuthority: _owner);
                _roomSessionData.RPC_PlayerJoin(_owner, this);
                AvatarMovementController avatarMovementController = PlayerAvatar.GetComponent<AvatarMovementController>();
                avatarMovementController.Initialize(_inputHandler);
            }
        }
    }
}
