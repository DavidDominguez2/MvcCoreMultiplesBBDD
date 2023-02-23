using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using MvcCoreMultiplesBBDD.Data;
using MvcCoreMultiplesBBDD.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;

#region PROCEDURES
//CREATE OR REPLACE PROCEDURE SP_DELETE_EMPLEADO
//(P_IDEMPLEADO EMP.EMP_NO%TYPE)
//AS
//BEGIN
//  DELETE FROM EMP WHERE EMP_NO = P_IDEMPLEADO;
//COMMIT;
//END;

//CREATE OR REPLACE PROCEDURE SP_UPDATE_EMPLEADO
//(P_IDEMPLEADO EMP.EMP_NO%TYPE,
//P_SALARIO EMP.SALARIO%TYPE,
//P_OFICIO EMP.OFICIO%TYPE)
//AS
//BEGIN
//  UPDATE EMP SET SALARIO = P_SALARIO, OFICIO = P_OFICIO WHERE EMP_NO = P_IDEMPLEADO;
//END;

//CREATE OR REPLACE PROCEDURE SP_DETAILS_EMPLEADO
//(P_IDEMPLEADO EMP.EMP_NO%TYPE,
//P_CURSOR_EMPLEADO OUT SYS_REFCURSOR)
//AS
//BEGIN
//  OPEN P_CURSOR_EMPLEADO FOR
//  SELECT * FROM EMP WHERE EMP_NO = P_IDEMPLEADO;
//END;
#endregion

namespace MvcCoreMultiplesBBDD.Repositories {
    public class OCLRepositoryEmpleados : IRepositoryEmpleados {
        private HospitalContext context;

        public OCLRepositoryEmpleados(HospitalContext context) {
            this.context = context;
        }

        public Empleado FindEmpleado(int idEmpleado) {
            string sql = "BEGIN SP_DETAILS_EMPLEADO (:P_IDEMPLEADO, :P_CURSOR_EMPLEADO); END;";
            OracleParameter pamid = new OracleParameter(":P_IDEMPLEADO", idEmpleado);

            OracleParameter pamcursor = new OracleParameter();
            pamcursor.ParameterName = ":P_CURSOR_EMPLEADO";
            pamcursor.OracleDbType = OracleDbType.RefCursor;
            pamcursor.Value = null;
            pamcursor.Direction = ParameterDirection.Output;

            var consulta = this.context.Empleados.FromSqlRaw(sql, pamid, pamcursor);

            Empleado empleado = consulta.ToList().FirstOrDefault();
            return empleado;
        }

        public List<Empleado> GetEmpleados() {
            string sql = "BEGIN SP_ALL_EMPLOYEES (:P_CURSOR_EMPLEADOS); END;";
            OracleParameter pamcursor = new OracleParameter();
            pamcursor.ParameterName = ":P_CURSOR_EMPLEADOS";
            pamcursor.OracleDbType = OracleDbType.RefCursor;
            pamcursor.Value = null;
            pamcursor.Direction = ParameterDirection.Output;

            var consulta = this.context.Empleados.FromSqlRaw(sql, pamcursor);

            List<Empleado> empleados = consulta.ToList();
            return empleados;
        }

        public async Task DeleteEmpleadoAsync(int id) {
            string sql = "begin SP_DELETE_EMPLEADO(:P_IDEMPLEADO); end;";
            OracleParameter pamid = new OracleParameter(":P_IDEMPLEADO", id);
            await this.context.Database.ExecuteSqlRawAsync(sql, pamid);

        }

        public async Task UpdateEmpleado(int idEmpleado, int salario, string oficio) {
            string sql = "begin SP_UPDATE_EMPLEADO(:P_IDEMPLEADO,:P_SALARIO, :P_OFICIO);END;";
            OracleParameter[] parameters = new OracleParameter[] {
                new OracleParameter(":P_IDEMPLEADO", idEmpleado),
                new OracleParameter(":P_SALARIO", salario),
                new OracleParameter(":P_OFICIO", oficio)
            };
            await this.context.Database.ExecuteSqlRawAsync(sql, parameters);
        }
    }
}
