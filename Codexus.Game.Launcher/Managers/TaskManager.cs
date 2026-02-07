using System.Threading.Tasks;

namespace Codexus.Game.Launcher.Managers;

public class TaskManager
{
    public static TaskManager Instance
    {
        get
        {
            TaskManager? taskManager;
            if ((taskManager = _instance) == null) taskManager = _instance = new TaskManager();
            return taskManager;
        }
    }

    private TaskManager()
    {
    }

    public TaskFactory GetFactory()
    {
        TaskFactory? taskFactory;
        if ((taskFactory = _factory) == null) taskFactory = _factory = new TaskFactory();
        return taskFactory;
    }

    private static TaskManager? _instance;
    private TaskFactory? _factory;
}