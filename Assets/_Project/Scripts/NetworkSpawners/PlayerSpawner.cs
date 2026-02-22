using System;
using _Project.Scripts.Installers;
using _Project.Scripts.Low.Input;
using _Project.Scripts.Session;
using Fusion;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.NetworkSpawners
{
    public class PlayerSpawner : SimulationBehaviour, IPlayerJoined, ISceneLoadDone
    {
        [SerializeField] private GameObject PlayerPrefab;

        private PlayerRef _player;
        private InputHandler _inputHandler;

        private void Start()
        {
            _inputHandler = ProjectContextInstaller.DiContainer.Resolve<InputHandler>();
        }

        public void PlayerJoined(PlayerRef player)
        {
            
            if (player == Runner.LocalPlayer)
            {
                _player = player;
                if (!Runner.IsSceneAuthority)
                {
                    SpawnPlayer();
                }
            }
        }

        public void SceneLoadDone(in SceneLoadDoneArgs sceneInfo)
        {
            SpawnPlayer();
        }

        public void SpawnPlayer()
        {
            NetworkObject player = Runner.Spawn(PlayerPrefab, new Vector3(0, 1, 0), Quaternion.identity, _player);
            PlayerInstance playerInstance = player.GetComponent<PlayerInstance>();
            playerInstance.Initialize(_inputHandler, _player);
        }
    }
}