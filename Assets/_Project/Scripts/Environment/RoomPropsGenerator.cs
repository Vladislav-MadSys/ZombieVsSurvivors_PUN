using Fusion;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Environment
{
    public class RoomPropsGenerator : NetworkBehaviour
    {
        [SerializeField] private NetworkObject[] roomPropsPrefabs;
        [SerializeField] private int areaToGenerate = 1000;
        [Range(0, 100)]
        [SerializeField] private int density = 10;

        private int _propsCount;
        private bool _isPropsSpawned = false;

        public override void Spawned()
        {
            if (!Object.HasStateAuthority || _isPropsSpawned) return;
        
            _propsCount = areaToGenerate * density;

            for (int i = 0; i < _propsCount; i++)
            {
                Vector3 randomPosition = new Vector3(
                    Random.Range(-areaToGenerate / 2, areaToGenerate / 2),
                    Random.Range(-areaToGenerate / 2, areaToGenerate / 2), 
                    0);
                Runner.Spawn(roomPropsPrefabs[Random.Range(0, roomPropsPrefabs.Length)], randomPosition, Quaternion.identity);
            }
            _isPropsSpawned = true;
        }
    }
}
