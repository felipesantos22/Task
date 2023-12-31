using Microsoft.EntityFrameworkCore;
using todo.Domain.Interfaces;
using todo.Infrastructure.Data;
using Task = todo.Domain.Entities.Task;

namespace todo.Infrastructure.Repository
{
    public class TaskRepository : ITask
    {

        private readonly DataContext _dataContext;

        public TaskRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Task> Create(Task task)
        {
            await _dataContext.Tasks.AddAsync(task);
            await _dataContext.SaveChangesAsync();
            return task;
        }

        public async Task<Task?> Destroy(int id)
        {
            var task = await _dataContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            _dataContext.Tasks.Remove(task!);
            await _dataContext.SaveChangesAsync();
            return task;
        }

        public async Task<List<Task>> Index()
        {
            var tasks = await _dataContext.Tasks.ToListAsync();
            return tasks;
        }

        public async Task<Task?> Show(int id)
        {
            var task = await _dataContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            return task;
        }

        public async Task<Task> Update(int id, Task task)
        {
            var updateTask = await _dataContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            updateTask!.Name = task.Name;
            await _dataContext.SaveChangesAsync();
            return updateTask;
        }
    }
}
