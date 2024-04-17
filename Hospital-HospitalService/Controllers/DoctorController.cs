using Hospital_HospitalService.DTO;
using Hospital_HospitalService.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_HospitalService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController(IDoctorService service) : ControllerBase
    {
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public IActionResult CreateDoctor(DoctorRequest request)
        {
            return Ok(service.CreateDoctor(request));
        }
        [HttpGet("GetDoctorById")]
        public IActionResult GetDoctorById(int doctorId)
        {
            return Ok(service.GetDoctorById(doctorId));
        }

        [HttpGet]
        public IActionResult GetAllDoctors()
        {
            return Ok(service.GetAllDoctors());
        }

        [Authorize(Roles ="Admin")]
        [HttpPut("{doctorId}")]
        public IActionResult UpdateDoctor(int doctorId, DoctorRequest request)
        {
            return Ok(service.UpdateDoctor(doctorId, request));
        }

        [HttpDelete("{doctorId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteDoctor(int doctorId)
        {
            return Ok(service.DeleteDoctor(doctorId));
        }
        [HttpGet("specialization/{specialization}")]
        public IActionResult GetDoctorsBySpecialization(string specialization)
        {
            return Ok(service.GetDoctorsBySpecialization(specialization));
        }
    }
}
