
using Task = todo.Domain.Entities.Task;

namespace todo.Domain.Interfaces
{
    public interface ITask
    {
        public Task<Task> Create(Task task);

        public Task<List<Task>> Index();

        public Task<Task?> Show(int id);

        public Task<Task> Update(int id,Task task);

        public Task<Task?> Destroy(int id);



    }
}
