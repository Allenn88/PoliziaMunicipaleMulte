using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PoliziaMunicipaleMulte.Models;

namespace PoliziaMunicipaleMulte.Controllers
{
    public class AnagraficaController : Controller
    {
            private string connString = "Server=DESKTOP-5MD1NN4\\SQLEXPRESS; Initial Catalog=PoliziaMunicipale; Integrated Security=true; TrustServerCertificate=True";

        [HttpGet]
        public IActionResult Anagrafica()
        {
            var conn = new SqlConnection(connString);
            List<Anagrafica> anagrafica = [];

            try
            {
                conn.Open();
                var command = new SqlCommand("select * from Anagrafica", conn);

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var anagraficaList = new Anagrafica()
                        {
                            IDAnagrafica = (int)reader["IDAnagrafica"],
                            Cognome = reader["Cognome"].ToString(),
                            Nome = reader["Nome"].ToString(),
                            Indirizzo = reader["Indirizzo"].ToString(),
                            Citta = reader["Citta"].ToString(),
                            Cap = reader["Cap"].ToString(),
                            CodiceFiscale = reader["CodiceFiscale"].ToString(),
                        };
                        anagrafica.Add(anagraficaList);
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
            return View(anagrafica);
        }
        [HttpGet]
        public IActionResult AddAnagrafica()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddAnagrafica(Anagrafica anagrafica)
        {
            try
            {
                var conn = new SqlConnection(connString);
                conn.Open();

                var command = new SqlCommand(@"
            INSERT INTO Anagrafica
            (Cognome, Nome, Indirizzo, Citta, Cap, CodiceFiscale) VALUES
            (@Cognome, @Nome, @Indirizzo, @Citta, @Cap, @CodiceFiscale)", conn);

                command.Parameters.AddWithValue("@Cognome", anagrafica.Cognome);
                command.Parameters.AddWithValue("@Nome", anagrafica.Nome);
                command.Parameters.AddWithValue("@Indirizzo", anagrafica.Indirizzo);
                command.Parameters.AddWithValue("@Citta", anagrafica.Citta);
                command.Parameters.AddWithValue("@Cap", anagrafica.Cap);
                command.Parameters.AddWithValue("@CodiceFiscale", anagrafica.CodiceFiscale);

                var nRows = command.ExecuteNonQuery();
                conn.Close();

                return RedirectToAction("Anagrafica");
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
    }
}

