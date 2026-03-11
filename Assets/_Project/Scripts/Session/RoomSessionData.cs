using System;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

namespace _Project.Scripts.Session
{
    public class RoomSessionData : NetworkBehaviour
    {
        public event Action<PlayerRef> OnPlayerJoined;
        public event Action<PlayerRef> OnPlayerLeft;
        
        [Networked] public NetworkDictionary<PlayerRef, PlayerInstance> PlayerInstances { get; } = new NetworkDictionary<PlayerRef, PlayerInstance>();
        [Networked] public bool IsRoomActive { get; private set; } = false;

        [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
        public void RPC_PlayerJoin(PlayerRef playerRef, PlayerInstance playerInstance)
        {
            if (!PlayerInstances.ContainsKey(playerRef) && playerRef != PlayerRef.None && playerInstance != null)
            {
                PlayerInstances.Add(playerRef, playerInstance);
                OnPlayerJoined?.Invoke(playerRef);
                IsRoomActive = true;
            }

            //RemoveDestroyedKeys();
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        public void RPC_PlayerLeave(PlayerRef playerRef)
        {
            OnPlayerLeft?.Invoke(playerRef);
            PlayerInstances.Remove(playerRef);
            if (PlayerInstances.Count == 0)
            {
                IsRoomActive = false;
            }

            //RemoveDestroyedKeys();
        }
        
        /*private void RemoveDestroyedKeys()
        {
            if(Object.StateAuthority != Runner.LocalPlayer) return;
            
            List<PlayerRef> keysToDestroy = new List<PlayerRef>();

            foreach (var kvp in PlayerInstances)
            {
                if (kvp.Key == default)
                {
                    keysToDestroy.Add(kvp.Key);
                }
            }

            foreach (var key in keysToDestroy)
            {
                PlayerInstances.Remove(key);
            }
        }*/
    }
}
