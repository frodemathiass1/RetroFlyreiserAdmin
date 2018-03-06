using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetroFlyreiser.Model
{
    // Regex på Adressefelt kræsjer programmet!
    public class Kunde
    {
        [Display(Name ="Fornavn")]
        //[Required(ErrorMessage ="Oppgi fornavn")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Fornavn er ikke gyldig!")]
        public string Fornavn { get; set; }


        [Display(Name = "Etternavn")]
        //[Required(ErrorMessage = "Oppgi etternavn")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Etternavn er ikke gyldig!")]
        public string Etternavn { get; set; }


        [Display(Name = "Adresse")]
        //[Required(ErrorMessage = "Oppgi adresse")]
        //[RegularExpression(@"^ (.+)\s(\d + (\s *[^\d\s] +) *)$", ErrorMessage = "Etternavn er ikke gyldig!")]
        public string Adresse { get; set; }


        [Display(Name = "Telefon")]
        //[Required(ErrorMessage = "Oppgi telefon nummer")]
        [RegularExpression("^[0-9]{8}$", ErrorMessage = "Telefon er ikke gyldig!")]
        public string Telefon { get; set; }


        [Display(Name = "Epost")]
        //[Required(ErrorMessage = "Oppgi e-post adresse")]
        [RegularExpression(@"^([\w\.\-]+)@((?!\.|\-)[\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Epost er ikke gyldig!")]
        [Key]
        public string Epost { get; set; }

        public bool Aktiv { get; set; }

        public virtual Poststed Poststed { get; set; }

        public virtual List<Bestilling> Bestillinger { get; set; }
    }
}