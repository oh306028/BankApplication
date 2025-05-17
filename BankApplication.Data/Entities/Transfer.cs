using BankApplication.Data.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Data.Entities
{
    public class Transfer
    {
        public int Id { get; set; }
        public Guid PublicId { get; set; }

        public int AccountFromId { get; set; }  
        public int AccountToId { get; set; }    

        public virtual BankAccount AccountFrom { get; set; }
        public virtual BankAccount AccountTo { get; set; }  

        public decimal Amount { get; set; }
        public DateTime TransferDate { get; set; }           

        public string Title { get; set; }
        public string Description { get; set; } 
        public int Status { get; set; }

        public TransferStatus DisplayStatus => (TransferStatus)Status;


    }

    public class TransferConfiguration : IEntityTypeConfiguration<Transfer>
    {
        public void Configure(EntityTypeBuilder<Transfer> builder)
        {
            builder.ToTable("Transfers");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.PublicId).IsRequired();
            builder.Property(t => t.Amount).IsRequired().HasPrecision(20,2);
            builder.Property(t => t.TransferDate).IsRequired();
            builder.Property(t => t.Title).IsRequired().HasMaxLength(200);
            builder.Property(t => t.Description).IsRequired(false).HasMaxLength(500);
            builder.Property(t => t.Status).IsRequired();

            builder.Ignore(t => t.DisplayStatus);


            builder.HasOne(t => t.AccountFrom)
                   .WithMany(a => a.TransfersSent)
                   .HasForeignKey(t => t.AccountFromId)
                   .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(t => t.AccountTo)
                   .WithMany(a => a.TransfersReceived)
                   .HasForeignKey(t => t.AccountToId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
