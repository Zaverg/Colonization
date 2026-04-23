using System;

public interface IBuild
{
    public event Action<IBuild> OnEndBuild;

    public void StartBuild(IBot builder);
}
