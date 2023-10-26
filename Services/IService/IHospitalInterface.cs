using MediPortal_Appointment.Models.Dtos;

namespace MediPortal_Appointment.Services.IService
{
    public interface IHospitalInterface
    {
        Task<HospitalDto> GetHospitalData(Guid hospitalId);
    }
}
