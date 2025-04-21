using Microsoft.AspNetCore.Mvc.RazorPages;
using VytalSign.Services;

public class IndexModel : PageModel
{
    private readonly ApiService _apiService;

    public IndexModel(ApiService apiService)
    {
        _apiService = apiService;
    }

    public List<HeartbeatDisplay> HeartbeatData { get; set; }
    public List<OccupancyDisplay> OccupancyData { get; set; }

    public bool ShowWarning { get; set; }

    public class HeartbeatDisplay
    {
        public int DeviceId { get; set; }
        public double BPM { get; set; }
        public DateTime DateLogged { get; set; }
    }

    public async Task OnGetAsync()
    {
        var heartbeats = await _apiService.GetHeartbeatsAsync();

        HeartbeatData = heartbeats
            .Where(h => h.DeviceId == 87)
            .Select(h => new HeartbeatDisplay
            {
                DeviceId = h.DeviceId,
                BPM = h.Val,
                DateLogged = DateTime.TryParse(h.Dt, out var dt) ? dt : DateTime.MinValue
            })
            .ToList();

        OccupancyData = heartbeats
            .Where(h => h.DeviceTypeId == "12")
            .Select(h => new OccupancyDisplay
            {
                DeviceId = h.DeviceId,
                OccupancyStatus = h.Val > 0 ? "Occupied" : "Not Occupied",
                DateLogged = DateTime.TryParse(h.Dt, out var dt) ? dt : DateTime.MinValue
            })
            .ToList();

        //Warning logic
        var heartbeat = HeartbeatData.FirstOrDefault(h => h.DeviceId == 87);
        var occupancy = OccupancyData.FirstOrDefault(); // Assuming one occupancy sensor

        if (heartbeat != null && occupancy != null)
        {
            var timeSinceHeartbeat = DateTime.UtcNow - heartbeat.DateLogged.ToUniversalTime();
            bool isOccupied = occupancy.OccupancyStatus == "Occupied";
            bool isStale = timeSinceHeartbeat.TotalSeconds > 30;

            ShowWarning = isOccupied && isStale;
        }
    }
}

public class OccupancyDisplay
{
    public int DeviceId { get; set; }
    public string OccupancyStatus { get; set; }
    public DateTime DateLogged { get; set; }
}