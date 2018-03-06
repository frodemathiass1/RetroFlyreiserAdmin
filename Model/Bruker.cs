using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace RetroFlyreiser.Model
{
    public class Bruker
    {
        [Key]
        [Required(ErrorMessage = "Brukernavn må oppgis")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Brukernavn er ugyldig! Bruk sammenhengende tegn")]
        public string Brukernavn { get; set; }

        [Required(ErrorMessage = "Passord må oppgis")]
        [RegularExpression("(?!^[0-9]*$)(?!^[a-zA-Z]*$)^(.{8,15})$", ErrorMessage = "Passord er ikke gyldig! Bruk 8-15 tegn,store og små bokstaver")]
        public string Passord { get; set; }
    }
    

    public class DBBRUKER
    {
        [Key]
        public string BRUKERNAVN { get; set; }

        public byte[] PASSORD { get; set; }
        public byte[] SALT { get; set; }
    }
}