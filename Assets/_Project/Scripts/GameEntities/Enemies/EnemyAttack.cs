using _Project.Scripts.GameEntities.PlayerAvatar;
using Fusion;
using UnityEngine;

public class EnemyAttack : NetworkBehaviour
{
    [SerializeField] private float Damage = 1;
    [SerializeField] private float AttackRadius = 2;
    [SerializeField] private float AttackInterval = 1;

    [Networked] private float _timer { get; set; }

    public override void FixedUpdateNetwork()
    {
        if (_timer > AttackInterval)
        {
            PlayerHP target = FindTarget();
            if (target != null)
            {
                Attack(target);
                _timer = 0;
            }
        }
        else
        {
            _timer += Time.fixedDeltaTime;
        }
    }

    public void Attack(PlayerHP target)
    {
        target.GetDamage(Damage);
        Debug.Log("Give damage to: " + target.name);
    }
    
    private PlayerHP FindTarget()
    {
        Collider2D[] colliders = new Collider2D[100];
        Physics2D.OverlapCircleNonAlloc(transform.position, AttackRadius, colliders);
        
        PlayerHP[] playerHps = new PlayerHP[colliders.Length];
        for(int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] != null && colliders[i].TryGetComponent(out PlayerHP playerHp))
            {
                playerHps[i] = playerHp;
            }
        }

        Transform target = null;
        if (playerHps.Length > 0)
        {
            foreach (var enemyHp in playerHps)
            {
                if (target == null && enemyHp != null)
                {
                    target = enemyHp.transform;
                }
                else if(enemyHp != null)
                {
                    Transform target2 = enemyHp.transform;
                    float distance1 = (transform.position - new Vector3(target.position.x, target.position.y, 0)).magnitude;
                    float distance2 = (transform.position - target2.position).magnitude;

                    if (distance1 > distance2)
                    {
                        target = target2;
                    }
                }
            }
        }

        return target != null ? target.GetComponent<PlayerHP>() : null;
    }
}
