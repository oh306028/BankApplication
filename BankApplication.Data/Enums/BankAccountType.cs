using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Data.Enums
{
    public enum BankAccountType
    {
        [Description("Oszczędnościowy")]
        Saving,

        [Description("Bieżący")]
        Current,

        [Description("Kredytowy")]
        Credit
    }
}
