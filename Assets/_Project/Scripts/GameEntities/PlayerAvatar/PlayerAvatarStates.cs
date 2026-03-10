using System;
using UnityEngine;

namespace _Project.Scripts.GameEntities.PlayerAvatar
{
    public class PlayerAvatarStates
    {
        public event Action<Vector2> OnPlayerPositionChanged;
        public event Action<float, float> OnPlayerHpChanged;
        public event Action<float, float> OnPlayerExpChanged;
        public event Action OnPlayerLevelUp;
        public event Action<PlayerAvatarUpgradeManager> OnPlayerUpgradeReady;
        public event Action OnPlayerUpgraded;
        public event Action<int> OnPlayerShoot;
        public event Action<Vector2> OnPlayerMove;
    
        public void ChangePlayerPosition(Vector3 newPosition) => OnPlayerPositionChanged?.Invoke(newPosition);
        public void ChangePlayerHp(float avatarCurrentHp, float avatarMaxHp) => OnPlayerHpChanged?.Invoke(avatarCurrentHp, avatarMaxHp);
        public void ChangePlayerExp(float avatarCurrentExp, float expToNextLevel) => OnPlayerExpChanged?.Invoke(avatarCurrentExp, expToNextLevel);
        public void PlayerLevelUp() => OnPlayerLevelUp?.Invoke();
        public void SetReadyPlayerUpgrade(PlayerAvatarUpgradeManager upgradeManager) => OnPlayerUpgradeReady?.Invoke(upgradeManager);
        public void PlayerUpgraded() => OnPlayerUpgraded?.Invoke();
        public void PlayerShoot(int newShootsCount) => OnPlayerShoot?.Invoke(newShootsCount);
        public void PlayerMove(Vector2 newPosition) => OnPlayerMove?.Invoke(newPosition);
    }
}
