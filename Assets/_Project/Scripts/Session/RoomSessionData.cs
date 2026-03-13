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
        [Networked] public string RoomName { get; private set; }

        public override void Spawned()
        {
            RoomName = Runner.SessionInfo.Name;
        }

        [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
        public void RPC_PlayerJoin(PlayerRef playerRef, PlayerInstance playerInstance)
        {
            if (!PlayerInstances.ContainsKey(playerRef) && playerRef != PlayerRef.None && playerInstance != null)
            {
                PlayerInstances.Add(playerRef, playerInstance);
                OnPlayerJoined?.Invoke(playerRef);
                IsRoomActive = true;
            }
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
        }
    }
}
