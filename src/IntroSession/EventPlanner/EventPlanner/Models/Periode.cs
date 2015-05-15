using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.Models
{
    [Table("Periode")]
    public class Periode
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int EvenementId { get; set; }

        public DateTime BeginPeriode { get; set; }

        public DateTime EindePeriode { get; set; }

        public string Opmerking { get; set; }

        public Evenement Evenement { get; set; }
        public ICollection<Dag> Dagen { get; set; }
        public ICollection<Straat> Straten { get; set; }
    }
}