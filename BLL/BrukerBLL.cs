using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroFlyreiser.DAL;
using RetroFlyreiser.Model;

namespace RetroFlyreiser.BLL
{
    public class BrukerBLL : IBrukerBLL
    {

        private IBrukerDAL _repository;

        public BrukerBLL()
        {
            _repository = new BrukerDAL();
        }

        public BrukerBLL(IBrukerDAL stub)
        {
            _repository = stub;
        }

        public byte[] lagHash(string innPassord)
        {
            return lagHash(innPassord);
        }

        public byte[] lagSalt(byte innSalt)
        {
            return lagSalt(innSalt);
        }

        public List<DBBRUKER> alleBrukere()
        {
            return _repository.alleBrukere();
        }

        public bool slettBruker(string brukernavn)
        {
            return _repository.slett(brukernavn);
        }

        public bool endreBruker(string brukernavn, DBBRUKER innBruker)
        {
            return _repository.endreBruker(brukernavn, innBruker);
        }

        public DBBRUKER hentEnBruker(string brukernavn)
        {
            return _repository.hentEnBruker(brukernavn);
        }

        public bool settInnBruker(Bruker innBruker)
        {
            return _repository.settInnBruker(innBruker);
        }

        public bool bruker_i_db(Bruker bruker)
        {
            return _repository.Bruker_i_DB(bruker);
        }
    }
}
