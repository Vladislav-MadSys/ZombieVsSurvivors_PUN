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

        [Networked] public NetworkObject PlayerAvatarObject { get; private set; }

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
                PlayerAvatarObject = Runner.Spawn(playerAvatarPrefab, inputAuthority: Owner);
                PlayerAvatarObject.GetComponent<PlayerAvatar>().Initialize(this, _inputHandler);
            }
        }

        public void AvatarKilled()
        {
            if (Object.HasInputAuthority)
            {
                Disconnect(Owner);
            }
        }

        private void Disconnect(PlayerRef playerRef)
        {
            Debug.Log("DISCONNECT " + playerRef);
            _roomSessionData = GameSceneContainer.Instance.RoomSessionData;
            
            if (Object.InputAuthority == playerRef)
            {
                _roomSessionData.RPC_PlayerLeave(playerRef);
                PlayerAvatarObject.gameObject.SetActive(false);
                //await UniTask.WaitForSeconds(1);
                Runner.Shutdown();
            }
        }
        
        private async UniTaskVoid RegisterWithRoom()
        {
            await UniTask.WaitUntil(() => 
                GameSceneContainer.Instance != null &&
                GameSceneContainer.Instance.RoomSessionData != null
            );

            _roomSessionData = GameSceneContainer.Instance.RoomSessionData;
            var roomData = _roomSessionData;
            roomData.RPC_PlayerJoin(Owner, this);
        }
    }
}
