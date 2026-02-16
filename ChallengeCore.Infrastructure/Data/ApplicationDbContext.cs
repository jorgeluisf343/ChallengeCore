using ChallengeCore.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using static ChallengeCore.Domain.Enums;

namespace ChallengeCore.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Challenge> Challenges => Set<Challenge>();
        public DbSet<Reward> Rewards => Set<Reward>();
        public DbSet<UserChallenge> UserChallenges => Set<UserChallenge>();
        public DbSet<UserReward> UserRewards => Set<UserReward>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserChallenge>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserChallenges)
                .HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<UserChallenge>()
                .HasOne(uc => uc.Challenge)
                .WithMany(c => c.UserChallenges)
                .HasForeignKey(uc => uc.ChallengeId);

            modelBuilder.Entity<UserReward>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRewards)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserReward>()
                .HasOne(ur => ur.Reward)
                .WithMany(r => r.UserRewards)
                .HasForeignKey(ur => ur.RewardId);

            // Seed Challenges
            modelBuilder.Entity<Challenge>().HasData(
                new Challenge
                {
                    Id = new Guid("4ed83a3c-6207-4996-b68d-973e2eb1d877"),
                    Title = "Completa tu perfil",
                    Description = "Verifica que tu información personal esté completa.",
                    Points = 10,
                    Type = ChallengeType.Simple,
                    IsBonus = false
                },
                new Challenge
                {
                    Id = new Guid("d3312eba-aeca-4721-a8b3-80bccc312409"),
                    Title = "Configura una meta de ahorro",
                    Description = "Ingresa el monto objetivo de ahorro mensual.",
                    Points = 20,
                    Type = ChallengeType.Input,
                    IsBonus = false
                },
                new Challenge
                {
                    Id = new Guid("2456a3c7-fcbf-48b8-8023-bc71e4888318"),
                    Title = "Selecciona tu tipo de inversión",
                    Description = "Elige entre las opciones de inversión disponibles.",
                    Points = 15,
                    Type = ChallengeType.Selection,
                    IsBonus = false
                },
                new Challenge
                {
                    Id = new Guid("5c3f088a-3acd-4363-870a-44b9f62e3e21"),
                    Title = "Simula un préstamo",
                    Description = "Realiza una simulación básica de préstamo.",
                    Points = 25,
                    Type = ChallengeType.Simple,
                    IsBonus = false
                },
                new Challenge
                {
                    Id = new Guid("55c567c9-3ec6-4540-92e3-db6ab334f9b4"),
                    Title = "Reto Premium: Educación Financiera",
                    Description = "Completa el módulo básico de educación financiera.",
                    Points = 50,
                    Type = ChallengeType.Simple,
                    IsBonus = true
                }

            );

            // Seed Rewards
            modelBuilder.Entity<Reward>().HasData(
                new Reward
                {
                    Id = new Guid("c57756f1-6b85-40f5-a102-0f78ba7c0ee2"),
                    Name = "Cashback $5",
                    Description = "Obtén $5 de devolución en tu próxima compra.",
                    Cost = 40
                },
                new Reward
                {
                    Id = new Guid("6170ca53-1b3a-4f3f-b4c7-9f6cd09f860e"),
                    Name = "Gift Card Digital $10",
                    Description = "Tarjeta de regalo digital de $10.",
                    Cost = 70
                },
                new Reward
                {
                    Id = new Guid("9f0f3f61-58db-4d70-8c4a-6b1b7b4df111"),
                    Name = "Acceso Premium 1 Mes",
                    Description = "Acceso a beneficios premium durante 30 días.",
                    Cost = 120
                }
            );
        }
    }
}
