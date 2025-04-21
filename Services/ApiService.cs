using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using VytalSign.Models;

namespace VytalSign.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public class HeartbeatApiResponse
        {
            [JsonPropertyName("success")]
            public bool Success { get; set; }

            [JsonPropertyName("items")]
            public List<Heartbeat> Items { get; set; }
        }

        public async Task<List<Heartbeat>> GetHeartbeatsAsync()
        {
            var response = await _httpClient.GetAsync("http://194.37.82.80:8080/sensors/api.php?username=usereclipse&password=P@ssw0rd&action=getresults&customerid=47&lastupdate=0");

            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine("RAW JSON: " + json);  // Debugging line

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    // Deserialize the JSON into a response object that contains a list of Heartbeat items
                    var heartbeatResponse = JsonSerializer.Deserialize<HeartbeatApiResponse>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (heartbeatResponse?.Items != null)
                    {
                        return heartbeatResponse.Items;  // Return the list of Heartbeats
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Deserialization Error: " + ex.Message);  // Log deserialization errors
                    throw;
                }
            }

            throw new Exception("Failed to fetch heartbeat data from API");
        }
    }
}