using System.ComponentModel.DataAnnotations;

namespace InnovationApi.Entities
{
    public class Goal
    {
        public Goal()
        {
            Tasks = new List<Task>();
        }

        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public DateTime CreatedDate { get;set; }
        public virtual List<Task> Tasks { get; set; }
    }
}
