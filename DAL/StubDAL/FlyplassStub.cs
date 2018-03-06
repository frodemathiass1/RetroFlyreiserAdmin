using RetroFlyreiser.Model;
using System.Collections.Generic;


namespace RetroFlyreiser.DAL
{
    public class FlyplassStub : IFlyplassDAL
    {
        public List<Flyplass> alleFlyplasser()
        {
            var flyplassListe = new List<Flyplass>();
            var flyplass = new Flyplass()
            {
                FlyplassKode = "OSL",
                By = "Oslo"
            };
            flyplassListe.Add(flyplass);
            flyplassListe.Add(flyplass);
            flyplassListe.Add(flyplass);
            return flyplassListe;
        }

        public Flyplass hentEnFlyplass(string flyplassKode)
        {
            if (flyplassKode == "")
            {
                var flyplass = new Flyplass();
                flyplass.FlyplassKode = "TST";
                flyplass.By = "Test";
                return flyplass;
            }
            else
            {
                var flyplass = new Flyplass()
                {
                    FlyplassKode = "OSL",
                    By = "Oslo"
                };
                return flyplass;
            }
        }

        public bool endreFlyplass(string flyplassKode, Flyplass innFlyplass)
        {
            if (flyplassKode == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
 
        public bool settInnFlyplass(Flyplass innFlyplass)
        {
            if (innFlyplass.FlyplassKode == "")
            {
                return false;
            }
            return true;
        }

        public bool slett(string flyplassKode)
        {
            if (flyplassKode == "")
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

