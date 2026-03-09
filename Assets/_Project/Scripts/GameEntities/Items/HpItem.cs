using _Project.Scripts.GameEntities.Items;
using _Project.Scripts.GameEntities.PlayerAvatar;
using Fusion;
using UnityEngine;

public class HpItem : NetworkBehaviour, IPickable
{
    [SerializeField] private float amount;
    
    [Networked] private bool IsUsed { get; set; } = false;

    public void PickUp(PlayerAvatar picker)
    {
        if (IsUsed) return;
        
        picker.AddHp(amount);
        IsUsed = true;
        Runner.Despawn(GetComponent<NetworkObject>());
    }
}
