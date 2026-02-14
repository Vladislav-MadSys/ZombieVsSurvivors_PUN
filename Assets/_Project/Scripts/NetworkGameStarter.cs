using System;
using _Project.Scripts.Low;
using _Project.Scripts.Low.ScenesController;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace _Project.Scripts
{
    public class NetworkGameStarter : BasicNetworkRunnerCallbacks
    {
        //private IScenesController _scenesController;
    
        [Inject]
        private void Inject(NetworkRunner runner, IScenesController scenesController)
        {
            _runner = runner;
            //_scenesController = scenesController;
        }


        public override void OnConnectedToServer(NetworkRunner runner)
        {
            Debug.Log("CONNECTED");

            runner.LoadScene("Gameplay");
        }

        public override void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
        {
            runner.LoadScene("MainMenu");
            //_scenesController.LoadMainMenu();
        }
    }
}
