using UnivAcademico.Domain.Entities;

namespace UnivAcademico.Domain.Interfaces
{
    public interface IMatriculaRepository
    {
        Task<Estudiante?> ObtenerEstudianteInfoAsync(int personaId);
        Task<List<OfertaAcad>> ObtenerOfertaAcademicaAsync(int estudianteId);
        Task<List<CursoMatriculado>> RegistrarMatriculaAsync(int estudianteId, List<int> horarios);
        Task<List<CursoMatriculado>> ObtenerCursosMatriculadosAsync(int matriculaId);
    }
}
