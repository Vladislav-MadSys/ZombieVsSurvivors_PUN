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
        if (_states != null)
        {
            _states.OnPlayerLevelUp -= StartUpgrade;
        }
    }
    
    public void StartUpgrade()
    {
        _states.SetReadyPlayerUpgrade(this); 
    }

    public void UpgradeFireRate()
    {
        machineGun.UpgradeFireRate();
        _states.PlayerUpgraded();
    }

    public void UpgradeDamage()
    {
        machineGun.UpgradeDamage();
        _states.PlayerUpgraded();
    }

    public void UpgradeHp()
    {
        playerAvatarHp.RPC_UpgradeMaxHp();
        _states.PlayerUpgraded();
    }
    
    public void UpgradeSpeed()
    {
        playerAvatarMovementController.UpgradeSpeed();
        _states.PlayerUpgraded();
    }
}
