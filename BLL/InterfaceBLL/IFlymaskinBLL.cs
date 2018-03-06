using System.Collections.Generic;
using RetroFlyreiser.Model;

namespace RetroFlyreiser.BLL
{
    public interface IFlymaskinBLL
    {
        List<Flymaskin> alleFlymaskiner();
        bool endreFlymaskin(string flyd, Flymaskin innFlymaskin);
        Flymaskin hentFlymaskin(string flyId);
        bool settInnFlymaskin(Flymaskin innFlymaskin);
        bool slettFlymaskin(string flyId);
    }
}