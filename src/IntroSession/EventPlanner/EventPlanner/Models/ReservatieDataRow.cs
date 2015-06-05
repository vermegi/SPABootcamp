using System;

namespace EventPlanner.Models
{
    public class ReservatieDataRow
    {
        public int Id { get; set; }
        public DateTime Reservatiedatum { get; set; }
        public string Titel { get; set; }
        public string Eigenaar { get; set; }
        public DateTime? DatumBeslissing { get; set; }
        public string Straten { get; set; }
    }
}