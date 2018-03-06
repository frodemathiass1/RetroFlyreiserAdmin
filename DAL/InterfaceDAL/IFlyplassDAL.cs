using System.Collections.Generic;
using RetroFlyreiser.Model;
using System;

namespace RetroFlyreiser.DAL
{
    public interface IFlyplassDAL
    {
        List<Flyplass> alleFlyplasser();
        bool endreFlyplass(string FlyplassKode, Flyplass innFlyplass);
        Flyplass hentEnFlyplass(string FlyplassKode);
        bool settInnFlyplass(Flyplass innFlyplass);
        bool slett(string FlyplassKode);
    }
}