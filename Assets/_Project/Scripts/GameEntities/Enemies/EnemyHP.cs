using _Project.Scripts.GameEntities.Enemies;
using Fusion;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHP : NetworkBehaviour
{
    [SerializeField] private float MaxHp = 100;
    
    [Networked]
    private float CurrentHp {get; set; }

    public override void Spawned()
    {
        base.Spawned();
        CurrentHp = MaxHp;
    }

    public void GetDamage(float damage)
    {
        CurrentHp = Mathf.Clamp(CurrentHp - damage, 0, MaxHp);

        if (CurrentHp <= 0)
        {
            Kill();    
        }
    }

    public void Kill()
    {
        if(TryGetComponent(out NetworkObject networkObject))
        Runner.Despawn(networkObject);
    }
}
