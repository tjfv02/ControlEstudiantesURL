using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlEstudiantes_Interfaz.ViewModel
{
    public class Alumnos
    {
        [DisplayName("Marca Temporal")]
        public DateTime Marca_Temporal { get; set; }
        [DisplayName("Carné")]
        public int Carne {get; set;}
        public string Carrera {get; set;}
        public string Curso {get; set;}
        public string Modalidad { get; set; }
        [DisplayName("Sección")]
        public int Seccion { get; set; }
        public string Grupo { get; set; }
    }
}
