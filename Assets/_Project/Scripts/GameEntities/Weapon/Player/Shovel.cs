using System;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class Shovel : NetworkBehaviour
{
    private const float RADIUS = 2;
    
    [SerializeField] private GameObject BladePrefab;
    [SerializeField] private Vector3 Speed = new Vector3(0, 0, 10);
    
    private Transform _transform;
    private int _bladesTargetCount = 1;
    private List<NetworkObject> _blades = new  List<NetworkObject>();

    public override void Spawned()
    {
        _transform = transform;
        
        IncreaseBlades(2);
    }

    public void Update()
    {
        if (!Object.HasStateAuthority) return;
        
        transform.Rotate(Speed * Time.deltaTime);    
    }

    public void IncreaseBlades(int delta = 1)
    {
        if (!Object.HasStateAuthority) return;
        
        _bladesTargetCount += delta;
        UpdateBlades();
    }
    
    private void UpdateBlades()
    {
        
        for (int i = 0; i < _bladesTargetCount; i++)
        {
            float angle = (360f / _bladesTargetCount) * i;
            float rad = angle * Mathf.Deg2Rad;
            float xPos = RADIUS * Mathf.Cos(rad);
            float yPos = RADIUS * Mathf.Sin(rad);

            NetworkObject blade;
            if (_blades.Count > i && _blades[i] != null)
            {
                blade = _blades[i];
            }
            else
            {
                blade = Runner.Spawn(BladePrefab);
                _blades.Add(blade);
            }

            blade.transform.parent = _transform;
            blade.transform.localPosition = new Vector3(xPos, yPos, 0);
            blade.transform.localRotation = Quaternion.Euler(0, 0, angle - 90);
            
        }
    }
}
