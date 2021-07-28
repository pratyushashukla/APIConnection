using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ApiConn.Model
{
    public partial class SchoolContext : DbContext
    {
        public SchoolContext()
        {
        }

        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        public virtual DbSet<student> students { get; set; }
        public virtual DbSet<teacher> teachers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=school;Username=postgres;Password=prats212");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "English_United States.1252");

            modelBuilder.Entity<student>(entity =>
            {
                entity.HasKey(e => e.s_id)
                    .HasName("student_pkey");

                entity.ToTable("student");

                entity.Property(e => e.s_name).HasMaxLength(20);
            });

            modelBuilder.Entity<teacher>(entity =>
            {
                entity.HasKey(e => e.t_id)
                    .HasName("teacher_pkey");

                entity.ToTable("teacher");

                entity.Property(e => e.t_name).HasColumnType("character varying");

                entity.HasOne(d => d.s_idNavigation)
                    .WithMany(p => p.teachers)
                    .HasForeignKey(d => d.s_id)
                    .HasConstraintName("teacher_s_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
