using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Login
    {
        private readonly EmpresaCoreContext _context;

        public Login(EmpresaCoreContext context)
        {
            _context = context;
        }

        public ML.Result LoggIn (ML.Login login)
        {
            ML.Result result = new ML.Result();
            try
            {
                var query = _context.UsuarioLogin.FromSqlRaw($"UsuarioLogin '{login.Email}' , '{login.Password}'").AsEnumerable().SingleOrDefault();

                if(query != null )
                {
                    ML.Usuario usuario = new ML.Usuario();
                    usuario.UsuarioNombre = query.UsuarioNombre;
                    usuario.ApellidoMaterno = query.ApellidoMaterno;
                    usuario.ApellidoPaterno = query.ApellidoPaterno;
                    usuario.RolNombre = query.RolNombre;

                    result.Object = usuario;

                    result.Correct = true;
                } else
                {
                    result.Correct = false;
                    result.ErrorMessage = "Correo o contraseña incorrectos";
                }

            } catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
    }
}
