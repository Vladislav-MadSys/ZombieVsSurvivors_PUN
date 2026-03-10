using Fusion;
using UnityEngine;

namespace _Project.Scripts.GameEntities.Enemies
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyHP : HPSystem
    {
        [SerializeField] private Enemy enemy;
        
        public override void Kill()
        {
            enemy.Kill();
            base.Kill();
        }
    }
}
