using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WpfApplication1.Models.Mapping
{
    public class Check_PaperMap : EntityTypeConfiguration<Check_Paper>
    {
        public Check_PaperMap()
        {
            // Primary Key
            this.HasKey(t => new { t.AId, t.CheckId, t.CheckPaperId });

            // Properties
            this.Property(t => t.AId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CheckId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CheckPaperId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Receiver)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Check_Paper");
            this.Property(t => t.AId).HasColumnName("AId");
            this.Property(t => t.CheckId).HasColumnName("CheckId");
            this.Property(t => t.CheckPaperId).HasColumnName("CheckPaperId");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.Receiver).HasColumnName("Receiver");
            this.Property(t => t.ReceivedDate).HasColumnName("ReceivedDate");

            // Relationships
            this.HasRequired(t => t.Check)
                .WithMany(t => t.Check_Paper)
                .HasForeignKey(d => new { d.AId, d.CheckId });

        }
    }
}
