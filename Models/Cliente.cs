﻿namespace ClienteMauiFrontend.Models
{
    //representan los datos que recibirás de la API.
    public class Cliente
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
