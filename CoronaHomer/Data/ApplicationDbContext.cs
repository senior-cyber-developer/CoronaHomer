using CoronaHomer.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoronaHomer.Data
{
	public class ApplicationDbContext : IdentityDbContext<User>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<UserQuest>()
				.HasKey(uq => new { uq.UserId, uq.QuestId });
			modelBuilder.Entity<UserQuest>()
				.HasOne(uq => uq.User)
				.WithMany(u => u.CompletedQuests)
				.HasForeignKey(uq => uq.UserId);
			modelBuilder.Entity<UserQuest>()
				.HasOne(uq => uq.Quest)
				.WithMany(q => q.CompletedBy)
				.HasForeignKey(uq => uq.QuestId);
		}

		public DbSet<Quest> Quest { get; set; }
		public DbSet<Category> Category { get; set; }
		public DbSet<UserQuest> UserQuest { get; set; }
	}
}