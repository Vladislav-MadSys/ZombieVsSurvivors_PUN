using _Project.Scripts.GameEntities.PlayerAvatar;
using _Project.Scripts.GameEntities.PlayerAvatar.Weapon;
using Fusion;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAvatarUpgradeManager : NetworkBehaviour
{
    [SerializeField] private MachineGun machineGun;
    [SerializeField] private PlayerAvatarHP playerAvatarHp;
    [SerializeField] private PlayerAvatarMovementController playerAvatarMovementController;
    
    private PlayerAvatarStates _states;

    public void Initialize(PlayerAvatarStates states)
    {
        _states = states;

        _states.OnPlayerLevelUp += StartUpgrade;
    }

    public void OnDestroy()
    {
        _states.OnPlayerLevelUp -= StartUpgrade;
    }
    
    public void StartUpgrade()
    {
        _states.SetReadyPlayerUpgrade(this); 
    }

    public void UpgradeFireRate()
    {
        machineGun.UpgradeFireRate();
    }

    public void UpgradeDamage()
    {
        machineGun.UpgradeDamage();
    }

    public void UpgradeHp()
    {
        playerAvatarHp.RPC_UpgradeMaxHp();
    }
    
    public void UpgradeSpeed()
    {
        playerAvatarMovementController.UpgradeSpeed();
    }
}
