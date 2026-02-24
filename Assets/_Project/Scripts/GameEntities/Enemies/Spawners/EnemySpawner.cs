using _Project.Scripts.Session;
using Cysharp.Threading.Tasks;
using Fusion;
using UnityEngine;

namespace _Project.Scripts.GameEntities.Enemies.Spawners
{
    public class EnemySpawner : NetworkBehaviour
    {
        private const float MIN_DISTANCE_FROM_PLAYER = 30;
        
        [SerializeField] private NetworkObject EnemyPrefab;
        [SerializeField] private float SpawnInterval = 1;
        
        private RoomSessionData _roomSessionData;

        [Networked] private float Timer { get; set; } = 0;

        public async override void Spawned()
        {
            base.Spawned();
            
            await UniTask.WaitUntil(() => GameSceneContainer.Instance != null);
            
            _roomSessionData = GameSceneContainer.Instance.RoomSessionData;
        }

        public override void FixedUpdateNetwork()
        {
            if (!Runner.IsSharedModeMasterClient)
                return;
            
            if (Timer >= SpawnInterval)
            {
                Spawn();
                Timer = 0;
            }
            else
            {
                Timer += Time.fixedDeltaTime;
            }
        }

        private void Spawn()
        {
            if (!Runner.IsSharedModeMasterClient)
                return;

            int directionHorizontal = Random.value > 0.5f ? 1 : -1;
            int directionVertical = Random.value > 0.5f ? 1 : -1;
            Vector2 farestPlayer = GetFarestPlayerCoordinates();
            Vector2 spawnPoint = new Vector2(
                (farestPlayer.x + MIN_DISTANCE_FROM_PLAYER) * directionHorizontal, 
                (farestPlayer.y + MIN_DISTANCE_FROM_PLAYER) * directionVertical
                );
            
            
            NetworkObject zombie = Runner.Spawn(EnemyPrefab, spawnPoint, Quaternion.identity);
            zombie.GetComponent<Enemy>().Initialize(_roomSessionData);
        }

        private Vector2 GetFarestPlayerCoordinates()
        {
            Vector3 basicPoint = Vector2.zero;
            PlayerInstance farestPlayer = null;
            foreach (var keyValuePair in _roomSessionData.PlayerInstances)
            {
                if (_roomSessionData.PlayerInstances[keyValuePair.Key].PlayerAvatar != null)
                {
                    if (farestPlayer != null)
                    {
                        if (Vector3.SqrMagnitude(basicPoint - farestPlayer.PlayerAvatar.transform.position)
                            > Vector3.SqrMagnitude(basicPoint - _roomSessionData.PlayerInstances[keyValuePair.Key].PlayerAvatar.transform.position))
                        {
                            farestPlayer = _roomSessionData.PlayerInstances[keyValuePair.Key];
                        }
                    }
                    else
                    {
                        farestPlayer = _roomSessionData.PlayerInstances[keyValuePair.Key];
                    }
                }
            }

            if (farestPlayer != null)
            {
                return new Vector2(farestPlayer.PlayerAvatar.transform.position.x, farestPlayer.PlayerAvatar.transform.position.y);
            }
            else
            {
                return basicPoint;
            }
        }
    }
}
