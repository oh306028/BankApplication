using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Data.Entities
{
    public class BecomeClientRequest
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        public int? AdminId { get; set; }    
        public virtual Client Admin { get; set; }   
        public DateTime RequestDate { get; set; }
        public DateTime? ManagedDate { get; set; }       
        public bool IsAccepted { get; set; }    
        public bool IsActive { get; set; }
        public Guid PublicId { get; set; } = Guid.NewGuid();

    }

    public class BecomeClientRequestConfiguration : IEntityTypeConfiguration<BecomeClientRequest>   
    {
        public void Configure(EntityTypeBuilder<BecomeClientRequest> builder)
        {
            builder.ToTable("BecomeClientRequests");

            builder.Property(n => n.ClientId).IsRequired();

            builder.Property(c => c.RequestDate).IsRequired();
            builder.Property(a => a.ManagedDate).IsRequired(false);
            builder.Property(a => a.AdminId).IsRequired(false);
            builder.Property(a => a.RequestDate).IsRequired();

            builder.HasOne(p => p.Client).WithOne(p => p.ClientRequest)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Admin).WithMany(p => p.ManagedRequests)
                .OnDelete(DeleteBehavior.SetNull);
            
        }
    }
}
