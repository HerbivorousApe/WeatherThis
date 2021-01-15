using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherThis.Common.Controllers;
using WeatherThis.Common.Models;
using Newtonsoft.Json;

namespace WeatherThis.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await APICallsController.GetGeoDataFromIP();
            await APICallsController.GetWeatherLocationData();
            await APICallsController.GetAlertData();
            await APICallsController.GetCurrentObservationStations();
            await APICallsController.GetCurrentObservationData();
            await APICallsController.GetSevenDayForecast();
            await APICallsController.GetSevenDayForecastHourly();

            //TestCon();

            CreateHostBuilder(args).Build().Run();
        }

        //public static CurrentObservationModel TestCon()
        //{
        //    return JsonConvert.DeserializeObject<CurrentObservationModel>(LocalValuesModel.CurrentObservation);
        //}

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
