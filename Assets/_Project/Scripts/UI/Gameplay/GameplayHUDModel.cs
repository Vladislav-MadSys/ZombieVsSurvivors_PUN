using System;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Scripts.UI.Gameplay
{
    public class GameplayHUDModel
    {
        public event Action<Vector2> OnPlayerPositionChanged;
        public event Action<float, float> OnPlayerHpChanged;
        
        private PlayerAvatarStates _avatarStates;

        public GameplayHUDModel(PlayerAvatarStates avatarStates)
        {
            _avatarStates = avatarStates;
        }
        
        public void Run()
        {
            _avatarStates.OnPlayerPositionChanged += ChangePlayerPosition;
            _avatarStates.OnPlayerHpChanged += ChangePlayerHp;
        }

        public void Dispose()
        {
            _avatarStates.OnPlayerPositionChanged -= ChangePlayerPosition;
            _avatarStates.OnPlayerHpChanged -= ChangePlayerHp;
        }

        private void ChangePlayerPosition(Vector2 newPosition)
        {
            OnPlayerPositionChanged?.Invoke(newPosition);
        }

        private void ChangePlayerHp(float currentHp, float maxHp)
        {
            OnPlayerHpChanged?.Invoke(currentHp, maxHp);
        }
    }
}
