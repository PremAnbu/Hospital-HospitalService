using Hospital_HospitalService.DTO;
using Hospital_HospitalService.Entity;

namespace Hospital_HospitalService.Service.Interface
{
    public interface IDoctorService
    {
        object? CreateDoctor(DoctorRequest request);
        DoctorEntity? GetDoctorById(int doctorId);
        List<DoctorEntity>? GetAllDoctors();
        object? UpdateDoctor(int doctorId, DoctorRequest request);
        int DeleteDoctor(int doctorId);
        public List<DoctorEntity>? GetDoctorsBySpecialization(string specialization);

    }
}
