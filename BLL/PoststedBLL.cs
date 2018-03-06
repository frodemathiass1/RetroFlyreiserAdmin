using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroFlyreiser.Model;
using RetroFlyreiser.DAL;

namespace RetroFlyreiser.BLL
{
    public class PoststedBLL : IPoststedBLL
    {


        private IPoststedDAL _repository;

        public PoststedBLL()
        {
            _repository = new PoststedDAL();
        }

        public PoststedBLL(IPoststedDAL stub)
        {
            _repository = stub;
        }

        public List<Poststed> allePoststeder()
        {
            return _repository.allePoststeder();
        }

        public bool slettPoststed(string postnr)
        {
            return _repository.slett(postnr);
        }

        public bool endrePoststed(string postnr, Poststed innPoststed)
        {
            return _repository.endrePoststed(postnr, innPoststed);
        }

        public Poststed hentPoststed(string postnr)
        {
            return _repository.hentEtPoststed(postnr);
        }

        public bool settInnPoststed(Poststed innPoststed)
        {
            return _repository.settInnPoststed(innPoststed);
        }
    }
}
