using _Project.Scripts;
using _Project.Scripts.GameEntities.PlayerAvatar;
using _Project.Scripts.Low.Input;
using Fusion;
using UnityEngine;

public class PlayerAvatar : NetworkBehaviour
{
    [SerializeField] private AvatarHP AvatarHP;
    [SerializeField] private AvatarMovementController AvatarMovementController;

    private PlayerInstance _playerInstance;
    private InputHandler _inputHandler;
    
    public void Initialize(PlayerInstance playerInstance, InputHandler inputHandler)
    {
        _playerInstance = playerInstance;
        _inputHandler = inputHandler;
        
        AvatarMovementController.Initialize(_inputHandler);
        AvatarHP.Initialize(_playerInstance);
    }
}
