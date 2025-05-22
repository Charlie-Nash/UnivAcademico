using Microsoft.AspNetCore.Mvc;
using System.Net;
using UnivAcademico.Api.Helpers;
using UnivAcademico.Application.UseCases;
using UnivAcademico.Application.DTOs;
using UnivAcademico.Domain.Entities;
using Microsoft.AspNetCore.Identity.Data;

namespace UnivAcademico.Api.Controllers
{
    [ApiController]
    [Route("api/v1/estudiante/matricula")]

    public class MatriculaController : ControllerBase
    {
        private readonly MatriculaService _matriculaService;
        private readonly AppAuth _apptAuth;

        public MatriculaController(MatriculaService matriculaService, AppAuth appAuth)
        {
            _matriculaService = matriculaService;
            _apptAuth = appAuth;
        }

        [HttpPost("info")]
        public async Task<IActionResult> ObtenerEstudianteInfo([FromBody] PersonaDto request)
        {
            if (!_apptAuth.AppSecretKeyValidation(Request.Headers["x-api-app-key"].ToString()))
            {
                return Unauthorized(new { status = HttpStatusCode.Unauthorized, mensaje = "No autorizado" });
            }

            if (request.id == 0)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, mensaje = "FALTA: Código de usuario" });
            }

            Estudiante? estudiante = await _matriculaService.ObtenerEstudianteInfo(request);

            if (estudiante == null)
            {
                return Unauthorized(new { status = HttpStatusCode.Unauthorized, mensaje = "Usuario no registrado o inactivo" });
            }

            return Ok(estudiante);
        }

        [HttpGet("oferta-academica/{estudianteId}")]
        public async Task<ActionResult<List<OfertaAcad>>> ObtenerOfertaAcademicaAsync(int estudianteId)
        {
            if (!_apptAuth.AppSecretKeyValidation(Request.Headers["x-api-app-key"].ToString()))
            {
                return Unauthorized(new { status = HttpStatusCode.Unauthorized, mensaje = "No autorizado" });
            }

            if (estudianteId <= 0)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, mensaje = "FALTA: ID del estudiante" });
            }

            var lstOfertaAcad = await _matriculaService.ObtenerOfertaAcademicaAsync(estudianteId);

            if (lstOfertaAcad == null || lstOfertaAcad.Count == 0)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, mensaje = "No hay cursos disponibles para este estudiante" });
            }

            return Ok(lstOfertaAcad);
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarMatriculaAsync([FromBody] MatriculaDto matricula)
        {
            if (!_apptAuth.AppSecretKeyValidation(Request.Headers["x-api-app-key"].ToString()))
            {
                return Unauthorized(new { status = HttpStatusCode.Unauthorized, mensaje = "No autorizado" });
            }

            if (matricula == null || matricula.estudiante_id <= 0 || matricula.horarios == null || matricula.horarios.Count == 0)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, mensaje = "Faltan datos de matrícula" });
            }

            var listaCursosMat = await _matriculaService.RegistrarMatriculaAsync(matricula);

            return Ok(listaCursosMat);
        }

        [HttpGet("cursos-matriculados/{matriculaId}")]
        public async Task<ActionResult<List<CursoMatriculado>>> ObtenerCursosMatriculadosAsync(int matriculaId)
        {
            if (!_apptAuth.AppSecretKeyValidation(Request.Headers["x-api-app-key"].ToString()))
            {
                return Unauthorized(new { status = HttpStatusCode.Unauthorized, mensaje = "No autorizado" });
            }

            if (matriculaId <= 0)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, mensaje = "FALTA: ID de la matrícula" });
            }

            var listaCursosMat = await _matriculaService.ObtenerCursosMatriculadosAsync(matriculaId);

            if (listaCursosMat == null || listaCursosMat.Count == 0)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, mensaje = "No hay cursos matriculados para este estudiante" });
            }

            return Ok(listaCursosMat);
        }
    }
}
