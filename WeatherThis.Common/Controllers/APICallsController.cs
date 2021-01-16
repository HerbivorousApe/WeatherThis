using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherThis.Common.Models;

namespace WeatherThis.Common.Controllers
{
    public class APICallsController
    {

        public static async Task<CoordsFromZipModel> GetCoordsFromZip(string zip) // link = http://api.zippopotam.us/us/36695
        {
            try
            { 
                var client = new HttpClient();
                var response = await client.GetStringAsync($"http://api.zippopotam.us/us/{zip}");
                CoordsFromZipModel infoReturn = JsonConvert.DeserializeObject<CoordsFromZipModel>(response);

                LocalValuesModel.Latitude = Convert.ToDouble(infoReturn.Places[0].Latitude);
                LocalValuesModel.Longitude = Convert.ToDouble(infoReturn.Places[0].Longitude);
                LocalValuesModel.City = infoReturn.Places[0].PlaceName;
                LocalValuesModel.State = infoReturn.Places[0].State;
                LocalValuesModel.RetryCount = 0;

            return infoReturn;
            }
            catch (Exception ex)
            {
                LocalValuesModel.RetryCount++;

                if (LocalValuesModel.RetryCount < 3)
                {
                    System.Threading.Thread.Sleep(3000);
                    await GetCoordsFromZip(zip);
                }

                LocalValuesModel.RetryCount = 0;
                throw new Exception($"Error: {ex}");
            }
        }

        public static async Task GetGeoDataFromIP() // link = http://ip-api.com/json/2600:1700:c910:1900::43?fields=regionName,city,district,zip,lat,lon
        {
            try
            {
                var client = new HttpClient();
                //string externalIp = await client.GetStringAsync("http://icanhazip.com");
                //externalIp = externalIp.Replace("\n", "");

                var response = await client.GetStringAsync($"http://ip-api.com/json/?fields=regionName,city,district,zip,lat,lon");

                GeoDataModel infoReturn = JsonConvert.DeserializeObject<GeoDataModel>(response);

                LocalValuesModel.Latitude = infoReturn.Lat;
                LocalValuesModel.Longitude = infoReturn.Lon;
                LocalValuesModel.City = infoReturn.City;
                LocalValuesModel.State = infoReturn.RegionName;
                LocalValuesModel.RetryCount = 0;

            }
            catch (Exception ex)
            {
                LocalValuesModel.RetryCount++;

                if (LocalValuesModel.RetryCount < 3)
                {
                    System.Threading.Thread.Sleep(3000);
                    await GetGeoDataFromIP();
                }

                LocalValuesModel.RetryCount = 0;
                throw new Exception($"Error: {ex}");
            }
        }

        public static async Task GetWeatherLocationData() // Link = https://api.weather.gov/points/34.0901,-118.4065
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent", "SlackShack");

                var response = await client.GetStringAsync($"https://api.weather.gov/points/{LocalValuesModel.Latitude},{LocalValuesModel.Longitude}");

                InfoReturnModel infoReturn = JsonConvert.DeserializeObject<InfoReturnModel>(response);

