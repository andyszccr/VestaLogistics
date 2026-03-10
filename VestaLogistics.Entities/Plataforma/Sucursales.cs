using System;
using System.Collections.Generic;
using System.Text;

namespace VestaLogistics.Entities.Plataforma
{
    public class Sucursales
    {
        public int Sucursalid { get; set; }
        public int Empresaid { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
