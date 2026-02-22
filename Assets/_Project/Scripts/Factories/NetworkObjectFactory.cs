using Fusion;
using UnityEngine;

namespace _Project.Scripts.Factories
{
    public class NetworkObjectFactory : IFactory<NetworkObject>
    {
        private readonly NetworkRunner _runner;
        private readonly NetworkObject _prefab;

        public NetworkObjectFactory(NetworkObject prefab, NetworkRunner runner)
        {
            _prefab = prefab;
            _runner = runner;
        }
    
        public NetworkObject Create()
        {
            return _runner.Spawn(_prefab);
        }
    
        public NetworkObject Create(Vector3 position, Quaternion rotation)
        {
            return _runner.Spawn(_prefab, position, rotation);
        }
    }
}
