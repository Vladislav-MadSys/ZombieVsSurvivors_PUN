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
        
        private Rigidbody2D _rb;
        private InputHandler _inputHandler;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Initialize(InputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }

        public void UpgradeSpeed()
        {
            Speed *= SPEED_UPGRADE_COEFFICIENT;
        }
        
        public override void FixedUpdateNetwork()
        {
            if (_rb != null && _inputHandler != null)
            {
                _rb.MovePosition(_rb.position + (_inputHandler.MoveInput * Speed * Time.fixedDeltaTime));
            }
            else
            {
                Debug.LogError("No Rigidbody or Input Handler!");
            }
        }
    }
}
