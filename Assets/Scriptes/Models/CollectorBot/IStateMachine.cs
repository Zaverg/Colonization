using UnityEngine;

public interface IStateMachine
{
    public Transform Transform { get; }
    public bool HasTask { get; }
    public CollectorBotTask CurrentTask { get; }

    public IMover Mover { get; }
    public ITaker Taker { get; }
    public IMiner Miner { get; }
    public IUnloader Dropper { get; }

    public CollectorBotAnimator AnimationController { get; }
}