using System.Collections.Generic;
using RetroFlyreiser.Model;

namespace RetroFlyreiser.DAL
{
    public interface IFlymaskinDAL
    {
        List<Flymaskin> alleFlymaskiner();
        bool endreFlymaskin(string FlyId, Flymaskin innFlymaskin);
        Flymaskin hentEnFlymaskin(string FlyId);
        bool settInnFlymaskin(Flymaskin innFlymaskin);
        bool slett(string FlyId);
    }
}