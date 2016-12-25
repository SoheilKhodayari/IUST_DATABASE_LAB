using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WpfApplication1.Models.Mapping
{
    public class BranchMap : EntityTypeConfiguration<Branch>
    {
        public BranchMap()
        {
            // Primary Key
            this.HasKey(t => t.BranchId);

            // Properties
            this.Property(t => t.BranchId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .HasMaxLength(100);

            this.Property(t => t.City)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Branch");
            this.Property(t => t.BranchId).HasColumnName("BranchId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.IsCentral).HasColumnName("IsCentral");
            this.Property(t => t.BankId_FK).HasColumnName("BankId_FK");

            // Relationships
            this.HasRequired(t => t.Bank)
                .WithMany(t => t.Branches)
                .HasForeignKey(d => d.BankId_FK);

        }
    }
}
