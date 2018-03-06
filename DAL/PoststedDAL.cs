using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using static RetroFlyreiser.Model.ErrorLogging;
using RetroFlyreiser.Model;


namespace RetroFlyreiser.DAL
{
    public class PoststedDAL : IPoststedDAL
    {

        public List<Poststed> allePoststeder()
        {
            try
            {
                var db = new RetroDb();
                List<Poststed> allePoststeder = db.Poststeder.ToList();
                return allePoststeder;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return null;
            }
        }

        public bool slett(string Postnr)
        {
            var db = new RetroDb();
            try
            {
                var slettPoststed = db.Poststeder.Find(Postnr);
                db.Poststeder.Remove(slettPoststed);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return false;
            }
            
        }

        public bool endrePoststed(string Postnr, Poststed innPoststed)
        {
            var db = new RetroDb();            
            try
            {
                Poststed endrePoststed = db.Poststeder.Find(Postnr);
                endrePoststed.Postnr = innPoststed.Postnr;
                endrePoststed.Sted = innPoststed.Sted;
                if (endrePoststed.Postnr != innPoststed.Postnr)
                {
                    // Postnummeret er endret. Må først sjekke om det nye postnummeret eksisterer i tabellen.
                    Poststed eksisterendePoststed = db.Poststeder.Find(innPoststed.Sted);
                    if (eksisterendePoststed == null)
                    {
                        // poststedet eksisterer ikke, må legges inn
                        var nyttPoststed = new Poststed()
                        {
                            Postnr = innPoststed.Postnr,
                            Sted = innPoststed.Sted
                        };
                        db.Poststeder.Add(nyttPoststed);
                    }
                    else
                    {   // Ønsket funksjonalitet - poststedet med det nye postnr eksisterer, endre bare postnummeret til kunden.                        
                        // Er kun mulig å endre poststed, men ikke postnummer pga. postnummer er 'PrimaryKey'
                        endrePoststed = eksisterendePoststed;
                    }
                };
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return false;
            }          
        }

        public Poststed hentEtPoststed(string Postnr)
        {
            var db = new RetroDb();
            var enDbPoststed = db.Poststeder.Find(Postnr);
            if (enDbPoststed == null)
            {
                LogErrorString(enDbPoststed.ToString());
                return null;
            }
            else
            {
                var utPoststed = new Poststed()
                {
                    Postnr = enDbPoststed.Postnr,
                    Sted = enDbPoststed.Sted
                };
                return utPoststed;
            }
        }

        public bool settInnPoststed(Poststed innPoststed)
        {
            var nyPoststed = new Poststed()
            {
                Postnr = innPoststed.Postnr,
                Sted = innPoststed.Sted
            };

            var db = new RetroDb();
            try
            {
                var eksistererPostnr = db.Poststeder.Find(innPoststed.Postnr);
                if (eksistererPostnr == null)
                {
                    var nyttPoststed = new Poststed()
                    {
                        Postnr = innPoststed.Postnr,
                        Sted = innPoststed.Sted
                    };
                    nyPoststed = nyttPoststed;
                }
                db.Poststeder.Add(nyPoststed);
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