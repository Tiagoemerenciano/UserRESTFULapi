using Microsoft.EntityFrameworkCore;
using User.Domain;

namespace DatabaseHelper
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<PhoneEntity> Phones { get; set; }
    }
}
