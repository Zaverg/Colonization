using UnityEngine;

public struct CollectorBotTask
{
    public StateType StateType { get; private set; }
    public Transform Target { get; private set; }
    public Vector3 TargetPosition { get; private set; }
    public IResource Mineral { get; private set; }
    public ICoroutineRunner CoroutineStarter { get; private set; }
    public IBuild BuildObject { get; private set; }

    public CollectorBotTask(StateType stateType, Vector3 targetPosition = default, Transform target = null, IResource mineral = null, ICoroutineRunner coroutineStarter = null,
        IBuild buildObject = null)
    {
        StateType = stateType;
        TargetPosition = targetPosition;
        Target = target;
        Mineral = mineral;
        CoroutineStarter = coroutineStarter;
        BuildObject = buildObject;
    }
}
