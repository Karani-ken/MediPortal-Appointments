using MediPortal_Appointment.Models.Dtos;
using MediPortal_Appointment.Services.IService;
using Newtonsoft.Json;

namespace MediPortal_Appointment.Services
{
    public class DoctorService:IDoctorInterface
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DoctorService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<DoctorDto> GetDoctorData(Guid DoctorId)        {

            var client = _httpClientFactory.CreateClient("Doctor");
            var response = await client.GetAsync($"/api/User/GetById?id={DoctorId}");
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (responseDto.IsSuccess)
            {
                return JsonConvert.DeserializeObject<DoctorDto>(Convert.ToString(responseDto.obj));
            }

            return new DoctorDto();
        }
    }
}
