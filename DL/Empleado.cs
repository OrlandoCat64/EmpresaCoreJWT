using System;
using System.Collections.Generic;

namespace DL;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public string Nombre { get; set; } = null!;

    public int Edad { get; set; }

    public int IdDepartamento { get; set; }

    public virtual Departamento IdDepartamentoNavigation { get; set; } = null!;
}
