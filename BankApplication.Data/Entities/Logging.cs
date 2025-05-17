using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Data.Entities
{
    public class Logging
    {
        public int Id { get; set; }
        public Guid PublicId { get; set; }

        public int AccountId { get; set; }
        public virtual Account Account { get; set; }

        public DateTime LogInDate { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class LoggingConfiguration : IEntityTypeConfiguration<Logging>
    {
        public void Configure(EntityTypeBuilder<Logging> builder)
        {
            builder.ToTable("loggings");

            builder.Property(s => s.IsSuccess).IsRequired();

            builder.Property(d => d.LogInDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");
        }
    }
}   
