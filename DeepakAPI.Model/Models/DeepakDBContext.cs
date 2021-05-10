using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DeepakAPI.Model.Models
{
    public partial class DeepakdbContext : DbContext
    {
        public DeepakdbContext()
        {
        }

        public DeepakdbContext(DbContextOptions<DeepakdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<UserDetail> UserDetail { get; set; }
        public virtual DbSet<UserDetailView> UserDetailView { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=shiva;Database=Deepakdb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.State)
                    .WithMany(p => p.City)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_City_City");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.State)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_State_Country");
            });

            modelBuilder.Entity<UserDetail>(entity =>
            {
                entity.Property(e => e.AddressLine1)
                    .HasColumnName("Address_Line1")
                    .HasMaxLength(30);

                entity.Property(e => e.AddressLine2)
                    .HasColumnName("Address_Line2")
                    .HasMaxLength(30);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.ForgotPasswordToken).HasMaxLength(30);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(13);

                entity.Property(e => e.ModifiedBy).HasMaxLength(30);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<UserDetailView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("UserDetailView");

                entity.Property(e => e.AddressLine1)
                    .HasColumnName("Address_Line1")
                    .HasMaxLength(30);

                entity.Property(e => e.AddressLine2)
                    .HasColumnName("Address_Line2")
                    .HasMaxLength(30);

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.ForgotPasswordToken).HasMaxLength(30);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(13);

                entity.Property(e => e.ModifiedBy).HasMaxLength(30);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
