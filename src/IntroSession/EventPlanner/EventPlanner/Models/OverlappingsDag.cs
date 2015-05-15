using System;
using System.Collections.Generic;

namespace EventPlanner.Models
{
    public class OverlappingsDag
    {
        public DateTime Dag { get; set; }

        public IEnumerable<Evenement> Overlappingen { get; set; }
    }
}