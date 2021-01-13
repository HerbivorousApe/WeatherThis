using System.Collections.Generic;

namespace WeatherThis.Common.Models
{
    public class AlertsModel
    {
        public List<AlertsFeatures> Features { get; set; }
    }

    public class AlertsFeatures
    {
        public AlertsFeaturesProperties Properties { get; set; }
    }

    public class AlertsFeaturesProperties
    {
        public static string Event { get; set; }
        public string Headline { get; set; }
        public string Description { get; set; }
        public string Instruction { get; set; }
    }
}
