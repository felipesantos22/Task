using Microsoft.AspNetCore.Mvc;
using todo.Infrastructure.Repository;
using todo.Service;
using Task = todo.Domain.Entities.Task;

namespace todo.Controllers
{
    [Route("api/task")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskRepository _taskRepository;
        private readonly ValidateTask _validateTask;

        public TaskController(TaskRepository taskRepository, ValidateTask validateTask)
        {
            _taskRepository = taskRepository;
            _validateTask = validateTask;
        }


        [HttpPost]
        public async Task<ActionResult<Task>> Create([FromBody] Task task)
        {
            if (string.IsNullOrWhiteSpace(task.Name))
            {
                return BadRequest(new { message = "Field name cannot be empty" });
            }

            var nameExists = _validateTask.TaskExists(task.Name);
            if (nameExists)
            {
                return BadRequest(new { message = "Name already registered" });
            }

            var createTask = await _taskRepository.Create(task);
            return Ok(createTask);
        }

        [HttpGet]
        public async Task<ActionResult<List<Task>>> Index()
        {
            return await _taskRepository.Index();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Task>> Show(int id)
        {
            var task = await _taskRepository.Show(id);
            if (task == null) return NotFound(new { message = "Task not found" });
            return Ok(task);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Task>> Destroy(int id)

        {
            var task = await _taskRepository.Show(id);
            if (task == null) return NotFound(new { message = "Task not found" });
            await _taskRepository.Destroy(id);
            return Ok(new { message = "Task deleted" });
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Task>> Update(int id, Task task)
        {
            
            
            var taskId = await _taskRepository.Show(id);
            if (taskId == null) return NotFound(new { message = "Task not found" });
            
            if (string.IsNullOrWhiteSpace(task.Name))
            {
                return BadRequest(new { message = "Field name cannot be empty" });
            }

            var nameExists = _validateTask.TaskExists(task.Name);
            if (nameExists)
            {
                return BadRequest(new { message = "Name already registered" });
            }
            
            var taskUpdate = await _taskRepository.Update(id, task);
            return Ok(taskUpdate);
        }
    }
}