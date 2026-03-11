using UnityEngine;

public class Builder : MonoBehaviour, IBuilder
{
    public void StartBuild(BuildProcess buildObject, IStateMachine stateMachine)
    {
        buildObject.gameObject.SetActive(true);
        buildObject.StartBuild(stateMachine);
    }
}
