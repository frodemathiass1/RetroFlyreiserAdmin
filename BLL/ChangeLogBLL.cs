using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroFlyreiser.DAL;
using RetroFlyreiser.Model;


namespace RetroFlyreiser.BLL
{
    public class ChangeLogBLL : IChangeLogBLL
    {
        private IChangeLogDAL _repository;

        public ChangeLogBLL()
        {
            _repository = new ChangeLoggDAL();
        }

        public ChangeLogBLL(IChangeLogDAL stub)
        {
            _repository = stub;
        }

        public List<ChangeLog> alleChangeLogger()
        {
            return _repository.alleChangeLogger();
        }
    }
}
