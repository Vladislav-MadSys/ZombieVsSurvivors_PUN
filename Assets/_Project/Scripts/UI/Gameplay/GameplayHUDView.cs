using System.Collections.Generic;
using _Project.Scripts.GameEntities.PlayerAvatar;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Gameplay
{
    public class GameplayHUDView : MonoBehaviour
    {
        [SerializeField] private GameObject otherPlayerDataPrefab;
        [SerializeField] private Image HpBar;
        [SerializeField] private Image ExpBar;

        [SerializeField] private GameObject UpgradePanel;
        [SerializeField] private Button UpgradeFireRateButton;
        [SerializeField] private Button UpgradeDamageButton;
        [SerializeField] private Button UpgradeHPButton;
        [SerializeField] private Button UpgradeMovementSpeedButton;
        
        private Dictionary<PlayerInstance, OtherPlayerData> _otherPlayersIndicators = new Dictionary<PlayerInstance, OtherPlayerData>();
        
        private UnityAction _onUpgradeFireRate;
        private UnityAction _onUpgradeDamage;
        private UnityAction _onUpgradeHp;
        private UnityAction _onUpgradeMovementSpeed;
        
        
        private GameplayHUDPresenter _presenter;
        private Camera _camera;
        
        public void Initialize(GameplayHUDPresenter presenter, Camera camera)
        {
            _presenter = presenter;  
            _camera = camera;
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

        public void OtherPlayerJoined(PlayerInstance player)
        {
            if (player == null || _otherPlayersIndicators.ContainsKey(player)) return;
            
            GameObject otherPlayerIndicator = Instantiate(otherPlayerDataPrefab);
            OtherPlayerData otherPlayerIndicatorScript = otherPlayerIndicator.GetComponent<OtherPlayerData>();
            otherPlayerIndicator.transform.SetParent(transform);
            _otherPlayersIndicators.Add(player, otherPlayerIndicatorScript);
            
            otherPlayerIndicatorScript.Initialize(player, _camera);
        }

        public void OtherPlayerLeft(PlayerInstance player)
        {
            Debug.Log("VIEW Player Left");
            if (player != null && _otherPlayersIndicators.ContainsKey(player) )
            {
                if (_otherPlayersIndicators[player] != null)
                {
                    OtherPlayerData otherPlayerIndicatorScript = _otherPlayersIndicators[player];
                    Destroy(otherPlayerIndicatorScript.gameObject);
                }
                _otherPlayersIndicators.Remove(player);
            }
            else
            {
                RemoveDestroyedKeys();
            }
        }

        public void RemoveDestroyedKeys()
        {
            List<PlayerInstance> keysToDestroy = new List<PlayerInstance>();

            foreach (PlayerInstance player in _otherPlayersIndicators.Keys)
            {
                if (player == null)
                {
                    keysToDestroy.Add(player);
                }
            }

            foreach (PlayerInstance key in keysToDestroy)
            {
                OtherPlayerData otherPlayerIndicatorScript = _otherPlayersIndicators[key];
                Destroy(otherPlayerIndicatorScript.gameObject);
                _otherPlayersIndicators.Remove(key);
            }
        }
    }
}
