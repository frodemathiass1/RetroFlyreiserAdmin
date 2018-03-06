using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using RetroFlyreiser.Model;
using System.Linq;
using System.Data.Entity.Infrastructure;


namespace RetroFlyreiser.DAL
{

    public class RetroDb : DbContext
    {
        public RetroDb() : base("name=RetroDB")
        {
            Database.CreateIfNotExists();
            Database.SetInitializer(new DbInit());
        }

        
        public virtual DbSet<DBBRUKER> Brukere { get; set; }
        public virtual DbSet<Kunde> Kunder { get; set; }
        public virtual DbSet<Poststed> Poststeder { get; set; }
        public virtual DbSet<Flyplass> Flyplasser { get; set; }
        public virtual DbSet<Flymaskin> Flymaskiner { get; set; }
        public virtual DbSet<Rute> Ruter { get; set; }
        public virtual DbSet<Bestilling> Bestillinger { get; set; }
        public virtual DbSet<ChangeLog> ChangeLogs { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Poststed>()
                .HasKey(p => p.Postnr);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        object GetPrimaryKeyValue(DbEntityEntry entry)
        {
            var objectStateEntry = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            return objectStateEntry.EntityKey.EntityKeyValues[0].Value;
        }

        public override int SaveChanges()
        {
            var modifiedEntities = ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Modified).ToList();
            var now = DateTime.UtcNow;

            foreach (var change in modifiedEntities)
            {
                var entityName = change.Entity.GetType().Name;
                var primaryKey = GetPrimaryKeyValue(change);

                foreach (var prop in change.OriginalValues.PropertyNames)
                {
                    var originalValue = change.OriginalValues[prop].ToString();
                    var currentValue = change.CurrentValues[prop].ToString();
                    if (originalValue != currentValue)
                    {
                        ChangeLog log = new ChangeLog()
                        {
                            EntityName = entityName,
                            PrimaryKeyValue = primaryKey.ToString(),
                            PropertyName = prop,
                            OldValue = originalValue,
                            NewValue = currentValue,
                            DateChanged = now
                        };
                        ChangeLogs.Add(log);
                    }
                }
            }
            return base.SaveChanges();
        }
    }
} 