                LocalValuesModel.ObservationStationLink = infoReturn.Properties.ObservationStations;
                LocalValuesModel.RadarStation = infoReturn.Properties.RadarStation;
                LocalValuesModel.SevenDayForecastLink = infoReturn.Properties.Forecast;
                LocalValuesModel.ForecastZone = infoReturn.Properties.ForecastZone.Replace("https://api.weather.gov/zones/forecast/", "");
                LocalValuesModel.RetryCount = 0;
            }
            catch (Exception ex)
            {
                LocalValuesModel.RetryCount++;

                if (LocalValuesModel.RetryCount < 3)
                {
                    System.Threading.Thread.Sleep(3000);
                    await GetWeatherLocationData();
                }

                LocalValuesModel.RetryCount = 0;
                throw new Exception($"Error: {ex}");
            }
        }

        public static async Task<AlertsModel> GetAlertData() //https://api.weather.gov/alerts?active=true&status=actual
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent", "SlackShack");
                var link = $"https://api.weather.gov/alerts?active=true&status=actual&zone={LocalValuesModel.ForecastZone}";
                //var link = $"https://api.weather.gov/alerts?active=true&status=actual"; //test
                var response = await client.GetStringAsync(link);

                AlertsModel infoReturn = JsonConvert.DeserializeObject<AlertsModel>(response);

                LocalValuesModel.Alerts = response;
                LocalValuesModel.RetryCount = 0;

                return infoReturn;
            }
            catch (Exception ex)
            {
                LocalValuesModel.RetryCount++;

                if (LocalValuesModel.RetryCount < 3)
                {
                    System.Threading.Thread.Sleep(3000);
                    await GetAlertData();
                }

                LocalValuesModel.RetryCount = 0;
                throw new Exception($"Error: {ex}");
            }
        }

        public static async Task GetSevenDayForecast() // apiLink = https://api.weather.gov/gridpoints/MOB/44,64/forecast?units=si
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent", "SlackShack");

                var link = LocalValuesModel.SevenDayForecastLink + "?units=si";
                var response = await client.GetStringAsync(link);
                LocalValuesModel.SevenDayForecast = response;

                link = LocalValuesModel.SevenDayForecastLink;
                var responseImperial = await client.GetStringAsync(link);
                LocalValuesModel.SevenDayForecastImperial = responseImperial;

                LocalValuesModel.RetryCount = 0;
            }

            catch (Exception ex)
            {
                LocalValuesModel.RetryCount++;

                if (LocalValuesModel.RetryCount < 3)
                {
                    System.Threading.Thread.Sleep(3000);
                    await GetSevenDayForecast();
                }

                LocalValuesModel.RetryCount = 0;
                throw new Exception($"Error: {ex}");
            }
        }

        public static async Task<SevenDayForecastHourlyModel> GetSevenDayForecastHourly() // link = https://api.weather.gov/gridpoints/MOB/44,64/forecast/hourly?units=si
        {

           try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent", "SlackShack");
            
                var link = LocalValuesModel.SevenDayForecastLink + "/hourly?units=si";
                var response = await client.GetStringAsync(link);

                SevenDayForecastHourlyModel infoReturn = JsonConvert.DeserializeObject<SevenDayForecastHourlyModel>(response);

                LocalValuesModel.SevenDayForecastHourly = response;
                LocalValuesModel.RetryCount = 0;

                return infoReturn;
            }
            catch (Exception ex)
            {
                LocalValuesModel.RetryCount++;

                if (LocalValuesModel.RetryCount < 3)
                {
                    System.Threading.Thread.Sleep(3000);
                    await GetSevenDayForecastHourly();
                }

                LocalValuesModel.RetryCount = 0;
                throw new Exception($"Error: {ex}");
            }
        }

        public static async Task<CurrentObservationModel> GetCurrentObservationData()  // link = https://api.weather.gov/stations/KMOB/observations
        {
            try
            {
                HttpClient client = new HttpClient();
                var link = $"https://api.weather.gov/stations/{LocalValuesModel.RadarStation}/observations";

                client.DefaultRequestHeaders.Add("User-Agent", "SlackShack");
                var response = await client.GetStringAsync(link);

                CurrentObservationModel infoReturn = JsonConvert.DeserializeObject<CurrentObservationModel>(response);

                LocalValuesModel.CurrentObservation = response;
                LocalValuesModel.RetryCount = 0;

                return infoReturn;
            }

            catch (Exception ex)
            {
                LocalValuesModel.RetryCount++;

                if (LocalValuesModel.RetryCount < 3)
                {
                    System.Threading.Thread.Sleep(3000);
                    await GetCurrentObservationData();
                }

                LocalValuesModel.RetryCount = 0;
                throw new Exception($"Error: {ex}");
            }
        }


        public static async Task<ObservationStationModel> GetCurrentObservationStations()  // link = https://api.weather.gov/gridpoints/MOB/44,65/stations
        {
            try
            {
                HttpClient client = new HttpClient();
                var link = LocalValuesModel.ObservationStationLink;

                client.DefaultRequestHeaders.Add("User-Agent", "SlackShack");
                var response = await client.GetStringAsync(link);

                ObservationStationModel infoReturn = JsonConvert.DeserializeObject<ObservationStationModel>(response);

                LocalValuesModel.RadarStation = infoReturn.Features[0].Properties.StationIdentifier;
                LocalValuesModel.RetryCount = 0;

                return infoReturn;
            }

            catch (Exception ex)
            {
                LocalValuesModel.RetryCount++;

                if (LocalValuesModel.RetryCount < 3)
                {
                    System.Threading.Thread.Sleep(3000);
                    await GetCurrentObservationStations();
                }

                LocalValuesModel.RetryCount = 0;
                throw new Exception($"Error: {ex}");
            }
        }
    }
}
