using RetroFlyreiser.Model;
using System.Collections.Generic;


namespace RetroFlyreiser.DAL
{
    public class BrukerStub : IBrukerDAL
    {

        public List<DBBRUKER> alleBrukere()
        {
            var brukerListe = new List<DBBRUKER>();
            var bruker = new DBBRUKER()
            {
                BRUKERNAVN = "TestBruker",
            };
            brukerListe.Add(bruker);
            brukerListe.Add(bruker);
            brukerListe.Add(bruker);
            return brukerListe;
        }

        public DBBRUKER hentEnBruker(string brukernavn)
        {
            if (brukernavn == "")
            {
                var bruker = new DBBRUKER()
                {
                    BRUKERNAVN = "Feil",
                };
                return bruker;
            }
            else
            {
                var bruker = new DBBRUKER()
                {
                    BRUKERNAVN = "TestBruker",
                };
                return bruker;
            }
        }



        public bool endreBruker(string brukernavn, DBBRUKER bruker)
        {
            if (brukernavn == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public bool settInnBruker(Bruker innBruker)
        {
            if (innBruker.Brukernavn == "")
            {
                return false;
            }
            return true;
        }


        public bool slett(string brukernavn)
        {
            if (brukernavn == "")
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public bool Bruker_i_DB(Bruker innBruker)
        {
            if (innBruker.Brukernavn == "")
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
