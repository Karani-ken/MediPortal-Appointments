namespace MediPortal_Appointment.Models.Dtos
{
    public class AppointmentDto
    {
        public Guid AppointmentId { get; set; }
        public string AppointmentType { get; set; } = string.Empty;

        public DoctorDto? Doctor { get; set; }

        public HospitalDto? Hospital { get; set; }
         public Guid PatientId { get; set; }
      
        public Guid HospitalId { get; set;}
        public PatientDto? Patient { get; set; }
         public Guid DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }

        public string Slot { get; set; } = string.Empty;

        public string Symptoms { get; set; } = string.Empty;

        public string AppointmentStatus { get; set; } = string.Empty;
    }
}
