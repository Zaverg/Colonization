using UnityEngine;

public struct CollectorBotTask
{
    public StateType StateType { get; private set; }
    public Transform Target { get; private set; }
    public Vector3 TargetPosition { get; private set; }
    public IResource Mineral { get; private set; }
    public ICoroutineRuner CoroutineStarter;

    public CollectorBotTask(StateType stateType, Vector3 targetPosition = default, Transform target = null, IResource mineral = null, ICoroutineRuner coroutineStarter = null)
    {
        StateType = stateType;
        TargetPosition = targetPosition;
        Target = target;
        Mineral = mineral;
        CoroutineStarter = coroutineStarter;
    }
}
