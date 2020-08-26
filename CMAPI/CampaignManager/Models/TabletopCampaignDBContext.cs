using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CampaignManager.Models
{
    public partial class TabletopCampaignDBContext : DbContext
    {
        public TabletopCampaignDBContext()
        {
        }

        public TabletopCampaignDBContext(DbContextOptions<TabletopCampaignDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Character> Character { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<OwnedItem> OwnedItem { get; set; }
        public virtual DbSet<Player> Player { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=TabletopCampaignDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>(entity =>
            {
                entity.Property(e => e.CharacterId).HasColumnName("CharacterID");

                entity.Property(e => e.Class)
                    .IsRequired()
                    .HasColumnName("class")
                    .HasMaxLength(50);

                entity.Property(e => e.InventoryId).HasColumnName("InventoryID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.PlayerId).HasColumnName("PlayerID");

                entity.HasOne(d => d.Inventory)
                    .WithMany(p => p.Character)
                    .HasForeignKey(d => d.InventoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Character_Inventory");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.Character)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Character_Player");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.Property(e => e.InventoryId).HasColumnName("InventoryID");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Weight).HasColumnName("weight");
            });

            modelBuilder.Entity<OwnedItem>(entity =>
            {
                entity.Property(e => e.OwnedItemId).HasColumnName("OwnedItemID");

                entity.Property(e => e.InventoryId).HasColumnName("InventoryID");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.OwnedItem)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OwnedItem_Item");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.Property(e => e.PlayerId).HasColumnName("PlayerID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
