using System.Collections.Generic;

namespace EventPlanner.Models
{
    internal class ReservatieListResult 
    {
        public List<ReservatieDataRow> Rows { get; set; }
        public int NumberOfPages { get; set; }
    }
}