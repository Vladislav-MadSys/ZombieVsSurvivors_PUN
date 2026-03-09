using _Project.Scripts.GameEntities.Items;
using _Project.Scripts.GameEntities.PlayerAvatar;
using Fusion;
using UnityEngine;

public class ExpItem : NetworkBehaviour, IPickable
{
    [SerializeField] public int amount;

    [Networked] private bool IsUsed { get; set; } = false;

    public void PickUp(PlayerAvatar picker)
    {
        if (IsUsed) return;
        
        picker.AddExp(amount);
        IsUsed = true;
        Runner.Despawn(GetComponent<NetworkObject>());
    }
}
