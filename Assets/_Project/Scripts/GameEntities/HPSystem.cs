using Fusion;
using UnityEngine;

namespace _Project.Scripts.GameEntities
{
    public class HPSystem : NetworkBehaviour
    {
        [SerializeField] protected float MaxHp = 100;
    
        [Networked]
        protected float CurrentHp {get; set; }
    
        private bool _isSpawned = false;
    
        public override void Spawned()
        {
            base.Spawned();
            CurrentHp = MaxHp;
            _isSpawned = true;
        }
    
        public virtual void GetDamage(float damage)
        {
            if(!_isSpawned) return;
        
            CurrentHp = Mathf.Clamp(CurrentHp - damage, 0, MaxHp);

            if (CurrentHp <= 0)
            {
                Kill();    
            }
        }

        public virtual void Kill()
        {
            Debug.Log("Killed");
            if(TryGetComponent(out NetworkObject networkObject))
                Runner.Despawn(networkObject);
        }
    }
}
