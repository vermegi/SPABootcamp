using System;
using System.Collections.Generic;
using EventPlanner.Models;

namespace EventPlanner.Contracts
{
    public class ReservatieResdata
    {
        public Evenement Evenement { get; set; }
        public DateTime Reservatiedatum { get; set; }
        public List<Straat> Straten { get; set; }
    }
}