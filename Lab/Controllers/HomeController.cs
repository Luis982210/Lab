using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab.Models;
using Lab.ViewModel;

namespace Lab.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Empleados(String empleado = "", Boolean office = false)
        {
            
            var viewModel = new CitasEmpleadoViewModel
            {
                Empleados = new List<Empleado>(0)

            };

            if (HttpContext.Application["CitasEmpleado"] != null)
            {
                viewModel = (CitasEmpleadoViewModel)HttpContext.Application["CitasEmpleado"];
            }

            HttpContext.Application["CitasEmpleado"] = viewModel;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Empleados(int ID = 0, string name = "")
        {
            var viewModel = (CitasEmpleadoViewModel)HttpContext.Application["CitasEmpleado"];
            viewModel.Empleados.Add(new Empleado
            {
                ID = ID,
                Name = name,
                Entrada = DateTime.Now,
                Hours = 0,
                Office = true,
                Citas = new List<Cita>(0)
            });
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Citas()
        {
            var viewModel = new CitasEmpleadoViewModel
            {
                Empleados = new List<Empleado>(0)

            };

            if (HttpContext.Application["CitasEmpleado"] != null)
            {
                viewModel = (CitasEmpleadoViewModel)HttpContext.Application["CitasEmpleado"];
            }

            HttpContext.Application["CitasEmpleado"] = viewModel;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Citas(String i)
        {
            var viewModel = (CitasEmpleadoViewModel)HttpContext.Application["CitasEmpleado"];
            Random rnd = new Random();
            foreach (var empleado in viewModel.Empleados)
            {
                var nCitas = rnd.Next(1, 5);
                for (int o = 0; o < nCitas; o++)
                {
                    TimeSpan tSpan = new TimeSpan(2 * (o + 1), 0, 0);
                    empleado.Citas.Add(new Cita { Client = "Cliente " + o, DateTime = empleado.Entrada + tSpan });
                }
            }
            HttpContext.Application["CitasEmpleado"] = viewModel;
            return View(viewModel);
        }
        [HttpGet]
        public ActionResult Salidas()
        {
            var viewModel = new CitasEmpleadoViewModel
            {
                Empleados = new List<Empleado>(0)

            };

            if (HttpContext.Application["CitasEmpleado"] != null)
            {
                viewModel = (CitasEmpleadoViewModel)HttpContext.Application["CitasEmpleado"];
            }

            HttpContext.Application["CitasEmpleado"] = viewModel;
            return View(viewModel);
            
        }

        [HttpPost]
        public ActionResult Salidas(int i =0)
        {
            var viemModel = (CitasEmpleadoViewModel)HttpContext.Application["CitasEmpleado"];
            foreach (var empleado in viemModel.Empleados)
            {
                empleado.Hours = 2 * empleado.Citas.Count;
                TimeSpan tSpan = new TimeSpan(2 * empleado.Citas.Count, 0, 0);
                empleado.Salida = empleado.Entrada + tSpan;
                empleado.Salary = empleado.Hours * 38;
                empleado.Office = false;

            }

            var JornadasModel = (JornadasEmpleadosViewMode)HttpContext.Application["JornadasModel"];
            if(JornadasModel ==null)
            {
                JornadasModel = new JornadasEmpleadosViewMode
                {
                    Empleados = new List<Empleado>(0)
                };
            }

            foreach (var empleado in viemModel.Empleados)
            {
                JornadasModel.Empleados.Add(empleado);

            }
            HttpContext.Application["JornadasModel"] = JornadasModel;
            
            return View(viemModel);
        }


        public ActionResult Jornada()
        {
            var JornadasModel = (JornadasEmpleadosViewMode)HttpContext.Application["JornadasModel"];
            if (JornadasModel == null)
            {
                JornadasModel = new JornadasEmpleadosViewMode
                {
                    Empleados = new List<Empleado>(0)
                };
            }
            return View(JornadasModel);
        }
    }
}