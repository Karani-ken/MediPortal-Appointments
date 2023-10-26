using MediPortal_Appointment.Data;
using MediPortal_Appointment.Models;
using MediPortal_Appointment.Models.Dtos;
using MediPortal_Appointment.Services.IService;
using Microsoft.EntityFrameworkCore;

namespace MediPortal_Appointment.Services
{
    public class AppointmentService : IAppointmentInterface
    {
        private readonly ApplicationDbContext _context;
        private readonly IDoctorInterface _doctorInterface;
        private readonly IHospitalInterface _hospitalInterface;
        public AppointmentService(ApplicationDbContext context,IHospitalInterface hospitalInterface, IDoctorInterface doctorInterface)
        {
            _context= context;
            _hospitalInterface = hospitalInterface;
            _doctorInterface = doctorInterface;
        }
        public async Task<string> AddAppointment(Appointment appointment)
        {
           
            try
            {
                
              
                    await _context.Appointments.AddAsync(appointment);
                                      
                    await _context.SaveChangesAsync();   
                    return "Appointment Booked successfully";                 
              
               
            }
            catch(Exception ex)
            {
                return ex.InnerException.Message;
            }            
            
        }
       

        public async Task<string> DeleteAppointment(Appointment appointment)
        {
            try
            {
                 _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
                return "Appointment was Deleted";
            }catch(Exception ex)
            {

                return ex.InnerException.Message;
            }
        }

        public async Task<AppointmentDto> GetAppointmentById(Guid Id)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.AppointmentId == Id);          
            if (appointment == null)
            {
                return new AppointmentDto();
            }
            var doctor = await _doctorInterface.GetDoctorData(appointment.DoctorId);
             var patient = await _doctorInterface.GetDoctorData(appointment.PatientId);
            var hospital = await _hospitalInterface.GetHospitalData(appointment.HospitalId);
            var newAppointment = new AppointmentDto()
            {
                 Patient = patient != null ? new PatientDto()
                {
                    firstname = patient.firstname,
                    lastname = patient.lastname,
                    surname = patient.surname
                }:null,
                Doctor = doctor != null ? new DoctorDto()
                {
                    firstname = doctor.firstname,
                    lastname = doctor.lastname,
                    surname = doctor.surname
                }:null,
                Hospital = hospital != null ? new HospitalDto()
                {
                    hospitalName=hospital.hospitalName,
                    location=hospital.location
                }:null,
                AppointmentType=appointment.AppointmentType,
               AppointmentDate = appointment.AppointmentDate,
               AppointmentStatus=appointment.AppointmentStatus,             
               Slot=appointment.Slot,
               Symptoms=appointment.Symptoms
            };

            return newAppointment;
        }

        public async Task<List<Appointment>> GetAppointments()
        {

            return await _context.Appointments.ToListAsync();
        }
        public async Task<List<AppointmentDto>> GetAllAppointments()
        {
            var appointments = await _context.Appointments.ToListAsync();
             var appointmentDtos = new List<AppointmentDto>();
            foreach(var appointment in appointments){
                  var doctor = await _doctorInterface.GetDoctorData(appointment.DoctorId);
                    var patient = await _doctorInterface.GetDoctorData(appointment.PatientId);
                    var hospital = await _hospitalInterface.GetHospitalData(appointment.HospitalId);
                    var newAppointment = new AppointmentDto()
                    {
                            Patient = patient != null ? new PatientDto()
                            {
                                firstname = patient.firstname,
                                lastname = patient.lastname,
                                surname = patient.surname
                            }:null,
                            Doctor = doctor != null ? new DoctorDto()
                            {
                                firstname = doctor.firstname,
                                lastname = doctor.lastname,
                                surname = doctor.surname
                            }:null,
                            Hospital = hospital != null ? new HospitalDto()
                            {
                                hospitalName=hospital.hospitalName,
                                location=hospital.location
                            }:null,
                            AppointmentType=appointment.AppointmentType,
                        AppointmentDate = appointment.AppointmentDate,
                        AppointmentStatus=appointment.AppointmentStatus,             
                        Slot=appointment.Slot,
                        Symptoms=appointment.Symptoms,
                        PatientId=appointment.PatientId,
                        DoctorId=appointment.DoctorId ,
                        AppointmentId=appointment.AppointmentId
                    };
                     appointmentDtos.Add(newAppointment); 
            }

           return appointmentDtos;
        }

        public async Task<string> UpdateAppointment(Appointment appointment)
        {
             try
            {
                _context.Appointments.Update(appointment);
                await _context.SaveChangesAsync();
                return "Appointment was Cancelled";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
    }
}
