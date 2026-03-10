using System;
using _Project.Scripts.UI.Gameplay;
using Fusion;
using UnityEngine;

namespace _Project.Scripts.GameEntities.PlayerAvatar
{
    public class PlayerAvatarHP : HPSystem
    {
        private const float HP_UPGRADE_COEFFICIENT = 1.5f;
        private PlayerInstance PlayerInstance { get; set; }
        private PlayerAvatarStates _states;
        
        public void Initialize(PlayerInstance playerInstance, PlayerAvatarStates states)
        {
            PlayerInstance = playerInstance;
            _states = states;
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
            if(!_isSpawned && Runner.LocalPlayer != Object.InputAuthority) return;

            if (Runner.LocalPlayer == Object.InputAuthority)
            {
                base.RPC_GetDamage(damage);
            }

            if (_states != null)
            {
                _states.ChangePlayerHp(CurrentHp, MaxHp);
            }
        }
        
        [Rpc(RpcSources.All, RpcTargets.All)]
        public override void RPC_AddHP(float amount)
        {
            if(!_isSpawned && Runner.LocalPlayer != Object.InputAuthority) return;
            
            base.RPC_AddHP(amount);
            if (_states != null)
            {
                _states.ChangePlayerHp(CurrentHp, MaxHp);
            }
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        public void RPC_UpgradeMaxHp()
        {
            if(!_isSpawned && Runner.LocalPlayer != Object.InputAuthority) return;
            
            float delta = MaxHp * HP_UPGRADE_COEFFICIENT - MaxHp;
            MaxHp *= HP_UPGRADE_COEFFICIENT;
            RPC_AddHP(delta);
            if (_states != null)
            {
                _states.ChangePlayerHp(CurrentHp, MaxHp);
            }
        }
        
        public override void Kill()
        {
            if (PlayerInstance != null && Object.InputAuthority == Runner.LocalPlayer)
            {
                PlayerInstance.AvatarKilled();
            }
            //base.Kill();
        }
    }
}
