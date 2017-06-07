using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace AdoGemeenschap
{
    public class PlantenManager
    {
        public List<string> GetPlantenBySoort(int soortNr)
        {
            List<string> planten = new List<string>();
            var manager = new TuinleverancierDbManager();
            using (var conTuin = manager.GetConnection())
            {
                using (var comGet = conTuin.CreateCommand())
                {
                    comGet.CommandType = CommandType.Text;
                    comGet.CommandText = "SELECT * FROM Planten WHERE SoortNr = @soortNr ORDER BY naam";

                    var parSoort = comGet.CreateParameter();
                    parSoort.ParameterName = "@soortNr";
                    parSoort.Value = soortNr;
                    comGet.Parameters.Add(parSoort);

                    conTuin.Open();
                    using (var rdrPlanten = comGet.ExecuteReader())
                    {
                        while (rdrPlanten.Read())
                        {
                            planten.Add(rdrPlanten["naam"].ToString());
                        }
                    }
                }
            }
            return planten;
        }

        public List<Soort> GetSoort()
        {
            List<Soort> soorten = new List<Soort>();
            var manager = new TuinleverancierDbManager();
            using (var conTuin = manager.GetConnection())
            {
                using (var comGet = conTuin.CreateCommand())
                {
                    comGet.CommandType = CommandType.Text;
                    comGet.CommandText = "SELECT SoortNr, Soort FROM Soorten ORDER BY Soort";
                    conTuin.Open();

                    using (var rdrPlanten = comGet.ExecuteReader())
                    {
                        Int32 soortNrPos = rdrPlanten.GetOrdinal("SoortNr");
                        Int32 soortNaamPos = rdrPlanten.GetOrdinal("Soort");

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
    }
}