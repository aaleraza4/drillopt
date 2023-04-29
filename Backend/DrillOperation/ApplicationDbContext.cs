using Microsoft.EntityFrameworkCore;
namespace DrillOperation
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions option) : base(option)
        { }

        public DbSet<DrillOperation> DrillOperation { get; set; }
    }

}
