using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WpfApplication1.Models.Mapping
{
    public class BankMap : EntityTypeConfiguration<Bank>
    {
        public BankMap()
        {
            // Primary Key
            this.HasKey(t => t.BankId);

            // Properties
            this.Property(t => t.BankId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Bank");
            this.Property(t => t.BankId).HasColumnName("BankId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Interest).HasColumnName("Interest");
        }
    }
}
