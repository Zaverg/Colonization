using UnityEngine;

public interface IStateMachine
{
    public Transform Transform { get; }
    public CollectorBotTask CurrentTask { get; }

    public IMover Mover { get; }
    public ITaker Taker { get; }
    public IMiner Miner { get; }
    public IUnloader Unloader { get; }
    public IBuilder Builder { get; }

    public CollectorBotAnimator AnimationController { get; }
}