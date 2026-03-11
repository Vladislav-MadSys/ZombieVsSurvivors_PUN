using System.Collections.Generic;
using _Project.Scripts.GameEntities.PlayerAvatar;
using _Project.Scripts.Session;
using Fusion;
using UnityEngine;

namespace _Project.Scripts.UI.Gameplay
{
    public class GameplayHUDPresenter
    {
        private GameplayHUDModel _model;
        private GameplayHUDView _view;
        private RoomSessionData _roomSessionData;

        private PlayerAvatarUpgradeManager _upgradeManager;
        
        public GameplayHUDPresenter(GameplayHUDModel model, GameplayHUDView view, RoomSessionData  roomSessionData)
        {
            _model = model;
            _view = view;
            _roomSessionData = roomSessionData;
        }

        public void Run()
        {
            //_model.OnPlayerJoined += OnPlayerJoined;
            _model.OnRecheckPlayers += RecheckAllPlayers;
            _model.OnPlayerLeft   += OnPlayerLeft;
            _model.OnPlayerHpChanged += OnPlayerHPChanged;
            _model.OnPlayerExpChanged += OnPlayerExpChanged;
            _model.OnPlayerLevelUpgrade += OnPlayerLevelUpgrade;
        }

        public void Dispose()
        {
            //_model.OnPlayerJoined -= OnPlayerJoined;
            _model.OnRecheckPlayers -= RecheckAllPlayers;
            _model.OnPlayerLeft   -= OnPlayerLeft;
            _model.OnPlayerHpChanged -= OnPlayerHPChanged;
            _model.OnPlayerExpChanged -= OnPlayerExpChanged;
            _model.OnPlayerLevelUpgrade -= OnPlayerLevelUpgrade;
        }

        private void RecheckAllPlayers(List<PlayerInstance> players)
        {
            Debug.Log("RECHECK ALL PLAYERS! COUNT: " +  players.Count);
            foreach (var player in players)
            {
                _view.OtherPlayerJoined(player);
            }
        }
        
        private void OnPlayerJoined(PlayerRef player)
        {
            _view.OtherPlayerJoined(_roomSessionData.PlayerInstances[player]);
        }
        
        private void OnPlayerLeft(PlayerRef player)
        {
            Debug.Log("PRESENTER Player Left");
            _view.OtherPlayerLeft(_roomSessionData.PlayerInstances[player]);
        }
        
        private void OnPlayerHPChanged(float currentHp, float maxHp)
        {
            float percentage = currentHp / maxHp;
            _view.ChangeHpBar(percentage);
        }
        
        private void OnPlayerExpChanged(float avatarCurrentExp, float expToNextLevel)
        {
            float percentage = avatarCurrentExp / expToNextLevel;
            _view.ChangeExpBar(percentage);
        }

        private void OnPlayerLevelUpgrade(PlayerAvatarUpgradeManager upgradeManager)
        {
            _upgradeManager = upgradeManager;
            _view.ShowUpgradePanel();
        }

        public void UpgradePlayer(string key)
        {
            if(_upgradeManager == null) return;

            _view.HideUpgradePanel();
            switch (key)
            {
                case PlayerUpgradesKeys.DAMAGE_UPGRADE_KEY:
                    _upgradeManager.UpgradeDamage();
                    break;
                case PlayerUpgradesKeys.FIRE_RATE_UPGRADE_KEY:
                    _upgradeManager.UpgradeFireRate();
                    break;
                case PlayerUpgradesKeys.HP_UPGRADE_KEY:
                    _upgradeManager.UpgradeHp();
                    break;
                case PlayerUpgradesKeys.MOVEMENT_SPEED_UPGRADE_KEY:
                    _upgradeManager.UpgradeSpeed();
                    break;
            }
        }
    }
}
