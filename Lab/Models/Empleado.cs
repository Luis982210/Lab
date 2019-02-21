using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab.Models
{
    public class Empleado
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public int Hours { get; set; }
        public int Salary { get; set; }
        public Boolean Office { get; set; }
        public List<Cita> Citas { get; set; }
        public DateTime Entrada { get; set; }
        public DateTime Salida { get; set; }
    }
}