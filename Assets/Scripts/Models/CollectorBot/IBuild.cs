using System;

public interface IBuild
{
    public event Action<IBuild> OnEndBuild;

    public void StartBuild(IStateMachine builder);
}
