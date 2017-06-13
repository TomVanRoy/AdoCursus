using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System;
using System.Collections.ObjectModel;

namespace AdoGemeenschap
{
    public class LeverancierManager
    {
        public ObservableCollection<Leverancier> GetLeveranciers()
        {
            ObservableCollection<Leverancier> leveranciers = new ObservableCollection<Leverancier>();
            var manager = new TuinDbManager();

            using (var conTuin = manager.GetConnection())
            {
                using (var comLeveranciers = conTuin.CreateCommand())
                {
                    comLeveranciers.CommandType = CommandType.Text;
                    comLeveranciers.CommandText = "SELECT * FROM Leveranciers";
                    conTuin.Open();

                    using (var rdrLeveranciers = comLeveranciers.ExecuteReader())
                    {
                        Int32 levNrPos = rdrLeveranciers.GetOrdinal("LevNr");
                        Int32 naamPos = rdrLeveranciers.GetOrdinal("Naam");
                        Int32 adresPos = rdrLeveranciers.GetOrdinal("Adres");
                        Int32 postNrPos = rdrLeveranciers.GetOrdinal("PostNr");
                        Int32 woonplaatsPos = rdrLeveranciers.GetOrdinal("Woonplaats");

                        while (rdrLeveranciers.Read())
                        {
                            leveranciers.Add(new Leverancier(
                                rdrLeveranciers.GetInt32(levNrPos),
                                rdrLeveranciers.GetString(naamPos),
                                rdrLeveranciers.GetString(adresPos),
                                rdrLeveranciers.GetString(postNrPos),
                                rdrLeveranciers.GetString(woonplaatsPos)
                                ));
                        } // do while
                    } // using rdrBrouwers
                } // using comBrouwers
            } // using conBieren
            return leveranciers;
        }

        public List<Leverancier> SchrijfVerwijderingen(List<Leverancier> leveranciers)
        {
            var nietVerwijderdeLeveranciers = new List<Leverancier>();
            var manager = new TuinDbManager();
            using (var conTuin = manager.GetConnection())
            {
                using (var comDelete = conTuin.CreateCommand())
                {
                    comDelete.CommandType = CommandType.Text;
                    comDelete.CommandText = "DELETE FROM Leveranciers WHERE LevNr=@levnr";

                    var parLevNr = comDelete.CreateParameter();
                    parLevNr.ParameterName = "@levnr";
                    comDelete.Parameters.Add(parLevNr);

                    conTuin.Open();

                    foreach (Leverancier l in leveranciers)
                    {
                        try
                        {
                            parLevNr.Value = l.LevNr;
                            Console.WriteLine(parLevNr);
                            if (comDelete.ExecuteNonQuery() == 0)
                            {
                                nietVerwijderdeLeveranciers.Add(l);
                            }
                        }
                        catch (Exception)
                        {
                            nietVerwijderdeLeveranciers.Add(l);
                        }
                    } // foreach
                } // comDelete
            } // conTuin
            return nietVerwijderdeLeveranciers;
        }

        public List<Leverancier> SchrijfToevoegingen(List<Leverancier> leveranciers)
        {
            var nietToegevoegdeLeveranciers = new List<Leverancier>();
            var manager = new TuinDbManager();
            using (var conTuin = manager.GetConnection())
            {
                using (var comInsert = conTuin.CreateCommand())
                {
                    comInsert.CommandType = CommandType.Text;
                    comInsert.CommandText = "INSERT INTO Leveranciers (Naam, Adres, PostNr, Woonplaats) VALUES (@naam, @adres, @postnr, @woonplaats)";

                    var parAdres = comInsert.CreateParameter();
                    parAdres.ParameterName = "@adres";
                    comInsert.Parameters.Add(parAdres);

                    var parNaam = comInsert.CreateParameter();
                    parNaam.ParameterName = "@naam";
                    comInsert.Parameters.Add(parNaam);

                    var parPostNr = comInsert.CreateParameter();
                    parPostNr.ParameterName = "@postnr";
                    comInsert.Parameters.Add(parPostNr);

                    var parWoonplaats = comInsert.CreateParameter();
                    parWoonplaats.ParameterName = "@woonplaats";
                    comInsert.Parameters.Add(parWoonplaats);

                    conTuin.Open();

                    foreach (Leverancier l in leveranciers)
                    {
                        try
                        {
                            parAdres.Value = l.Adres;
                            parNaam.Value = l.Naam;
                            parPostNr.Value = l.PostNr;
                            parWoonplaats.Value = l.Woonplaats;

                            if (comInsert.ExecuteNonQuery() == 0)
                            {
                                nietToegevoegdeLeveranciers.Add(l);
                            }
                        }
                        catch (Exception)
                        {
                            nietToegevoegdeLeveranciers.Add(l);
                        }
                    } // foreach
                } // comDelete
            } // conTuin
            return nietToegevoegdeLeveranciers;
        }

        public List<Leverancier> SchrijfWijzigingen(List<Leverancier> leveranciers)
        {
            List<Leverancier> nietGewijzigdeLeveranciers = new List<Leverancier>();
            var manager = new TuinDbManager();
            using (var conTuin = manager.GetConnection())
            {
                using (var comUpdate = conTuin.CreateCommand())
                {
                    comUpdate.CommandType = CommandType.Text;
                    comUpdate.CommandText = "UPDATE Leveranciers SET Naam=@naam, Adres=@adres, PostNr=@postnr, Woonplaats=@woonplaats WHERE LevNr=@levnr";

                    var parAdres = comUpdate.CreateParameter();
                    parAdres.ParameterName = "@adres";
                    comUpdate.Parameters.Add(parAdres);

                    var parLevNr = comUpdate.CreateParameter();
                    parLevNr.ParameterName = "@levnr";
                    comUpdate.Parameters.Add(parLevNr);

                    var parNaam = comUpdate.CreateParameter();
                    parNaam.ParameterName = "@naam";
                    comUpdate.Parameters.Add(parNaam);

                    var parPostNr = comUpdate.CreateParameter();
                    parPostNr.ParameterName = "@postnr";
                    comUpdate.Parameters.Add(parPostNr);

                    var parWoonplaats = comUpdate.CreateParameter();
                    parWoonplaats.ParameterName = "@woonplaats";
                    comUpdate.Parameters.Add(parWoonplaats);

                    conTuin.Open();

                    foreach (Leverancier l in leveranciers)
                    {
                        try
                        {
                            parAdres.Value = l.Adres;
                            parLevNr.Value = l.LevNr;
                            parNaam.Value = l.Naam;
                            parPostNr.Value = l.PostNr;
                            parWoonplaats.Value = l.Woonplaats;

                            if (comUpdate.ExecuteNonQuery() == 0)
                            {
                                nietGewijzigdeLeveranciers.Add(l);
                            }
                        }
                        catch (Exception)
                        {
                            nietGewijzigdeLeveranciers.Add(l);
                        }
                    }
                }
            }
            return nietGewijzigdeLeveranciers;
        }
    }
}