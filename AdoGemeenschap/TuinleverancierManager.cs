using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Windows.Input;
using System.Diagnostics;
using System.Transactions;

namespace AdoGemeenschap
{
    public class TuinleverancierManager
    {
        public Int32 Eindejaarskorting()
        {
            TuinleverancierDbManager dbManager = new TuinleverancierDbManager();
            using (var conTuin = dbManager.GetConnection())
            {
                using (var comEindejaarskorting = conTuin.CreateCommand())
                {
                    comEindejaarskorting.CommandType = CommandType.StoredProcedure;
                    comEindejaarskorting.CommandText = "Eindejaarskorting";

                    conTuin.Open();
                    return comEindejaarskorting.ExecuteNonQuery();
                }
            }
        }

        public bool Toevoegen(string name, string adres, string postcode, string plaats)
        {
            TuinleverancierDbManager DbManager = new TuinleverancierDbManager();
            using (var conTuin = DbManager.GetConnection())
            {
                using (var comTuin = conTuin.CreateCommand())
                {
                    comTuin.CommandType = CommandType.StoredProcedure;
                    comTuin.CommandText = "Toevoegen";

                    DbParameter parName = comTuin.CreateParameter();
                    parName.ParameterName = "@naam";
                    parName.Value = name;
                    comTuin.Parameters.Add(parName);

                    DbParameter parAdres = comTuin.CreateParameter();
                    parAdres.ParameterName = "@adres";
                    parAdres.Value = adres;
                    comTuin.Parameters.Add(parAdres);

                    DbParameter parPostcode = comTuin.CreateParameter();
                    parPostcode.ParameterName = "@postcode";
                    parPostcode.Value = postcode;
                    comTuin.Parameters.Add(parPostcode);

                    DbParameter parPlaats = comTuin.CreateParameter();
                    parPlaats.ParameterName = "@plaats";
                    parPlaats.Value = plaats;
                    comTuin.Parameters.Add(parPlaats);

                    conTuin.Open();
                    return comTuin.ExecuteNonQuery() != 0;
                }
            }
        }

        public void VervangLeverancier(string oudeLeverancierNr, string nieuweLeverancierNr)
        {
            var DbManager = new TuinleverancierDbManager();

            var opties = new TransactionOptions();
            opties.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (var traVervangLeverancier = new TransactionScope(TransactionScopeOption.Required, opties))
            {
                using (var conTuin = DbManager.GetConnection())
                {
                    using (var comVervangen = conTuin.CreateCommand())
                    {
                        comVervangen.CommandType = CommandType.Text;
                        comVervangen.CommandText = "UPDATE Planten SET Levnr=@nieuw WHERE Levnr=@oud";

                        var parOud = comVervangen.CreateParameter();
                        parOud.ParameterName = "@oud";
                        parOud.Value = oudeLeverancierNr;
                        comVervangen.Parameters.Add(parOud);

                        var parNieuw = comVervangen.CreateParameter();
                        parNieuw.ParameterName = "@nieuw";
                        parNieuw.Value = nieuweLeverancierNr;
                        comVervangen.Parameters.Add(parNieuw);

                        conTuin.Open();
                        if (comVervangen.ExecuteNonQuery() == 0)
                        {
                            throw new Exception("Een van de leveranciers bestaat niet");
                        }
                    } // using comVervangen
                    using (var comVerwijder = conTuin.CreateCommand())
                    {
                        comVerwijder.CommandType = CommandType.Text;
                        comVerwijder.CommandText = "DELETE FROM Leveranciers WHERE LevNr=@oud";

                        var parOud = comVerwijder.CreateParameter();
                        parOud.ParameterName = "@oud";
                        parOud.Value = oudeLeverancierNr;
                        comVerwijder.Parameters.Add(parOud);

                        if (comVerwijder.ExecuteNonQuery() == 0)
                        {
                            throw new Exception("Het opgegeven oude leveranciers nummer bestaat niet");
                        }
                        traVervangLeverancier.Complete();
                    } // using comVerwijder
                }
            }
        }

        public decimal BerekenGemiddeldeKostprijs(string soort)
        {
            var dbManager = new TuinleverancierDbManager();
            using (var conTuin = dbManager.GetConnection())
            {
                using (var comGemPrijs = conTuin.CreateCommand())
                {
                    comGemPrijs.CommandType = CommandType.StoredProcedure;
                    comGemPrijs.CommandText = "BerekenGemiddeldeKostprijs";

                    var parSoort = comGemPrijs.CreateParameter();
                    parSoort.ParameterName = "@soort";
                    parSoort.Value = soort;
                    comGemPrijs.Parameters.Add(parSoort);

                    conTuin.Open();
                    object result = comGemPrijs.ExecuteScalar();
                    if (result == null)
                    {
                        throw new Exception("Soortnummer bestaat niet");
                    }
                    else
                    {
                        return (decimal)result;
                    }
                }
            }
        }
    }
}