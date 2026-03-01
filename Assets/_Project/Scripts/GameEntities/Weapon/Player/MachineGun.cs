using _Project.Scripts.GameEntities.Enemies;
using Fusion;
using UnityEngine;

namespace _Project.Scripts.GameEntities.Weapon.Player
{
    public class MachineGun : NetworkBehaviour
    {
        [SerializeField] private GameObject BulletPrefab;
        [SerializeField] private float ShootInterval = 1;
        [SerializeField] private float AimRadius = 10;

        [Networked] private float _timer { get; set; }
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public override void FixedUpdateNetwork()
        {
            if (_timer > ShootInterval)
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
            Physics2D.OverlapCircleNonAlloc(transform.position, AimRadius, colliders);
        
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
            NetworkObject bullet = Runner.Spawn(BulletPrefab, _transform.position, _transform.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bullet.transform.parent = null;
            bulletScript.Init(target);
        }
    }
}
