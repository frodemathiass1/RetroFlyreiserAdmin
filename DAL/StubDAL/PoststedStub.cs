using RetroFlyreiser.Model;
using System.Collections.Generic;


namespace RetroFlyreiser.DAL
{
    public class PoststedStub : IPoststedDAL
    {

        public List<Poststed> allePoststeder()
        {
            var poststedListe = new List<Poststed>();
            var poststed = new Poststed()
            {
                Postnr = "1000",
                Sted = "Oslo"
            };
            poststedListe.Add(poststed);
            poststedListe.Add(poststed);
            poststedListe.Add(poststed);
            return poststedListe;
        }

        public Poststed hentEtPoststed(string postnr)
        {
            if (postnr == "")
            {
                var poststed = new Poststed()
                {
                    Postnr = "0000",
                    Sted = "Feil"
                };

                return poststed;
            }
            else
            {
                var poststed = new Poststed()
                {
                    Postnr = "1000",
                    Sted = "Oslo"
                };
                return poststed;
            }
        }

        public bool endrePoststed(string postnr, Poststed poststed)
        {
            if (postnr == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool settInnPoststed(Poststed innPoststed)
        {
            if (innPoststed.Postnr == "")
            {
                return false;
            }
            return true;
        }

        public bool slett(string postnr)
        {
            if (postnr == "")
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}
