using EventPlanner.Models;

namespace EventPlanner.Contracts
{
    public class EvenementWithMessage
    {
        public Evenement Evenement { get; set; }
        public string Message { get; set; }
    }
}