using System;
using _Project.Scripts.GameEntities.Enemies;
using Fusion;
using UnityEngine;

namespace _Project.Scripts.GameEntities.Weapon.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : NetworkBehaviour
    {
        [SerializeField] private float Speed = 5;
        [SerializeField] private float Damage = 50;

        private Transform _transform;
        private Rigidbody2D _rb;
    
        private void Awake()
        {
            _transform = transform;
            _rb = GetComponent<Rigidbody2D>();
        }
    
        public void Init(Vector2 target)
        {
            Vector2 direction = target - (Vector2)transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        public override void FixedUpdateNetwork()
        {
            Vector2 direction = _transform.right;
            _rb.MovePosition(_rb.position + direction * Speed * Time.fixedDeltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<EnemyHP>(out EnemyHP enemyHp))
            {
                enemyHp.GetDamage(Damage);
                Runner.Despawn(GetComponent<NetworkObject>());
            }
        }
    }
}
