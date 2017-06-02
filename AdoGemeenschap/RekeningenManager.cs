﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Transactions;
using System.Configuration;

namespace AdoGemeenschap
{
    public class RekeningenManager
    {
        public void Overschrijven(decimal bedrag, string vanRekening, string naarRekening)
        {
            var dbManager = new BankDbManager();
            var dbManager2 = new Bank2DbManager();

            var opties = new TransactionOptions();
            opties.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (var traOverschrijven = new TransactionScope(TransactionScopeOption.Required, opties))
            {
                using (var conBank = dbManager.GetConnection())
                {
                    using (var comAftrekken = conBank.CreateCommand())
                    {
                        comAftrekken.CommandType = CommandType.Text;
                        comAftrekken.CommandText = "UPDATE Rekeningen SET Saldo = Saldo-@bedrag WHERE RekeningNr = @reknr";

                        var parBadrag = comAftrekken.CreateParameter();
                        parBadrag.ParameterName = "@bedrag";
                        parBadrag.Value = bedrag;
                        comAftrekken.Parameters.Add(parBadrag);

                        var parRekNr = comAftrekken.CreateParameter();
                        parRekNr.ParameterName = "@reknr";
                        parRekNr.Value = vanRekening;
                        comAftrekken.Parameters.Add(parRekNr);

                        conBank.Open();
                        if (comAftrekken.ExecuteNonQuery() == 0)
                        {
                            throw new Exception("Van rekening bestaat niet");
                        }
                    }
                }
                using (var conBank = dbManager2.GetConnection())
                {
                    using (var comBijtellen = conBank.CreateCommand())
                    {
                        comBijtellen.CommandType = CommandType.Text;
                        comBijtellen.CommandText = "UPDATE Rekeningen SET Saldo = Saldo + @bedrag WHERE RekeningNr = @reknr";

                        var parBedrag = comBijtellen.CreateParameter();
                        parBedrag.ParameterName = "@bedrag";
                        parBedrag.Value = bedrag;
                        comBijtellen.Parameters.Add(parBedrag);

                        var parRekNr = comBijtellen.CreateParameter();
                        parRekNr.ParameterName = "@reknr";
                        parRekNr.Value = naarRekening;
                        comBijtellen.Parameters.Add(parRekNr);

                        conBank.Open();
                        if (comBijtellen.ExecuteNonQuery() == 0)
                        {
                            throw new Exception("Naar rekening bestaat niet");
                        }

                        traOverschrijven.Complete();
                    }
                }
            }
        }

        public Int32 SaldoBonus()
        {
            var DbManager = new BankDbManager();
            using (var conBank = DbManager.GetConnection())
            {
                using (var comBonus = conBank.CreateCommand())
                {
                    comBonus.CommandType = CommandType.Text;
                    comBonus.CommandText = "UPDATE Rekeningen SET Saldo = ROUND(Saldo * 1.0011, 2)";
                    conBank.Open();
                    return comBonus.ExecuteNonQuery();
                }
            }
        }

        public Boolean Storten(decimal teStorten, string rekeningNr)
        {
            BankDbManager dbManager = new BankDbManager();
            using (var conBank = dbManager.GetConnection())
            {
                using (var comStorten = conBank.CreateCommand())
                {
                    comStorten.CommandType = CommandType.StoredProcedure;
                    comStorten.CommandText = "Storten";

                    DbParameter parTeStorten = comStorten.CreateParameter();
                    parTeStorten.ParameterName = "@teStorten";
                    parTeStorten.Value = teStorten;
                    parTeStorten.DbType = DbType.Currency;
                    comStorten.Parameters.Add(parTeStorten);

                    DbParameter parRekeningNr = comStorten.CreateParameter();
                    parRekeningNr.ParameterName = "@rekeningNr";
                    parRekeningNr.Value = rekeningNr;
                    comStorten.Parameters.Add(parRekeningNr);

                    conBank.Open();
                    return comStorten.ExecuteNonQuery() != 0;
                }
            }
        }

        public decimal SaldoRekeningRaadplegen(string rekeningnr)
        {
            var dbManager = new BankDbManager();
            using (var conBank = dbManager.GetConnection())
            {
                using (var comSaldo = conBank.CreateCommand())
                {
                    comSaldo.CommandType = CommandType.StoredProcedure;
                    comSaldo.CommandText = "SaldoRekeningRaadplegen";

                    var parRekNr = comSaldo.CreateParameter();
                    parRekNr.ParameterName = "@rekeningNr";
                    parRekNr.Value = rekeningnr;
                    comSaldo.Parameters.Add(parRekNr);

                    conBank.Open();

                    Object result = comSaldo.ExecuteScalar();
                    if (result == null)
                    {
                        throw new Exception("Rekening bestaat niet");
                    }
                    else
                    {
                        return (decimal)result;
                    }
                }
            }
        }

        public RekeningInfo RekeningInfoRaadplegen(string rekeningNr)
        {
            var dbManager = new BankDbManager();
            using (var conBank = dbManager.GetConnection())
            {
                using (var comSaldo = conBank.CreateCommand())
                {
                    comSaldo.CommandType = CommandType.StoredProcedure;
                    comSaldo.CommandText = "RekeningInfoRaadplegen";

                    var parRekNr = comSaldo.CreateParameter();
                    parRekNr.ParameterName = "@rekeningNr";
                    parRekNr.Value = rekeningNr;
                    comSaldo.Parameters.Add(parRekNr);

                    var parSaldo = comSaldo.CreateParameter();
                    parSaldo.ParameterName = "@Saldo";
                    parSaldo.DbType = DbType.Currency;
                    parSaldo.Direction = ParameterDirection.Output;
                    comSaldo.Parameters.Add(parSaldo);

                    var parKlantNaam = comSaldo.CreateParameter();
                    parKlantNaam.ParameterName = "@KlantNaam";
                    parKlantNaam.DbType = DbType.String;
                    parKlantNaam.Size = 50;
                    parKlantNaam.Direction = ParameterDirection.Output;
                    comSaldo.Parameters.Add(parKlantNaam);

                    conBank.Open();
                    comSaldo.ExecuteNonQuery();

                    if (parSaldo.Value.Equals(DBNull.Value))
                    {
                        throw new Exception("Rekening bestaat niet");
                    }
                    else
                    {
                        return new RekeningInfo((decimal)parSaldo.Value, (string)parKlantNaam.Value);
                    }
                }
            }
        }
    }
}