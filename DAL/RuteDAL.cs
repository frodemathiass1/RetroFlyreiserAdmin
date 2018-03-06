using System;
using System.Collections.Generic;
using System.Linq;
using static RetroFlyreiser.Model.ErrorLogging;
using RetroFlyreiser.Model;


namespace RetroFlyreiser.DAL
{
    public class RuteDAL : IRuteDAL
    {
        public List<Rute> alleRuter()
        {
            var db = new RetroDb();
            try
            {              
                List<Rute> alleRuter = db.Ruter.ToList();
                return alleRuter;
            }
            catch(Exception ex)
            {
                LogError(ex);
                return null;
            }      
        }

        public bool slett(string RuteId)
        {
            var db = new RetroDb();          
            try
            {
                var slettRute = db.Ruter.Find(RuteId);
                db.Ruter.Remove(slettRute);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return false;
            }           
        }

        public bool endreRute(string RuteId, Rute innRute)
        {
            var db = new RetroDb();
            try
            {
                Rute endreRute = db.Ruter.Find(RuteId);
                endreRute.RuteId = innRute.RuteId;
                endreRute.ReiseFra.By = innRute.ReiseFra.By;
                endreRute.ReiseTil.By = innRute.ReiseTil.By;
                endreRute.Tid = innRute.Tid;
                endreRute.Dato = innRute.Dato;
                endreRute.ReiseTid = innRute.ReiseTid;
                endreRute.Flymaskin.Kapasitet = innRute.Flymaskin.Kapasitet;
                endreRute.Pris = innRute.Pris;
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                LogError(ex);
                return false;
            }

        }

        public Rute hentEnRute(string RuteId)
        {
            var db = new RetroDb();
            var enDbRute = db.Ruter.Find(RuteId);
            if (enDbRute == null)
            {
                LogErrorString(enDbRute.ToString());
                return null;
            }
            else
            {
                var utRute = new Rute()
                {
                    RuteId = enDbRute.RuteId,
                    ReiseFra = enDbRute.ReiseFra,
                    ReiseTil = enDbRute.ReiseTil,
                    Tid = enDbRute.Tid,
                    Dato = enDbRute.Dato,
                    ReiseTid = enDbRute.ReiseTid,
                    Flymaskin = enDbRute.Flymaskin,
                    Pris = enDbRute.Pris
                };
                return utRute;
            }
        }

        public bool settInnRute(Rute innRute)
        {
            var nyRute= new Rute()
            {
                RuteId = innRute.RuteId,
                ReiseFra = innRute.ReiseFra,
                ReiseTil= innRute.ReiseTil,
                Tid= innRute.Tid,
                Dato = innRute.Dato,
                ReiseTid = innRute.ReiseTid,
                Flymaskin= innRute.Flymaskin,
                Pris = innRute.Pris
            };

            var db = new RetroDb();
            try
            {
                var eksistererRuteId = db.Ruter.Find(innRute.RuteId);
                if (eksistererRuteId == null)               
                db.Ruter.Add(nyRute);
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