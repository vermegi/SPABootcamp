using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.Models
{
    [Table("straat")]
    public class Straat
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public int Id { get; set; }

        [Column("straatnaam", TypeName = "text")]
        public string Straatnaam { get; set; }

        [Column("postcode", TypeName = "text")]
        public string Postcode { get; set; }

        [Column("gemeente", TypeName = "text")]
        public string Gemeente { get; set; }

        public ICollection<Periode> Periodes { get; set; }
    }
}