using System.Threading.Tasks;
using WeatherThis.ConsoleApp.Views;

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
