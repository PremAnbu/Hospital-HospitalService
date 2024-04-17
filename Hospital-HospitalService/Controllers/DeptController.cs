using Hospital_HospitalService.DTO;
using Hospital_HospitalService.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_HospitalService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeptController(IDeptService dept) : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CreateDepartment(DeptRequest deptRequest)
        {
            return Ok(dept.CreateDept(deptRequest)+" nd dept id");
        }

        [HttpGet("int")]
        public IActionResult getByDeptId(int id)
        {
            return Ok(dept.getByDeptId(id));
        }
        [HttpGet("ByName")]
        public IActionResult getByDeptName(String name)
        {
            return Ok(dept.getByDeptName(name));
        }
    }
}
