using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(NavMeshAgent), typeof(CollectorBotAnimator))]
public class CollectorBot : MonoBehaviour, IStateMachine, ICreatable
{
    [SerializeField] private Mover _mover;
    [SerializeField] private Taker _taker;
    [SerializeField] private Miner _miner;
    [SerializeField] private Unloader _unloader;
    [SerializeField] private Builder _builder;

    private Queue<CollectorBotTask> _tasks = new Queue<CollectorBotTask>();
    private Dictionary<StateType, CollectorBotState> _states = new Dictionary<StateType, CollectorBotState>();

    private CollectorBotAnimator _animationController;

    private CollectorBotTask _currentTask;
    private CollectorBotState _currentState;

    public event Action<CollectorBot> OnBotAvailable;

    public IMover Mover => _mover;
    public ITaker Taker => _taker;
    public IMiner Miner => _miner;
    public IUnloader Unloader => _unloader;
    public IBuilder Builder => _builder;
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
        _states[StateType.Building] = new BuildState();

        _currentState = _states[StateType.Idle];

        _animationController = GetComponent<CollectorBotAnimator>();

        _currentState.Entry(this);
    }

    private void Update()
    {
        _currentState.Run();       
    }

    public void AssignTasks(Queue<CollectorBotTask> tasks)
    {
        _tasks = new Queue<CollectorBotTask>(tasks);

        SwitchToState();
    }

    public void ResetTasks()
    {
        _currentState.Exit();
        _tasks.Clear();
    }

    private void SwitchToState()
    {
        CollectorBotState state = GetState();

        _currentState.Completed -= SwitchToState;
        _currentState.Exit();
        _currentState = state;
        _currentState.Completed += SwitchToState;
        _currentState.Entry(this);
    }

    private CollectorBotState GetState() 
    {
        if (_tasks.Count > 0)
        {
            _currentTask = _tasks.Dequeue();

            return _states[_currentTask.StateType];
        }
       
        OnBotAvailable?.Invoke(this);

        return _states[StateType.Idle];
    }
}