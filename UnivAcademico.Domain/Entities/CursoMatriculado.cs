namespace UnivAcademico.Domain.Entities
{
    public class CursoMatriculado
    {
        public int id { get; set; }
        public int ciclo { get; set; }
        public int curso_id { get; set; }
        public string nombre { get; set; } = "";
        public int creditos { get; set; }
        public string tipo { get; set; } = "";
        public string seccion { get; set; } = "";
        public string modalidad { get; set; } = "";
        public string horario { get; set; } = "";
        public int matricula_id { get; set; }
    }
}
