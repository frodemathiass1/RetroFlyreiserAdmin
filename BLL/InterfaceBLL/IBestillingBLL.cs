using System.Collections.Generic;
using RetroFlyreiser.Model;

namespace RetroFlyreiser.BLL
{
    public interface IBestillingBLL
    {
        List<Bestilling> alleBestillinger();
        bool endreBestilling(int Id, Bestilling innBestilling);
        Bestilling hentEnBestilling(int Id);
        bool settInnBestilling(Bestilling bestilling);
        bool slettBestilling(int Id);
    }
}