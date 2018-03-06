using System.Collections.Generic;
using RetroFlyreiser.Model;

namespace RetroFlyreiser.DAL
{
    public interface IChangeLogDAL
    {
        List<ChangeLog> alleChangeLogger();
    }
}
