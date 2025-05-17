using BankApplication.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Data.Entities
{
    public class BankStatement
    {
        public int Id { get; set; }
        public Guid PublicId { get; set; }

        public int BankAccountId { get; set; }
        public virtual BankAccount BankAccount { get; set; }

        public DateTime CreatedDate { get; set; }

        public int Format { get; set; }
        public StatementFormat DisplayFormat => (StatementFormat)Format;
    }

    public class BankStatementConfiguration : IEntityTypeConfiguration<BankStatement>
    {
        public void Configure(EntityTypeBuilder<BankStatement> builder)
        {
            builder.ToTable("BankStatements");

            builder.Property(f => f.Format).IsRequired();
            builder.Ignore(df => df.DisplayFormat);

            builder.Property(p => p.CreatedDate)
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()");
        }
    }
}   
