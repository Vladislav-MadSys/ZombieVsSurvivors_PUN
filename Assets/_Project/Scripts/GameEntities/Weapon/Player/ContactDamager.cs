using UnityEngine;

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
