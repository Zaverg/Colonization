using UnityEngine;

public struct CollectorBotTask
{
    public StateType StateType { get; private set; }
    public Transform Target { get; private set; }
    public Vector3 TargetPosition { get; private set; }
    public IResource Resource { get; private set; }
    public ICoroutineRunner CoroutineRunner { get; private set; }
    public BuildProcess BuildProcess { get; private set; }

    public CollectorBotTask(StateType stateType, Vector3 targetPosition = default, Transform target = null, IResource resource = null, ICoroutineRunner coroutineRunner = null,
        BuildProcess buildProcess = null)
    {
        StateType = stateType;
        TargetPosition = targetPosition;
        Target = target;
        Resource = resource;
        CoroutineRunner = coroutineRunner;
        BuildProcess = buildProcess;
    }
}