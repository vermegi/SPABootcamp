using System;
using System.Collections.Generic;
using System.Linq;

namespace EventPlanner.Contracts
{
    public class PeriodeRequestBase
    {
        private List<DayOfWeek> _checkedDays;
        public DateTime BeginDatumPeriode { get; set; }
        public DateTime EindDatumPeriode { get; set; }
        public Boolean Alle_Dagen { get; set; }
        public Boolean Maandag { get; set; }
        public Boolean Dinsdag { get; set; }
        public Boolean Woensdag { get; set; }
        public Boolean Donderdag { get; set; }
        public Boolean Vrijdag { get; set; }
        public Boolean Zaterdag { get; set; }
        public Boolean Zondag { get; set; }
        public IEnumerable<int> GeselecteerdeStraten { get; set; }

        public IEnumerable<DayOfWeek> CheckedDays
        {
            get { return _checkedDays ?? GenereerCheckedDays(); }
        }

        public int Id { get; set; }

        private List<DayOfWeek> GenereerCheckedDays()
        {
            _checkedDays = new List<DayOfWeek>();
            if (Maandag || Alle_Dagen)
                _checkedDays.Add(DayOfWeek.Monday);
            if (Dinsdag || Alle_Dagen)
                _checkedDays.Add(DayOfWeek.Tuesday);
            if (Woensdag || Alle_Dagen)
                _checkedDays.Add(DayOfWeek.Wednesday);
            if (Donderdag || Alle_Dagen)
                _checkedDays.Add(DayOfWeek.Thursday);
            if (Vrijdag || Alle_Dagen)
                _checkedDays.Add(DayOfWeek.Friday);
            if (Zaterdag || Alle_Dagen)
                _checkedDays.Add(DayOfWeek.Saturday);
            if (Zondag || Alle_Dagen)
                _checkedDays.Add(DayOfWeek.Sunday);
            return _checkedDays;
        }

        internal IEnumerable<DateTime> GenereerDagen()
        {
            var dagen = new List<DateTime>();

            foreach (DateTime weekdag in GetDays())
            {
                if (HasChecked(weekdag))
                {
                    dagen.Add(weekdag);
                }
            }
            return dagen;
        }

        internal IEnumerable<DateTime> GetDays()
        {
            DateTime startDag = BeginDatumPeriode;
            while (startDag < EindDatumPeriode)
            {
                yield return startDag;
                startDag = startDag.AddDays(1);
            }
        }

        internal bool HasChecked(DateTime weekdag)
        {
            return CheckedDays.Any(d => d == weekdag.DayOfWeek);
        }
    }
}