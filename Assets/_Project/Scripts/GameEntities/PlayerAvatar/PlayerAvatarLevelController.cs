using System;
using _Project.Scripts.GameEntities.PlayerAvatar;
using Fusion;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAvatarLevelController : NetworkBehaviour
{
    private const float EXP_UPGRADE_COEFFICIENT = 1.5f;

    [SerializeField] private int expToNextLevel = 100;
    [SerializeField] private PlayerAvatarUpgradeManager upgradeManager;

    private PlayerAvatarStates _states;
    [Networked] private int CurrentExp { get; set; } = 0;
    private int _availableUpgrades = 0;
    private int baseExp;
    public int CurrentLevel { get; private set; } = 1;

    public void Initialize(PlayerAvatarStates states)
    {
        _states = states;
        
        baseExp = expToNextLevel;
        _states.OnPlayerUpgraded += OnUpgraded;
    }

    public void OnDestroy()
    {
        _states.OnPlayerUpgraded -= OnUpgraded;
    }
    
    public void AddExp(int amount)
    {
        CurrentExp += amount;
        if (_states != null)
        {
            _states.ChangePlayerExp(CurrentExp, expToNextLevel);
        }

        if (CurrentExp >= expToNextLevel)
        {
            
            LevelUp();
        }
        Debug.Log(CurrentLevel + " | " + CurrentExp + " / " + expToNextLevel);
    }

    private void LevelUp()
    {
        while (CurrentExp >= baseExp * Math.Pow(EXP_UPGRADE_COEFFICIENT, CurrentLevel-1))
        {
            CurrentLevel++;
            _availableUpgrades++;
            CurrentExp -= expToNextLevel;
            expToNextLevel = (int)(baseExp * Math.Pow(EXP_UPGRADE_COEFFICIENT, CurrentLevel-1));
        }
        
        if (_states != null)
        {
            _states.ChangePlayerExp(CurrentExp, expToNextLevel);
            _states.PlayerLevelUp();
        }
    }
    
    private void OnUpgraded()
    {
        _availableUpgrades--;
        if (_availableUpgrades > 0)
        {
            upgradeManager.StartUpgrade();
        }
    }
}
