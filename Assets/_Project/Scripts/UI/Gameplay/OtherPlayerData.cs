using System;
using _Project.Scripts;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OtherPlayerData : MonoBehaviour
{
    private PlayerInstance _owner;
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
            _target = playerInstance.PlayerAvatarObject.transform;
        }
        else
        {
            Debug.Log("Player Avatar is null");
        }
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

    public void UpdatePosition(Vector3 position)
    {
        positionText.text = "Position " + position;
    }

    public void UpdateHpBar(float currentHp, float maxHp)
    {
        hpBar.fillAmount = currentHp / maxHp;
    }
}
