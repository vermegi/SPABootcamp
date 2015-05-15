using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.Models
{
    [Table("Dag")]
    public class Dag
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("periodeid")]
        public int PeriodeId { get; set; }

        [Column("datum", TypeName = "date")]
        public DateTime Datum { get; set; }

        [Column("beginuur", TypeName = "time")]
        public TimeSpan Beginuur { get; set; }

        [Column("einduur", TypeName = "time")]
        public TimeSpan Einduur { get; set; }

        [Column("opmerking", TypeName = "text")]
        public string Opmerking { get; set; }

        public Periode Periode { get; set; }
    }
}