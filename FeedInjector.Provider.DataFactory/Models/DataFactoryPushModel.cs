using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FeedInjector.Provider.DataFactory.Models
{
    /// <summary>
    /// Equipo1_Nombre Equipo1_Goles - Equipo2_Nombre Equipo2_Goles
    /// </summary>
    public class ResultadoModel
    {
        [Required]
        public string Equipo1_Nombre { get; set; }
        [Required]
        public int? Equipo1_ID { get; set; }
        [Required]
        public int? Equipo1_Goles { get; set; }
        [Required]
        public string Equipo2_Nombre { get; set; }
        [Required]
        public int? Equipo2_ID { get; set; }
        [Required]
        public int? Equipo2_Goles { get; set; }

    }

    /// <summary>
    /// Equipo1_Jugador de Equipo1_Nombre tuvo una Evento_Glosa contra Equipo2_Nombre!
    /// </summary>
    public class EventoModel
    {
        [Required]
        public string Equipo1_Nombre { get; set; }
        [Required]
        public int? Equipo1_ID { get; set; }
        [Required]
        public string Equipo1_Jugador { get; set; }
        [Required]
        public string Evento_Glosa { get; set; }
        [Required]
        public string Equipo2_Nombre { get; set; }
        [Required]
        public int? Equipo2_ID { get; set; }
    }
    /*
     •	Resultados diarios
•	Resultados al término del partido
•	Goles, Tarjetas rojas y resultado final de un partido
*/
}