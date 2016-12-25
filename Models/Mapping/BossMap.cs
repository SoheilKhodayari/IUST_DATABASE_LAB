using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WpfApplication1.Models.Mapping
{
    public class BossMap : EntityTypeConfiguration<Boss>
    {
        public BossMap()
        {
            // Primary Key
            this.HasKey(t => t.BId);

            // Properties
            this.Property(t => t.BId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Boss");
            this.Property(t => t.BId).HasColumnName("BId");
            this.Property(t => t.AbsenceCount).HasColumnName("AbsenceCount");
            this.Property(t => t.SystemPassword).HasColumnName("SystemPassword");
            this.Property(t => t.BranchId_FK).HasColumnName("BranchId_FK");

            // Relationships
            this.HasRequired(t => t.Person)
                .WithOptional(t => t.Boss);
            this.HasRequired(t => t.Branch)
                .WithMany(t => t.Bosses)
                .HasForeignKey(d => d.BranchId_FK);

        }
    }
}
