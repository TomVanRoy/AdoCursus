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
    public class BrouwerManager
    {
        public ObservableCollection<Brouwer> GetBrouwersBeginNaam(string beginNaam)
        {
            ObservableCollection<Brouwer> brouwers = new ObservableCollection<Brouwer>();
            var manager = new BierenDbManager();
            using (var conBieren = manager.GetConnection())
            {
                using (var comBrouwers = conBieren.CreateCommand())
                {
                    comBrouwers.CommandType = CommandType.Text;
                    if (beginNaam != string.Empty)
                    {
                        comBrouwers.CommandText = "SELECT * FROM Brouwers WHERE BrNaam LIKE @zoals ORDER BY BrNaam";

                        var parZoals = comBrouwers.CreateParameter();
                        parZoals.ParameterName = "@zoals";
                        parZoals.Value = beginNaam + "%";
                        comBrouwers.Parameters.Add(parZoals);
                    }
                    else
                    {
                        comBrouwers.CommandText = "SELECT * FROM Brouwers";
                    }
                    conBieren.Open();

                    using (var rdrBrouwers = comBrouwers.ExecuteReader())
                    {
                        Int32 brouwerNrPos = rdrBrouwers.GetOrdinal("BrouwerNr");
                        Int32 brNaamPos = rdrBrouwers.GetOrdinal("BrNaam");
                        Int32 adresPos = rdrBrouwers.GetOrdinal("Adres");
                        Int32 postcodePos = rdrBrouwers.GetOrdinal("Postcode");
                        Int32 gemeentePos = rdrBrouwers.GetOrdinal("Gemeente");
                        Int32 omzetPos = rdrBrouwers.GetOrdinal("Omzet");

                        Int32? omzet;

                        while (rdrBrouwers.Read())
                        {
                            if (rdrBrouwers.IsDBNull(omzetPos))
                            {
                                omzet = null;
                            }
                            else
                            {
                                omzet = rdrBrouwers.GetInt32(omzetPos);
                            }
                            brouwers.Add(new Brouwer(
                                rdrBrouwers.GetInt32(brouwerNrPos),
                                rdrBrouwers.GetString(brNaamPos),
                                rdrBrouwers.GetString(adresPos),
                                rdrBrouwers.GetInt16(postcodePos),
                                rdrBrouwers.GetString(gemeentePos),
                                omzet)
                                );
                        } // do while
                    } // using rdrBrouwers
                } // using comBrouwers
            } // using conBieren
            return brouwers;
        }

        public List<Brouwer> SchrijfToevoegingen(List<Brouwer> brouwers)
        {
            List<Brouwer> nietToegevoegdeBrouwers = new List<Brouwer>();
            var manager = new BierenDbManager();
            using (var conBieren = manager.GetConnection())
            {
                using (var comInsert = conBieren.CreateCommand())
                {
                    comInsert.CommandType = CommandType.Text;
                    comInsert.CommandText = "Insert into brouwers (BrNaam, Adres, Postcode, Gemeente, Omzet) values(@brnaam, @adres, @postcode, @gemeente, @omzet)";

                    var parBrNaam = comInsert.CreateParameter();
                    parBrNaam.ParameterName = "@brnaam";
                    comInsert.Parameters.Add(parBrNaam);

                    var parAdres = comInsert.CreateParameter();
                    parAdres.ParameterName = "@adres";
                    comInsert.Parameters.Add(parAdres);

                    var parPostcode = comInsert.CreateParameter();
                    parPostcode.ParameterName = "@postcode";
                    comInsert.Parameters.Add(parPostcode);

                    var parGemeente = comInsert.CreateParameter();
                    parGemeente.ParameterName = "@gemeente";
                    comInsert.Parameters.Add(parGemeente);

                    var parOmzet = comInsert.CreateParameter();
                    parOmzet.ParameterName = "@omzet";
                    comInsert.Parameters.Add(parOmzet);

                    conBieren.Open();
                    foreach (Brouwer eenBrouwer in brouwers)
                    {
                        try
                        {
                            parBrNaam.Value = eenBrouwer.BrNaam;
                            parAdres.Value = eenBrouwer.Adres;
                            parPostcode.Value = eenBrouwer.Postcode;
                            parGemeente.Value = eenBrouwer.Gemeente;
                            if (eenBrouwer.Omzet.HasValue)
                            {
                                parOmzet.Value = eenBrouwer.Omzet;
                            }
                            else
                            {
                                parOmzet.Value = DBNull.Value;
                            }
                            if (comInsert.ExecuteNonQuery() == 0)
                            {
                                nietToegevoegdeBrouwers.Add(eenBrouwer);
                            }
                        }
                        catch (Exception)
                        {
                            nietToegevoegdeBrouwers.Add(eenBrouwer);
                        }
                    } // foreach
                } // comInsert
            } // conBieren
            return nietToegevoegdeBrouwers;
        }

        public List<Brouwer> SchrijfVerwijderingen(List<Brouwer> brouwers)
        {
            List<Brouwer> nietVerwijderdeBrouwers = new List<Brouwer>();
            var manager = new BierenDbManager();
            using (var conBieren = manager.GetConnection())
            {
                conBieren.Open();
                using (var comDelete = conBieren.CreateCommand())
                {
                    comDelete.CommandType = CommandType.Text;
                    comDelete.CommandText = "delete from brouwers where BrouwerNr = @brouwernr";
                    var parBrouwerNr = comDelete.CreateParameter();
                    parBrouwerNr.ParameterName = "@brouwernr";
                    comDelete.Parameters.Add(parBrouwerNr);
                    foreach (Brouwer eenBrouwer in brouwers)
                    {
                        try
                        {
                            parBrouwerNr.Value = eenBrouwer.BrouwerNr;
                            if (comDelete.ExecuteNonQuery() == 0)
                                nietVerwijderdeBrouwers.Add(eenBrouwer);
                        }
                        catch (Exception)
                        {
                            nietVerwijderdeBrouwers.Add(eenBrouwer);
                        }
                    } // foreach
                } // comDelete
            } // conBieren
            return nietVerwijderdeBrouwers;
        }

        public List<Brouwer> SchrijfWijzigingen(List<Brouwer> brouwers)
        {
            List<Brouwer> nietDoorgevoerdeBrouwers = new List<Brouwer>();
            var manager = new BierenDbManager();
            using (var conBier = manager.GetConnection())
            {
                using (var comEdit = conBier.CreateCommand())
                {
                    comEdit.CommandType = CommandType.Text;
                    comEdit.CommandText = "UPDATE Brouwers SET BrNaam=@brnaam, Adres=@adres, Postcode=@postcode, Gemeente=@gemeente, Omzet=@omzet WHERE BrouwerNr=@brouwernr";

                    var parAdres = comEdit.CreateParameter();
                    parAdres.ParameterName = "@adres";
                    comEdit.Parameters.Add(parAdres);

                    var parBrNaam = comEdit.CreateParameter();
                    parBrNaam.ParameterName = "@brnaam";
                    comEdit.Parameters.Add(parBrNaam);

                    var parBrouwerNr = comEdit.CreateParameter();
                    parBrouwerNr.ParameterName = "@brouwernr";
                    comEdit.Parameters.Add(parBrouwerNr);

                    var parGemeente = comEdit.CreateParameter();
                    parGemeente.ParameterName = "@gemeente";
                    comEdit.Parameters.Add(parGemeente);

                    var parOmzet = comEdit.CreateParameter();
                    parOmzet.ParameterName = "@omzet";
                    comEdit.Parameters.Add(parOmzet);

                    var parPostcode = comEdit.CreateParameter();
                    parPostcode.ParameterName = "@postcode";
                    comEdit.Parameters.Add(parPostcode);

                    conBier.Open();

                    foreach (Brouwer eenBrouwer in brouwers)
                    {
                        try
                        {
                            parAdres.Value = eenBrouwer.Adres;
                            parBrNaam.Value = eenBrouwer.BrNaam;
                            parBrouwerNr.Value = eenBrouwer.BrouwerNr;
                            parGemeente.Value = eenBrouwer.Gemeente;
                            parPostcode.Value = eenBrouwer.Postcode;
                            if (eenBrouwer.Omzet.HasValue)
                            {
                                parOmzet.Value = eenBrouwer.Omzet;
                            }
                            else
                            {
                                parOmzet.Value = DBNull.Value;
                            }
                            if (comEdit.ExecuteNonQuery() == 0)
                            {
                                nietDoorgevoerdeBrouwers.Add(eenBrouwer);
                            }
                        }
                        catch (Exception)
                        {
                            nietDoorgevoerdeBrouwers.Add(eenBrouwer);
                        }
                    } // foreach
                } // using comEdit
            } // conBier
            return nietDoorgevoerdeBrouwers;
        }
    }
}