using System;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

namespace _Project.Scripts.Session
{
    public class RoomSessionData : NetworkBehaviour, IPlayerLeft
    {
        public event Action<PlayerRef> OnPlayerJoined;
        public event Action<PlayerRef> OnPlayerLeft;
        
        [Networked]
        public NetworkDictionary<PlayerRef, PlayerInstance> PlayerInstances { get; } = new NetworkDictionary<PlayerRef, PlayerInstance>();
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
        }

        public void PlayerLeft(PlayerRef player)
        {
            RPC_PlayerLeave(player);
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        private void RPC_PlayerLeave(PlayerRef playerRef)
        {
            PlayerInstances.Remove(playerRef);
            OnPlayerLeft?.Invoke(playerRef);
            if (PlayerInstances.Count == 0)
            {
                IsRoomActive = false;
            }
        }
    }
}
