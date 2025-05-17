using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Data.Entities
{
    public class Blockade
    {
        public int Id { get; set; }
        public Guid PublicId { get; set; }

        public int AccountId { get; set; }
        public virtual Account Account { get; set; }

        public string Reason { get; set; }
        public DateTime BlockDateFrom { get; set; }
        public DateTime? BlockDateTo { get; set; }

        public bool IsActive {get; set; }
    }

    public class BlockadeConfiguration : IEntityTypeConfiguration<Blockade>
    {
        public void Configure(EntityTypeBuilder<Blockade> builder)
        {
            builder.ToTable("Blockades");

            builder.Property(r => r.Reason)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(a => a.IsActive).IsRequired();

            builder.Property(d => d.BlockDateFrom)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
