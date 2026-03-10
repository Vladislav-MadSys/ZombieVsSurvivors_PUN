using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Gameplay
{
    public class GameplayHUDView : MonoBehaviour
    {
        [SerializeField] private Image HpBar;
        [SerializeField] private Image ExpBar;

        [SerializeField] private GameObject UpgradePanel;
        [SerializeField] private Button UpgradeFireRateButton;
        [SerializeField] private Button UpgradeDamageButton;
        [SerializeField] private Button UpgradeHPButton;
        [SerializeField] private Button UpgradeMovementSpeedButton;
        
        private UnityAction _onUpgradeFireRate;
        private UnityAction _onUpgradeDamage;
        private UnityAction _onUpgradeHp;
        private UnityAction _onUpgradeMovementSpeed;
        
        
        private GameplayHUDPresenter _presenter;
        
        public void Initialize(GameplayHUDPresenter presenter)
        {
            _presenter = presenter;    
        }

        public void Run()
        {
            _onUpgradeFireRate = () =>
            {
                _presenter.UpgradePlayer(PlayerUpgradesKeys.FIRE_RATE_UPGRADE_KEY);
            };
            UpgradeFireRateButton.onClick.AddListener(_onUpgradeFireRate);
            
            _onUpgradeDamage = () =>
            {
                _presenter.UpgradePlayer(PlayerUpgradesKeys.DAMAGE_UPGRADE_KEY);
            };
            UpgradeDamageButton.onClick.AddListener(_onUpgradeDamage);
            
            _onUpgradeHp = () =>
            {
                _presenter.UpgradePlayer(PlayerUpgradesKeys.HP_UPGRADE_KEY);
            };
            UpgradeHPButton.onClick.AddListener(_onUpgradeHp);
            
            _onUpgradeMovementSpeed = () =>
            {
                _presenter.UpgradePlayer(PlayerUpgradesKeys.MOVEMENT_SPEED_UPGRADE_KEY);
            };
            UpgradeMovementSpeedButton.onClick.AddListener(_onUpgradeMovementSpeed);
        }
        
        public void ChangeHpBar(float percentage)
        {
            HpBar.fillAmount = percentage;
        }
        
        public void ChangeExpBar(float percentage)
        {
            ExpBar.fillAmount = percentage;
        }

        public void ShowUpgradePanel()
        {
            UpgradePanel.SetActive(true);
        }

        public void HideUpgradePanel()
        {
            UpgradePanel.SetActive(false);
        }
    }
}
