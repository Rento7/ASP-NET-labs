using AspLab3MinimalApiEntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;

namespace AspLab3MinimalApiEntityFramework.Data
{
    public class TodoistContext : DbContext
    {
        public DbSet<Todo> Todos {  get; set; }
        public DbSet<User> Users {  get; set; }
        public DbSet<UserSettings> UserSettings {  get; set; }

        public TodoistContext()
        {
        }

        public TodoistContext(DbContextOptions<TodoistContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Username).HasMaxLength(50);

                entity.HasOne(e => e.Settings)
                    .WithOne(e => e.User)
                    .HasForeignKey<UserSettings>(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                entity.HasMany(e => e.Todos)
                    .WithOne(e => e.User)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity<Todo>(entity =>
            {
                entity.ToTable("Todo");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Text).HasMaxLength(450);

                entity.HasOne(e => e.User)
                    .WithMany(e => e.Todos)
                    .HasForeignKey(e => e.UserId)
                    .IsRequired();
            });


            modelBuilder.Entity<UserSettings>(entity =>
            {
                entity.ToTable("UserSettings");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Language).HasMaxLength(50);

                entity.HasOne(e => e.User)
                    .WithOne(e => e.Settings)
                    .HasForeignKey<UserSettings>(e => e.UserId)
                    .IsRequired();
            });
        }
    }
}
