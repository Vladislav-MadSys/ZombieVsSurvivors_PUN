using _Project.Scripts.GameEntities.PlayerAvatar;
using Fusion;
using UnityEngine;

public class PlayerAvatarLevelController : NetworkBehaviour
{
    private const float EXP_MULTIPLER = 1.5f;

    [SerializeField] private int ExpToNextLevel = 100;

    private PlayerAvatarStates _states;
    [Networked] private int CurrentExp { get; set; } = 0;
    public int CurrentLevel { get; private set; } = 1;

    public void Initialize(PlayerAvatarStates states)
    {
        _states = states;
    }
    
    public void AddExp(int amount)
    {
        CurrentExp += amount;
        if (CurrentExp >= ExpToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        CurrentLevel++;
        CurrentExp = CurrentLevel == ExpToNextLevel ? 0 : CurrentLevel - ExpToNextLevel;
        ExpToNextLevel = (int)(ExpToNextLevel * EXP_MULTIPLER);
    }
}
