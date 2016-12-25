using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WpfApplication1.Models.Mapping
{
    public class CreditCardMap : EntityTypeConfiguration<CreditCard>
    {
        public CreditCardMap()
        {
            // Primary Key
            this.HasKey(t => t.CardNumber);

            // Properties
            this.Property(t => t.CardNumber)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(16);

            this.Property(t => t.CVV2)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.Password)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.SecondaryPassword)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CreditCard");
            this.Property(t => t.CardNumber).HasColumnName("CardNumber");
            this.Property(t => t.Remainder).HasColumnName("Remainder");
            this.Property(t => t.ExpirationDate).HasColumnName("ExpirationDate");
            this.Property(t => t.CVV2).HasColumnName("CVV2");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.SecondaryPassword).HasColumnName("SecondaryPassword");
            this.Property(t => t.CardType).HasColumnName("CardType");
            this.Property(t => t.AId_FK).HasColumnName("AId_FK");

            // Relationships
            this.HasOptional(t => t.SavingAccount)
                .WithMany(t => t.CreditCards)
                .HasForeignKey(d => d.AId_FK);

        }
    }
}
