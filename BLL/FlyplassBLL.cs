using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroFlyreiser.DAL;
using RetroFlyreiser.Model;

namespace RetroFlyreiser.BLL
{
    public class FlyplassBLL : IFlyplassBLL
    {
        private IFlyplassDAL _repository;

        public FlyplassBLL()
        {
            _repository = new FlyplassDAL();
        }

        public FlyplassBLL(IFlyplassDAL stub)
        {
            _repository = stub;
        }

        public List<Flyplass> alleFlyplasser()
        {
            List<Flyplass> alleFlyplasser = _repository.alleFlyplasser();
            return alleFlyplasser;
        }

        public bool slettFlyplass(string flyplassKode)
        {
            return _repository.slett(flyplassKode);
        }

        public bool endreFlyplass(string flyplassKode, Flyplass innFlyplass)
        {
            return _repository.endreFlyplass(flyplassKode, innFlyplass);
        }

        public Flyplass hentFlyplass(string flyplassKode)
        {
            return _repository.hentEnFlyplass(flyplassKode);
        }

        public bool settInnFlyplass(Flyplass innFlyplass)
        {
            return _repository.settInnFlyplass(innFlyplass);
        }
    }
}
