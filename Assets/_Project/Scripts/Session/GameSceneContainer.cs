using Fusion;
using UnityEngine;

namespace _Project.Scripts.Session
{
    public class GameSceneContainer : NetworkBehaviour
    {
        public static GameSceneContainer Instance { get; private set; }

        [field: SerializeField] public RoomSessionData RoomSessionData { get; private set; }

        private void Awake()
        {
            GameSceneContainer.Instance = this;
        }
    }
}
