using System;
using Fusion;
using UnityEngine;

public class Shovel : NetworkBehaviour
{
    [SerializeField] private Vector3 Speed = new Vector3(0, 0, 10);
    
    public void Update()
    {
        if (!Object.HasStateAuthority) return;
        
        transform.Rotate(Speed * Time.deltaTime);    
    }
}
