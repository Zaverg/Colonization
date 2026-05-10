using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Mover : MonoBehaviour
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

    public void SetTarget(Vector3 target)
    {
        _targetPosition = target;
        _agent.SetDestination(_targetPosition);
    }

    public void Move()
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

    public bool HasReachedTarget()
    {
        if (_agent == null)
            return false;

        if (_agent.pathPending)
            return false;

        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            _agent.ResetPath();
            return true;
        }

        return false;
    }

    public void Stop()
    {
        _agent.ResetPath();
    }
}