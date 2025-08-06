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
    public class BankAccount
    {
        public int Id { get; set; }
        public Guid PublicId { get; set; } = Guid.NewGuid();

        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        public string BankAccountNumber { get; set; }
        public string Currency { get; set; }
        public decimal Balance { get; set; }    
        public int Type { get; set; }
        public decimal? InteresetRate { get; set; } 
        public BankAccountType DisplayType => (BankAccountType)Type;

        public virtual List<BankAccountBlockRequest> BlockadeRequests { get; set; }
        public virtual List<BankStatement> BankStatements { get; set; } = new List<BankStatement>();
        public virtual List<Transfer> TransfersSent { get; set; } = new List<Transfer>();
        public virtual List<Transfer> TransfersReceived { get; set; } = new List<Transfer>();

    }

    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.ToTable("BankAccounts");

            builder.Property(n => n.BankAccountNumber)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Currency).IsRequired().HasMaxLength(10);
            builder.Property(a => a.Balance).IsRequired().HasPrecision(20, 2);

            builder.Property(t => t.Type).IsRequired();
            builder.Ignore(t => t.DisplayType);

            builder.Property(ir => ir.InteresetRate).IsRequired(false).HasPrecision(20, 2);

            builder.HasMany(bs => bs.BankStatements)
                .WithOne(a => a.BankAccount)
                .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
