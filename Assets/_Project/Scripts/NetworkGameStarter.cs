using System;
using _Project.Scripts.Low;
using _Project.Scripts.Low.ScenesController;
using _Project.Scripts.Utils;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace _Project.Scripts
{
    public class NetworkGameStarter : BasicNetworkRunnerCallbacks
    {
        public override void OnConnectedToServer(NetworkRunner runner)
        {
            if (runner.IsSceneAuthority)
            {
                runner.LoadScene("Gameplay");
            }
        }

        public override void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
        {
            runner.LoadScene("MainMenu");
        }
    }
}
