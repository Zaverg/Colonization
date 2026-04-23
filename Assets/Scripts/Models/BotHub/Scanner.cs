using System;
using UnityEngine;

public class Scanner
{
    private Vector3 _center;
    private float _scaneRadius;
    private LayerMask _layr;

    private Collider[] _collidersBuffer = new Collider[5];

    public event Action<IResource> Detected;

    public Scanner(Vector3 center,LayerMask layr, float radius = 0)
    {
        _center = center;
        _layr = layr;
        _scaneRadius = radius;
    }

    public void Scan() 
    {
        if (Physics.OverlapSphereNonAlloc(_center, _scaneRadius, _collidersBuffer, _layr) == 0)
            return;

        for (int i = 0; i < _collidersBuffer.Length; i++)
        {
            if (_collidersBuffer[i] == null) 
                continue;

            if (_collidersBuffer[i].TryGetComponent(out IResource resource))
                Detected?.Invoke(resource);
        }
    }
}
