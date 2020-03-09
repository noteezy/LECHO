using System;
using System.Collections.Generic;

namespace LECHO.Infrastructure
{
    public partial class Period
    {
        public int PeriodId { get; set; }
        public DateTime PeriodBegining { get; set; }
        public DateTime PeriodEnd { get; set; }
    }
}
