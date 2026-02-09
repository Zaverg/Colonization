using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshMover : Mover
{
    [SerializeField] private float _intervalUpdatePath;
    [SerializeField] private float _stoppingDistance;

    private float _currentSeconds;
    private float _lastUpdateTime;

    private NavMeshAgent _agent;
    private Vector3 _targetPosition;

    public void Awake()
    {
        _agent = transform.GetComponent<NavMeshAgent>();
        _agent.stoppingDistance = _stoppingDistance;
    }

    public override void SetTarget(Vector3 target) 
    {
        if (target == null)
            return;

        _targetPosition = target;
        _agent.SetDestination(_targetPosition);
    }

    public override void Move() 
    {
        if (_agent == null)
            return;

        _currentSeconds += Time.deltaTime;

        if (_currentSeconds - _lastUpdateTime >= _intervalUpdatePath)
        {
            _agent.SetDestination(_targetPosition);
            _lastUpdateTime = _currentSeconds;
        }
    }

    public override bool HasReachedTarget()
    {
        bool isPlace = _agent.remainingDistance <= _agent.stoppingDistance;

        if (isPlace)
        {
            _agent.ResetPath();
        }

        return isPlace;
    }
}