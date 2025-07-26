using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Data.Enums
{
    public enum InterestRate    
    {
        [Description("1%")]
        OnePercent = 1,

        [Description("2%")]
        TwoPercent = 2,

        [Description("5%")]
        FivePercent = 5
    }
}
