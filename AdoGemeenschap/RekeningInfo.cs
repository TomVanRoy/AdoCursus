using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoGemeenschap
{
    public class RekeningInfo
    {
        private string klantNaamValue;
        private decimal saldoValue;

        public RekeningInfo(decimal saldo, string klantNaam)
        {
            saldoValue = saldo;
            klantNaamValue = klantNaam;
        }

        public string KlantNaam
        {
            get
            {
                return klantNaamValue;
            }
        }

        public decimal Saldo
        {
            get
            {
                return saldoValue;
            }
        }
    }
}