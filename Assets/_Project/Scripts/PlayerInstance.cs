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
        }

        public async override void Spawned()
        {
            await UniTask.WaitUntil(() => Owner != PlayerRef.None);
            
            if (Object.HasInputAuthority)
            {
                _inputHandler = ProjectContextInstaller.DiContainer.Resolve<InputHandler>();
                PlayerAvatar = Runner.Spawn(playerAvatarPrefab, inputAuthority: Owner);
                PlayerAvatar.GetComponent<PlayerAvatar>().Initialize(this, _inputHandler);
            }
        }

        public void AvatarKilled()
        { 
            RPC_Disconnect(Owner);
        }

        [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
        private void RPC_Disconnect(PlayerRef playerRef)
        {
            if (Runner.LocalPlayer == playerRef)
            {
                Runner.Shutdown();
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
