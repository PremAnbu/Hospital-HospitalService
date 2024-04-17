using Hospital_HospitalService.DTO;
using Hospital_HospitalService.Entity;

namespace Hospital_HospitalService.Service.Interface
{
    public interface IDeptService
    {
        object? CreateDept(DeptRequest deptRequest);
        DepartmentEntity? getByDeptId(int id);
        DepartmentEntity? getByDeptName(string name);
    }
}
