using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WpfApplication1.Models.Mapping
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            // Primary Key
            this.HasKey(t => t.CId);

            // Properties
            this.Property(t => t.CId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Customer");
            this.Property(t => t.CId).HasColumnName("CId");

            // Relationships
            this.HasRequired(t => t.Person)
                .WithOptional(t => t.Customer);

        }
    }
}
