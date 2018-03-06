using RetroFlyreiser.Model;
using static RetroFlyreiser.Model.ErrorLogging;
using System;
using System.Collections.Generic;
using System.Linq;


namespace RetroFlyreiser.DAL
{
    public class BestillingDAL : IBestillingDAL
    {
       
        public List<Bestilling> alleBestillinger()
        {
            var db = new RetroDb();
            try
            {
                List<Bestilling> alleBestillinger = db.Bestillinger.ToList();
                return alleBestillinger;
            }
            catch(Exception ex)
            {
                LogError(ex);
                return null;
            }                         
        }

        public bool slett(int Id)
        {
            var db = new RetroDb();            
            try
            {
                var slettBestilling = db.Bestillinger.Find(Id);
                db.Bestillinger.Remove(slettBestilling);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return false;
            }         
        }

        public bool endreBestilling(int Id, Bestilling innBestilling)
        {
            var db = new RetroDb();
            try
            {
                Bestilling endreBestilling = db.Bestillinger.Find(Id);
                endreBestilling.Rute.RuteId = innBestilling.Rute.RuteId;
                endreBestilling.Rute.ReiseFra.By = innBestilling.Rute.ReiseFra.By;
                endreBestilling.Rute.ReiseTil.By = innBestilling.Rute.ReiseTil.By;
                endreBestilling.Rute.Dato = innBestilling.Rute.Dato;
                endreBestilling.Rute.Tid = innBestilling.Rute.Tid;
                endreBestilling.Rute.ReiseTid = innBestilling.Rute.ReiseTid;
                endreBestilling.Rute.Pris = innBestilling.Rute.Pris;
                endreBestilling.Kunde.Fornavn = innBestilling.Kunde.Fornavn;
                endreBestilling.Kunde.Etternavn = innBestilling.Kunde.Etternavn;
                endreBestilling.Kunde.Adresse = innBestilling.Kunde.Adresse;
                endreBestilling.Kunde.Poststed.Postnr = innBestilling.Kunde.Poststed.Postnr;
                endreBestilling.Kunde.Poststed.Sted = innBestilling.Kunde.Poststed.Sted;
                endreBestilling.Kunde.Epost = innBestilling.Kunde.Epost;
                endreBestilling.Kunde.Telefon = innBestilling.Kunde.Telefon;
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                LogError(ex);
                return false;
            }
        }

        public Bestilling hentEnBestilling(int Id)
        {
            var db = new RetroDb();
            var enDbBestilling = db.Bestillinger.Find(Id);
            if (enDbBestilling == null)
            {
                LogErrorString(enDbBestilling.ToString());
                return null;
            }
            else
            {
                var utBestilling = new Bestilling()
                {
                    Id = enDbBestilling.Id,
                    Kunde = enDbBestilling.Kunde,
                    Rute = enDbBestilling.Rute
                };
                return utBestilling;
            }
        }

        public bool settInnBestilling(Bestilling innBestilling)
        {
            var nyBestilling = new Bestilling()
            {
                Id = innBestilling.Id,
                Kunde = innBestilling.Kunde,
                Rute = innBestilling.Rute
            };

            var db = new RetroDb();
            try
            {
                var eksistererBestilling = db.Bestillinger.Find(innBestilling.Id);
                if (eksistererBestilling == null)
                db.Bestillinger.Add(nyBestilling);
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