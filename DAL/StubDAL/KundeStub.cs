using RetroFlyreiser.Model;
using System.Collections.Generic;


namespace RetroFlyreiser.DAL
{
    public class KundeStub : IKundeDAL
    {
        private Poststed poststed = new Poststed()
        {
            Postnr = "1000",
            Sted = "Oslo"
        };

        public List<Kunde> alleKunder()
        {
            var kundeListe = new List<Kunde>();
            var kunde = new Kunde()
            {
                Fornavn = "Per",
                Etternavn = "Olsen",
                Adresse = "Osloveien 82",
                Poststed = poststed,
                Telefon = "12345678",
                Epost = "unittest@test.no",
                Aktiv = true
            };
            kundeListe.Add(kunde);
            kundeListe.Add(kunde);
            kundeListe.Add(kunde);
            return kundeListe;
        }

        public Kunde hentEnKunde(string epost)
        {
            if (epost == "")
            {
                var kunde = new Kunde();
                kunde.Epost = "null@test.no";
                kunde.Fornavn ="test";
                kunde.Etternavn = "testesen";
                kunde.Adresse = "testveien";
                kunde.Poststed = poststed;
                kunde.Telefon = "22334455";
                kunde.Aktiv = false;


                return kunde;
            }
            else
            {
                var kunde = new Kunde()
                {
                    Fornavn = "Per",
                    Etternavn = "Olsen",
                    Adresse = "Osloveien 82",
                    Poststed = poststed,
                    Telefon = "12345678",
                    Epost = "unittest@test.no",
                    Aktiv = true
                };
                return kunde;
            }
        }

        public bool endreKunde(string epost, Kunde innKunde)
        {
            if (epost == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool settInnKunde(Kunde innKunde)
        {
            if (innKunde.Epost == "")
            {
                return false;
            }
            return true;
        }

        public bool slett(string epost)
        {
            if (epost == "")
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

