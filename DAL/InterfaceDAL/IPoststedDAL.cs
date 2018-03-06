using System.Collections.Generic;
using RetroFlyreiser.Model;

namespace RetroFlyreiser.DAL
{
    public interface IPoststedDAL
    {
        List<Poststed> allePoststeder();
        bool endrePoststed(string Postnr, Poststed innPoststed);
        Poststed hentEtPoststed(string Postnr);
        bool settInnPoststed(Poststed innPoststed);
        bool slett(string Postnr);
    }
}