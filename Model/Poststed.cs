using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetroFlyreiser.Model
{
    public class Poststed
    {
        
        [Display(Name = "Postnr")]
        //[Required(ErrorMessage = "Oppgi e-post adresse")]
        [RegularExpression("^[0-9]{4}$", ErrorMessage = "Ugyldig postnummer! Bruk 4 siffer")]
        [Key]
        public string Postnr { get; set; }

        [Display(Name = "Sted")]
        //[Required(ErrorMessage = "Oppgi poststed")]
        [RegularExpression(@"^[^0-9\#\$\@\+]*$", ErrorMessage = "Poststed er ikke gyldig!")]
        public string Sted { get; set; }
        public virtual List<Kunde> Kunder { get; set; }
    }
}