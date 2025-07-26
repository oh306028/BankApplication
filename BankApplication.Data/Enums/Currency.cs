using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Data.Enums
{
    public enum Currency
    {
        [Description("PLN")] 
        PolishZloty = 0,

        [Description("USD")]
        Dollar = 1, 

        [Description("EUR")]
        Euro = 2        
    }
}
