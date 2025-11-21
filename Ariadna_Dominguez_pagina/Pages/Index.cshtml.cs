using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Numerics;

namespace Ariadna_Dominguez_pagina.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty] public string? nombre { get; set; }
        [BindProperty] public string? apellido { get; set; }
        [BindProperty] public string? genero { get; set; }
        [BindProperty] public string? correo { get; set; }
        [BindProperty] public string? numero { get; set; }

        public string mensaje = "";

        public void OnPost()
        {
            string connectionString = "server=localhost;port=3306;database=formulario_db;user=root;password=arisita1103";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Agregamos el campo 'genero' en el INSERT
                    string query = @"INSERT INTO registros (nombre, apellido, genero, correo, numero)
                                     VALUES (@nombre, @apellido, @genero, @correo, @numero)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@apellido", apellido);
                        cmd.Parameters.AddWithValue("@genero", genero); // Nuevo parámetro
                        cmd.Parameters.AddWithValue("@correo", correo);
                        cmd.Parameters.AddWithValue("@numero", numero);

                        cmd.ExecuteNonQuery();
                        mensaje = "Registro exitoso";

                        // Limpiamos los campos del formulario
                        nombre = "";
                        apellido = "";
                        genero = "";
                        correo = "";
                        numero = "";
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al registrar: " + ex.Message;
            }
        }
    }
}
