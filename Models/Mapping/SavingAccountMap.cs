using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WpfApplication1.Models.Mapping
{
    public class SavingAccountMap : EntityTypeConfiguration<SavingAccount>
    {
        public SavingAccountMap()
        {
            // Primary Key
            this.HasKey(t => t.AId);

            // Properties
            this.Property(t => t.AId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("SavingAccount");
            this.Property(t => t.AId).HasColumnName("AId");
            this.Property(t => t.Remainder).HasColumnName("Remainder");
            this.Property(t => t.OpeningDate).HasColumnName("OpeningDate");
            this.Property(t => t.CId_FK).HasColumnName("CId_FK");
            this.Property(t => t.BranchId_FK).HasColumnName("BranchId_FK");

            // Relationships
            this.HasRequired(t => t.Branch)
                .WithMany(t => t.SavingAccounts)
                .HasForeignKey(d => d.BranchId_FK);
            this.HasRequired(t => t.Customer)
                .WithMany(t => t.SavingAccounts)
                .HasForeignKey(d => d.CId_FK);

        }
    }
}
