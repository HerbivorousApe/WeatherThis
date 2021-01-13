using System.Collections.Generic;

namespace WeatherThis.Common.Models
{
    public class ObservationStationModel
    {
        public List<ObservationStationFeatures> Features { get; set; }
    }

    public class ObservationStationFeatures
    {
        public ObservationStationProperties Properties { get; set; }
    }

    public class ObservationStationProperties
    {
        public string StationIdentifier { get; set; }
        public string Name { get; set; }
    }

}
