using BankApplication.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Data.Entities
{
    public class Authentication
    {
        public int Id { get; set; }
        public Guid PublicId { get; set; }

        public int AccountId { get; set; }
        public virtual Account Account { get; set; }

        public int Type { get; set; }

        public AuthenticationType DisplayType => (AuthenticationType)Type;

    }
}
