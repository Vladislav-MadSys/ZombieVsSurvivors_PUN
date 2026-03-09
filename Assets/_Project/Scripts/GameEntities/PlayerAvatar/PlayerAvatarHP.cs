using System;
using _Project.Scripts.UI.Gameplay;
using Fusion;
using UnityEngine;

namespace _Project.Scripts.GameEntities.PlayerAvatar
{
    public class PlayerAvatarHP : HPSystem
    {
        [Networked] private PlayerInstance PlayerInstance { get; set; }
        private PlayerAvatar _playerAvatar;
        
        public void Initialize(PlayerInstance playerInstance)
        {
            PlayerInstance = playerInstance;
        }

        private void Start()
        {
            _playerAvatar = PlayerInstance.PlayerAvatar.GetComponent<PlayerAvatar>();
        }

        private void Update()
        {
            //Kill test
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RPC_GetDamage(MaxHp);
            }
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        public override void RPC_GetDamage(float damage)
        {
            if(!_isSpawned) return;
            
            base.RPC_GetDamage(damage);
            if (_playerAvatar.States != null)
            {
                _playerAvatar.States.ChangePlayerHp(CurrentHp, MaxHp);
            }
        }

        public override void Kill()
        {
            base.Kill();
            if (PlayerInstance != null)
            {
                PlayerInstance.AvatarKilled();
            }
        }
    }
}
