using Fusion;
using UnityEngine;

namespace _Project.Scripts.GameEntities
{
    public class HPSystem : NetworkBehaviour
    {
        [SerializeField] protected float MaxHp = 100;
    
        [Networked]
        protected float CurrentHp {get; set; }
    
        protected bool _isSpawned = false;
    
        public override void Spawned()
        {
            base.Spawned();
            CurrentHp = MaxHp;
            _isSpawned = true;
        }
    
        [Rpc(RpcSources.All, RpcTargets.All)]
        public virtual void RPC_GetDamage(float damage)
        {
            if(!_isSpawned) return;
        
            CurrentHp = Mathf.Clamp(CurrentHp - damage, 0, MaxHp);

            if (CurrentHp <= 0)
            {
                Kill();    
            }
        }
        
        [Rpc(RpcSources.All, RpcTargets.All)]
        public virtual void RPC_AddHP(float amount)
        {
            CurrentHp = Mathf.Clamp(CurrentHp + amount, 0, MaxHp);
        }

        public virtual void Kill()
        {
            if (TryGetComponent(out NetworkObject networkObject))
            {
                _isSpawned = false;
                Runner.Despawn(networkObject);
            }
        }
    }
}
