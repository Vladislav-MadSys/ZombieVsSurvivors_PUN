using System;
using _Project.Scripts.GameEntities.PlayerAvatar;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Scripts.UI.Gameplay
{
    public class GameplayHUDModel
    {
        public event Action<Vector2> OnPlayerPositionChanged;
        public event Action<float, float> OnPlayerHpChanged;
        public event Action<float, float> OnPlayerExpChanged;
        
        private PlayerAvatarStates _avatarStates;

        public GameplayHUDModel(PlayerAvatarStates avatarStates)
        {
            _avatarStates = avatarStates;
        }
        
        public void Run()
        {
            _avatarStates.OnPlayerPositionChanged += ChangePlayerPosition;
            _avatarStates.OnPlayerHpChanged += ChangePlayerHp;
            _avatarStates.OnPlayerExpChanged += ChangePlayerExp;
        }

        public void Dispose()
        {
            _avatarStates.OnPlayerPositionChanged -= ChangePlayerPosition;
            _avatarStates.OnPlayerHpChanged -= ChangePlayerHp;
            _avatarStates.OnPlayerExpChanged -= ChangePlayerExp;
        }

        private void ChangePlayerPosition(Vector2 newPosition)
        {
            OnPlayerPositionChanged?.Invoke(newPosition);
        }

        private void ChangePlayerHp(float currentHp, float maxHp)
        {
            OnPlayerHpChanged?.Invoke(currentHp, maxHp);
        }
        
        private void ChangePlayerExp(float currentHp, float maxHp)
        {
            OnPlayerExpChanged?.Invoke(currentHp, maxHp);
        }
    }
}
