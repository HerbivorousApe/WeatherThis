using System;
using System.Collections.Generic;

namespace WeatherThis.Common.Models
{
    public class CurrentObservationModel
    {
        public List<COFeatures> Features { get; set; }
    }

    public class COFeatures
    {
        public COFeaturesProperties Properties { get; set; }
    }

    public class COFeaturesProperties
    {
        public DateTime Timestamp { get; set; }
        public string TextDescription { get; set; }
        public COFPTemperature Temperature { get; set; }
        public COFPDewpoint Dewpoint { get; set; }
        public COFPWindDirection WindDirection { get; set; }
        public COFPWindSpeed WindSpeed { get; set; }
        public COFPRelativeHumidity RelativeHumidity { get; set; }
    }

    public class COFPTemperature { public decimal? Value { get; set; } }
    public class COFPDewpoint { public decimal? Value { get; set; } }
    public class COFPWindDirection { public decimal? Value { get; set; } }
    public class COFPWindSpeed { public decimal? Value { get; set; } }
    public class COFPRelativeHumidity { public decimal? Value { get; set; } }
}
