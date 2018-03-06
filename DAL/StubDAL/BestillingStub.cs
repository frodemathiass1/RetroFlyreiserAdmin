using RetroFlyreiser.Model;
using System.Collections.Generic;


namespace RetroFlyreiser.DAL
{
    public class BestillingStub : IBestillingDAL
    {

        public List<Bestilling> alleBestillinger()
        {
            // OK
            var reiseFra = new Flyplass() { FlyplassKode = "OSL", By = "Oslo" };
            var reiseTil = new Flyplass() { FlyplassKode = "KRS", By = "Kristiansand" };
            var flymaskin = new Flymaskin() { FlyId = "AA00", Type = "Boeing", Kapasitet = 100 };
            var rute = new Rute() {RuteId = "OSLKRS000", ReiseFra = reiseFra, ReiseTil = reiseTil, Dato = "10.10.2017", Tid = "10:00", ReiseTid = "10min", Flymaskin = flymaskin, Pris = 999};
            var poststed = new Poststed() { Postnr="1000",Sted="Oslo"};
            var kunde = new Kunde(){Fornavn = "Per", Etternavn = "Olsen", Adresse = "Osloveien 82", Poststed = poststed, Telefon = "12345678", Epost = "unittest@test.no", Aktiv = true };

            var bestillingListe = new List<Bestilling>();
            var bestilling = new Bestilling()
            {
                Id = 1,
                Rute = rute,
                Kunde = kunde
 
            };
            bestillingListe.Add(bestilling);
            bestillingListe.Add(bestilling);
            bestillingListe.Add(bestilling);
            return bestillingListe;
        }

        public Bestilling hentEnBestilling(int id)
        {
            // OK
            var reiseFra = new Flyplass() { FlyplassKode = "OSL", By = "Oslo" };
            var reiseTil = new Flyplass() { FlyplassKode = "KRS", By = "Kristiansand" };
            var flymaskin = new Flymaskin() { FlyId = "AA00", Type = "Boeing", Kapasitet = 100 };
            var rute = new Rute() { RuteId = "OSLKRS000", ReiseFra = reiseFra, ReiseTil = reiseTil, Dato = "10.10.2017", Tid = "10:00", ReiseTid = "10min", Flymaskin = flymaskin, Pris = 999 };
            var poststed = new Poststed() { Postnr = "1000", Sted = "Oslo" };
            var kunde = new Kunde() { Fornavn = "Per", Etternavn = "Olsen", Adresse = "Osloveien 82", Poststed = poststed, Telefon = "12345678", Epost = "unittest@test.no", Aktiv = true };

            // Feil
            var reiseFraFeil = new Flyplass() { FlyplassKode = "TST", By = "Feil" };
            var reiseTilFeil = new Flyplass() { FlyplassKode = "TST", By = "Feil" };
            var flymaskinFeil = new Flymaskin() { FlyId = "AA00", Type = "Boeing", Kapasitet = 100 };
            var ruteFeil = new Rute() { RuteId = "TSTTST000", ReiseFra = reiseFraFeil, ReiseTil = reiseTilFeil, Dato = "10.10.2017", Tid = "10:00", ReiseTid = "10min", Flymaskin = flymaskinFeil, Pris = 999 };
            var poststedFeil = new Poststed() { Postnr = "0000", Sted = "Feil" };
            var kundeFeil = new Kunde() { Fornavn = "Feil", Etternavn = "Feil", Adresse = "Osloveien 82", Poststed = poststedFeil, Telefon = "12345678", Epost = "unittest@test.no", Aktiv = true };


            if (id == 0)
            {
                var bestilling = new Bestilling()
                {
                    Id = 0,
                    Rute = ruteFeil,
                    Kunde = kundeFeil

                };
                return bestilling;
            }
            else
            {
                var bestilling = new Bestilling()
                {
                    Id = 1,
                    Rute = rute,
                    Kunde = kunde
                };
                return bestilling;
            }
        }

        public bool endreBestilling(int id, Bestilling innBestilling)
        {
            if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool settInnBestilling(Bestilling innBestilling)
        {
            if (innBestilling.Id == 0)
            {
                return false;
            }
            return true;
        }

        public bool slett(int Id)
        {
            if (Id == 0)
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
