using _Project.Scripts.GameEntities.Enemies;
using Fusion;
using UnityEngine;

namespace _Project.Scripts.GameEntities.PlayerAvatar.Weapon
{
    public class MachineGun : NetworkBehaviour
    {
        private const float FIRE_RATE_UPGRADE_COEFFICIENT = 1.3f;
        private const float DAMAGE_UPGRADE_COEFFICIENT = 1.3f;
        
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float damage = 50;
        [SerializeField] private float shootInterval = 1;
        [SerializeField] private float aimRadius = 10;

        [Networked] private float _timer { get; set; }
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public void UpgradeFireRate()
        {
            shootInterval /= FIRE_RATE_UPGRADE_COEFFICIENT;
        }
        
        public void UpgradeDamage()
        {
            damage *= DAMAGE_UPGRADE_COEFFICIENT;
        }
        
        public override void FixedUpdateNetwork()
        {
            if (_timer > shootInterval)
            {
                Vector2 target = FindTarget();
                if (target != Vector2.zero)
                {
                    Shoot(target);
                    _timer = 0;
                }
            }
            else
            {
                _timer += Time.fixedDeltaTime;
            }
        }

        private Vector2 FindTarget()
        {
            Collider2D[] colliders = new Collider2D[100];
            Physics2D.OverlapCircleNonAlloc(transform.position, aimRadius, colliders);
        
            EnemyHP[] enemyHps =new EnemyHP[colliders.Length];
            for(int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != null && colliders[i].TryGetComponent(out EnemyHP enemyHp))
                {
                    enemyHps[i] = enemyHp;
                }
            }

            Vector2 target = Vector2.zero;
            if (enemyHps.Length > 0)
            {
                foreach (var enemyHp in enemyHps)
                {
                    if (target == Vector2.zero && enemyHp != null)
                    {
                        target = enemyHp.transform.position;
                    }
                    else if(enemyHp != null)
                    {
                        Vector3 target2 = enemyHp.transform.position;
                        float distance1 = (transform.position - new Vector3(target.x, target.y, 0)).magnitude;
                        float distance2 = (transform.position - target2).magnitude;

                        if (distance1 > distance2)
                        {
                            target = target2;
                        }
                    }
                }
            }

            return target;
        }

        private void Shoot(Vector2 target)
        {
            NetworkObject bullet = Runner.Spawn(bulletPrefab, _transform.position, _transform.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bullet.transform.parent = null;
            bulletScript.Init(target, damage);
        }
    }
}
