using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WpfApplication1.Models.Mapping
{
    public class LotteryMap : EntityTypeConfiguration<Lottery>
    {
        public LotteryMap()
        {
            // Primary Key
            this.HasKey(t => t.LotteryId);

            // Properties
            this.Property(t => t.LotteryId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Lottery");
            this.Property(t => t.LotteryId).HasColumnName("LotteryId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.BranchId_FK).HasColumnName("BranchId_FK");

            // Relationships
            this.HasMany(t => t.SavingAccounts)
                .WithMany(t => t.Lotteries)
                .Map(m =>
                    {
                        m.ToTable("Lottery_SavingAccount_Participate");
                        m.MapLeftKey("LotteryId");
                        m.MapRightKey("AId");
                    });

            this.HasMany(t => t.SavingAccounts1)
                .WithMany(t => t.Lotteries1)
                .Map(m =>
                    {
                        m.ToTable("Lottery_SavingAccount_Wins");
                        m.MapLeftKey("LotteryId");
                        m.MapRightKey("AId");
                    });

            this.HasRequired(t => t.Branch)
                .WithMany(t => t.Lotteries)
                .HasForeignKey(d => d.BranchId_FK);

        }
    }
}
