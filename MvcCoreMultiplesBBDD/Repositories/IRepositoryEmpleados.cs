using MvcCoreMultiplesBBDD.Models;

namespace MvcCoreMultiplesBBDD.Repositories {
    public interface IRepositoryEmpleados {

        List<Empleado> GetEmpleados();
        Empleado FindEmpleado(int idEmpleado);
        Task DeleteEmpleadoAsync(int idEmpleado);
        Task UpdateEmpleado(int idEmpleado, int salario, string oficio);
    }
}
