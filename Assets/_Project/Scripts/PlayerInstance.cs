using Fusion;
using UnityEngine;

namespace _Project.Scripts
{
    public class PlayerInstance : NetworkBehaviour
    {
        [SerializeField] private GameObject playerAvatarPrefab;

        private void Start()
        {
            if (Object.HasInputAuthority)
            {
                Runner.Spawn(playerAvatarPrefab);
            }
        }
    }
}
