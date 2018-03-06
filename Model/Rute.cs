using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetroFlyreiser.Model
{
    
    public class Rute
    {

        //[Display(Name = "Rute Id")]
        //[Required(ErrorMessage = "Oppgi rute id")]
        [RegularExpression("^[A-Z]{6}[0-9]{3}$", ErrorMessage = "Rute Id er ikke gyldig!")]
        [Key]
        public string RuteId { get; set; }

        public virtual Flyplass ReiseFra { get; set; }

        public virtual Flyplass ReiseTil { get; set; }


        [Display(Name = "Tid")]
        //[Required(ErrorMessage = "Oppgi tid")]
        [RegularExpression("^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Ugyldig tid! Bruk HH:MM")]
        public string Tid { get; set; }


        [Display(Name = "Dato")]
        //[Required(ErrorMessage = "Oppgi dato")]
        [RegularExpression("^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.]((19|20)\\d\\d)$", ErrorMessage = "Ugyldig Dato! Bruk HH.MM.YYYY")]
        public string Dato { get; set; }


        [Display(Name = "Reisetid")]
        //[Required(ErrorMessage = "Oppgi reisetid")]
        [RegularExpression(@"^\w+$", ErrorMessage = "Rute Id er ikke gyldig!")]
        public string ReiseTid { get; set; }

        public virtual Flymaskin Flymaskin { get; set; } 
        public int Pris { get; set; }

        public virtual List<Flyplass> Flyplasser { get; set; }
        public virtual List<Flymaskin> Flymaskiner { get; set; }
        public virtual List<Bestilling> Bestillinger { get; set; }

    }
}