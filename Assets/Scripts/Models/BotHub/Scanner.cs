using System;
using UnityEngine;

public class Scanner : MonoBehaviour 
{
    [SerializeField] private float _scaneInterval;
    [SerializeField] private float _scaneRadius;
    [SerializeField] private LayerMask _layr;

    private Timer _timer;

    private Collider[] _collidersBuffer = new Collider[5];

    public event Action<IResource> Detected;

    public Timer Timer => _timer;

    public void OnDisable()
    {
        if (_timer == null)
            return;

        _timer.Ended -= Scan;
    }

    public void Initialize(Timer timer)
    {
        _timer = timer;
        _timer.SetDuration(_scaneInterval);
        _timer.Ended += Scan;

        _timer.Run();
    }

    public void Scan() 
    {
        if (Physics.OverlapSphereNonAlloc(transform.position, _scaneRadius, _collidersBuffer, _layr) == 0)
            return;

        for (int i = 0; i < _collidersBuffer.Length; i++)
        {
            if (_collidersBuffer[i] == null) 
                continue;

            if (_collidersBuffer[i].TryGetComponent(out IResource resource))
                Detected?.Invoke(resource);
        }

        _timer.Run();
    }
}
