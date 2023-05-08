using EFCoreShadowForeignKey.Model;
using Microsoft.EntityFrameworkCore;

namespace EFCoreShadowForeignKey.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaseEntity>().ToTable("BaseEntity");
            modelBuilder.Entity<BaseEntity>().Property<int>("Id")
              .HasColumnOrder(1)
              .UseIdentityColumn()
              .ValueGeneratedOnAdd();
            modelBuilder.Entity<BaseEntity>().HasKey("Id");
            modelBuilder.Entity<BaseEntity>().HasAlternateKey(entity => entity.Key);
            modelBuilder.Entity<BaseEntity>().Property(entity => entity.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<BaseEntity>().Property<string>("Discriminator").HasMaxLength(20);
            modelBuilder.Entity<BaseEntity>().HasDiscriminator<string>("Discriminator")
                .HasValue<RootEntity>(nameof(RootEntity))
                .HasValue<LeafEntity>(nameof(LeafEntity));
            modelBuilder.Entity<RootEntity>().HasData(
                new { Id = 1, Key = "root-1", Name = "Root One" },
                new { Id = 2, Key = "root-2", Name = "Root Two" },
                new { Id = 3, Key = "root-3", Name = "Root Three" });

            modelBuilder.Entity<LeafEntity>().HasOne(entity => entity.Root)
                .WithMany()
                .HasForeignKey("RootId")
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<LeafEntity>().HasData(
                new { Id = 4, Key = "root-1/leaf-1", Name = "Leaf One-One", RootId = 1 },
                new { Id = 5, Key = "root-1/leaf-2", Name = "Leaf One-Two", RootId = 1 },
                new { Id = 6, Key = "root-1/leaf-3", Name = "Leaf One-Three", RootId = 1 },
                new { Id = 7, Key = "root-2/leaf-1", Name = "Leaf Two-One", RootId = 2 },
                new { Id = 8, Key = "root-2/leaf-2", Name = "Leaf Two-Two", RootId = 2 },
                new { Id = 9, Key = "root-3/leaf-1", Name = "Leaf Three-One", RootId = 3 },
                new { Id = 10, Key = "root-3/leaf-2", Name = "Leaf Three-Two", RootId = 3 });

            modelBuilder.Entity<RelatedEntity>().ToTable("RelatedEntity");
            modelBuilder.Entity<RelatedEntity>().HasKey(entity => entity.Id);
            modelBuilder.Entity<RelatedEntity>().HasMany(entity => entity.Entities).WithMany()
                .UsingEntity(builder =>
                {
                    builder.ToTable("RelatedEntities");

                    builder.Property<int>("Id").UseIdentityColumn().ValueGeneratedOnAdd();
                    builder.Property<Guid>("RelatedEntityId");
                    builder.Property<int>("EntitiesId");

                    builder.HasKey("Id");
                    builder.HasAlternateKey("RelatedEntityId", "EntitiesId");
                });
        }
    }
}
