using InnovationApi.Entities;
using Microsoft.EntityFrameworkCore;
using Task = InnovationApi.Entities.Task;

namespace InnovationApi.Context
{
    public class InnovationContext :DbContext
    {
        public InnovationContext(DbContextOptions options) : base(options) {

        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Goal> Goals { get; set; }
    }
}
