using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroFlyreiser.DAL;
using RetroFlyreiser.Model;



namespace RetroFlyreiser.BLL
{
  
    public class RuteBLL : IRuteBLL
    {


        private IRuteDAL _repository;

        public RuteBLL()
        {
            _repository = new RuteDAL();
        }

        public RuteBLL(IRuteDAL stub)
        {
            _repository = stub;
        }

        public List<Rute> alleRuter()
        {
            return _repository.alleRuter();
        }

        public bool slettRute(string ruteId)
        {
            return _repository.slett(ruteId);
        }

        public bool endreRute(string ruteId, Rute innRute)
        {
            return _repository.endreRute(ruteId, innRute);
        }

        public Rute hentRute(string ruteId)
        {
            return _repository.hentEnRute(ruteId);
        }

        public bool settInnRute(Rute innRute)
        {
            return _repository.settInnRute(innRute);
        }
    }
}
