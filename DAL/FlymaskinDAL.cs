using System;
using System.Collections.Generic;
using System.Linq;
using static RetroFlyreiser.Model.ErrorLogging;
using RetroFlyreiser.Model;


namespace RetroFlyreiser.DAL
{
    public class FlymaskinDAL : IFlymaskinDAL
    {

        public List<Flymaskin> alleFlymaskiner()
        {
            try
            {
                var db = new RetroDb();
                List<Flymaskin> alleFlymaskiner = db.Flymaskiner.ToList();
                return alleFlymaskiner;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return null;
            }                 
        }

        public bool slett(string FlyId)
        {
            var db = new RetroDb();            
            try
            {
                var slettFly = db.Flymaskiner.Find(FlyId);
                db.Flymaskiner.Remove(slettFly);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return false;
            }        
        }

        public bool endreFlymaskin(string FlyId, Flymaskin innFlymaskin)
        {
            var db = new RetroDb();
            try
            {
                Flymaskin endreFlymaskin = db.Flymaskiner.Find(FlyId);
                endreFlymaskin.FlyId = innFlymaskin.FlyId;
                endreFlymaskin.Type = innFlymaskin.Type;
                endreFlymaskin.Kapasitet = innFlymaskin.Kapasitet;
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                LogError(ex);
                return false;
            }
        }

        public Flymaskin hentEnFlymaskin(string FlyId)
        {
            var db = new RetroDb();
            var enDbFlymaskin = db.Flymaskiner.Find(FlyId);
            if (enDbFlymaskin == null)
            {
                LogErrorString(enDbFlymaskin.ToString());
                return null;
            }
            else
            {
                var utFlymaskin = new Flymaskin()
                {
                    FlyId = enDbFlymaskin.FlyId,
                    Type = enDbFlymaskin.Type
                };
                return utFlymaskin;
            }
        }

        public bool settInnFlymaskin(Flymaskin innFlymaskin)
        {
            var nyFly = new Flymaskin()
            {
                FlyId = innFlymaskin.FlyId,
                Type = innFlymaskin.Type,
                Kapasitet = innFlymaskin.Kapasitet
            };

            var db = new RetroDb();
            try
            {
                var eksistererFlymaskin = db.Flymaskiner.Find(innFlymaskin.FlyId);
                if (eksistererFlymaskin == null)
                db.Flymaskiner.Add(nyFly);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                LogError(ex);
                Console.Write("FlyId finnes allerede");
                return false;
            }
        }
    }
}
