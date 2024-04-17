using Dapper;
using Hospital_HospitalService.Context;
using Hospital_HospitalService.DTO;
using Hospital_HospitalService.Entity;
using Hospital_HospitalService.Service.Interface;
using System.Data;
using System.Net.Http;

namespace Hospital_HospitalService.Service.Impl
{
    public class DoctorServiceImpl(IHttpClientFactory httpClientFactory,DapperContext context) : IDoctorService
    {
        public object? CreateDoctor(DoctorRequest request)
        {
            DoctorEntity e = MapToEntity(request, getUserById(request.DoctorId));
            var parameters = new DynamicParameters();
            parameters.Add("@DoctorId", e.DoctorId);
            parameters.Add("@DeptId", e.DeptId);
            parameters.Add("@DoctorName", e.DoctorName);
            parameters.Add("@DoctorAge", e.DoctorAge);
            parameters.Add("@DoctorAvailable", e.DoctorAvailable);
            parameters.Add("@Specialization", e.Specialization);
            parameters.Add("@Qualifications", e.Qualifications);

            using (var connection = context.getConnection())
            {
                var result = connection.Execute("spCreateDoctor", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        private DoctorEntity MapToEntity(DoctorRequest request,  UserObject userObject)
        {
            return new DoctorEntity
            {
                DoctorId= userObject.userID,
                DeptId=request.DeptId,
                DoctorName=userObject.firstName,
                DoctorAge=request.DoctorAge,
                DoctorAvailable =request.DoctorAvailable,
                Specialization=request.Specialization,
                Qualifications=request.Qualifications
            };
        }
        public DoctorEntity? GetDoctorById(int doctorId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@DoctorId", doctorId);
            using (var connection = context.getConnection())
            {
                var result = connection.QueryFirstOrDefault<DoctorEntity>("GetDoctorById", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public List<DoctorEntity>? GetAllDoctors()
        {
            string query = "SELECT * FROM Doctor;";
            return context.getConnection().Query<DoctorEntity>(query).ToList();
        }


    public object? UpdateDoctor(int doctorId, DoctorRequest request)
    {
            DoctorEntity existingDoctor = GetDoctorById(doctorId) as DoctorEntity;
            if (existingDoctor == null)
                return null; // Doctor not found
            existingDoctor.DeptId = request.DeptId;
            existingDoctor.Specialization = request.Specialization;
            existingDoctor.Qualifications = request.Qualifications;
            existingDoctor.DoctorAge = request.DoctorAge;
            existingDoctor.DoctorAvailable = request.DoctorAvailable;

            string query = "spUpdateDoctor";
            var parameters = new DynamicParameters();
            parameters.Add("@DoctorId", doctorId);
            parameters.Add("@DeptId", request.DeptId);
            parameters.Add("@Specialization", request.Specialization);
            parameters.Add("@Qualifications", request.Qualifications);

            int rowsAffected = context.getConnection().Execute(query, parameters, commandType: CommandType.StoredProcedure);
            return rowsAffected;
       
    }
    public int DeleteDoctor(int doctorId)
        {
            string query = "DELETE FROM Doctor WHERE DId = @DoctorId;";
            return context.getConnection().Execute(query, new { DoctorId = doctorId });
        }
        public UserObject getUserById(int doctorId)
        {
            var httpclient = httpClientFactory.CreateClient("userByEmail");
            var responce = httpclient.GetAsync($"GetUserById?UserId={doctorId}").Result;
            if (responce.IsSuccessStatusCode)
            {
                return responce.Content.ReadFromJsonAsync<UserObject>().Result;
            }
            throw new Exception("UserNotFound Create User FIRST OE TRY DIFFERENT EMAIL ID");
        }
        public List<DoctorEntity>? GetDoctorsBySpecialization(string specialization)
        {
            string query = "SELECT * FROM Doctor WHERE Specialization = @Specialization;";
            return context.getConnection().Query<DoctorEntity>(query, new { Specialization = specialization }).ToList();
        }
    }
}
