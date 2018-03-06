using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroFlyreiser.Model;
using RetroFlyreiser.DAL;

namespace RetroFlyreiser.BLL
{
    public interface IChangeLogBLL
    {
        List<ChangeLog> alleChangeLogger();
    }
}
