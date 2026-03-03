using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    [Keyless]
    public class EmpleadoGetByIdDTOs
    {
        public int IdEmpleado { get; set; }

        public string? NombreEmpleado { get; set; }

        public int Edad { get; set; }

        public int IdDepartamento { get; set; }

        public string? NombreDepartamento { get; set; }
    }
}