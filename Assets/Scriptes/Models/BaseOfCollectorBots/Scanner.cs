using System;
using UnityEngine;

public class Scanner
{
    private Vector3 _center;
    private float _scaneRadius;
    private LayerMask _layr;

    private Collider[] collidersBuffer = new Collider[5];

    public event Action<IResource> Detected;

    public Scanner(Vector3 center,LayerMask layer, float radius = 0)
    {
        _center = center;
        _layr = layer;
        _scaneRadius = radius;
    }

    public void Scan() 
    {
        if (Physics.OverlapSphereNonAlloc(_center, _scaneRadius, collidersBuffer, _layr) == 0)
            return;

        for (int i = 0; i < collidersBuffer.Length; i++)
        {
            if (collidersBuffer[i] == null) 
                continue;

            if (collidersBuffer[i].TryGetComponent(out IResource colectable))
                Detected.Invoke(colectable);
        }
    }
}
