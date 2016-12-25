using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WpfApplication1.Models.Mapping
{
    public class Branch_PhoneMap : EntityTypeConfiguration<Branch_Phone>
    {
        public Branch_PhoneMap()
        {
            // Primary Key
            this.HasKey(t => new { t.BranchId_FK, t.Phone });

            // Properties
            this.Property(t => t.BranchId_FK)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Phone)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Branch_Phone");
            this.Property(t => t.BranchId_FK).HasColumnName("BranchId_FK");
            this.Property(t => t.Phone).HasColumnName("Phone");

            // Relationships
            this.HasRequired(t => t.Branch)
                .WithMany(t => t.Branch_Phone)
                .HasForeignKey(d => d.BranchId_FK);

        }
    }
}
