using EventStore;
using Newtonsoft.Json;
using System.Net;

namespace ParkingPlateRecognizer
{
    public class ParkingPlateRecognizerService : IParkingPlateRecognizerService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ParkingPlateRecognizerService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<Boolean> checkEventAsync(string _licensePlate, Event _event)
        {
            string url = $"https://localhost:7239/api/EventFeed?_licensePlate={_licensePlate}&&_event={_event}";

            using (var c = _httpClientFactory.CreateClient())
            {
                var result = await c.GetAsync(url);
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync().Result;
                    String car = JsonConvert.DeserializeObject<String>(content);

                    if (car != null)
                    {
                        //parkingSpaces.carÍnfo = car;
                        return true;

                    }
                    return false;

                }
                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    return false;
                }
                else
                {
                    return true;
                    //throw new MotorApiException(result.StatusCode, result.Content);
                }
            }
        }
    }
}
