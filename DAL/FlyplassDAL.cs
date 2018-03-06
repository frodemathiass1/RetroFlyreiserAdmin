using System;
using System.Collections.Generic;
using System.Linq;
using static RetroFlyreiser.Model.ErrorLogging;
using RetroFlyreiser.Model;

namespace RetroFlyreiser.DAL
{
    public class FlyplassDAL : DAL.IFlyplassDAL
    {

        public List<Flyplass> alleFlyplasser()
        {
            var db = new RetroDb();
            try
            {               
                List<Flyplass> alleFlyplasser = db.Flyplasser.ToList();
                return alleFlyplasser;
            }
            catch(Exception ex)
            {
                LogError(ex);
                return null;
            }
        }

        public bool slett(string FlyplassKode)
        {
            var db = new RetroDb();      
            try
            {
                var slettFlyplass = db.Flyplasser.Find(FlyplassKode);
                db.Flyplasser.Remove(slettFlyplass);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return false;
            }           
        }

        public bool endreFlyplass(string FlyplassKode, Flyplass innFlyplass)
        {
            var db = new RetroDb();
            try
            {
                Flyplass endreFlyplass = db.Flyplasser.Find(FlyplassKode);
                endreFlyplass.FlyplassKode = innFlyplass.FlyplassKode;
                endreFlyplass.By = innFlyplass.By;
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                LogError(ex);
                return false;
            }
        }

        public Flyplass hentEnFlyplass(string FlyplassKode)
        {
            var db = new RetroDb();
            var enDbFlyplass = db.Flyplasser.Find(FlyplassKode);
            if (enDbFlyplass == null)
            {
                LogErrorString(enDbFlyplass.ToString());
                return null;
            }
            else
            {
                var utFlyplass = new Flyplass()
                {
                    FlyplassKode = enDbFlyplass.FlyplassKode,
                    By = enDbFlyplass.By
                };
                return utFlyplass;
            }
        }

        public bool settInnFlyplass(Flyplass innFlyplass)
        {
            var nyFlyplass = new Flyplass()
            {
                FlyplassKode = innFlyplass.FlyplassKode,
                By = innFlyplass.By,
            };

            var db = new RetroDb();
            try
            {
                var eksistererFlyplass= db.Flyplasser.Find(innFlyplass.FlyplassKode);
                if (eksistererFlyplass == null)
                db.Flyplasser.Add(nyFlyplass);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return false;
            }
        }
    } 
}