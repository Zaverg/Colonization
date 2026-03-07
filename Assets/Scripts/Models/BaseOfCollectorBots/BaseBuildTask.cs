using System.Collections.Generic;

public class BaseBuildTask : CollectorBaseTask
{
    public override Queue<CollectorBotTask> CreateTask()
    {
        Queue<CollectorBotTask> task = new Queue<CollectorBotTask>();

        return task;
    }
}