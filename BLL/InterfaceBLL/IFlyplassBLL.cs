using System.Collections.Generic;
using RetroFlyreiser.Model;

namespace RetroFlyreiser.BLL
{
    public interface IFlyplassBLL
    {
        List<Flyplass> alleFlyplasser();
        bool endreFlyplass(string flyplassKode, Flyplass innFlyplass);
        Flyplass hentFlyplass(string flyplassKode);
        bool settInnFlyplass(Flyplass innFlyplass);
        bool slettFlyplass(string flyplassKode);
    }
}