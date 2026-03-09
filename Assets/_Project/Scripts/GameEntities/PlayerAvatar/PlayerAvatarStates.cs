using System;
using UnityEngine;

namespace _Project.Scripts.GameEntities.PlayerAvatar
{
    public class PlayerAvatarStates
    {
        public event Action<Vector2> OnPlayerPositionChanged;
        public event Action<float, float> OnPlayerHpChanged;
    
        public void ChangePlayerPosition(Vector3 newPosition) => OnPlayerPositionChanged?.Invoke(newPosition);
        public void ChangePlayerHp(float avatarCurrentHp, float avatarMaxHp) => OnPlayerHpChanged?.Invoke(avatarCurrentHp, avatarMaxHp);
    }
}
