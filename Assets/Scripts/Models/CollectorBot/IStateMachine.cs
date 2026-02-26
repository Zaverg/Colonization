using UnityEngine;

public interface IStateMachine
{
    public Transform Transform { get; }
    public bool HasTask { get; }
    public CollectorBotTask CurrentTask { get; }

    public IMover Mover { get; }
    public ITaker Taker { get; }
    public IMiner Miner { get; }
    public IUnloader Unloader { get; }

    public CollectorBotAnimator AnimationController { get; }
}