using Fusion;
using UnityEngine;

namespace _Project.Scripts.NetworkSpawners
{
    public class PlayerSpawner : SimulationBehaviour, IPlayerJoined, ISceneLoadDone
    {
        [SerializeField] private GameObject PlayerPrefab;

        private PlayerRef _player;

        public void PlayerJoined(PlayerRef player)
        {
            if (player == Runner.LocalPlayer)
            {
                _player = player;
            }
        }

        public void SceneLoadDone(in SceneLoadDoneArgs sceneInfo)
        {
            Runner.Spawn(PlayerPrefab, new Vector3(0, 1, 0), Quaternion.identity, _player);
        }
    }
}