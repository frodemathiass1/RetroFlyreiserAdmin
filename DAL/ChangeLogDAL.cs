using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using static RetroFlyreiser.Model.ErrorLogging;
using RetroFlyreiser.Model;

namespace RetroFlyreiser.DAL
{
    public class ChangeLoggDAL :  IChangeLogDAL
    {
        public List<ChangeLog> alleChangeLogger()
        {
            var db = new RetroDb();
            try
            {
                List<ChangeLog> alleChangeLogger = db.ChangeLogs.ToList();
                return alleChangeLogger;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return null;
            }
        }
    }
}
