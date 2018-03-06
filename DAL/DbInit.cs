using System.Collections.Generic;
using System.Data.Entity;
using RetroFlyreiser.Model;

namespace RetroFlyreiser.DAL
{
    public class DbInit : DropCreateDatabaseAlways<RetroDb>
    {

        protected override void Seed(RetroDb retroDb)
        {

            /*
             * Poststeder
             */
            List<Poststed> innPoststeder = new List<Poststed>();
            var innP1 = new Poststed() { Postnr = "1234", Sted = "Oslo" };
            var innP2 = new Poststed() { Postnr = "1000", Sted = "Oslo" };
            var innP3 = new Poststed() { Postnr = "1001", Sted = "Oslo" };
            var innP4 = new Poststed() { Postnr = "1800", Sted = "Askim" };
            var innP5 = new Poststed() { Postnr = "5000", Sted = "Bergen" };
            var innP6 = new Poststed() { Postnr = "4000", Sted = "Stavanger" };
            var innP7 = new Poststed() { Postnr = "2000", Sted = "Lillestrøm" };
            var innP8 = new Poststed() { Postnr = "2005", Sted = "Rælingen" };
            var innP9 = new Poststed() { Postnr = "2020", Sted = "Skedsmokorset" };
           var innP10 = new Poststed() { Postnr = "2014", Sted = "Blystadlia" };
           var innP11 = new Poststed() { Postnr = "6000", Sted = "Ålesund" };
           var innP12 = new Poststed() { Postnr = "8000", Sted = "Bodø" };
           var innP13 = new Poststed() { Postnr = "3000", Sted = "Drammen" };
           var innP14 = new Poststed() { Postnr = "7000", Sted = "Trondheim" };
           var innP15 = new Poststed() { Postnr = "9000", Sted = "Troms" };
           var innP16 = new Poststed() { Postnr = "9900", Sted = "Kirkenes" };
           var innP17 = new Poststed() { Postnr = "9500", Sted = "Alta" };
            innPoststeder.Add(innP1);
            innPoststeder.Add(innP2);
            innPoststeder.Add(innP3);
            innPoststeder.Add(innP4);
            innPoststeder.Add(innP5);
            innPoststeder.Add(innP6);
            innPoststeder.Add(innP7);
            innPoststeder.Add(innP8);
            innPoststeder.Add(innP9);
            innPoststeder.Add(innP10);
            innPoststeder.Add(innP11);
            innPoststeder.Add(innP12);
            innPoststeder.Add(innP13);
            innPoststeder.Add(innP14);
            innPoststeder.Add(innP15);
            innPoststeder.Add(innP16);
            innPoststeder.Add(innP17);

            foreach (Poststed p in innPoststeder)
            {
                retroDb.Poststeder.Add(p);
            }
            base.Seed(retroDb);

            /*
             * Kunder
             */
            List<Kunde> innKunder = new List<Kunde>();
            innKunder.Add(new Kunde() { Fornavn = "Ola", Etternavn = "Nordmann", Adresse = "Eventyrveien 11", Poststed = innP1, Telefon = "22222222", Epost = "ola@olsen.no",Aktiv=true});
            innKunder.Add(new Kunde() { Fornavn = "Kari", Etternavn = "Svensken", Adresse = "Korvgatan 22", Poststed = innP2, Telefon = "33333333", Epost = "Kari@Svensken.se",Aktiv=true });
            innKunder.Add(new Kunde() { Fornavn = "Baron", Etternavn = "Blod", Adresse = "Helvete", Poststed = innP3, Telefon = "44444444", Epost = "Satan@Helvete.net",Aktiv=false });
            innKunder.Add(new Kunde() { Fornavn = "Kristin", Etternavn = "Hansen", Adresse = "Blåbærgata 55", Poststed = innP4, Telefon = "55555555", Epost = "kristin@gmail.com", Aktiv = true });
            innKunder.Add(new Kunde() { Fornavn = "Petter", Etternavn = "Bolla", Adresse = "Solstien 5", Poststed = innP5, Telefon = "66666666", Epost = "pet@shop.se", Aktiv = true });
            innKunder.Add(new Kunde() { Fornavn = "Jesus", Etternavn = "Kristus", Adresse = "På korset", Poststed = innP6, Telefon = "77777777", Epost = "skjeggete@mann.no", Aktiv = false });

            foreach (Kunde kunde in innKunder)
            {
                retroDb.Kunder.Add(kunde);
            }
            base.Seed(retroDb);


            /*
             * Flymaskiner
             */
            List<Flymaskin> innFlymaskiner = new List<Flymaskin>();
            var f1 = new Flymaskin() { FlyId = "AA01", Type = "Boeing 737", Kapasitet = 500 };
            var f2 = new Flymaskin() { FlyId = "AA02", Type = "Boeing 747", Kapasitet = 400 };
            var f3 = new Flymaskin() { FlyId = "AA03", Type = "Boeing 767", Kapasitet = 300 };
            var f4 = new Flymaskin() { FlyId = "AA04", Type = "Boeing 637", Kapasitet = 200 };
            var f5 = new Flymaskin() { FlyId = "AA05", Type = "Boeing 747", Kapasitet = 200 };
            var f6 = new Flymaskin() { FlyId = "AA06", Type = "Boeing 657", Kapasitet = 150 };
            var f7 = new Flymaskin() { FlyId = "AA07", Type = "Boeing 747", Kapasitet = 200 };
            var f8 = new Flymaskin() { FlyId = "AA08", Type = "Boeing 657", Kapasitet = 150 };
            var f9 = new Flymaskin() { FlyId = "AA09", Type = "Boeing 747", Kapasitet = 200 };
           var f10 = new Flymaskin() { FlyId = "AA10", Type = "Boeing 657", Kapasitet = 150 };
           var f11 = new Flymaskin() { FlyId = "BB01", Type = "Boeing 777", Kapasitet = 500 };
           var f12 = new Flymaskin() { FlyId = "BB02", Type = "Boeing 786", Kapasitet = 400 };
           var f13 = new Flymaskin() { FlyId = "BB03", Type = "Boeing 787", Kapasitet = 300 };
           var f14 = new Flymaskin() { FlyId = "BB04", Type = "Boeing 637", Kapasitet = 200 };
           var f15 = new Flymaskin() { FlyId = "BB05", Type = "Boeing 765", Kapasitet = 200 };
           var f16 = new Flymaskin() { FlyId = "BB06", Type = "Boeing 657", Kapasitet = 150 };
           var f17 = new Flymaskin() { FlyId = "BB07", Type = "Boeing 777", Kapasitet = 200 };
           var f18 = new Flymaskin() { FlyId = "BB08", Type = "Boeing 657", Kapasitet = 150 };
           var f19 = new Flymaskin() { FlyId = "BB09", Type = "Boeing 787", Kapasitet = 200 };
           var f20 = new Flymaskin() { FlyId = "BB10", Type = "Boeing 657", Kapasitet = 150 };
            innFlymaskiner.Add(f1);
            innFlymaskiner.Add(f2);
            innFlymaskiner.Add(f3);
            innFlymaskiner.Add(f4);
            innFlymaskiner.Add(f5);
            innFlymaskiner.Add(f6);
            innFlymaskiner.Add(f7);
            innFlymaskiner.Add(f8);
            innFlymaskiner.Add(f9);
            innFlymaskiner.Add(f10);
            innFlymaskiner.Add(f11);
            innFlymaskiner.Add(f12);
            innFlymaskiner.Add(f13);
            innFlymaskiner.Add(f14);
            innFlymaskiner.Add(f15);
            innFlymaskiner.Add(f16);
            innFlymaskiner.Add(f17);
            innFlymaskiner.Add(f18);
            innFlymaskiner.Add(f19);
            innFlymaskiner.Add(f20);

            foreach (Flymaskin fly in innFlymaskiner)
            {
                retroDb.Flymaskiner.Add(fly);
            }
            base.Seed(retroDb);




            // For innsetting i Rute tabell
            List<Flyplass> innFlyplasser = new List<Flyplass>();
            var OSL = new Flyplass() { FlyplassKode = "OSL", By = "Oslo" };
            var KRS = new Flyplass() { FlyplassKode = "KRS", By = "Kristiansand" };
            var TRD = new Flyplass() { FlyplassKode = "TRD", By = "Trondheim" };
            var MOL = new Flyplass() { FlyplassKode = "MOL", By = "Molde" };
            var BOD = new Flyplass() { FlyplassKode = "BOD", By = "Bodø" };
            var SVG = new Flyplass() { FlyplassKode = "SVG", By = "Stavanger"};
            var BRD = new Flyplass() { FlyplassKode = "BRD", By = "Bardufoss"};
            var KRK = new Flyplass() { FlyplassKode = "KRK", By = "Kirkenes"};
            var TRS = new Flyplass() { FlyplassKode = "TRS", By = "Tromsø"};
            var ALS = new Flyplass() { FlyplassKode = "ALS", By = "Ålesund"};
            var NRV = new Flyplass() { FlyplassKode = "NRV", By = "Narvik/Harstad"};
            var LKS = new Flyplass() { FlyplassKode = "LKS", By = "Lakselv"};
            var HGS = new Flyplass() { FlyplassKode = "HGS", By = "Haugesund"};
            var ALT = new Flyplass() { FlyplassKode = "ALT", By = "Alta" };

            innFlyplasser.Add(OSL);
            innFlyplasser.Add(KRS);
            innFlyplasser.Add(TRD);
            innFlyplasser.Add(MOL);
            innFlyplasser.Add(BOD);
            innFlyplasser.Add(SVG);
            innFlyplasser.Add(BRD);
            innFlyplasser.Add(KRK);
            innFlyplasser.Add(TRS);
            innFlyplasser.Add(ALS);
            innFlyplasser.Add(NRV);
            innFlyplasser.Add(LKS);
            innFlyplasser.Add(HGS);
            innFlyplasser.Add(ALT);

            foreach (Flyplass flyplass in innFlyplasser)
            {
                retroDb.Flyplasser.Add(flyplass);
            }
            base.Seed(retroDb);



            // Fra Oslo 10.10.2017
            List<Rute> innRuter = new List<Rute>();
            var OSL001 = new Rute() { RuteId = "OSLSVG001", ReiseFra = OSL, ReiseTil = SVG, Tid = "12:00", Dato = "10.10.2017", ReiseTid = "50min", Flymaskin = f1, Pris = 499 };
            var OSL002 = new Rute() { RuteId = "OSLKRS001", ReiseFra = OSL, ReiseTil = KRS, Tid = "12:00", Dato = "10.10.2017", ReiseTid = "55min", Flymaskin = f1, Pris = 499 };
            var OSL003 = new Rute() { RuteId = "OSLTRD001", ReiseFra = OSL, ReiseTil = TRD, Tid = "12:00", Dato = "10.10.2017", ReiseTid = "59min", Flymaskin = f2, Pris = 499 };
            var OSL004 = new Rute() { RuteId = "OSLMOL001", ReiseFra = OSL, ReiseTil = MOL, Tid = "12:00", Dato = "10.10.2017", ReiseTid = "47min", Flymaskin = f2, Pris = 499 };
            var OSL005 = new Rute() { RuteId = "OSLSVG002", ReiseFra = OSL, ReiseTil = SVG, Tid = "18:00", Dato = "10.10.2017", ReiseTid = "50min", Flymaskin = f3, Pris = 499 };
            var OSL006 = new Rute() { RuteId = "OSLKRS002", ReiseFra = OSL, ReiseTil = KRS, Tid = "18:00", Dato = "10.10.2017", ReiseTid = "55min", Flymaskin = f3, Pris = 499 };
            var OSL007 = new Rute() { RuteId = "OSLTRD002", ReiseFra = OSL, ReiseTil = TRD, Tid = "18:00", Dato = "10.10.2017", ReiseTid = "59min", Flymaskin = f4, Pris = 499 };
            var OSL008 = new Rute() { RuteId = "OSLMOL002", ReiseFra = OSL, ReiseTil = MOL, Tid = "18:00", Dato = "10.10.2017", ReiseTid = "47min", Flymaskin = f4, Pris = 499 };

            //Fra Stavanger 10.10.2017
            var SVG001 = new Rute() { RuteId = "SVGOSL111", ReiseFra = SVG, ReiseTil = OSL, Tid = "09:00", Dato = "10.10.2017", ReiseTid = "50min", Flymaskin = f1, Pris = 399 };
            var SVG002 = new Rute() { RuteId = "SVGOSL222", ReiseFra = SVG, ReiseTil = OSL, Tid = "13:00", Dato = "10.10.2017", ReiseTid = "50min", Flymaskin = f2, Pris = 499 };
            var SVG003 = new Rute() { RuteId = "SVGOSL333", ReiseFra = SVG, ReiseTil = OSL, Tid = "19:00", Dato = "10.10.2017", ReiseTid = "50min", Flymaskin = f1, Pris = 599 };
            var SVG004 = new Rute() { RuteId = "SVGKRS111", ReiseFra = SVG, ReiseTil = KRS, Tid = "12:00", Dato = "10.10.2017", ReiseTid = "55min", Flymaskin = f2, Pris = 399 };
            var SVG005 = new Rute() { RuteId = "SVGKRS222", ReiseFra = SVG, ReiseTil = KRS, Tid = "18:00", Dato = "10.10.2017", ReiseTid = "55min", Flymaskin = f1, Pris = 499 };
            var SVG006 = new Rute() { RuteId = "SVGKRS333", ReiseFra = SVG, ReiseTil = KRS, Tid = "22:00", Dato = "10.10.2017", ReiseTid = "55min", Flymaskin = f4, Pris = 599 };
            var SVG007 = new Rute() { RuteId = "SVGTRD111", ReiseFra = SVG, ReiseTil = TRD, Tid = "06:00", Dato = "10.10.2017", ReiseTid = "55min", Flymaskin = f5 ,Pris = 399 };
            var SVG008 = new Rute() { RuteId = "SVGTRD222", ReiseFra = SVG, ReiseTil = TRD, Tid = "15:00", Dato = "10.10.2017", ReiseTid = "59min", Flymaskin = f4, Pris = 499 };
            var SVG009 = new Rute() { RuteId = "SVGTRD333", ReiseFra = SVG, ReiseTil = TRD, Tid = "23:00", Dato = "10.10.2017", ReiseTid = "59min", Flymaskin = f5, Pris = 599 };
            var SVG010 = new Rute() { RuteId = "SVGMOL111", ReiseFra = SVG, ReiseTil = MOL, Tid = "07:00", Dato = "10.10.2017", ReiseTid = "47min", Flymaskin = f6, Pris = 399 };
            var SVG011 = new Rute() { RuteId = "SVGMOL222", ReiseFra = SVG, ReiseTil = MOL, Tid = "16:00", Dato = "10.10.2017", ReiseTid = "47min", Flymaskin = f4, Pris = 499 };
            var SVG012 = new Rute() { RuteId = "SVGMOL333", ReiseFra = SVG, ReiseTil = MOL, Tid = "21:00", Dato = "10.10.2017", ReiseTid = "47min", Flymaskin = f2, Pris = 599 };
            innRuter.Add(OSL001);
            innRuter.Add(OSL001);
            innRuter.Add(OSL003);
            innRuter.Add(OSL004);
            innRuter.Add(OSL005);
            innRuter.Add(OSL006);
            innRuter.Add(OSL007);
            innRuter.Add(OSL008);
            innRuter.Add(SVG001);
            innRuter.Add(SVG002);
            innRuter.Add(SVG003);
            innRuter.Add(SVG004);
            innRuter.Add(SVG005);
            innRuter.Add(SVG006);
            innRuter.Add(SVG007);
            innRuter.Add(SVG008);
            innRuter.Add(SVG009);
            innRuter.Add(SVG010);
            innRuter.Add(SVG011);
            innRuter.Add(SVG012);
            foreach (Rute rute in innRuter)
            {
                retroDb.Ruter.Add(rute);
            }
            base.Seed(retroDb);

            // BESTILLINGER
            var kunde1 = new Kunde() { Fornavn = "Per", Etternavn = "Olsen", Adresse = "Eventyrveien 2", Poststed = innP4, Epost = "per@olsen.no", Telefon = "22223333", Aktiv = true };
            var kunde2 = new Kunde() { Fornavn = "Kari", Etternavn = "PEttersen", Adresse = "Portveien 42", Poststed = innP5, Epost = "kari@nilsen.no", Telefon = "22224455", Aktiv = true };
            var kunde3 = new Kunde() { Fornavn = "Ola", Etternavn = "Rasmussen", Adresse = "Eventyrveien 2", Poststed = innP4, Epost = "jfk@krf.no", Telefon = "22223333", Aktiv = true };
            var kunde4 = new Kunde() { Fornavn = "Krister", Etternavn = "Hermansen", Adresse = "Portveien 42", Poststed = innP5, Epost = "krisi@hotmail.no", Telefon = "22224455", Aktiv = true };
            var kunde5 = new Kunde() { Fornavn = "Lene", Etternavn = "Pedersen", Adresse = "Eventyrveien 2", Poststed = innP4, Epost = "l3n3@ped.no", Telefon = "22223333", Aktiv = true };
            var kunde6 = new Kunde() { Fornavn = "Trine", Etternavn = "Nilsen", Adresse = "Portveien 42", Poststed = innP5, Epost = "nils1@nilsen.no", Telefon = "22224455", Aktiv = true };



            List<Bestilling> innBestillinger = new List<Bestilling>();
            var b1 = new Bestilling() { Kunde = kunde1, Rute = OSL001 };
            var b2 = new Bestilling() { Kunde = kunde2, Rute = OSL002 };
            var b3 = new Bestilling() { Kunde = kunde3, Rute = OSL003 };
            var b4 = new Bestilling() { Kunde = kunde4, Rute = SVG001 };
            var b5 = new Bestilling() { Kunde = kunde5, Rute = SVG002 };
            var b6 = new Bestilling() { Kunde = kunde6, Rute = SVG003 };

            innBestillinger.Add(b1);
            innBestillinger.Add(b2);
            innBestillinger.Add(b3);
            innBestillinger.Add(b4);
            innBestillinger.Add(b5);
            innBestillinger.Add(b6);
            foreach (Bestilling bestilling in innBestillinger)
            {
                retroDb.Bestillinger.Add(bestilling);
            }
            base.Seed(retroDb);


            // Ruter påfyll
            //
            // Fra Molde 
            innRuter.Add(new Rute() { RuteId = "MOLOSL001", ReiseFra = MOL, ReiseTil = OSL, Tid = "12:00", Dato = "10.10.2017", ReiseTid = "50min", Flymaskin = f5, Pris = 499 });
            innRuter.Add(new Rute() { RuteId = "MOLSVG001", ReiseFra = MOL, ReiseTil = SVG, Tid = "12:00", Dato = "11.10.2017", ReiseTid = "55min", Flymaskin = f6, Pris = 499 });
            innRuter.Add(new Rute() { RuteId = "MOLTRD001", ReiseFra = MOL, ReiseTil = TRD, Tid = "12:00", Dato = "12.10.2017", ReiseTid = "59min", Flymaskin = f4, Pris = 499 });
            innRuter.Add(new Rute() { RuteId = "MOLKRS001", ReiseFra = MOL, ReiseTil = KRS, Tid = "12:00", Dato = "13.10.2017", ReiseTid = "47min", Flymaskin = f5, Pris = 499 });
            innRuter.Add(new Rute() { RuteId = "MOLOSL002", ReiseFra = MOL, ReiseTil = OSL, Tid = "18:00", Dato = "10.10.2017", ReiseTid = "50min", Flymaskin = f5, Pris = 499 });
            innRuter.Add(new Rute() { RuteId = "MOLSVG002", ReiseFra = MOL, ReiseTil = SVG, Tid = "18:00", Dato = "11.10.2017", ReiseTid = "55min", Flymaskin = f3, Pris = 499 });
            innRuter.Add(new Rute() { RuteId = "MOLTRD002", ReiseFra = MOL, ReiseTil = TRD, Tid = "18:00", Dato = "12.10.2017", ReiseTid = "59min", Flymaskin = f5, Pris = 499 });
            innRuter.Add(new Rute() { RuteId = "MOLKRS002", ReiseFra = MOL, ReiseTil = KRS, Tid = "18:00", Dato = "13.10.2017", ReiseTid = "47min", Flymaskin = f3, Pris = 499 });


            // Fra Stavanger
            innRuter.Add(new Rute() { RuteId = "SVGOSL001", ReiseFra = SVG, ReiseTil = OSL, Tid = "12:00", Dato = "10.10.2017", ReiseTid = "50min", Flymaskin = f5, Pris = 499 });
            innRuter.Add(new Rute() { RuteId = "SVGKRS001", ReiseFra = SVG, ReiseTil = OSL, Tid = "12:00", Dato = "11.10.2017", ReiseTid = "55min", Flymaskin = f6, Pris = 499 });
            innRuter.Add(new Rute() { RuteId = "SVGTRD001", ReiseFra = SVG, ReiseTil = TRD, Tid = "12:00", Dato = "12.10.2017", ReiseTid = "55min", Flymaskin = f5, Pris = 499 });
            innRuter.Add(new Rute() { RuteId = "SVGMOL001", ReiseFra = SVG, ReiseTil = MOL, Tid = "12:00", Dato = "13.10.2017", ReiseTid = "47min", Flymaskin = f6, Pris = 499 });
            innRuter.Add(new Rute() { RuteId = "SVGOSL002", ReiseFra = SVG, ReiseTil = OSL, Tid = "18:00", Dato = "10.10.2017", ReiseTid = "50min", Flymaskin = f5, Pris = 499 });
            innRuter.Add(new Rute() { RuteId = "SVGKRS002", ReiseFra = SVG, ReiseTil = KRS, Tid = "18:00", Dato = "11.10.2017", ReiseTid = "55min", Flymaskin = f6, Pris = 499 });
            innRuter.Add(new Rute() { RuteId = "SVGTRD002", ReiseFra = SVG, ReiseTil = TRD, Tid = "18:00", Dato = "12.10.2017", ReiseTid = "59min", Flymaskin = f5, Pris = 499 });
            innRuter.Add(new Rute() { RuteId = "SVGMOL002", ReiseFra = SVG, ReiseTil = MOL, Tid = "18:00", Dato = "13.10.2017", ReiseTid = "47min", Flymaskin = f6, Pris = 499 });

            // Fra Trondheim
            innRuter.Add(new Rute() { RuteId = "TRDKRS001", ReiseFra = TRD, ReiseTil = KRS, Tid = "12:00", Dato = "10.10.2017", ReiseTid = "50min", Flymaskin = f5, Pris = 499 });
            innRuter.Add(new Rute() { RuteId = "TRDMOL001", ReiseFra = TRD, ReiseTil = MOL, Tid = "12:00", Dato = "11.10.2017", ReiseTid = "55min", Flymaskin = f6, Pris = 499 });
            innRuter.Add(new Rute() { RuteId = "TRDOSL001", ReiseFra = TRD, ReiseTil = OSL, Tid = "12:00", Dato = "12.10.2017", ReiseTid = "59min", Flymaskin = f5, Pris = 499 });
            innRuter.Add(new Rute() { RuteId = "TRDSVG001", ReiseFra = TRD, ReiseTil = SVG, Tid = "12:00", Dato = "13.10.2017", ReiseTid = "47min", Flymaskin = f6, Pris = 499 });
            innRuter.Add(new Rute() { RuteId = "TRDKRS002", ReiseFra = TRD, ReiseTil = KRS, Tid = "18:00", Dato = "10.10.2017", ReiseTid = "50min", Flymaskin = f5, Pris = 499 });
            innRuter.Add(new Rute() { RuteId = "TRDMOL002", ReiseFra = TRD, ReiseTil = MOL, Tid = "18:00", Dato = "11.10.2017", ReiseTid = "55min", Flymaskin = f6, Pris = 499 });
            innRuter.Add(new Rute() { RuteId = "TRDOSL002", ReiseFra = TRD, ReiseTil = OSL, Tid = "18:00", Dato = "12.10.2017", ReiseTid = "59min", Flymaskin = f5, Pris = 499 });
            innRuter.Add(new Rute() { RuteId = "TRDSVG002", ReiseFra = TRD, ReiseTil = SVG, Tid = "18:00", Dato = "13.10.2017", ReiseTid = "47min", Flymaskin = f6, Pris = 499 });


            foreach (Rute rute in innRuter)
            {
                retroDb.Ruter.Add(rute);
            }
            base.Seed(retroDb);
        }
            
    }
}


    