namespace UnivAcademico.Application.DTOs
{
    public class MatriculaDto
    {
        public int estudiante_id { get; set; }
        public int semestre_id { get; set; }
        public int categoria_id { get; set; }

        public List<HorarioDto> horarios { get; set; } = new List<HorarioDto>();
    }
}
