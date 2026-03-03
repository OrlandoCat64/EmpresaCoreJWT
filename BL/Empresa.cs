using DL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ML;

namespace BL
{
    public class Empresa
    {

        private readonly DL.EmpresaCoreContext _context;

        public Empresa(DL.EmpresaCoreContext context)
        {

            _context = context;
        }

        //-------------------------------------------------------------GetByEmpleado 
        public ML.Result GetByIdEmpleado(int idEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                var dto = _context.Set<EmpleadoGetByIdDTOs>()
                    .FromSqlRaw(
                        "EXEC dbo.sp_GetByIdEmpleado @IdEmpleado",
                        new SqlParameter("@IdEmpleado", idEmpleado)
                    )
                    .AsNoTracking()
                    .AsEnumerable()
                    .FirstOrDefault();

                if (dto != null)
                {
                    ML.Empleado empleado = new ML.Empleado
                    {
                        IdEmpleado = dto.IdEmpleado,
                        Nombre = dto.NombreEmpleado,
                        Edad = dto.Edad,

                        Departamento = new ML.Departamento
                        {
                            IdDepartamento = dto.IdDepartamento,
                            Nombre = dto.NombreDepartamento
                        }
                    };

                    result.Object = empleado;
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "Empleado no encontrado";
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
        //-------------------------------------------------------------GetAllEmpleado 
        public ML.Result GetAllEmpleado()
        {
            ML.Result result = new ML.Result();

            try
            {
                var empleadosDb = _context.Set<EmpleadoGetAllDTOs>()
                    .FromSqlRaw("EXEC dbo.sp_GetAllEmpleado")
                    .AsNoTracking()
                    .ToList();

                result.Objects = new List<object>();

                foreach (EmpleadoGetAllDTOs registro in empleadosDb)
                {
                    ML.Empleado empleado = new ML.Empleado
                    {
                        IdEmpleado = registro.IdEmpleado,
                        Nombre = registro.NombreEmpleado,
                        Edad = registro.Edad,

                        Departamento = new ML.Departamento
                        {
                            IdDepartamento = registro.IdDepartamento,
                            Nombre = registro.NombreDepartamento
                        }
                    };

                    result.Objects.Add(empleado);
                }

                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        //-------------------------------------------------------------Add

        public ML.Result AddEmpleado(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                _context.Database.ExecuteSqlRaw(
                    "EXEC dbo.sp_AddEmpleado @Nombre, @Edad, @IdDepartamento",

                    new SqlParameter("@Nombre", empleado.Nombre),
                    new SqlParameter("@Edad", empleado.Edad),
                    new SqlParameter("@IdDepartamento", empleado.Departamento?.IdDepartamento ?? 0)
                );

                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        //-------------------------------------------------------------Update

        public ML.Result UpdateEmpleado(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                _context.Database.ExecuteSqlRaw(
                    "EXEC dbo.sp_UpdateEmpleado @IdEmpleado, @Nombre, @Edad, @IdDepartamento",

                    new SqlParameter("@IdEmpleado", empleado.IdEmpleado),
                    new SqlParameter("@Nombre", empleado.Nombre),
                    new SqlParameter("@Edad", empleado.Edad),
                    new SqlParameter("@IdDepartamento", empleado.Departamento?.IdDepartamento ?? 0)
                );

                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }


        //-------------------------------------------------------------Delete

        public ML.Result DeleteEmpleado(int idEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                _context.Database.ExecuteSqlRaw(
                    "EXEC dbo.sp_DeleteEmpleado @IdEmpleado",
                    new SqlParameter("@IdEmpleado", idEmpleado)
                );

                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
    }
}