using todo.Infrastructure.Data;

namespace todo.Service;

public class ValidateTask
{
    private readonly DataContext _dataContext;

    public ValidateTask(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public bool TaskExists(string name)
    {
        return _dataContext.Tasks.Any(t => t.Name == name);
    }
}