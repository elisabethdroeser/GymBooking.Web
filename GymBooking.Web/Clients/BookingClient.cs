using GymBooking.Web.Models.Entities;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace GymBooking.Web.Clients
{
    public class BookingClient
    {
        private readonly HttpClient httpClient; //flyttar från configurationsklassen

        public BookingClient(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.httpClient = httpClient;
        }


        public async Task<IEnumerable<GymClass>> GetWithStreamsAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/gymclasses");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //förväntar sig json o gör om nedan till ett c#

            IEnumerable<GymClass> gymClasses;

            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, CancellationToken.None);

            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                response.EnsureSuccessStatusCode();

                using (var streamReader = new StreamReader(stream)) //något som kan läsa json
                {
                    using (var jsonReader = new JsonTextReader(streamReader))
                    {
                        var serializer = new Newtonsoft.Json.JsonSerializer();
                        gymClasses = serializer.Deserialize<IEnumerable<GymClass>>(jsonReader);
                    }
                }
            }

            return gymClasses;
        }

    }
}
