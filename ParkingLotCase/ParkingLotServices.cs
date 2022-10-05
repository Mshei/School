using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using CarTypeService.Models;
using SendParkingEmail.Model;

namespace ParkingLotCase
{
    public class ParkingLotServices : IParkingLotServices
    {
        private readonly IHttpClientFactory _httpClientFactory;


        public ParkingLotServices(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ParkingSpaces> GetDescriptionAsync(ParkingSpaces parkingSpaces, string licensePlate)
        {
            string url = $"https://localhost:49153/MotorApi?licensePlate={licensePlate}";

            using (var c = _httpClientFactory.CreateClient())
            {
                var result = await c.GetAsync(url);
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync().Result;
                    String car = JsonConvert.DeserializeObject<String>(content);

                    if (car != null)
                    {
                        parkingSpaces.carÍnfo = car;
                        return parkingSpaces;
                        
                    }
                    return parkingSpaces;

                }
                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    return parkingSpaces;
                }
                else
                {
                    return parkingSpaces;
                    //throw new MotorApiException(result.StatusCode, result.Content);
                }
            }

        }

        public async Task<Boolean> SendEmailAsync(String _licensePlate, String _email)
        {
            string url = $"https://localhost:49155/api/Email?_licensePlate={_licensePlate}&&_email={_email}";
            String mailSent;

            using (var c = _httpClientFactory.CreateClient())
            {
                var result = await c.GetAsync(url);
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync().Result;
                    mailSent = JsonConvert.DeserializeObject<String>(content);

                }
                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    return false;
                }
                else
                {
                    return true;
                }


                /*if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    return false;
                }
                else
                {
                    return true;
                }*/
            }

        }
    }
}
