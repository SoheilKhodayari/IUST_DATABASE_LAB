using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WpfApplication1.Models.Mapping
{
    public class MessageMap : EntityTypeConfiguration<Message>
    {
        public MessageMap()
        {
            // Primary Key
            this.HasKey(t => t.MsgId);

            // Properties
            this.Property(t => t.MsgId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Title)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Message");
            this.Property(t => t.MsgId).HasColumnName("MsgId");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.BId_FK).HasColumnName("BId_FK");
            this.Property(t => t.BankId_FK).HasColumnName("BankId_FK");

            // Relationships
            this.HasRequired(t => t.Bank)
                .WithMany(t => t.Messages)
                .HasForeignKey(d => d.BankId_FK);
            this.HasRequired(t => t.Boss)
                .WithMany(t => t.Messages)
                .HasForeignKey(d => d.BId_FK);

        }
    }
}
