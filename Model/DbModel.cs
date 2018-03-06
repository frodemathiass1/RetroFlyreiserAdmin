using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetroFlyreiser.Model
{
    public class DbModel
    {
        public IEnumerable<Kunde> Kunder { get; set; }
        public IEnumerable<Poststed> Poststeder { get; set; }
        public IEnumerable<Rute> Ruter { get; set; }
        public IEnumerable<Flyplass> Flyplasser { get; set; }
        public IEnumerable<Flymaskin> Flymaskiner { get; set; }
        public IEnumerable<Bestilling> Bestillinger { get; set; }
        public IEnumerable<DBBRUKER> Brukere { get; set; }

    }
}