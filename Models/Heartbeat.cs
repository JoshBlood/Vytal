using System.Text.Json.Serialization;
namespace VytalSign.Models;
public class Heartbeat
{
    [JsonPropertyName("deviceid")]
    public int DeviceId { get; set; }

    [JsonPropertyName("devicename")]
    public string DeviceName { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("devicetypeid")]
    public string DeviceTypeId { get; set; }

    [JsonPropertyName("customerid")]
    public string CustomerId { get; set; }

    [JsonPropertyName("customerkey")]
    public string CustomerKey { get; set; }

    [JsonPropertyName("customername")]
    public string CustomerName { get; set; }

    [JsonPropertyName("dt")]
    public string Dt { get; set; } 

    [JsonPropertyName("val")]
    public double Val { get; set; }  

    [JsonPropertyName("lat")]
    public double Lat { get; set; }

    [JsonPropertyName("lng")]
    public double Lng { get; set; }

    [JsonPropertyName("unit")]
    public string Unit { get; set; }

    [JsonPropertyName("descriptiondevicetype")]
    public string DescriptionDeviceType { get; set; }

    [JsonPropertyName("groupid")]
    public string GroupId { get; set; }

    [JsonPropertyName("locationid")]
    public int LocationId { get; set; }

    [JsonPropertyName("deleted")]
    public bool Deleted { get; set; }

    [JsonPropertyName("lastupdate")]
    public long LastUpdate { get; set; }

    [JsonPropertyName("online")]
    public bool Online { get; set; }
}