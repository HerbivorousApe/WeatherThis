using System;
using System.Collections.Generic;

namespace WeatherThis.Common.Models
{
    public class SevenDayHistoryHourlyModel
        {
            public SevenDayForecastHourlyProperties Properties { get; set; }
        }

    public class SevenDayHistoryHourlyProperties
        {
            public List<SevenDayForecastHourlyPeriods> Periods { get; set; }
        }

    public class SevenDayHistoryHourlyPeriods
        {
            public DateTime StartTime { get; set; }
            public decimal? Temperature { get; set; }
            public string WindSpeed { get; set; }
            public string WindDirection { get; set; }
        }
    }

