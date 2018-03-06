using System.Collections.Generic;
using RetroFlyreiser.Model;

namespace RetroFlyreiser.BLL
{
    public interface IRuteBLL
    {
        List<Rute> alleRuter();
        bool endreRute(string ruteId, Rute innRute);
        Rute hentRute(string postnr);
        bool settInnRute(Rute innRute);
        bool slettRute(string ruteId);
    }
}