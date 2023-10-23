using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace todo_test_server.Models
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [ForeignKey("TaskId")]
        public ICollection<Task> Tasks { get; set; } = new List<Task>();

        [ForeignKey("TagId")]
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        public bool IsArchived { get; set; } = false;

        public Todo(string name)
        {
            Name = name;
        }
    }
}
