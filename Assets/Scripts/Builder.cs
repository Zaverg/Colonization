using UnityEngine;

public class Builder : MonoBehaviour, IBuilder
{
    public void StartBuild(BuildProcess buildObject, IBot stateMachine)
    {
        buildObject.gameObject.SetActive(true);
        buildObject.StartBuild(stateMachine);
    }
}
