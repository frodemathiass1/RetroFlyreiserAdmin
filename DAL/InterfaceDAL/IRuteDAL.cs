using System.Collections.Generic;
using RetroFlyreiser.Model;

namespace RetroFlyreiser.DAL
{
    public interface IRuteDAL
    {
        List<Rute> alleRuter();
        bool endreRute(string RuteId, Rute innRute);
        Rute hentEnRute(string RuteId);
        bool settInnRute(Rute innRute);
        bool slett(string RuteId);
    }
}