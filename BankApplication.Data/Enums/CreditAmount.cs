using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Data.Enums
{
    public enum CreditAmount
    {   
        [Description("10 000")]
        TenThousand = 10000,

        [Description("50 000")]
        FiftyThousand = 50000,

        [Description("1 000 000")]
        OneMilion = 1000000   
    }   
}
