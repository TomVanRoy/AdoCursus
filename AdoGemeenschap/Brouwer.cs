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
        private Int32 brouwersNrValue;
        private string gemeenteValue;
        private Int32? omzetValue;
        private Int16 postcodeValue;

        public Brouwer(Int32 brNr, string brNaam, string adres, Int16 postcode, string gemeente, Int32? omzet)
        {
            Adres = adres;
            BrNaam = brNaam;
            brouwersNrValue = brNr;
            Gemeente = gemeente;
            Omzet = omzet;
            Postcode = postcode;
        }

        public string Adres
        {
            get { return adresValue; }
            set { adresValue = value; }
        }

        public string BrNaam
        {
            get { return brNaamValue; }
            set { brNaamValue = value; }
        }

        public Int32 BrouwersNr
        {
            get { return brouwersNrValue; }
        }

        public string Gemeente
        {
            get { return gemeenteValue; }
            set { gemeenteValue = value; }
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
                }
            }
        }

        public Int16 Postcode
        {
            get { return postcodeValue; }
            set
            {
                if (value < 1000 || value > 9999)
                {
                    throw new Exception("Postcode moet tussen 1000 en 9999 liggen");
                }
                else
                {
                    postcodeValue = value;
                }
            }
        }
    }
}