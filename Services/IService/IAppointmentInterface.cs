using MediPortal_Appointment.Models;
using MediPortal_Appointment.Models.Dtos;

namespace MediPortal_Appointment.Services.IService
{
    public interface IAppointmentInterface
    {
        //Booking appointment
        Task<string> AddAppointment(Appointment appointment);
      
        //Deleting Appointment
        Task<string> DeleteAppointment(Appointment appointment);

        //update appointment
        Task<string> UpdateAppointment(Appointment appointment);
        //get all appoinments
        Task<List<Appointment>> GetAppointments();

        //get a single appointment
        Task<AppointmentDto> GetAppointmentById(Guid Id);
         Task<List<AppointmentDto>> GetAllAppointments();
    }
}
