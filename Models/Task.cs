using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace todo_test_server.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName="nvarchar(50)")]
        public string Name { get; set; }

        public bool IsCompleted { get; set; } = false;

        public bool IsArchived { get; set; } = false;

        public int TodoId { get; set; }

        public Task(string name)
        {
            Name = name;
        }
    }
}
