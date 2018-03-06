using RetroFlyreiser.Model;
using System.Collections.Generic;


namespace RetroFlyreiser.DAL
{
    public class FlymaskinStub : IFlymaskinDAL
    {
        public List<Flymaskin> alleFlymaskiner()
        {
            var flymaskinListe = new List<Flymaskin>();
            var flymaskin = new Flymaskin()
            {
                FlyId = "AA00",
                Type = "Boeing",
                Kapasitet = 100
            };
            flymaskinListe.Add(flymaskin);
            flymaskinListe.Add(flymaskin);
            flymaskinListe.Add(flymaskin);
            return flymaskinListe;
        } 

        public Flymaskin hentEnFlymaskin(string flyId)
        {
            if (flyId == "")
            {
                var flymaskin = new Flymaskin();
                flymaskin.FlyId = "TST";
                flymaskin.Type = "";
                flymaskin.Kapasitet = 0;
                return flymaskin;
            }
            else
            {
                var flymaskin = new Flymaskin()
                {
                    FlyId = "AA00",
                    Type = "Boeing",
                    Kapasitet = 100
                };
                return flymaskin;
            }
        }


        public bool endreFlymaskin(string flyId, Flymaskin innFlymaskin)
        {
            if (flyId == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool settInnFlymaskin(Flymaskin innFlymaskin)
        {
            if (innFlymaskin.FlyId == "")
            {
                return false;
            }
            return true;
        }

        public bool slett(string flyId)
        {
            if (flyId == "")
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}
