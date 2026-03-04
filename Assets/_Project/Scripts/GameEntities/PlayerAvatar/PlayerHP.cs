using UnityEngine;

namespace _Project.Scripts.GameEntities.PlayerAvatar
{
    public class PlayerHP : HPSystem
    {
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
