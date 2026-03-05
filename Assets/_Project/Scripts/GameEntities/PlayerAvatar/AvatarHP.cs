using UnityEngine;

namespace _Project.Scripts.GameEntities.PlayerAvatar
{
    public class AvatarHP : HPSystem
    {
        private PlayerInstance _playerInstance;
        
        public void Initialize(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
        }
        
        public override void GetDamage(float damage)
        {
            base.GetDamage(damage);
            Debug.Log(CurrentHp + " / " + MaxHp);
            
        }

        public override void Kill()
        {
            base.Kill();
        }
    }
}
