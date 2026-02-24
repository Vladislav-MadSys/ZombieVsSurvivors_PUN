using _Project.Scripts.GameEntities.PlayerAvatar;
using _Project.Scripts.Installers;
using _Project.Scripts.Low.Input;
using _Project.Scripts.Session;
using Fusion;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts
{
    public class PlayerInstance : NetworkBehaviour
    {
        [SerializeField] private GameObject playerAvatarPrefab;
        
        private InputHandler _inputHandler;
        private RoomSessionData _roomSessionData;

        [Networked] public PlayerRef Owner { get; private set; }

        [Networked] public NetworkObject PlayerAvatar { get; private set; }

        public void Initialize(PlayerRef owner)
        {
            Owner = owner;
            RegisterWithRoom().Forget();
            /*
            _roomSessionData = GameSceneContainer.Instance.RoomSessionData;
            _roomSessionData.RPC_PlayerJoin(Owner, this);*/
        }

        public async override void Spawned()
        {
            await UniTask.WaitUntil(() => Owner != PlayerRef.None);
            
            if (Object.HasInputAuthority)
            {
                _inputHandler = ProjectContextInstaller.DiContainer.Resolve<InputHandler>();
                PlayerAvatar = Runner.Spawn(playerAvatarPrefab, inputAuthority: Owner);
                AvatarMovementController avatarMovementController = PlayerAvatar.GetComponent<AvatarMovementController>();
                avatarMovementController.Initialize(_inputHandler);
                
            }
        }
        
        private async UniTaskVoid RegisterWithRoom()
        {
            await UniTask.WaitUntil(() => 
                GameSceneContainer.Instance != null &&
                GameSceneContainer.Instance.RoomSessionData != null
            );

            var roomData = GameSceneContainer.Instance.RoomSessionData;
            roomData.RPC_PlayerJoin(Owner, this);
        }
    }
}
