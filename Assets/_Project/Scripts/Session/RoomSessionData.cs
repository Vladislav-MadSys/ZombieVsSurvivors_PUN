using System;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

namespace _Project.Scripts.Session
{
    public class RoomSessionData : NetworkBehaviour, IPlayerLeft
    {   
        [Networked]
        public NetworkDictionary<PlayerRef, PlayerInstance> PlayerInstances { get; } = new NetworkDictionary<PlayerRef, PlayerInstance>();


        [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
        public void RPC_PlayerJoin(PlayerRef playerRef, PlayerInstance playerInstance)
        {
            Debug.Log(playerRef + " / " + playerInstance);
            if (!PlayerInstances.ContainsKey(playerRef) && playerRef != PlayerRef.None && playerInstance != null)
            {
                PlayerInstances.Add(playerRef, playerInstance);
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
        }
    }
}
