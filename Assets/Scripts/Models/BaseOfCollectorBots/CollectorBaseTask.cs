using System.Collections.Generic;
using System.Threading.Tasks;

public abstract class CollectorBaseTask
{
    public abstract Queue<CollectorBotTask> CreateTask();
} 
