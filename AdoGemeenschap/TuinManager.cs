using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace AdoGemeenschap
{
    public class TuinManager
    {
        public List<Plant> GetPlanten(int soortnr)
        {
            var planten = new List<Plant>();
            var manager = new TuinDbManager();
            using (var conTuin = manager.GetConnection())
            {
                using (var comPlanten = conTuin.CreateCommand())
                {
                    comPlanten.CommandType = CommandType.Text;
                    comPlanten.CommandText = "select * from planten where soortnr = @soortnr order by naam";
                    var parSoortNr = comPlanten.CreateParameter();
                    parSoortNr.ParameterName = "@soortnr";
                    parSoortNr.Value = soortnr;
                    comPlanten.Parameters.Add(parSoortNr);
                    conTuin.Open();
                    using (var rdrPlanten = comPlanten.ExecuteReader())
                    {
                        var plantNaamPos = rdrPlanten.GetOrdinal("Naam");
                        var plantNrPos = rdrPlanten.GetOrdinal("PlantNr");
                        var levnrPos = rdrPlanten.GetOrdinal("Levnr");
                        var prijsPos = rdrPlanten.GetOrdinal("VerkoopPrijs");
                        var kleurPos = rdrPlanten.GetOrdinal("Kleur");
                        var soortPos = rdrPlanten.GetOrdinal("SoortNr");
                        while (rdrPlanten.Read())
                        {
                            var eenPlant = new Plant(
                                rdrPlanten.GetInt32(plantNrPos),
                                rdrPlanten.GetString(plantNaamPos),
                                rdrPlanten.GetInt32(soortPos),
                                rdrPlanten.GetInt32(levnrPos),
                                rdrPlanten.GetString(kleurPos),
                                rdrPlanten.GetDecimal(prijsPos)
                            );
                            planten.Add(eenPlant);
                        }
                    }
                }
            }
            return planten;
        }

        public void GewijzigdePlantenOpslaan(List<Plant> gewijzigdePlanten)
        {
            var manager = new TuinDbManager();
            using (var conTuin = manager.GetConnection())
            {
                using (var comUpate = conTuin.CreateCommand())
                {
                    comUpate.CommandType = CommandType.Text;
                    comUpate.CommandText = "UPDATE Planten SET Kleur=@kleur, VerkoopPrijs=@verkoopprijs WHERE PlantNr=@plantnr";

                    var parKleur = comUpate.CreateParameter();
                    parKleur.ParameterName = "@kleur";
                    comUpate.Parameters.Add(parKleur);

                    var parPlantNr = comUpate.CreateParameter();
                    parPlantNr.ParameterName = "@plantnr";
                    comUpate.Parameters.Add(parPlantNr);

                    var parVerkoopPrijs = comUpate.CreateParameter();
                    parVerkoopPrijs.ParameterName = "@verkoopprijs";
                    comUpate.Parameters.Add(parVerkoopPrijs);

                    conTuin.Open();

                    foreach (Plant p in gewijzigdePlanten)
                    {
                        parKleur.Value = p.Kleur;
                        parPlantNr.Value = p.PlantNr;
                        parVerkoopPrijs.Value = p.VerkoopPrijs;
                        if (comUpate.ExecuteNonQuery() == 0)
                        {
                            throw new Exception(p.PlantNaam + " opslaan mislukt");
                        }
                    }
                }
            }
        }

        public List<Soort> GetSoorten()
        {
            List<Soort> soorten = new List<Soort>();
            var manager = new TuinDbManager();
            using (var conTuin = manager.GetConnection())
            {
                using (var comSoorten = conTuin.CreateCommand())
                {
                    comSoorten.CommandType = CommandType.Text;
                    comSoorten.CommandText = "SELECT SoortNr, Soort FROM Soorten ORDER BY Soort";

                    conTuin.Open();

                    using (var rdrPlanten = comSoorten.ExecuteReader())
                    {
                        var soortNrPos = rdrPlanten.GetOrdinal("SoortNr");
                        var soortNaamPos = rdrPlanten.GetOrdinal("Soort");

                        while (rdrPlanten.Read())
                        {
                            soorten.Add(
                                new Soort(
                                    rdrPlanten.GetInt32(soortNrPos),
                                    rdrPlanten.GetString(soortNaamPos)
                                )
                             );
                        }
                    }
                }
            }
            return soorten;
        }

        public List<Plant> SchrijfWijzigingen(List<Plant> planten)
        {
            List<Plant> nietDoorgevoerdePlanten = new List<Plant>();
            var manager = new TuinDbManager();
            using (var conTuin = manager.GetConnection())
            {
                using (var comEdit = conTuin.CreateCommand())
                {
                    comEdit.CommandType = CommandType.Text;
                    comEdit.CommandText = "UPDATE Planten SET Kleur=@kleur, VerkoopPrijs=@verkoopprijs WHERE PlantNr=@plantnr";

                    var parKleur = comEdit.CreateParameter();
                    parKleur.ParameterName = "@kleur";
                    comEdit.Parameters.Add(parKleur);

                    var parPlantNr = comEdit.CreateParameter();
                    parPlantNr.ParameterName = "@plantnr";
                    comEdit.Parameters.Add(parPlantNr);

                    var parVerkoopPrijs = comEdit.CreateParameter();
                    parVerkoopPrijs.ParameterName = "@verkoopprijs";
                    comEdit.Parameters.Add(parVerkoopPrijs);

                    conTuin.Open();

                    foreach (Plant eenPlant in planten)
                    {
                        try
                        {
                            parKleur.Value = eenPlant.Kleur;
                            parPlantNr.Value = eenPlant.PlantNr;
                            parVerkoopPrijs.Value = eenPlant.VerkoopPrijs;
                            if (comEdit.ExecuteNonQuery() == 0)
                            {
                                nietDoorgevoerdePlanten.Add(eenPlant);
                            }
                        }
                        catch (Exception)
                        {
                            nietDoorgevoerdePlanten.Add(eenPlant);
                        }
                    } // foreach
                } // using comEdit
            } // conBier
            return nietDoorgevoerdePlanten;
        }
    }
}