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
        public const string GAMEPLAY_SCENE_NAME = "Gameplay";
        public const string MAIN_MENU_SCENE_NAME = "MainMenu";
        
        public override void OnConnectedToServer(NetworkRunner runner)
        {
            if (runner.IsSceneAuthority)
            {
                runner.LoadScene(GAMEPLAY_SCENE_NAME);
            }
        }
    }
}
