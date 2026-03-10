using UnityEngine;

public interface IBuilder
{
    public void StartBuild(IBuild buildObject, IStateMachine stateMachine);
}