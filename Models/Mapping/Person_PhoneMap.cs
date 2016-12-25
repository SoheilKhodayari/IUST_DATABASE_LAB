using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WpfApplication1.Models.Mapping
{
    public class Person_PhoneMap : EntityTypeConfiguration<Person_Phone>
    {
        public Person_PhoneMap()
        {
            // Primary Key
            this.HasKey(t => new { t.PId_FK, t.Phone });

            // Properties
            this.Property(t => t.PId_FK)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Phone)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Person_Phone");
            this.Property(t => t.PId_FK).HasColumnName("PId_FK");
            this.Property(t => t.Phone).HasColumnName("Phone");

            // Relationships
            this.HasRequired(t => t.Person)
                .WithMany(t => t.Person_Phone)
                .HasForeignKey(d => d.PId_FK);

        }
    }
}
