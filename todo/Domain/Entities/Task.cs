using System.ComponentModel.DataAnnotations;

namespace todo.Domain.Entities
{
    public class Task
    {
        [Key]
        public int Id {  get; set; }
        public string? Name { get; set; }
       
    }
}
