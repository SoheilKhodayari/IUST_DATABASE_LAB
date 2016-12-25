using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WpfApplication1.Models.Mapping
{
    public class CheckMap : EntityTypeConfiguration<Check>
    {
        public CheckMap()
        {
            // Primary Key
            this.HasKey(t => new { t.AId, t.CheckId });

            // Properties
            this.Property(t => t.AId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CheckId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("Check");
            this.Property(t => t.AId).HasColumnName("AId");
            this.Property(t => t.CheckId).HasColumnName("CheckId");
            this.Property(t => t.ExpirationDate).HasColumnName("ExpirationDate");
            this.Property(t => t.PaperNumber).HasColumnName("PaperNumber");

            // Relationships
            this.HasRequired(t => t.CurrentAccount)
                .WithMany(t => t.Checks)
                .HasForeignKey(d => d.AId);

        }
    }
}
