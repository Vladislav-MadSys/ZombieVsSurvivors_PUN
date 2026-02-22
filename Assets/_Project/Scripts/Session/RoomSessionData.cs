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


        [Rpc(RpcSources.All, RpcTargets.All)]
        public void RPC_PlayerJoin(PlayerRef playerRef, PlayerInstance playerInstance)
        {
            if (!PlayerInstances.ContainsKey(playerRef))
            {
                PlayerInstances.Add(playerRef, playerInstance);
            }
        }

        public void PlayerLeft(PlayerRef player)
        {
            RPC_PlayerLeave(player);
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        public void RPC_PlayerLeave(PlayerRef playerRef)
        {
            if (PlayerInstances.ContainsKey(playerRef))
            {
                PlayerInstances.Remove(playerRef);
            }
        }
    }
}
