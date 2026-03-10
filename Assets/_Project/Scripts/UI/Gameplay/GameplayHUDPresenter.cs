using UnityEngine;

namespace _Project.Scripts.UI.Gameplay
{
    public class GameplayHUDPresenter
    {
        private GameplayHUDModel _model;
        private GameplayHUDView _view;

        private PlayerAvatarUpgradeManager _upgradeManager;
        
        public GameplayHUDPresenter(GameplayHUDModel model, GameplayHUDView view)
        {
            _model = model;
            _view = view;
        }

        public void Run()
        {
            _model.OnPlayerHpChanged += OnPlayerHPChanged;
            _model.OnPlayerExpChanged += OnPlayerExpChanged;
            _model.OnPlayerLevelUpgrade += OnPlayerLevelUpgrade;
        }

        public void Dispose()
        {
            _model.OnPlayerHpChanged -= OnPlayerHPChanged;
            _model.OnPlayerExpChanged -= OnPlayerExpChanged;
            _model.OnPlayerLevelUpgrade -= OnPlayerLevelUpgrade;
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
            
            _view.HideUpgradePanel();
        }
    }
}
