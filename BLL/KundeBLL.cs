using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroFlyreiser.Model;
using RetroFlyreiser.DAL;


namespace RetroFlyreiser.BLL
{
    public class KundeBLL : IKundeBLL
    {

        private IKundeDAL _repository;

        public KundeBLL()
        {
            _repository = new KundeDAL();
        }

        public KundeBLL(IKundeDAL stub)
        {
            _repository = stub;
        }

        public List<Kunde> alleKunder()
        {
            return _repository.alleKunder();
        }

        public bool endreKunde(string epost, Kunde innKunde)
        {
            return _repository.endreKunde(epost, innKunde);
        }

        public Kunde hentKunde(string epost)
        {
            return _repository.hentEnKunde(epost);
        }

        public bool settInnKunde(Kunde innKunde)
        {
            return _repository.settInnKunde(innKunde);
        }

        public bool slettKunde(string epost)
        {
            return _repository.slett(epost);
        }
    }
}
