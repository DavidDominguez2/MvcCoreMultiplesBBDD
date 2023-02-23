using MvcCoreMultiplesBBDD.Data;
using MvcCoreMultiplesBBDD.Models;


#region PROCEDURE
//CREATE PROCEDURE SP_FIND_EMPLOYEE
//(@IDEMPLEADO INT)
//AS
//	SELECT * FROM EMP WHERE EMP_NO = @IDEMPLEADO
//GO
#endregion


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
            return consulta.ToList().FirstOrDefault();
        }

        public List<Empleado> GetEmpleados() {
            var consulta = from data in this.context.Empleados
                           select data;
            return consulta.ToList();
        }

        public async Task DeleteEmpleadoAsync(int id) {
            Empleado empleado = this.FindEmpleado(id);
            this.context.Empleados.Remove(empleado);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateEmpleado(int idEmpleado, int salario, string oficio) {
            Empleado empleado = this.FindEmpleado(idEmpleado);
            empleado.Salario = salario;
            empleado.Oficio = oficio;
            await this.context.SaveChangesAsync();
        }
    }
}
