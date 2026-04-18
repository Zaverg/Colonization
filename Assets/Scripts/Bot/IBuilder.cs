using UnityEngine;

public interface IBuilder
{
    public void StartBuild(BuildProcess buildObject, IStateMachine stateMachine);
}