using MvcCoreMultiplesBBDD.Data;
using MvcCoreMultiplesBBDD.Models;

namespace MvcCoreMultiplesBBDD.Repositories {
    public class SQLRepositoryEmpleados : IRepositoryEmpleados {

        private HospitalContext context;

        public SQLRepositoryEmpleados(HospitalContext context) {
            this.context = context;
        }

        public Empleado FindEmpleado(int idEmpleado) {
            var consulta = from data in this.context.Empleados
                           where data.IdEmpleado == idEmpleado
                           select data;
            return consulta.FirstOrDefault();
        }

        public List<Empleado> GetEmpleados() {
            var consulta = from data in this.context.Empleados
                           select data;
            return consulta.ToList();
        }
    }
}
