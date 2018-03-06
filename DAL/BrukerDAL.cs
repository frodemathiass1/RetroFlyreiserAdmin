using System;
using System.Collections.Generic;
using System.Linq;
using static RetroFlyreiser.Model.ErrorLogging;
using RetroFlyreiser.Model;
using System.Security.Cryptography;

namespace RetroFlyreiser.DAL
{

    public class BrukerDAL : IBrukerDAL
    {

        public static byte[] lagHash(string innPassord, byte[] innSalt)
        {
            const int keyLength = 24;
            var pbkdf2 = new Rfc2898DeriveBytes(innPassord, innSalt, 1000); // Hashes 1000 ganger
            return pbkdf2.GetBytes(keyLength);
        }
        public static byte[] lagSalt()
        {
            var csprng = new RNGCryptoServiceProvider();
            var salt = new byte[24];
            csprng.GetBytes(salt);
            return salt;
        }

        public List<DBBRUKER> alleBrukere()
        {
            var db = new RetroDb();
            try
            {
                
                var alleBrukere = db.Brukere.ToList();
                return alleBrukere;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return null;
            }
        }

        public bool slett(string Brukernavn)
        {
            var db = new RetroDb();          
                try
                {
                    DBBRUKER slettBruker = db.Brukere.Find(Brukernavn);
                    db.Brukere.Remove(slettBruker);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    LogError(ex);
                    return false;
                }           
        }


        public bool endreBruker(string brukernavn, DBBRUKER innBruker)
        {
            var db = new RetroDb();
            try
            {
                DBBRUKER endreBruker = db.Brukere.Find(brukernavn);
                endreBruker.BRUKERNAVN = innBruker.BRUKERNAVN;
                endreBruker.PASSORD = innBruker.PASSORD;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return false;
            }
        }

        public DBBRUKER hentEnBruker(string brukernavn)
        {
            var db = new RetroDb();
            DBBRUKER enDbBruker = db.Brukere.Find(brukernavn);
            if (enDbBruker == null)
            {
                LogErrorString(enDbBruker.ToString());
                return null;
            }
            else
            {
                var utBruker = new DBBRUKER()
                {
                    BRUKERNAVN = enDbBruker.BRUKERNAVN,
                    PASSORD = enDbBruker.PASSORD
                };
                return utBruker;
            }
        }

        public bool settInnBruker(Bruker innBruker)
        {
            var db = new RetroDb();
            try
            {
                var nyBruker = new DBBRUKER();
                byte[] salt = lagSalt();
                byte[] passordDb = lagHash(innBruker.Passord,salt);
                nyBruker.SALT = salt;
                nyBruker.PASSORD = passordDb;
                nyBruker.BRUKERNAVN = innBruker.Brukernavn;
                db.Brukere.Add(nyBruker);

                var eksistererBruker = db.Brukere.Find(innBruker.Brukernavn);
                if (eksistererBruker == null)
                    db.Brukere.Add(nyBruker);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return false;
            }
        }
        public bool Bruker_i_DB(Bruker innBruker)
        {
            var db = new RetroDb();
            DBBRUKER funnetBruker = db.Brukere.FirstOrDefault
             (b => b.BRUKERNAVN == innBruker.Brukernavn);
            if (funnetBruker == null)
            {
                return false;
            }
            else
            {
                byte[] passordDb = lagHash(innBruker.Passord, funnetBruker.SALT);
                var riktigBruker = funnetBruker.PASSORD.SequenceEqual(passordDb);
                return true;
            }
        }
    }
}