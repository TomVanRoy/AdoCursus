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
        private string kleurValue;
        private Int32 levNrValue;
        private string plantNaamValue;
        private Int32 plantNrValue;
        private Int32 soortNrValue;
        private decimal verkoopPrijsValue;

        public Plant(Int32 plantNr, string plantNaam, Int32 soortNr, Int32 levNr, string kleur, decimal verkoopPrijs)
        {
            Kleur = kleur;
            LevNr = levNr;
            PlantNaam = plantNaam;
            plantNrValue = plantNr;
            SoortNr = soortNr;
            VerkoopPrijs = verkoopPrijs;
            Changed = false;
        }

        public bool Changed { get; set; }

        public string Kleur
        {
            get { return kleurValue; }
            set
            {
                kleurValue = value;
                Changed = true;
            }
        }

        public Int32 LevNr
        {
            get { return levNrValue; }
            set
            {
                levNrValue = value;
                Changed = true;
            }
        }

        public string PlantNaam
        {
            get { return plantNaamValue; }
            set
            {
                plantNaamValue = value;
                Changed = true;
            }
        }

        public Int32 PlantNr
        {
            get { return plantNrValue; }
        }

        public Int32 SoortNr
        {
            get { return soortNrValue; }
            set
            {
                soortNrValue = value;
                Changed = true;
            }
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
                    Changed = true;
                }
            }
        }
    }
}