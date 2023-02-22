using Microsoft.AspNetCore.Mvc;
using MvcCoreMultiplesBBDD.Models;
using MvcCoreMultiplesBBDD.Repositories;

namespace MvcCoreMultiplesBBDD.Controllers {
    public class EmpleadoController : Controller {

        IRepositoryEmpleados repo;

        public EmpleadoController(IRepositoryEmpleados repo) {
            this.repo = repo;
        }

        public IActionResult Index() {
            List<Empleado> empleados = this.repo.GetEmpleados();
            return View(empleados);
        }

        public IActionResult Details(int idEmpleado) {
            Empleado empleado = this.repo.FindEmpleado(idEmpleado);
            return View(empleado);
        }
    }
}
