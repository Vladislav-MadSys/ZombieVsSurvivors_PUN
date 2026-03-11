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

        [Networked]
        private RoomSessionData _roomData { get; set; }
    
        [Networked]
        public PlayerRef Target { get; set; }
        public void Initialize(RoomSessionData roomData)
        {
            _roomData = roomData;
        
            UpdateTarget();
        }

        public Transform GetTarget()
        {
            if (Target != PlayerRef.None && (_roomData != null && _roomData.IsRoomActive && _roomData.PlayerInstances.ContainsKey(Target)))
            {
                if (_roomData.PlayerInstances[Target] != null && _roomData.PlayerInstances[Target].PlayerAvatarObject != null)
                {
                    return _roomData.PlayerInstances[Target].PlayerAvatarObject.transform;
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

            PlayerRef closestPlayer = PlayerRef.None;
            float bestDistanceSqr = float.MaxValue;
            Vector3 myPosition = transform.position;

            foreach (var kvp in _roomData.PlayerInstances)
            {
                if (kvp.Value?.PlayerAvatarObject == null)
                    continue;

                float distSqr = (myPosition - kvp.Value.PlayerAvatarObject.transform.position).sqrMagnitude;
                if (distSqr < bestDistanceSqr)
                {
                    bestDistanceSqr = distSqr;
                    closestPlayer = kvp.Key;
                }
            }

            Target = closestPlayer;
        }
    }
}
