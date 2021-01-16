using System;
using System.Threading.Tasks;
using WeatherThis.ConsoleApp.Views;
using WeatherThis.Common.Models;

namespace WeatherThis.ConsoleApp
{
    class Program
    {
        static async Task Main()
        {
            MainWelcomeView.Header();

            await APICallsView.GetGeoDataFromIP();
            await APICallsView.GetLocationData();

        }
    }
}
