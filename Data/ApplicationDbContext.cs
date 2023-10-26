using MediPortal_Appointment.Models;
using MediPortal_Appointment.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace MediPortal_Appointment.Data
{
    public class ApplicationDbContext:DbContext
    {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options) { }

        public DbSet<Appointment> Appointments { get; set; }
        
    }
}
