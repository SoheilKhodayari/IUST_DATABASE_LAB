using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WpfApplication1.Models.Mapping
{
    public class Available_Foreign_CurrencyMap : EntityTypeConfiguration<Available_Foreign_Currency>
    {
        public Available_Foreign_CurrencyMap()
        {
            // Primary Key
            this.HasKey(t => t.AFCID);

            // Properties
            this.Property(t => t.AFCID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Currency)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Available_Foreign_Currency");
            this.Property(t => t.AFCID).HasColumnName("AFCID");
            this.Property(t => t.Currency).HasColumnName("Currency");
            this.Property(t => t.CurrencyAmount).HasColumnName("CurrencyAmount");
            this.Property(t => t.BranchId_FK).HasColumnName("BranchId_FK");

            // Relationships
            this.HasRequired(t => t.Branch)
                .WithMany(t => t.Available_Foreign_Currency)
                .HasForeignKey(d => d.BranchId_FK);

        }
    }
}
