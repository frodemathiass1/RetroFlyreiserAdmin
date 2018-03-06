using RetroFlyreiser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroFlyreiser.DAL;

namespace RetroFlyreiser.BLL
{

    
    public class BestillingBLL : IBestillingBLL
    {

        private IBestillingDAL _repository;


        public BestillingBLL()
        {
            _repository = new BestillingDAL();
        }

        public BestillingBLL(IBestillingDAL stub)
        {
            _repository = stub;
        }

        public List<Bestilling> alleBestillinger()
        {
            return _repository.alleBestillinger();
        }

        public bool slettBestilling(int Id)
        {
            return _repository.slett(Id);
        }

        public bool endreBestilling(int Id, Bestilling innBestilling)
        {
            return _repository.endreBestilling(Id, innBestilling);
        }

        public Bestilling hentEnBestilling(int Id)
        {
            return _repository.hentEnBestilling(Id);
        }

        public bool settInnBestilling(Bestilling bestilling)
        {
            return _repository.settInnBestilling(bestilling);
        }
    }
}
