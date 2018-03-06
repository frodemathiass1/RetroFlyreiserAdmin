using System.Collections.Generic;
using RetroFlyreiser.Model;

namespace RetroFlyreiser.DAL
{
    public interface IBrukerDAL
    {
        List<DBBRUKER> alleBrukere();
        bool Bruker_i_DB(Bruker innBruker);
        bool endreBruker(string brukernavn, DBBRUKER innBruker);
        DBBRUKER hentEnBruker(string brukernavn);
        bool settInnBruker(Bruker innBruker);
        bool slett(string Brukernavn);
    }
}