using System.Collections.Generic;
using RetroFlyreiser.Model;

namespace RetroFlyreiser.BLL
{
    public interface IBrukerBLL
    {
        List<DBBRUKER> alleBrukere();
        bool bruker_i_db(Bruker bruker);
        bool endreBruker(string brukernavn, DBBRUKER innBruker);
        DBBRUKER hentEnBruker(string brukernavn);
        byte[] lagHash(string innPassord);
        bool settInnBruker(Bruker innBruker);
        bool slettBruker(string brukernavn);
    }
}