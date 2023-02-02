using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ShopApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User>? Users { get; set; }
        public DbSet<Contract>? Contracts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User[]
                {
                    new User()
                    {
                        Id = 1,
                        Name = "user1"
                    },
                    new User()
                    {
                        Id = 2,
                        Name = "user2"
                    }
                });


            modelBuilder.Entity<Contract>().HasData(
                new Contract[]
                {
                    new Contract()
                    {
                        Id = 1,
                        Name = "Contract1",
                        Amount = 12000,
                        UserId = 1,
                        Status = EContractStatus.Active
                    },
                    new Contract()
                    {
                        Id = 2,
                        Name = "Contract2",
                        Amount = 24000,
                        UserId = 1,
                        Status = EContractStatus.Active
                    },
                    new Contract()
                    {
                        Id = 3,
                        Name = "Contract2",
                        Amount = 34000,
                        UserId = 2,
                        Status = EContractStatus.Active
                    },
                    new Contract()
                    {
                        Id = 4,
                        Name = "Contract2",
                        Amount = 14000,
                        UserId = 2,
                        Status = EContractStatus.Created
                    }
                });
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<Contract>? Contracts { get; set; }
    }

    public class Contract
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Amount { get; set; }

        public EContractStatus Status { get; set; }

        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
    }

    public enum EContractStatus
    {
        Created,
        Active,
        Paid
    }
}
