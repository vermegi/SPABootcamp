using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Contracts
{
    public class BasisDataReservatieAanpassenCommand
    {
        [Required]
        public string Omschrijving { get; set; }

        public int Id { get; set; }
        public bool MuziekVergunning { get; set; }
    }
}