using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WpfApplication1.Models.Mapping
{
    public class LoanMap : EntityTypeConfiguration<Loan>
    {
        public LoanMap()
        {
            // Primary Key
            this.HasKey(t => t.LoadId);

            // Properties
            this.Property(t => t.LoadId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Loan");
            this.Property(t => t.LoadId).HasColumnName("LoadId");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.Interest).HasColumnName("Interest");
            this.Property(t => t.ReturningDuration).HasColumnName("ReturningDuration");
            this.Property(t => t.BranchId_FK).HasColumnName("BranchId_FK");
            this.Property(t => t.CId_FK).HasColumnName("CId_FK");

            // Relationships
            this.HasRequired(t => t.Branch)
                .WithMany(t => t.Loans)
                .HasForeignKey(d => d.BranchId_FK);
            this.HasRequired(t => t.Customer)
                .WithMany(t => t.Loans)
                .HasForeignKey(d => d.CId_FK);

        }
    }
}
