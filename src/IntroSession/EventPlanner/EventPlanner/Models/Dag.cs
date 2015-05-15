using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.Models
{
    [Table("Dag")]
    public class Dag
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int PeriodeId { get; set; }

        public DateTime Datum { get; set; }

        public string Beginuur { get; set; }

        public string Einduur { get; set; }

        public string Opmerking { get; set; }

        public Periode Periode { get; set; }
    }
}