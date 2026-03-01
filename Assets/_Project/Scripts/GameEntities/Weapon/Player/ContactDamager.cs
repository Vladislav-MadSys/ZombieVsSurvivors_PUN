using _Project.Scripts.GameEntities.Enemies;
using UnityEngine;

namespace _Project.Scripts.GameEntities.Weapon.Player
{
    public class ContactDamager : MonoBehaviour
    {
        [SerializeField] private float Damage = 5;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out EnemyHP enemyHp))
            {
                enemyHp.GetDamage(Damage);
            }
        }
    }
}
