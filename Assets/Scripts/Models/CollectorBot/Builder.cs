public class Builder : IBuilder
{
    public void StartBuild(IBuild buildObject, IStateMachine stateMachine)
    {
        buildObject.StartBuild(stateMachine);
    }
}
