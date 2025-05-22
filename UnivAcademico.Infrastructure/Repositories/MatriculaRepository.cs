using UnivAcademico.Domain.Entities;
using UnivAcademico.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace UnivAcademico.Infrastructure.Repositories
{
    public class MatriculaRepository : IMatriculaRepository
    {
        private readonly string _connectionString;

        public MatriculaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("dbAcademico")!;
        }

        public async Task<Estudiante?> ObtenerEstudianteInfo(int personaId)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            using var cmd = new NpgsqlCommand("SELECT * FROM tf_estudiante_persona(@personaId);", conn)
            {
                CommandType = CommandType.Text
            };

            cmd.Parameters.AddWithValue("personaId", personaId);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Estudiante
                {
                    id = Convert.ToInt32(reader["id"]),
                    sede = reader["sede"].ToString() ?? "",
                    facultad = reader["facultad"].ToString() ?? "",
                    programa = reader["programa"].ToString() ?? "",
                    plan = Convert.ToInt32(reader["plan"]),
                    curricula_id = Convert.ToInt32(reader["curricula_id"]),
                    categoria_id = Convert.ToInt32(reader["categoria_id"]),
                    semestre_id = Convert.ToInt32(reader["semestre_id"]),
                    semestre = reader["semestre"].ToString() ?? "",
                    matricula_id = Convert.ToInt32(reader["matricula_id"])
                };
            }

            return null;
        }

        public async Task<List<OfertaAcad>> ObtenerOfertaAcademicaAsync(int estudianteId)
        {
            var lista = new List<OfertaAcad>();

            using var conn = new NpgsqlConnection(_connectionString);
            using var cmd = new NpgsqlCommand("SELECT * FROM tf_curso_programado(@estudianteId);", conn)
            {
                CommandType = CommandType.Text
            };

            cmd.Parameters.AddWithValue("estudianteId", estudianteId);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var oferta = new OfertaAcad
                {
                    id = Convert.ToInt32(reader["id"]),
                    ciclo = Convert.ToInt32(reader["ciclo"]),
                    curso_id = Convert.ToInt32(reader["curso_id"]),
                    nombre = reader["nombre"].ToString()!,
                    creditos = Convert.ToInt32(reader["creditos"]),
                    tipo = reader["tipo"].ToString()!,
                    seccion = reader["seccion"].ToString()!,
                    modalidad = reader["modalidad"].ToString()!,
                    horario = reader["horario"].ToString()!
                };

                lista.Add(oferta);
            }

            return lista;
        }

        public async Task<List<CursoMatriculado>> RegistrarMatriculaAsync(int estudianteId, List<int> horarios)
        {
            var listaCursosMat = new List<CursoMatriculado>();

            try
            {
                int matricula_id = 0;
                string caso = "";

                using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();

                using var tx = await conn.BeginTransactionAsync();

                // Abrir matrícula
                using (var cmdOpen = new NpgsqlCommand("select * from tfi_matricula(@estudianteId);", conn, tx))
                {
                    cmdOpen.CommandType = CommandType.Text;
                    cmdOpen.Parameters.AddWithValue("estudianteId", estudianteId);

                    using var drOpen = await cmdOpen.ExecuteReaderAsync();

                    while (await drOpen.ReadAsync())
                    {
                        matricula_id = Convert.ToInt32(drOpen["id"]);
                        caso = drOpen["caso"].ToString()!;
                    }

                    await drOpen.CloseAsync();
                }

                // Registrar horarios
                foreach (var horario_id in horarios)
                {
                    using var cmdReg = new NpgsqlCommand("select tfi_curso_matriculado(@matriculaId, @horarioId);", conn, tx)
                    {
                        CommandType = CommandType.Text
                    };

                    cmdReg.Parameters.AddWithValue("matriculaId", matricula_id);
                    cmdReg.Parameters.AddWithValue("horarioId", horario_id);

                    await cmdReg.ExecuteNonQueryAsync();
                }

                // Cerrar matrícula
                using (var cmdClose = new NpgsqlCommand("select tfu_matricula(@matriculaId);", conn, tx))
                {
                    cmdClose.CommandType = CommandType.Text;
                    cmdClose.Parameters.AddWithValue("matriculaId", matricula_id);

                    await cmdClose.ExecuteNonQueryAsync();
                }

                // Confirmar transacción
                await tx.CommitAsync();

                // Obtener cursos matriculados
                using var cmdMat = new NpgsqlCommand("select * from tf_curso_matriculado(@matriculaId);", conn)
                {
                    CommandType = CommandType.Text
                };
                cmdMat.Parameters.AddWithValue("matriculaId", matricula_id);

                using var drMat = await cmdMat.ExecuteReaderAsync();

                while (await drMat.ReadAsync())
                {
                    var cursoMat = new CursoMatriculado
                    {
                        id = Convert.ToInt32(drMat["id"]),
                        ciclo = Convert.ToInt32(drMat["ciclo"]),
                        curso_id = Convert.ToInt32(drMat["curso_id"]),
                        nombre = drMat["nombre"].ToString() ?? "",
                        creditos = Convert.ToInt32(drMat["creditos"]),
                        tipo = drMat["tipo"].ToString() ?? "",
                        seccion = drMat["seccion"].ToString() ?? "",
                        modalidad = drMat["modalidad"].ToString() ?? "",
                        horario = drMat["horario"].ToString() ?? "",
                        matricula_id = Convert.ToInt32(drMat["matricula_id"])
                    };

                    listaCursosMat.Add(cursoMat);
                }

                await drMat.CloseAsync();
            }
            catch (Exception ex)
            {                
                Console.WriteLine("RegistrarMatriculaAsync: Error. " + ex.Message);
            }

            return listaCursosMat;
        }

        public async Task<List<CursoMatriculado>> ObtenerCursosMatriculadosAsync(int matriculaId)
        {
            var listaCursosMat = new List<CursoMatriculado>();

            using var conn = new NpgsqlConnection(_connectionString);
            using var cmd = new NpgsqlCommand("select * from tf_curso_matriculado(@matriculaId);", conn)
            {
                CommandType = CommandType.Text
            };

            cmd.Parameters.AddWithValue("matriculaId", matriculaId);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var cursoMat = new CursoMatriculado
                {
                    id = Convert.ToInt32(reader["id"]),
                    ciclo = Convert.ToInt32(reader["ciclo"]),
                    curso_id = Convert.ToInt32(reader["curso_id"]),
                    nombre = reader["nombre"].ToString()!,
                    creditos = Convert.ToInt32(reader["creditos"]),
                    tipo = reader["tipo"].ToString()!,
                    seccion = reader["seccion"].ToString()!,
                    modalidad = reader["modalidad"].ToString()!,
                    horario = reader["horario"].ToString()!,
                    matricula_id = Convert.ToInt32(reader["matricula_id"])
                };

                listaCursosMat.Add(cursoMat);
            }

            return listaCursosMat;
        }

    }
}
