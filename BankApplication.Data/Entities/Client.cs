using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Data.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public Guid PublicId { get; set; } = Guid.NewGuid();

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; }
        public string Phone { get; set; }   
        public string PESEL { get; set; }
        public bool IsActive { get; set; } 
        public DateTime CreatedDate { get; set; }
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Number { get; set; }

        //Unikalny kod klienta do rejestracji konta
        public string ClientCode { get; set; }


        //Rachunek klienta => moze miec ich kilka
        public virtual List<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();
        
        //Konto klienta
        public virtual Account Account { get; set; }
   
    }

    public class ClientConfigurationb : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");

            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(50);
            builder.Ignore(p => p.FullName);

            builder.Property(p => p.Country).IsRequired().HasMaxLength(30);
            builder.Property(p => p.City).IsRequired().HasMaxLength(50);
            builder.Property(p => p.PostalCode).IsRequired().HasMaxLength(20);
            builder.Property(p => p.Number).IsRequired().HasMaxLength(20);

            builder.Property(p => p.Email).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Phone).IsRequired().HasMaxLength(20);
            builder.Property(p => p.PESEL).IsRequired().HasMaxLength(20);
            builder.Property(p => p.CreatedDate)
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()");

            builder.Property(p => p.Nationality).IsRequired().HasMaxLength(50);
            builder.Property(p => p.BirthDate).IsRequired();


            builder.HasOne(a => a.Account)
                 .WithOne(c => c.Client)
                 .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(ba => ba.BankAccounts)
                .WithOne(c => c.Client)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(
               new Client()
               {
                   Id = 1,
                   FirstName = "Admin",
                   LastName = "Admin",
                   Email = "admin@admin.pl",
                   Phone = "",
                   PESEL = "",
                   IsActive = true,
                   BirthDate = new DateTime(2025,5,5),
                   Nationality = "Polski",
                   Country = "Polska",
                   PostalCode = "",
                   City = "Gliwice",
                   Number = "",
                   ClientCode = "UniqueCode42553211"
               });

        }

    }
}
