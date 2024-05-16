using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETLPipeline.Repository.Models
{
    public enum PositionSource
    {
        [Description("ADS-B")]
        [AmbientValue("0")]
        ADSB = 0,    // 0 = ADS-B
        [Description("ASTERIX")]
        [AmbientValue("1")]
        ASTERIX = 1, // 1 = ASTERIX
        [Description("MLAT")]
        [AmbientValue("2")]
        MLAT = 2,    // 2 = MLAT (Multilateration)
        [Description("FLARM")]
        [AmbientValue("3")]
        FLARM = 3    // 3 = FLARM (Flight Location and Radio Management)
    }
    public class Flights
    {
        [Key]
        public int Id { get; set; }
        public string? Icao24 { get; set; }
        public string? Callsign { get; set; }
        public string? OriginCountry { get; set; }
        public int? TimePosition { get; set; }
        public int? LastContact { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public double? BaroAltitude { get; set; }
        public bool? OnGround { get; set; }
        public double? Velocity { get; set; }
        public double? TrueTrack { get; set; }
        public double? VerticalRate { get; set; }
        public int[]? Sensors { get; set; }
        public double? GeoAltitude { get; set; }
        public string? Squawk { get; set; }
        public bool? Spi { get; set; }
        public PositionSource? PositionSource { get; set; }
        public DateTime CollectedDate { get; set; } = DateTime.Now;
    }
}
