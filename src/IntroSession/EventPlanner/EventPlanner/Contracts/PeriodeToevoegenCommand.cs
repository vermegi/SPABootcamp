using System;
using System.Collections.Generic;

namespace EventPlanner.Contracts
{
    public class PeriodeToevoegenCommand : PeriodeRequestBase
    {
        public int EvenementId { get; set; }
        public IEnumerable<DateTime> DagenToSkip { get; set; }
    }
}