using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.Models
{
    [Table("Evenement")]
    public class Evenement
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? Organisatorid { get; set; }

        public string Titel { get; set; }

        public string Omschrijving { get; set; }

        public DateTime? DatumBeslissing { get; set; }

        [StringLength(50)]
        public string Eigenaar { get; set; }

        public bool? Optie { get; set; }

        public bool? MuziekVergunning { get; set; }

        public ICollection<Periode> Periodes { get; set; }
    }
}