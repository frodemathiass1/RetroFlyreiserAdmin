using System.Collections.Generic;
using RetroFlyreiser.Model;


namespace RetroFlyreiser.DAL
{
    public interface IKundeDAL
    {
        List<Kunde> alleKunder();
        bool endreKunde(string Epost, Kunde innKunde);
        Kunde hentEnKunde(string Epost);
        bool settInnKunde(Kunde innKunde);
        bool slett(string Epost);
    }
}

