using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.Models
{
    [Table("Periode")]
    public class Periode
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("vergunningid")]
        public int VergunningId { get; set; }

        [Column("beginperiode", TypeName = "date")]
        public DateTime BeginPeriode { get; set; }

        [Column("eindeperiode", TypeName = "date")]
        public DateTime EindePeriode { get; set; }

        [Column("opmerking", TypeName = "text")]
        public string Opmerking { get; set; }

        public Evenement Evenement { get; set; }
        public ICollection<Dag> Dagen { get; set; }
        public ICollection<Straat> Straten { get; set; }
    }
}