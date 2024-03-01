using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PoliziaMunicipaleMulte.Models;
using System;
using System.Collections.Generic;

namespace PoliziaMunicipaleMulte.Controllers
{
    public class ViolazioneController : Controller
    {
        private string connString = "Server=DESKTOP-5MD1NN4\\SQLEXPRESS; Initial Catalog=PoliziaMunicipale; Integrated Security=true; TrustServerCertificate=True";

        [HttpGet]
        public IActionResult Violazione()
        {
            var conn = new SqlConnection(connString);
            List<Violazione> violazioneList = new List<Violazione>();

            try
            {
                conn.Open();
                var command = new SqlCommand("SELECT * FROM Violazione", conn);

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var violazione = new Violazione()
                        {
                            IDViolazione = (int)reader["IDViolazione"],
                            Descrizione = reader["Descrizione"].ToString(),
                        };
                        violazioneList.Add(violazione);
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
            return View(violazioneList);
        }

        [HttpGet]
        public IActionResult AddViolazione()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddViolazione(Violazione violazione)
        {
            try
            {
                var conn = new SqlConnection(connString);
                conn.Open();

                var command = new SqlCommand(@"
                    INSERT INTO Violazione
                    (Descrizione) VALUES (@Descrizione)", conn);

                command.Parameters.AddWithValue("@Descrizione", violazione.Descrizione);

                var nRows = command.ExecuteNonQuery();
                conn.Close();

                return RedirectToAction("Violazione");
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
    }
}
