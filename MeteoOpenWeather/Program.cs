using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace MeteoOpenWeather
{
    internal class Program
    {
        static async Task Main()
        {
            HttpClient client = new HttpClient();
           
            try
            {
                Console.WriteLine("Inserisci posizione: (es:cesena)");
                string position = Console.ReadLine().Trim();

                string host = "https://api.openweathermap.org";
                string path = "/data/2.5/";
                string location = $"weather?q={position}";
                string APIkey = "&APPID=[openWeatherKEY]";
                HttpResponseMessage response = await client.GetAsync(host + path + location + APIkey);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                string json = responseBody;
                JObject obj = JObject.Parse(json);
                double temp = (double)obj["main"]["temp"];
                Console.WriteLine("La temperatura è: " + temp);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                string position = "apod";

                string host = "https://api.nasa.gov";
                string path = "/planetary/";
                string location = $"{position}";
                string APIkey = "?api_key=[NASAapiKey]";
                HttpResponseMessage response = await client.GetAsync(host + path + location + APIkey);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                string json = responseBody;
                JObject obj = JObject.Parse(json);
                string url = (string)obj["hdurl"];
                Process.Start("\"C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe\"", url);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            try
            {
                string cat = "technology";
                string country = "it";
                int page = 1;

                Console.WriteLine("Inserisci il codice del paese dove vuoi visualizzare le notizie: (es: it)");
                country = Console.ReadLine().Trim();

                Console.WriteLine("Inserisci la categoria che vuoi visualizzare: (es: technology)");
                cat = Console.ReadLine().Trim();

                string host = "https://newsapi.org";
                string path = "/v2/top-headlines";

                string location = $"?country={country}";
                string category = $"&category={cat}";
                string pageNum = $"&page={page}";
                string APIkey = "&apiKey=[newsAPIkey]";

                HttpResponseMessage response = await client.GetAsync(host + path + location + category + pageNum + APIkey);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                string json = responseBody;
                JObject obj = JObject.Parse(json);
                int totalPage = (int)obj["totalResults"] / 20;

                if (obj != null)
                {
                    foreach (var item in obj["articles"])
                    {
                        Console.WriteLine("Provenienza: " + item["source"]["name"]);
                        Console.WriteLine("Autore:" + item["author"]);
                        Console.WriteLine("Titolo: " + item["title"]);
                        Console.WriteLine("Link all'articolo completo:" + item["url"]);
                        Console.WriteLine("Data pubblicazione" + item["publishedAt"] + "\n\n");
                    }
                    Console.WriteLine("Pagina " + page + " di " + (totalPage == 0 ? 1 : totalPage));

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

    }
}
