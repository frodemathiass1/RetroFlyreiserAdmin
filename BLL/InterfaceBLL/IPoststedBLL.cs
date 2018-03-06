using System.Collections.Generic;
using RetroFlyreiser.Model;

namespace RetroFlyreiser.BLL
{
    public interface IPoststedBLL
    {
        List<Poststed> allePoststeder();
        bool endrePoststed(string postnr, Poststed innPoststed);
        Poststed hentPoststed(string postnr);
        bool settInnPoststed(Poststed innPoststed);
        bool slettPoststed(string postnr);
    }
}