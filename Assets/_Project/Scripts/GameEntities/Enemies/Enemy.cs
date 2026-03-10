using System;
using _Project.Scripts.GameEntities.PlayerAvatar;
using _Project.Scripts.Session;
using Fusion;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.GameEntities.Enemies
{
    public class Enemy : NetworkBehaviour
    {
        public event Action OnEnemyDead;

        [SerializeField] private NetworkObject[] loot;
        [SerializeField] private float dropChance = 50;

        private RoomSessionData _roomData;
    
        [Networked]
        public PlayerRef Target { get; set; }
        public void Initialize(RoomSessionData roomData)
        {
            _roomData = roomData;
        
            UpdateTarget();
        }

        public Transform GetTarget()
        {
            if (Target != PlayerRef.None && _roomData.IsRoomActive && _roomData.PlayerInstances.ContainsKey(Target))
            {
                if (_roomData.PlayerInstances[Target] != null && _roomData.PlayerInstances[Target].PlayerAvatar != null)
                {
                    return _roomData.PlayerInstances[Target].PlayerAvatar.transform;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                UpdateTarget();
                return null;
            }
        }

        public void Kill()
        {
            OnEnemyDead?.Invoke();
            if (Random.Range(0f, 1f) <= dropChance / 100)
            {
                NetworkObject lootToSpawn = loot[Random.Range(0, loot.Length)];
                NetworkObject spawnedLoot = Runner.Spawn(lootToSpawn, transform.position, transform.rotation);
                spawnedLoot.transform.parent = null;
            }
        }
        
        private void UpdateTarget()
        {
            if (_roomData == null)
            {
                Debug.LogError("_roomData == null");
                return;
            }
        
            PlayerRef closestPlayer = default;

            foreach (var keyValuePair in _roomData.PlayerInstances)
            {
                if (closestPlayer == PlayerRef.None && _roomData.PlayerInstances.Count > 0)
                {
                    if (keyValuePair.Value.PlayerAvatar != null)
                    {
                        closestPlayer = keyValuePair.Value.Owner;
                    }
                }
                else if(_roomData.PlayerInstances[closestPlayer].PlayerAvatar != null)
                {
                    Vector3 myPosition = transform.position;
                    Vector3 avatar1 = _roomData.PlayerInstances[closestPlayer].PlayerAvatar.transform.position;
                    Vector3 avatar2 = keyValuePair.Value.PlayerAvatar.transform.position;

                    float distance1 = (myPosition - avatar1).sqrMagnitude;
                    float distance2 = (myPosition - avatar2).sqrMagnitude;

                    if (distance1 > distance2)
                    {
                        closestPlayer = keyValuePair.Value.Owner;
                    }
                }
            }
        
            Target = closestPlayer;
        }
    }
}
