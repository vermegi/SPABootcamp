using System.Collections.Generic;

namespace EventPlanner.Models
{
    public class ReservatieListResult
    {
        public int NumberOfPages { get; set; }
        public List<ReservatieDataRow> Rows { get; set; }
    }
}