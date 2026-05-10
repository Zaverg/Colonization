using UnityEngine;

public interface IBot
{
    public Transform Transform { get; }
    public CollectorBotTask CurrentTask { get; }

    public Mover Mover { get; }
    public Taker Taker { get; }
    public Miner Miner { get; }
    public Unloader Unloader { get; }
    public Builder Builder { get; }

    public CollectorBotAnimator Animator { get; }
}