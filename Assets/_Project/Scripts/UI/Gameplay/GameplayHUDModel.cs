using System;
using System.Collections.Generic;
using _Project.Scripts.GameEntities.PlayerAvatar;
using _Project.Scripts.Session;
using Fusion;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Scripts.UI.Gameplay
{
    public class GameplayHUDModel
    {
        public event Action<PlayerRef> OnPlayerJoined;
        public event Action<PlayerRef> OnPlayerLeft;
        public event Action<List<PlayerInstance>> OnRecheckPlayers;
        public event Action<Vector2> OnPlayerPositionChanged;
        public event Action<float, float> OnPlayerHpChanged;
        public event Action<float, float> OnPlayerExpChanged;
        public event Action<PlayerAvatarUpgradeManager> OnPlayerLevelUpgrade;

        private PlayerInstance _owner;
        private RoomSessionData _roomSessionData;
        private PlayerAvatarStates _avatarStates;
        private List<PlayerInstance> _otherPlayers = new List<PlayerInstance>();
        
        private PlayerAvatarUpgradeManager _playerAvatarUpgradeManager;

        public GameplayHUDModel(RoomSessionData roomSessionData, PlayerInstance owner, PlayerAvatarStates avatarStates)
        {
            _roomSessionData = roomSessionData;
            _owner = owner;
            _avatarStates = avatarStates;
        }
        
        public async void Run()
        {
            _roomSessionData.OnPlayerJoined += PlayerJoined;
            _roomSessionData.OnPlayerLeft += PlayerLeft;
            
            _avatarStates.OnPlayerPositionChanged += ChangePlayerPosition;
            _avatarStates.OnPlayerHpChanged += ChangePlayerHp;
            _avatarStates.OnPlayerExpChanged += ChangePlayerExp;
            _avatarStates.OnPlayerUpgradeReady += PlayerUpgradeReady;
        }
        
        public void Dispose()
        {
            _roomSessionData.OnPlayerJoined -= PlayerJoined;
            _roomSessionData.OnPlayerLeft -= PlayerLeft;
            
            _avatarStates.OnPlayerPositionChanged -= ChangePlayerPosition;
            _avatarStates.OnPlayerHpChanged -= ChangePlayerHp;
            _avatarStates.OnPlayerExpChanged -= ChangePlayerExp;
            _avatarStates.OnPlayerUpgradeReady -= PlayerUpgradeReady;
        }
        
        public void RecheckOtherPlayers()
        {
            foreach (var kvp in _roomSessionData.PlayerInstances)
            {
                if (!_otherPlayers.Contains(kvp.Value) && kvp.Value != _owner)
                {
                    _otherPlayers.Add(kvp.Value);
                }
            }
            OnRecheckPlayers?.Invoke(_otherPlayers);
        }
        
        private void PlayerJoined(PlayerRef playerRef)
        {
            Debug.Log("JOINED " + _roomSessionData.PlayerInstances[playerRef]);
            
            if (playerRef != _owner.Owner)
            {
                _otherPlayers.Add(_roomSessionData.PlayerInstances[playerRef]);
                OnPlayerJoined?.Invoke(playerRef);
            }

            RecheckOtherPlayers();
        }

        private void PlayerLeft(PlayerRef playerRef)
        {
            Debug.Log("MODEL Player Left");
            if (playerRef != _owner.Owner)
            {
                _otherPlayers.Remove(_roomSessionData.PlayerInstances[playerRef]);
                OnPlayerLeft?.Invoke(playerRef);
            }

            RecheckOtherPlayers();
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

        private void PlayerUpgradeReady(PlayerAvatarUpgradeManager upgradeManager)
        {
            Debug.Log("PlayerUpgradeReady");
            _playerAvatarUpgradeManager = upgradeManager;
            OnPlayerLevelUpgrade?.Invoke(_playerAvatarUpgradeManager);
        }
    }
}
