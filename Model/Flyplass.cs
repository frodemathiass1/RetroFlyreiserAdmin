using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetroFlyreiser.Model
{
    public class Flyplass
    {

        // Problemer med validering via modell. Html validering er lagt til.

        [Display(Name = "Flyplasskode")]
        //[Required(ErrorMessage = "Oppgi flyplasskode")]
        [RegularExpression("^[A-Z]{3}", ErrorMessage = "FlyplassKode er ikke gyldig! Bruk 3 tegn:A-Z")]
        [Key]
        public string FlyplassKode { get; set; }

        [Display(Name = "By")]
        //[Required(ErrorMessage = "Oppgi by")]
        [RegularExpression(@"^[^0-9\#\$\@\+]*$", ErrorMessage = "By er ikke gyldig!")]
        public string By { get; set; }
        public virtual List<Rute> Ruter { get; set; }
        
    }
}