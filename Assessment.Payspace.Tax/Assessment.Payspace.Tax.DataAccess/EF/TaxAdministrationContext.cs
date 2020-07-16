using Assessment.Payspace.Tax.DataAccess.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assessment.Payspace.Tax.DataAccess.EF
{
    public partial class TaxAdministrationContext : DbContext
    {
        public TaxAdministrationContext()
        {
        }

        public TaxAdministrationContext(DbContextOptions<TaxAdministrationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FlatTaxRate> FlatTaxRate { get; set; }
        public virtual DbSet<FlatValueTaxRate> FlatValueTaxRate { get; set; }
        public virtual DbSet<ProgressiveTaxRate> ProgressiveTaxRate { get; set; }
        public virtual DbSet<TaxAssessment> TaxAssessment { get; set; }
        public virtual DbSet<TaxType> TaxType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLExpress;Database=TaxAdministration;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FlatTaxRate>(entity =>
            {
                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Percentage).HasColumnType("decimal(4, 2)");

                entity.Property(e => e.UserChanged)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserCreated)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('System')");
            });

            modelBuilder.Entity<FlatValueTaxRate>(entity =>
            {
                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MaximumAmount).HasColumnType("money");

                entity.Property(e => e.Percentage).HasColumnType("decimal(4, 2)");

                entity.Property(e => e.UserChanged).HasMaxLength(50);

                entity.Property(e => e.UserCreated)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('System')");

                entity.Property(e => e.Value).HasColumnType("money");
            });

            modelBuilder.Entity<ProgressiveTaxRate>(entity =>
            {
                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MaximumAmount).HasColumnType("money");

                entity.Property(e => e.MinimumAmount).HasColumnType("money");

                entity.Property(e => e.Percentage).HasColumnType("decimal(4, 2)");

                entity.Property(e => e.UserChanged).HasMaxLength(50);

                entity.Property(e => e.UserCreated)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('System')");
            });

            modelBuilder.Entity<TaxAssessment>(entity =>
            {
                entity.Property(e => e.NettIncome).HasColumnType("money");

                entity.Property(e => e.IncomeTax).HasColumnType("money");

                entity.Property(e => e.UserChanged).HasMaxLength(50);

                entity.Property(e => e.UserCreated).HasMaxLength(50);

                entity.HasOne(d => d.TaxType)
                    .WithMany(p => p.TaxAssessment)
                    .HasForeignKey(d => d.TaxTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaxAssessment_TaxType");
            });

            modelBuilder.Entity<TaxType>(entity =>
            {
                entity.Property(e => e.CalculationType)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserChange).HasMaxLength(50);

                entity.Property(e => e.UserCreated)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('System')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
