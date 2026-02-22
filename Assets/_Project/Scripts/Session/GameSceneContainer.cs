using System;
using _Project.Scripts.Session;
using Fusion;
using UnityEngine;

public class GameSceneContainer : NetworkBehaviour
{
    public static GameSceneContainer Instance { get; private set; }

    [field: SerializeField] public RoomSessionData RoomSessionData { get; private set; }

    private void Awake()
    {
        GameSceneContainer.Instance = this;
    }
}
