using Microsoft.EntityFrameworkCore;
using System;
using XrnCourse.BucketList.WebApi.Domain;

namespace XrnCourse.BucketList.WebApi.Data
{
    public class BucketlistContext : DbContext
    {
        public DbSet<Bucketlist> Bucketlists { get; set; }
        public DbSet<BucketlistItem> BucketlistItems { get; set; }
        public DbSet<User> Users { get; set; }

        public BucketlistContext(DbContextOptions<BucketlistContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bucketlist>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Bucketlist>()
                .Property(e => e.Description)
                .IsRequired();

            modelBuilder.Entity<Bucketlist>()
                .Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(30);

            modelBuilder.Entity<BucketlistItem>()
               .Property(e => e.ItemDescription)
               .IsRequired()
               .HasMaxLength(50);

            modelBuilder.Entity<BucketlistItem>()
               .HasOne(e => e.ParentBucket)
               .WithMany(b => b.Items)
               .HasForeignKey(e => e.BucketId)
               .IsRequired();

            modelBuilder.Entity<User>()
               .Property(e => e.Email)
               .IsRequired();

            modelBuilder.Entity<User>()
               .Property(e => e.UserName)
               .IsRequired();

            //seeding
            modelBuilder.Entity<User>()
                .HasData(new User[]
                {
                    new User{
                        Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                        UserName = "Siegfried",
                        Email="siegfried@bucketlists.test",
                    },
                    new User{
                        Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                        UserName = "Deidre",
                        Email="deidre@bucketlists.test",
                    }
                });

            modelBuilder.Entity<Bucketlist>()
                .HasData(new Bucketlist[]
                {
                    new Bucketlist{
                        Id = Guid.Parse("11111111-0000-0000-0000-000000000001"),
                        OwnerId = Guid.Parse("00000000-0000-0000-0000-000000000001"), //the first user
                        Title = "Programming Dreams",
                        Description = "Advancing my programming skills", ImageUrl = null, IsFavorite = true,
                    },
                    new Bucketlist{
                        Id = Guid.Parse("11111111-0000-0000-0000-000000000002"),
                        OwnerId = Guid.Parse("00000000-0000-0000-0000-000000000001"), //the first user
                        Title = "World Travels",
                        Description = "How I'm gonna spend the money earned from programming", ImageUrl = null, IsFavorite = true,
                    }
                });

            modelBuilder.Entity<BucketlistItem>().HasData(
                new BucketlistItem[]
                {
                    new BucketlistItem {
                        Id = Guid.Parse("11111111-1111-0000-0000-000000000001"),
                        BucketId = Guid.Parse("11111111-0000-0000-0000-000000000001"),  //first BL
                        ItemDescription ="Become better in C#",
                        CompletionDate = DateTime.Now.AddMonths(-6) },
                    new BucketlistItem {
                        Id = Guid.Parse("11111111-1111-0000-0000-000000000002"),
                        BucketId = Guid.Parse("11111111-0000-0000-0000-000000000001"),  //first BL
                        ItemDescription="Learn Xamarin",
                        CompletionDate = DateTime.Now.AddMonths(-1) },
                    new BucketlistItem {
                        Id = Guid.Parse("11111111-1111-0000-0000-000000000003"),
                        BucketId = Guid.Parse("11111111-0000-0000-0000-000000000001"),  //first BL
                        ItemDescription="Publish my first mobile app" },
                    new BucketlistItem {
                        Id = Guid.Parse("22222222-1111-0000-0000-000000000001"),
                        BucketId = Guid.Parse("11111111-0000-0000-0000-000000000002"),  //second BL
                        ItemDescription ="Hiking New Zealand"},
                });
        }
    }
}
