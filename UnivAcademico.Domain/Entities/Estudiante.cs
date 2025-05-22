using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnivAcademico.Domain.Entities
{
    public class Estudiante
    {
        public int id { get; set; }
        public string sede { get; set; } = "";
        public string facultad { get; set; } = "";
        public string programa { get; set; } = "";
        public int plan { get; set; }
        public int curricula_id { get; set; }
        public int categoria_id { get; set; }
        public int semestre_id { get; set; }
        public string semestre { get; set; } = "";
        public int matricula_id { get; set; }
    }
}
