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
        private Int16 postcodeValue;
        private Int32 brouwerNrValue;
        private Int32? omzetValue;
        private string adresValue;
        private string brNaamValue;
        private string gemeenteValue;

        public Brouwer(Int32 brNr, string brNaam, string adres, Int16 postcode, string gemeente, Int32? omzet)
        {
            Adres = adres;
            BrNaam = brNaam;
            brouwerNrValue = brNr;
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

        public Int32 BrouwerNr
        {
            get { return brouwerNrValue; }
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
            set { postcodeValue = value; }
        }
    }
}