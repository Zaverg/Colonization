using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(NavMeshAgent), typeof(CollectorBotAnimator))]
public class CollectorBot : MonoBehaviour, IStateMachine
{
    [SerializeField] private Mover _mover;
    [SerializeField] private Taker _taker;
    [SerializeField] private Miner _miner;
    [SerializeField] private Unloader _unloader;

    private Queue<CollectorBotTask> _tasks = new Queue<CollectorBotTask>();
    private Dictionary<StateType, CollectorState> _states = new Dictionary<StateType, CollectorState>();

    private CollectorBotAnimator _animationController;

    private CollectorBotTask _currentTask;
    private CollectorState _currentState;

    public event Action<CollectorBot> OnBotAvailable;

    public bool HasTask => _tasks.Count > 0;
    public IMover Mover => _mover;
    public ITaker Taker => _taker;
    public IMiner Miner => _miner;
    public IUnloader Dropper => _unloader;
    public Transform Transform => transform;
    public CollectorBotTask CurrentTask => _currentTask;
    public CollectorBotAnimator AnimationController => _animationController;

    public void Awake()
    {
        _states[StateType.Idle] = new IdleState();
        _states[StateType.Moving] = new MovingState();
        _states[StateType.Taking] = new TakingState();
        _states[StateType.Dropping] = new UnloaderState();
        _states[StateType.Mining] = new MiningState();

        _currentState = _states[StateType.Idle];

        _animationController = GetComponent<CollectorBotAnimator>();

        _currentState.Entry(this);
    }

    private void Update()
    {
        if (HasTask == false && _currentState != _states[StateType.Idle])
        {
            _currentState = _states[StateType.Idle];
            OnBotAvailable?.Invoke(this);
        }

        _currentState.Run();       
    }

    public void AssignTasks(Queue<CollectorBotTask> tasks)
    {
        _tasks = new Queue<CollectorBotTask>(tasks);

        SwitchToState();
    }

    private void SwitchToState()
    {
        _currentTask = _tasks.Dequeue();
        CollectorState state = _states[_currentTask.StateType];

        _currentState.Completed -= SwitchToState;
        _currentState.Exit();

        _currentState = state;
        _currentState.Completed += SwitchToState;
        _currentState.Entry(this);
    }
}