using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace todo_test_server.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [ForeignKey("TodoId")]
        public ICollection<Todo> Todos { get; set; } = new List<Todo>();

        public bool IsArchived { get; set; } = false;

        public Tag(string name)
        {
            Name = name;
        }
    }
}
