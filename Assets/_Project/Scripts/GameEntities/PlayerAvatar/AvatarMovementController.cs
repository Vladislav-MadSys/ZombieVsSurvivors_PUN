using _Project.Scripts.Low.Input;
using Fusion;
using UnityEngine;

namespace _Project.Scripts.GameEntities.PlayerAvatar
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class AvatarMovementController : NetworkBehaviour
    {
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

        public override void FixedUpdateNetwork()
        {
            if (_rb != null)
            {
                _rb.MovePosition(_rb.position + (_inputHandler.MoveInput * Speed * Time.fixedDeltaTime));
            }
            else
            {
                Debug.LogError("No Rigidbody!");
            }
        }
    }
}
