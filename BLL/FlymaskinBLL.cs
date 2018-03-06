using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroFlyreiser.DAL;
using RetroFlyreiser.Model;

namespace RetroFlyreiser.BLL
{
    public class FlymaskinBLL : IFlymaskinBLL
    {


        private IFlymaskinDAL _repository;

        public FlymaskinBLL()
        {
            _repository = new FlymaskinDAL();
        }

        public FlymaskinBLL(IFlymaskinDAL stub)
        {
            _repository = stub;
        }

        public List<Flymaskin> alleFlymaskiner()
        {
            return _repository.alleFlymaskiner();
        }

        public bool slettFlymaskin(string flyId)
        {
            return _repository.slett(flyId);
        }

        public bool endreFlymaskin(string flyId, Flymaskin innFlymaskin)
        {
            return _repository.endreFlymaskin(flyId, innFlymaskin);
        }

        public Flymaskin hentFlymaskin (string flyId)
        {
            return _repository.hentEnFlymaskin(flyId);
        }

        public bool settInnFlymaskin(Flymaskin innFlymaskin)
        {
            return _repository.settInnFlymaskin(innFlymaskin);
        }
    }
}
