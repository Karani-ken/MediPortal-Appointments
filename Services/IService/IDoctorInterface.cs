using MediPortal_Appointment.Models.Dtos;

namespace MediPortal_Appointment.Services.IService
{
    public interface IDoctorInterface
    {
        Task<DoctorDto> GetDoctorData(Guid DoctorId);
    }
}
