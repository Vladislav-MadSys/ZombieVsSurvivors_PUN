using _Project.Scripts.Session;
using Fusion;
using UnityEngine;

namespace _Project.Scripts.GameEntities.Enemies
{
    public class Enemy : NetworkBehaviour
    {

        private RoomSessionData _roomData;
    
        [Networked]
        public PlayerRef Target { get; set; }
    
        public void Initialize(RoomSessionData roomData)
        {
            _roomData = roomData;
        
            UpdateTarget();
        }

        public void UpdateTarget()
        {
            if (_roomData == null)
            {
                Debug.LogError("_roomData == null");
                return;
            }
        
            PlayerRef closestPlayer = default;

            foreach (var keyValuePair in _roomData.PlayerInstances)
            {
                if (closestPlayer == PlayerRef.None && _roomData.PlayerInstances.Count > 0)
                {
                    if (keyValuePair.Value.PlayerAvatar != null)
                    {
                        closestPlayer = keyValuePair.Value.Owner;
                    }
                }
                else if(_roomData.PlayerInstances[closestPlayer].PlayerAvatar != null)
                {
                    Vector3 myPosition = transform.position;
                    Vector3 avatar1 = _roomData.PlayerInstances[closestPlayer].PlayerAvatar.transform.position;
                    Vector3 avatar2 = keyValuePair.Value.PlayerAvatar.transform.position;

                    float distance1 = (myPosition - avatar1).sqrMagnitude;
                    float distance2 = (myPosition - avatar2).sqrMagnitude;

                    if (distance1 > distance2)
                    {
                        closestPlayer = keyValuePair.Value.Owner;
                    }
                }
            }
        
            Target = closestPlayer;
        }

        public Transform GetTarget()
        {
            if (Target != PlayerRef.None && _roomData.PlayerInstances.Count > 0 && _roomData.PlayerInstances.ContainsKey(Target))
            {
                return _roomData.PlayerInstances[Target].PlayerAvatar.transform;
            }
            else
            {
                UpdateTarget();
                return null;
            }
        }
    }
}
