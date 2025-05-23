using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnivAcademico.Application.Events
{
    public class MatriculaRegistradaEvent
    {
        public int estudiante_id { get; set; }
        public int semestre_id { get; set; }
        public int categoria_id { get; set; }
        public int creditos { get; set; }
    }
}
