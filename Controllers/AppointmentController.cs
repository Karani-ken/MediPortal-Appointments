using AutoMapper;
using MediPortal_Appointment.Models;
using MediPortal_Appointment.Models.Dtos;
using MediPortal_Appointment.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MediPortal_Appointment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentInterface _appointmentInterface;
        private readonly ResponseDto _response;
        public AppointmentController(IAppointmentInterface appointmentInterface, IMapper mapper)
        {
            _mapper = mapper;
            _appointmentInterface = appointmentInterface;
            _response = new ResponseDto();
        }

        //Add a new appointment
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> AddAppointment(AppointmentRequestDto newAppointment)
        {
                 var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                var NewAppointment = _mapper.Map<Appointment>(newAppointment);
                NewAppointment.PatientId = Guid.Parse(userIdClaim.Value);
                var res = await _appointmentInterface.AddAppointment(NewAppointment);
                if (string.IsNullOrWhiteSpace(res))
                {
                    _response.IsSuccess = false;
                    _response.Message = "Something went wrong";

                    return BadRequest(_response);
                }
                 _response.Message = res;
                return Ok(_response);
          
        }
        //get appointments
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> AllAppointments()
        {
            var res = await _appointmentInterface.GetAppointments();
            if(res == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Could not fetch Appointments";

                return BadRequest(_response);
            }

            _response.obj=res;
            return Ok(_response);
        }
          [HttpGet("GetAppointments")]
        public async Task<ActionResult<ResponseDto>> GetAllAppointments()
        {
            var res = await _appointmentInterface.GetAllAppointments();
            if(res == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Could not fetch Appointments";

                return BadRequest(_response);
            }

            _response.obj=res;
            return Ok(_response);
        }
        [HttpGet("ById")]

        public async Task<ActionResult<ResponseDto>> AppointmentById(Guid Id)
        {
            var res = await _appointmentInterface.GetAppointmentById(Id);
            if (res == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Could not fetch Appointments";

                return BadRequest(_response);
            }

            _response.obj = res;
            return Ok(_response);
        }

        [HttpPut]
        //update appointment     
        public async Task<ActionResult<ResponseDto>> UpdateAppointment(Guid Id,AppointmentRequestDto appointmentRequest)
        {

            var Appointments = await _appointmentInterface.GetAppointments();
            var AppointmentToUpdate = Appointments.FirstOrDefault(a => a.AppointmentId == Id);
            if (AppointmentToUpdate == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Could not fetch Appointment";

                return BadRequest(_response);
            }

            var UpdatedAppointment = _mapper.Map(appointmentRequest, AppointmentToUpdate);
            var res = await _appointmentInterface.UpdateAppointment(UpdatedAppointment);

            _response.Message = res;

            return Ok(_response);
        }
    }
}
