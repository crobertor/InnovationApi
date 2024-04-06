using System.ComponentModel.DataAnnotations;

namespace InnovationApi.Entities
{
    public class Task
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public DateTime Date { get; set; }
        public bool Priority { get; set; }
        public string Status { get; set; } = default!;
        public Guid GoalId { get; set; }
        public Goal? Goal { get; set; }
    }
}
