using Fusion;
using UnityEngine;

public class NetworkedOnlyObjects : NetworkBehaviour
{
    [SerializeField] private GameObject[] ObjectsToHideForOthers;

    public override void Spawned()
    {
        bool isLocal = Object.HasStateAuthority;

        if (isLocal)
        {
            foreach (GameObject obj in ObjectsToHideForOthers)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }
        }
    }
}
