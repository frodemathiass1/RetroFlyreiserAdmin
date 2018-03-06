using System.Collections.Generic;
using RetroFlyreiser.Model;

namespace RetroFlyreiser.BLL
{
    public interface IKundeBLL
    {
        List<Kunde> alleKunder();
        bool endreKunde(string epost, Kunde innKunde);
        Kunde hentKunde(string epost);
        bool settInnKunde(Kunde innKunde);
        bool slettKunde(string epost);
    }
}