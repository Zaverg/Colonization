using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(NavMeshAgent), typeof(CollectorBotAnimator))]
public class CollectorBot : MonoBehaviour, IBot
{
    [SerializeField] private Mover _mover;
    [SerializeField] private Taker _taker;
    [SerializeField] private Miner _miner;
    [SerializeField] private Unloader _unloader;
    [SerializeField] private Builder _builder;

    private Queue<CollectorBotTask> _tasks = new Queue<CollectorBotTask>();
    private Dictionary<StateType, BotState> _states = new Dictionary<StateType, BotState>();

    private CollectorBotAnimator _animator;

    private CollectorBotTask _currentTask;
    private BotState _currentState;

    public event Action<CollectorBot> OnBotAvailable;

    public IMover Mover => _mover;
    public ITaker Taker => _taker;
    public IMiner Miner => _miner;
    public IUnloader Unloader => _unloader;
    public IBuilder Builder => _builder;
    public Transform Transform => transform;
    public CollectorBotTask CurrentTask => _currentTask;
    public CollectorBotAnimator Animator => _animator;

    public void Awake()
    {
        _states[StateType.Idle] = new IdleState();
        _states[StateType.Moving] = new MovingState();
        _states[StateType.Taking] = new TakingState();
        _states[StateType.Unloading] = new UnloaderState();
        _states[StateType.Mining] = new MiningState();
        _states[StateType.Building] = new BuildState();

        _currentState = _states[StateType.Idle];

        _animator = GetComponent<CollectorBotAnimator>();

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
        _tasks.Clear();
        SwitchToState();
    }

    private void SwitchToState()
    {
        BotState state = GetState();

        _currentState.Completed -= SwitchToState;
        _currentState.Exit();
        _currentState = state;
        _currentState.Completed += SwitchToState;
        _currentState.Entry(this);
    }

    private BotState GetState() 
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