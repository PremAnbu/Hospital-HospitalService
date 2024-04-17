using Dapper;
using Hospital_HospitalService.Context;
using Hospital_HospitalService.DTO;
using Hospital_HospitalService.Entity;
using Hospital_HospitalService.Service.Interface;
using System.Data;

namespace Hospital_HospitalService.Service.Impl
{
    public class DeptServiceImpl(DapperContext context) : IDeptService
    {
        public object? CreateDept(DeptRequest deptRequest)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@DeptName", deptRequest.DeptName);

            using (var connection = context.getConnection())
            {
                var result = connection.QueryFirstOrDefault<int>("spCreateDepartment", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public DepartmentEntity? getByDeptId(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@DeptId", id);
            using (var connection = context.getConnection())
            {
                var result = connection.QueryFirstOrDefault<DepartmentEntity>("spGetDepartmentById", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public DepartmentEntity? getByDeptName(string name)
        {
            String query = "Select * from Department where DeptName=@dname";
            return context.getConnection().Query<DepartmentEntity>(query, new { dname = name }).FirstOrDefault();
        }

        private DepartmentEntity MapToEntity(DeptRequest request) => new DepartmentEntity { DeptName = request.DeptName };

    }
}
