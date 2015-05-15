using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.Models
{
    [Table("Straat")]
    public class Straat
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Straatnaam { get; set; }

        public string Postcode { get; set; }

        public string Gemeente { get; set; }

        public ICollection<Periode> Periodes { get; set; }
    }
}