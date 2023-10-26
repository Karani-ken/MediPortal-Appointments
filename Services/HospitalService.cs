using MediPortal_Appointment.Models.Dtos;
using MediPortal_Appointment.Services.IService;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace MediPortal_Appointment.Services
{
    public class HospitalService : IHospitalInterface
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HospitalService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory= httpClientFactory;
        }
        public async Task<HospitalDto> GetHospitalData(Guid hospitalId)
        {
            var client = _httpClientFactory.CreateClient("Hospital");
            var response = await client.GetAsync($"/api/Hospital/ById?Id={hospitalId}");
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);
            Console.WriteLine(responseDto);
            if (responseDto.IsSuccess)
            {
                return JsonConvert.DeserializeObject<HospitalDto>(Convert.ToString(responseDto.obj));
            }

            return new HospitalDto();
        }
    }
}
