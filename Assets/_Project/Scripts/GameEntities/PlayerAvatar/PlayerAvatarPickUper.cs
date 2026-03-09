using System;
using _Project.Scripts.GameEntities.Items;
using _Project.Scripts.GameEntities.PlayerAvatar;
using Fusion;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerAvatar))]
public class PlayerAvatarPickUper : NetworkBehaviour
{
    private PlayerAvatar _playerAvatar;
    private bool _isSpawned = false;

    public override void Spawned()
    {
        _playerAvatar = GetComponent<PlayerAvatar>();
        _isSpawned = true;
    }

    public void OnDestroy()
    {
        _isSpawned = false;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!_isSpawned) return;
        
        if (other.TryGetComponent(out IPickable pickable))
        {
            pickable.RPC_PickUp(_playerAvatar);
        }
    }
}
