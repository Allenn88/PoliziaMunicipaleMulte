using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PoliziaMunicipaleMulte.Models; 
using System.Collections.Generic;

public class SearchController : Controller
{
    private string connString = "Server=DESKTOP-5MD1NN4\\SQLEXPRESS; Initial Catalog=PoliziaMunicipale; Integrated Security=true; TrustServerCertificate=True";

    public IActionResult Search()
    {
        return View();
    }

    [HttpGet]
    public IActionResult SearchTotaleTrasgressione()
    {
        // Aprire la connessione
        using (var conn = new SqlConnection(connString))
        {
            List<VerbaliTrascritti> trascrizioni = new List<VerbaliTrascritti>();

            try
            {
                conn.Open();
                // Creare il comando
                var command = new SqlCommand(@"
                  SELECT
                      A.[IDAnagrafica],
                      A.[Cognome],
                      A.[Nome],
                      COUNT(V.[IDVerbale]) AS TotaleVerbaliTrascritti
                  FROM
                      [PoliziaMunicipale].[dbo].[Anagrafica] A
                  JOIN
                      [PoliziaMunicipale].[dbo].[Verbale] V ON A.[IDAnagrafica] = V.[IDAnagrafica]
                  GROUP BY
                      A.[IDAnagrafica],
                      A.[Cognome],
                      A.[Nome]", conn);

                // Eseguire il comando
                var reader = command.ExecuteReader();

                // Usare i dati
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var verbaliTrascritti = new VerbaliTrascritti()
                        {
                            IDAnagrafica = (int)reader["IDAnagrafica"],
                            Cognome = reader["Cognome"].ToString(),
                            Nome = reader["Nome"].ToString(),
                            TotaleVerbaliTrascritti = (int)reader["TotaleVerbaliTrascritti"]
                        };
                        trascrizioni.Add(verbaliTrascritti);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log dell'errore o gestione dell'errore appropriata
                return View("Error");
            }

            return View(trascrizioni);
        }
    }
    public IActionResult SearchImporto400()
    {
        // Aprire la connessione
        using (var conn = new SqlConnection(connString))
        {
            List<VerbaleDettaglio> trascrizioni = new List<VerbaleDettaglio>();

            try
            {
                conn.Open();
                // Creare il comando
                var command = new SqlCommand(@"
         SELECT
             A.[IDAnagrafica],
             A.[Cognome],
             A.[Nome],
             V.[Importo],
             V.[DataViolazione],
             V.[DecurtamentoPunti]
         FROM
             [PoliziaMunicipale].[dbo].[Anagrafica] A
         JOIN
             [PoliziaMunicipale].[dbo].[Verbale] V ON A.[IDAnagrafica] = V.[IDAnagrafica]
         WHERE
             V.[Importo] > 400
     ", conn);

                // Eseguire il comando
                var reader = command.ExecuteReader();

                // Usare i dati
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var VerbaleDettaglio = new VerbaleDettaglio()
                        {
                            IDAnagrafica = (int)reader["IDAnagrafica"],
                            Cognome = reader["Cognome"].ToString(),
                            Nome = reader["Nome"].ToString(),
                            Importo = (decimal)reader["Importo"],
                            DataViolazione = (DateTime)reader["DataViolazione"],
                            DecurtamentoPunti = (int)reader["DecurtamentoPunti"]
                        };
                        trascrizioni.Add(VerbaleDettaglio);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log dell'errore o gestione dell'errore appropriata
                return View("Error");
            }

            return View(trascrizioni);
        }
    }
    public IActionResult SearchPunti()
    {
        // Aprire la connessione
        using (var conn = new SqlConnection(connString))
        {
            List<VerbaliTrascritti> trascrizioni = new List<VerbaliTrascritti>();

            try
            {
                conn.Open();
                // Creare il comando
                var command = new SqlCommand(@"
         SELECT
             A.[IDAnagrafica],
             A.[Cognome],
             A.[Nome],
             SUM(V.[DecurtamentoPunti]) AS TotalePuntiDecurtati
         FROM
             [PoliziaMunicipale].[dbo].[Anagrafica] A
         JOIN
             [PoliziaMunicipale].[dbo].[Verbale] V ON A.[IDAnagrafica] = V.[IDAnagrafica]
         GROUP BY
             A.[IDAnagrafica],
             A.[Cognome],
             A.[Nome]
     ", conn);

                // Eseguire il comando
                var reader = command.ExecuteReader();

                // Usare i dati
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var VerbaliTrascritto = new VerbaliTrascritti()
                        {
                            IDAnagrafica = (int)reader["IDAnagrafica"],
                            Cognome = reader["Cognome"].ToString(),
                            Nome = reader["Nome"].ToString(),
                            TotaleVerbaliTrascritti = (int)reader["TotalePuntiDecurtati"]  // Corretto il nome della colonna
                        };
                        trascrizioni.Add(VerbaliTrascritto);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log dell'errore o gestione dell'errore appropriata
                return View("Error");
            }

            return View(trascrizioni);
        }
    }
    public IActionResult SearchPunti10()
    {
        // Aprire la connessione
        using (var conn = new SqlConnection(connString))
        {
            List<VerbaleDettaglio> trascrizioni = new List<VerbaleDettaglio>();

            try
            {
                conn.Open();
                // Creare il comando
                var command = new SqlCommand(@"
         SELECT
             A.[IDAnagrafica],
             A.[Cognome],
             A.[Nome],
             V.[Importo],
             V.[DataViolazione],
             V.[DecurtamentoPunti]
         FROM
             [PoliziaMunicipale].[dbo].[Anagrafica] A
         JOIN
             [PoliziaMunicipale].[dbo].[Verbale] V ON A.[IDAnagrafica] = V.[IDAnagrafica]
         WHERE
             V.[DecurtamentoPunti] > 10
     ", conn);

                // Eseguire il comando
                var reader = command.ExecuteReader();

                // Usare i dati
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var VerbaleDettaglio = new VerbaleDettaglio()
                        {
                            IDAnagrafica = (int)reader["IDAnagrafica"],
                            Cognome = reader["Cognome"].ToString(),
                            Nome = reader["Nome"].ToString(),
                            Importo = (decimal)reader["Importo"],
                            DataViolazione = (DateTime)reader["DataViolazione"],
                            DecurtamentoPunti = (int)reader["DecurtamentoPunti"]
                        };
                        trascrizioni.Add(VerbaleDettaglio);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log dell'errore o gestione dell'errore appropriata
                return View("Error");
            }

            return View(trascrizioni);
        }

    }
}

