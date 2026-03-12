using System;
using _Project.Scripts.Low.Input;
using Fusion;
using UnityEngine;

namespace _Project.Scripts.GameEntities.PlayerAvatar
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerAvatarMovementController : NetworkBehaviour
    {
        private const float SPEED_UPGRADE_COEFFICIENT = 1.3f;
        [SerializeField] private float Speed;

        private Transform _transform;
        private Rigidbody2D _rb;
        private InputHandler _inputHandler;
        private PlayerAvatarStates _states;

        private void Awake()
        {
            _transform = transform;
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Initialize(InputHandler inputHandler, PlayerAvatarStates states)
        {
            _inputHandler = inputHandler;
            _states = states;
        }

        public void InitializeOnlyStates(PlayerAvatarStates states)
        {
            _states = states;
        }

        public void UpgradeSpeed()
        {
            Speed *= SPEED_UPGRADE_COEFFICIENT;
        }

        private void FixedUpdate()
        {
            _states.ChangePlayerPosition(_transform.position);
            if (_rb != null && _inputHandler != null)
            {
                _rb.MovePosition(_rb.position + (_inputHandler.MoveInput * Speed * Time.fixedDeltaTime));
            }
            else
            {
                Debug.Log("No Rigidbody or Input Handler!");
            }
        }

    }
}
