using System;
using _Project.Scripts.Session;
using Fusion;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameEntities.Enemies.Spawners
{
    public class EnemySpawner : NetworkBehaviour, IInitializable, IDisposable
    {
        [SerializeField] private NetworkObject EnemyPrefab;

        [SerializeField] private RoomSessionData _roomSessionData;
        
        [Inject]
        public void Inject(RoomSessionData roomSessionData)
        {
            //_roomSessionData = roomSessionData;
        }

        public override void Spawned()
        {
            base.Spawned();
            NetworkObject zombie = Runner.Spawn(EnemyPrefab);
            zombie.GetComponent<Enemy>().Initialize(_roomSessionData);
        }

        public void Initialize()
        {
            
        }

        public void Dispose()
        {
        
        }
    }
}
