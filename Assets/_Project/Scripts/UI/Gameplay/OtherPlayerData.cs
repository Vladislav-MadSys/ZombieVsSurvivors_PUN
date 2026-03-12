using System;
using _Project.Scripts;
using _Project.Scripts.GameEntities.PlayerAvatar;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OtherPlayerData : MonoBehaviour
{
    private PlayerInstance _owner;
    private PlayerAvatar _playerAvatar;
    private Transform _target;
    
    
    [SerializeField] private Image hpBar;
    [SerializeField] private TextMeshProUGUI positionText;
    [SerializeField] private TextMeshProUGUI shootsText;

    private Transform _transform;
    private Camera _camera;

    private void Awake()
    {
        _transform = transform;
    }

    public void Initialize(PlayerInstance playerInstance, Camera camera)
    {
        _owner = playerInstance;
        _camera = camera;
        if (playerInstance.PlayerAvatarObject != null)
        {
            _playerAvatar = playerInstance.PlayerAvatarObject.GetComponent<PlayerAvatar>();
            _target = _playerAvatar.transform;
            
            _playerAvatar.States.OnPlayerShoot += UpdateShoots;
            _playerAvatar.States.OnPlayerPositionChanged += UpdatePosition;
            _playerAvatar.States.OnPlayerHpChanged += UpdateHpBar;
            Debug.Log("STATES FOR OTHER PLAYER READY " + _playerAvatar.States.GetHashCode());
        }
        else
        {
            Debug.Log("Player Avatar is null");
        }
    }

    private void OnDestroy()
    {
        _playerAvatar.States.OnPlayerShoot -= UpdateShoots;
        _playerAvatar.States.OnPlayerPositionChanged -= UpdatePosition;
        _playerAvatar.States.OnPlayerHpChanged -= UpdateHpBar;
    }

    private void Update()
    {
        if (_target != null && _camera != null)
        {
            _transform.position = _camera.WorldToScreenPoint(_target.position);
        }
        else
        {
            Debug.Log("Target or Camera is null");
            if (_owner == null)
            {
                Destroy(gameObject);
            }
        }
    }

    public void UpdateShoots(int shoots)
    {
        shootsText.text = "Shoots " + shoots;
    }

    public void UpdatePosition(Vector2 position)
    {
        positionText.text = position.ToString();
    }

    public void UpdateHpBar(float currentHp, float maxHp)
    {
        hpBar.fillAmount = currentHp / maxHp;
    }
}
