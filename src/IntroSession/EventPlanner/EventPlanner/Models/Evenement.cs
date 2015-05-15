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
        [Column("id")]
        public int Id { get; set; }

        [Column("organisatorid")]
        public int? Organisatorid { get; set; }

        [Column("titel", TypeName = "text")]
        public string Titel { get; set; }

        [Column("omschrijving", TypeName = "text")]
        public string Omschrijving { get; set; }

        [Column("datumbeslissing", TypeName = "date")]
        public DateTime? DatumBeslissing { get; set; }

        [Column("eigenaar")]
        [StringLength(50)]
        public string Eigenaar { get; set; }

        [Column("optie")]
        public bool? Optie { get; set; }

        [Column("muziekvergunning")]
        public bool? MuziekVergunning { get; set; }

        public ICollection<Periode> Periodes { get; set; }
    }
}