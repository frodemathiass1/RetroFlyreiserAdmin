using System.Collections.Generic;
using RetroFlyreiser.Model;

namespace RetroFlyreiser.DAL
{
    public interface IBestillingDAL
    {
        List<Bestilling> alleBestillinger();
        bool endreBestilling(int Id, Bestilling innBestilling);
        Bestilling hentEnBestilling(int Id);
        bool settInnBestilling(Bestilling innBestilling);
        bool slett(int Id);
    }
}