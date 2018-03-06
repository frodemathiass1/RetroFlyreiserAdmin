using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetroFlyreiser.Model
{
    public class Bestilling
    {
        public int Id { get; set; }

        public virtual Kunde Kunde { get; set; }

        public virtual Rute Rute { get; set; }
    }
}