using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetroFlyreiser.Model
{
    public class Flymaskin
    {
        // Problemer med validering via modell. Html validering er lagt til.
        [Display(Name = "Fly Id")]
        //[Required(ErrorMessage = "Oppgi Fly Id")]
        [RegularExpression("^[A-Z]{2}[0-9]{2}", ErrorMessage = "FlyId er ikke gyldig! Bruk 2 Bokstaver og 2 tall, eks: AA99")]
        [Key]
        public string FlyId { get; set; }

        [Display(Name = "Type")]
        //[Required(ErrorMessage = "Oppgi fly type")]
        [RegularExpression(@"^([a-zA-Z0-9]+\s?)*$", ErrorMessage = "Fly type er ikke gyldig!")]
        public string Type { get; set; }

        [Display(Name = "Kapasitet")]
        //[Required(ErrorMessage = "Oppgi kapasitet")]
        [RegularExpression(@"^(.*[^0-9]|)(1000|[1-9]\d{0,2})([^0-9].*|)$", ErrorMessage = "Kapasitet er ikke gyldig")]
        public int Kapasitet { get; set; }
        public virtual List<Rute> Rute { get; set; }

    }
}