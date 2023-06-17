using EF_Relationships.Models;
using Microsoft.EntityFrameworkCore;


namespace EF_Relationships.Context
{
    public class RelationDbContext : DbContext
    {
        public RelationDbContext(DbContextOptions<RelationDbContext> options) : base(options) { }

        public DbSet<User> USers { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Skill> Skills { get; set; }
    }
}
