using RetroFlyreiser.Model;
using System.Collections.Generic;


namespace RetroFlyreiser.DAL
{
    public class RuteStub : IRuteDAL
    {

        public List<Rute> alleRuter()
        {
            // OK
            var reiseFra = new Flyplass() { FlyplassKode = "OSL", By = "Oslo" };
            var reiseTil = new Flyplass() { FlyplassKode = "KRS", By = "Kristiansand" };
            var flymaskin = new Flymaskin() { FlyId = "AA00", Type = "Boeing", Kapasitet = 100 };

            var ruteListe = new List<Rute>();
            var rute = new Rute()
            {
                RuteId = "OSLKRS000",
                ReiseFra = reiseFra,
                ReiseTil = reiseTil,
                Dato = "10.10.2017",
                Tid = "10:00",
                ReiseTid = "10min",
                Flymaskin = flymaskin,
                Pris = 999
            };
            ruteListe.Add(rute);
            ruteListe.Add(rute);
            ruteListe.Add(rute);
            return ruteListe;
        }

        public Rute hentEnRute(string ruteId)
        {
            // OK
            var reiseFra = new Flyplass(){ FlyplassKode = "OSL", By="Oslo" };
            var reiseTil = new Flyplass() { FlyplassKode = "KRS", By = "Kristiansand" };
            var flymaskin = new Flymaskin() { FlyId = "AA00", Type="Boeing", Kapasitet= 100 };

            // Feil
            var reiseFraFeil = new Flyplass() { FlyplassKode = "TST", By = "Feil" };
            var reiseTilFeil = new Flyplass() { FlyplassKode = "TST", By = "Feil" };
            var flymaskinFeil = new Flymaskin() { FlyId = "TS00", Type = "Feil", Kapasitet = 1 };

            if (ruteId == "")
            {
                var rute = new Rute()
                {
                    RuteId = "TSTTST000",
                    ReiseFra = reiseFraFeil,
                    ReiseTil= reiseTilFeil,
                    Dato="00.00.0000",
                    Tid= "00:00",
                    ReiseTid = "0min",
                    Flymaskin = flymaskinFeil,
                    Pris = 999
                };
                return rute;
            }
            else
            {
                var rute = new Rute()
                {
                    RuteId = "OSLKRS000",
                    ReiseFra = reiseFra,
                    ReiseTil = reiseTil,
                    Dato = "10.10.2017",
                    Tid = "10:00",
                    ReiseTid = "10min",
                    Flymaskin = flymaskin,
                    Pris = 999
                };
                return rute;
            }
        }



        public bool endreRute(string ruteId, Rute innRute)
        {
            if (ruteId == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public bool settInnRute(Rute innRute)
        {
            if (innRute.RuteId == "")
            {
                return false;
            }
            return true;
        }


        public bool slett(string ruteId)
        {
            if (ruteId == "")
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}
