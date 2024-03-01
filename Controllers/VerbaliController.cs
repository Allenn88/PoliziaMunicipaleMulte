using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PoliziaMunicipaleMulte.Models;

namespace PoliziaMunicipaleMulte.Controllers
{
    public class VerbaliController : Controller
    {
        private string connString = "Server=DESKTOP-5MD1NN4\\SQLEXPRESS; Initial Catalog=PoliziaMunicipale; Integrated Security=true; TrustServerCertificate=True";

        [HttpGet]
        public IActionResult Verbali()
        {
            var conn = new SqlConnection(connString);
            List<Verbale> Verbali = [];

            try
            {
                conn.Open();
                var command = new SqlCommand("select * from Verbale", conn);

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var verbali = new Verbale()
                        {
                            IDVerbale = (int)reader["IDVerbale"],
                            DataViolazione = ((DateTime)reader["DataViolazione"]).Date,
                            IndirizzoViolazione = reader["IndirizzoViolazione"].ToString(),
                            NominativoAgente = reader["NominativoAgente"].ToString(),
                            DataTrascrizioneVerbale = ((DateTime)reader["DataTrascrizioneVerbale"]).Date,
                            Importo = (decimal)reader["Importo"],
                            DecurtamentoPunti = (int)reader["DecurtamentoPunti"],
                            IDAnagrafica = reader["IDAnagrafica"] is DBNull ? 0 : (int)reader["IDAnagrafica"],
                            IDViolazione = reader["IDViolazione"] is DBNull ? 0 : (int)reader["IDViolazione"],
                        };
                        Verbali.Add(verbali);
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
            return View(Verbali);
        }

        [HttpGet]
        public IActionResult AddMulta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMulta(Verbale verbali)
        {
            try
            {
                var conn = new SqlConnection(connString);
                conn.Open();

                var command = new SqlCommand(@"
            INSERT INTO Verbale
            (DataViolazione, IndirizzoViolazione, NominativoAgente, DataTrascrizioneVerbale, Importo, DecurtamentoPunti, IDAnagrafica, IDViolazione) VALUES
            (@DataViolazione, @IndirizzoViolazione, @NominativoAgente, @DataTrascrizioneVerbale, @Importo, @DecurtamentoPunti, @IDAnagrafica, @IDViolazione)", conn);

                command.Parameters.AddWithValue("@DataViolazione", verbali.DataViolazione);
                command.Parameters.AddWithValue("@IndirizzoViolazione", verbali.IndirizzoViolazione);
                command.Parameters.AddWithValue("@NominativoAgente", verbali.NominativoAgente);
                command.Parameters.AddWithValue("@DataTrascrizioneVerbale", verbali.DataTrascrizioneVerbale);
                command.Parameters.AddWithValue("@Importo", verbali.Importo);
                command.Parameters.AddWithValue("@DecurtamentoPunti", verbali.DecurtamentoPunti);
                command.Parameters.AddWithValue("@IDAnagrafica", verbali.IDAnagrafica);
                command.Parameters.AddWithValue("@IDViolazione", verbali.IDViolazione);

                var nRows = command.ExecuteNonQuery();
                conn.Close();

                return RedirectToAction("Verbali");
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }
    }
}
