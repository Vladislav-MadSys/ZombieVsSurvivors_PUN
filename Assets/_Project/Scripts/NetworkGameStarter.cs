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
        [Inject]
        private void Inject(NetworkRunner runner)
        {
            _runner = runner;
        }


        public override void OnConnectedToServer(NetworkRunner runner)
        {
            if (runner.IsSceneAuthority)
            {
                runner.LoadScene("Gameplay");
            }
        }

        public override void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
        {
            if (runner.IsSceneAuthority)
            {
                runner.LoadScene("MainMenu");
            }
        }
    }
}
