using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Data.Entities
{
    public class BankAccountBlockRequest    
    {
        public int Id { get; set; } 
        public int BankAccountId { get; set; }  
        public virtual BankAccount BankAccount { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? ManagedDate { get; set; }       
        public bool? IsAccepted { get; set; } 

        public int? AdminId { get; set; }    
        public Client Admin { get; set; }
        public bool IsActive { get; set; }      
    }

    public class BankAccountBlockRequestConfiguration : IEntityTypeConfiguration<BankAccountBlockRequest>
    {
        public void Configure(EntityTypeBuilder<BankAccountBlockRequest> builder)
        {
            builder.ToTable("BankAccountBlockRequests");

            builder.Property(n => n.BankAccountId).IsRequired();

            builder.Property(c => c.RequestDate).IsRequired();
            builder.Property(a => a.IsAccepted).IsRequired(false);
            builder.Property(a => a.ManagedDate).IsRequired(false);
            builder.Property(a => a.AdminId).IsRequired(false);
            builder.Property(a => a.RequestDate).IsRequired();

            builder.HasOne(p => p.BankAccount).WithOne(p => p.BlockadeRequest)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Admin).WithOne(p => p.BlockRequest)
                .OnDelete(DeleteBehavior.SetNull);

        }   
    }
}
