using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using static RetroFlyreiser.Model.ErrorLogging;
using RetroFlyreiser.Model;

namespace RetroFlyreiser.DAL
{
    public class KundeDAL : IKundeDAL
    {

        public List<Kunde> alleKunder()
        {
            try
            {
                var db = new RetroDb();
                List<Kunde> alleKunder = db.Kunder.ToList();
                return alleKunder;
            }
            catch(Exception ex)
            {
                LogError(ex);
                return null;
            }             
        }

        public bool endreKunde(string Epost, Kunde innKunde)
        {
            var db = new RetroDb();
            try
            {
                Kunde endreKunde = db.Kunder.Find(Epost);
                endreKunde.Fornavn = innKunde.Fornavn;
                endreKunde.Etternavn = innKunde.Etternavn;
                endreKunde.Adresse = innKunde.Adresse;
                endreKunde.Poststed.Postnr = innKunde.Poststed.Postnr;
                endreKunde.Poststed.Sted = innKunde.Poststed.Sted;
                endreKunde.Telefon = innKunde.Telefon;
                endreKunde.Epost = innKunde.Epost;
                endreKunde.Aktiv = innKunde.Aktiv;
                if (endreKunde.Poststed.Postnr != innKunde.Poststed.Postnr)
                {
                    // Postnummeret er endret. Må først sjekke om det nye postnummeret eksisterer i tabellen.
                    Poststed eksisterendePoststed = db.Poststeder.Find(innKunde.Poststed);
                    if (eksisterendePoststed == null)
                    {
                        // poststedet eksisterer ikke, må legges inn
                        var nyttPoststed = new Poststed()
                        {
                            Postnr = innKunde.Poststed.Postnr,
                            Sted = innKunde.Poststed.Sted
                        };
                        db.Poststeder.Add(nyttPoststed);
                    }
                    else
                    {   // Ønsket funksjonalitet - Endre poststed og postnummer.                        
                        // Er kun mulig å endre poststed, men ikke postnummer pga. postnummer er 'PrimaryKey'
                        // Må evt. populere tabell med postnummer og poststed, men dette er ikke gjort pga mye merarbied.
                        endreKunde.Poststed = eksisterendePoststed;
                    }
                };
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                LogError(ex);
                return false;
            }
            
        }

        public Kunde hentEnKunde(string Epost)
        {
            var db = new RetroDb();
            
                var enDbKunde = db.Kunder.Find(Epost);
                if (enDbKunde == null)
                {
                    LogErrorString(enDbKunde.ToString());   
                    return null;
                }
                else
                {
                    var utKunde = new Kunde()
                    {
                        Fornavn = enDbKunde.Fornavn,
                        Etternavn = enDbKunde.Etternavn,
                        Adresse = enDbKunde.Adresse,
                        Poststed = enDbKunde.Poststed,
                        Telefon = enDbKunde.Telefon,
                        Epost = enDbKunde.Epost,
                        Aktiv = enDbKunde.Aktiv
                    };
                    return utKunde;
                }          
        }

        public bool settInnKunde(Kunde innKunde)
        {
            var nyKunde = new Kunde()
            {
                Fornavn = innKunde.Fornavn,
                Etternavn = innKunde.Etternavn,
                Adresse = innKunde.Adresse,
                Poststed = innKunde.Poststed,
                Telefon = innKunde.Telefon,
                Epost = innKunde.Epost,
                Aktiv = innKunde.Aktiv,
            };

            var db = new RetroDb();          
                try
                {
                    var eksistererPostnr = db.Poststeder.Find(innKunde.Poststed.Postnr);
                    if (eksistererPostnr == null)
                    {
                        var nyttPoststed = new Poststed()
                        {
                            Postnr = innKunde.Poststed.Postnr,
                            Sted = innKunde.Poststed.Sted
                        };
                        nyKunde.Poststed = nyttPoststed;
                    }
                    db.Kunder.Add(nyKunde);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    LogError(e);
                    return false;
                }            
        }

        public bool slett(string Epost)
        {
            var db = new RetroDb();
            try
            {
                var slettKunde = db.Kunder.Find(Epost);
                db.Kunder.Remove(slettKunde);
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