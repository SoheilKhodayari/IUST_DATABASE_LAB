using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WpfApplication1.Models.Mapping
{
    public class StaffMap : EntityTypeConfiguration<Staff>
    {
        public StaffMap()
        {
            // Primary Key
            this.HasKey(t => t.SId);

            // Properties
            this.Property(t => t.SId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Staff");
            this.Property(t => t.SId).HasColumnName("SId");
            this.Property(t => t.AbsenceCount).HasColumnName("AbsenceCount");
            this.Property(t => t.SystemPassword).HasColumnName("SystemPassword");
            this.Property(t => t.BranchId_FK).HasColumnName("BranchId_FK");

            // Relationships
            this.HasRequired(t => t.Branch)
                .WithMany(t => t.Staffs)
                .HasForeignKey(d => d.BranchId_FK);
            this.HasRequired(t => t.Person)
                .WithOptional(t => t.Staff);

        }
    }
}
