using RetroFlyreiser.Model;
using System;
using System.Collections.Generic;


namespace RetroFlyreiser.DAL
{
    public class ChangeLogStub : IChangeLogDAL
    {
        public List<ChangeLog> alleChangeLogger()
        {

            var dateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            
            var changeLogListe = new List<ChangeLog>();
            var changeLog = new ChangeLog()
            {
                Id = 1,
                EntityName = "EntityName",
                PropertyName = "PropertyName",
                PrimaryKeyValue = "PK",
                OldValue = "OldValue",
                NewValue = "NewValue",
                DateChanged = dateTime,

            };
            changeLogListe.Add(changeLog);
            changeLogListe.Add(changeLog);
            changeLogListe.Add(changeLog);
            return changeLogListe;
        }
    }
}
