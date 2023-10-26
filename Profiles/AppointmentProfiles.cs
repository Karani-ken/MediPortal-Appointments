using AutoMapper;
using MediPortal_Appointment.Models;
using MediPortal_Appointment.Models.Dtos;

namespace MediPortal_Appointment.Profiles
{
    public class AppointmentProfiles:Profile
    {
        public AppointmentProfiles()
        {
            CreateMap<Appointment, AppointmentRequestDto>().ReverseMap();
        }
    }
}
