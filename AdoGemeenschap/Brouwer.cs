using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System;

namespace AdoGemeenschap
{
    public class Brouwer
    {
        private string adresValue;
        private string brNaamValue;
        private Int32 brouwerNrValue;
        private string gemeenteValue;
        private Int32? omzetValue;
        private Int16 postcodeValue;

        public Brouwer() { }

        public Brouwer(Int32 brNr, string brNaam, string adres, Int16 postcode, string gemeente, Int32? omzet)
        {
            Adres = adres;
            BrNaam = brNaam;
            brouwerNrValue = brNr;
            Gemeente = gemeente;
            Omzet = omzet;
            Postcode = postcode;
            Changed = false;
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

        public string BrNaam
        {
            get { return brNaamValue; }
            set
            {
                brNaamValue = value;
                Changed = true;
            }
        }

        public Int32 BrouwerNr
        {
            get { return brouwerNrValue; }
        }

        public bool Changed { get; set; }
        public string Gemeente
        {
            get { return gemeenteValue; }
            set
            {
                gemeenteValue = value;
                Changed = true;
            }
        }

        public Int32? Omzet
        {
            get { return omzetValue; }
            set
            {
                if (value.HasValue && Convert.ToInt32(value) < 0)
                {
                    throw new Exception("Omzet moet positief zijn");
                }
                else
                {
                    omzetValue = value;
                    Changed = true;
                }
            }
        }

        public Int16 Postcode
        {
            get { return postcodeValue; }
            set
            {
                postcodeValue = value;
                Changed = true;
            }
        }
    }
}