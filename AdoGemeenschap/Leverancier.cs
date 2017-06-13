using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoGemeenschap
{
    public class Leverancier
    {
        private string adresValue;
        private int levNrValue;
        private string naamValue;
        private string postNrValue;
        private string woonplaatsValue;

        public Leverancier(int levnr, string naam, string adres, string postnr, string woonplaats)
        {
            LevNr = levnr;
            Naam = naam;
            Adres = adres;
            PostNr = postnr;
            Woonplaats = woonplaats;
            Changed = false;
        }

        public Leverancier()
        {
        }

        public string Adres
        {
            get { return adresValue; }
            set
            {
                adresValue = value;
                Changed = true;
            }
        }

        public bool Changed { get; set; }

        public int LevNr
        {
            get { return levNrValue; }
            set
            {
                levNrValue = value;
                Changed = true;
            }
        }

        public string Naam
        {
            get { return naamValue; }
            set
            {
                naamValue = value;
                Changed = true;
            }
        }

        public string PostNr
        {
            get { return postNrValue; }
            set
            {
                postNrValue = value;
                Changed = true;
            }
        }

        public string Woonplaats
        {
            get { return woonplaatsValue; }
            set
            {
                woonplaatsValue = value;
                Changed = true;
            }
        }

        public override string ToString()
        {
            return $"{LevNr} : {Naam}";
        }
    }
}