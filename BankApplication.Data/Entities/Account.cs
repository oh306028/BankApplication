using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Data.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public Guid PublicId { get; set; } = Guid.NewGuid();

        public int ClientId { get; set; }   
        public virtual Client Client { get; set; }

        public bool IsBlocked { get; set; }
        public int WrongLoginAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ClosedDate { get; set; }

        public string Login { get; set; }
        public string PasswordHash { get; set; }

        public bool IsDoubleAuthenticated { get; set; } = true;            

        public virtual Blockade Blockade { get; set; }
        public virtual Authentication Authentication { get; set; }
        public virtual List<Logging> Loggings { get; set; } 

        public bool IsEmployee { get; set; }

    }

    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");

            builder.Property(w => w.WrongLoginAmount).IsRequired();
            builder.Property(b => b.IsBlocked).IsRequired();
            builder.Property(d => d.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(l => l.Login).IsRequired().HasMaxLength(150);
            builder.Property(p => p.PasswordHash).IsRequired();
            builder.Property(p => p.IsDoubleAuthenticated).IsRequired();

            builder.HasOne(b => b.Blockade).WithOne(a => a.Account);
            builder.HasOne(b => b.Authentication).WithOne(a => a.Account);
            builder.HasMany(b => b.Loggings).WithOne(a => a.Account);

            builder.Property(e => e.IsEmployee).IsRequired();

            builder.HasData(
                new Account()
                {
                    Id = 1,
                    ClientId = 1,
                    IsBlocked = false,
                    WrongLoginAmount = 0,
                    Login = "adminTab",
                    PasswordHash = "AQAAAAIAAYagAAAAELBQ1+PDtUJPc31emYY8MdjP9zirLtLeWppq20jtNO5ZgkKvg0sCJ5elSbDPJULweA==",
                    IsEmployee = true

                });

         
        }
    }
}
