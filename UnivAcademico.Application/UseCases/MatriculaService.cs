using UnivAcademico.Application.DTOs;
using UnivAcademico.Domain.Entities;
using UnivAcademico.Domain.Interfaces;

namespace UnivAcademico.Application.UseCases
{
    public class MatriculaService
    {
        private readonly IMatriculaRepository _matriculaRepository;

        public MatriculaService(IMatriculaRepository matriculaRepository)
        {
            _matriculaRepository = matriculaRepository;
        }

        public async Task<Estudiante?> ObtenerEstudianteInfo(PersonaDto persona)
        {
            return await _matriculaRepository.ObtenerEstudianteInfo(persona.id);
        }

        public async Task<List<OfertaAcad>> ObtenerOfertaAcademicaAsync(int estudianteId)
        {
            return await _matriculaRepository.ObtenerOfertaAcademicaAsync(estudianteId);
        }

        public async Task<List<CursoMatriculado>> RegistrarMatriculaAsync(MatriculaDto matricula)
        {
            List<int> horarios = new List<int>();

            foreach (var item in matricula.horarios)
            {
                horarios.Add(item.horario);
            }

            return await _matriculaRepository.RegistrarMatriculaAsync(matricula.estudiante_id, horarios);
        }

        public async Task<List<CursoMatriculado>> ObtenerCursosMatriculadosAsync(int matriculaId)
        {
            return await _matriculaRepository.ObtenerCursosMatriculadosAsync(matriculaId);
        }
    }
}