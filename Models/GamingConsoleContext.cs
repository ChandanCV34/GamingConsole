using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace GamingConsole.Models
{
    public partial class GamingConsoleContext : DbContext
    {
        public GamingConsoleContext()
        {
        }

        public GamingConsoleContext(DbContextOptions<GamingConsoleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FriendUser> FriendUsers { get; set; }
        public virtual DbSet<SomeWord> SomeWords { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=KANINI-LTP-456\\SQLSERVER2021CV;user id=sa;password=Chandancv@123;Initial catalog=GamingConsole");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<FriendUser>(entity =>
            {
                entity.HasKey(e => e.Fid)
                    .HasName("PK__Friend_u__D9908D648EB590EE");

                entity.ToTable("Friend_user");

                entity.Property(e => e.Fid).HasColumnName("fid");

                entity.Property(e => e.Score).HasColumnName("score");

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.Word)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("word");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.FriendUsers)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("FK__Friend_us__usern__3B75D760");
            });

            modelBuilder.Entity<SomeWord>(entity =>
            {
                entity.ToTable("Some_Words");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Score).HasColumnName("score");

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.Word)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("word");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.SomeWords)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("FK__Some_Word__usern__38996AB5");
            });

            modelBuilder.Entity<UserDetail>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__User_det__F3DBC5733002E505");

                entity.ToTable("User_details");

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
