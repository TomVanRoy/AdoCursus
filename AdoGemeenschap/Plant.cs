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
    public class Plant
    {
        private decimal verkoopPrijsValue;
        private Int16 levNrValue;
        private Int16 soortNrValue;
        private Int32 plantNrValue;
        private string kleurValue;
        private string plantNaamValue;

        public Plant(Int32 plantNr, string plantNaam, Int16 soortNr, Int16 levNr, string kleur, decimal verkoopPrijs)
        {
            Kleur = kleur;
            LevNr = levNr;
            PlantNaam = plantNaam;
            plantNrValue = plantNr;
            SoortNr = soortNr;
            VerkoopPrijs = verkoopPrijs;
        }

        public string PlantNaam
        {
            get { return plantNaamValue; }
            set { plantNaamValue = value; }
        }

        public string Kleur
        {
            get { return kleurValue; }
            set { kleurValue = value; }
        }

        public Int32 PlantNr
        {
            get { return plantNrValue; }
        }

        public Int16 SoortNr
        {
            get { return soortNrValue; }
            set { soortNrValue = value; }
        }

        public decimal VerkoopPrijs
        {
            get { return verkoopPrijsValue; }
            set
            {
                if (Convert.ToDecimal(value) < 0)
                {
                    throw new Exception("Verkoop prijs moet positief zijn");
                }
                else
                {
                    verkoopPrijsValue = value;
                }
            }
        }

        public Int16 LevNr
        {
            get { return levNrValue; }
            set { levNrValue = value; }
        }
    }
}